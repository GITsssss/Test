using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HLVR.EventSystems;
using HLVR.InputSystem;
using UnityEngine.Animations.Rigging;
using System.Linq;

namespace HLVR.Interaction 
{
    [DisallowMultipleComponent]//禁止重复添加这个组件
    [RequireComponent(typeof(Rigidbody))]
    public class GrabObjects : MonoBehaviour
    {
        [Tooltip("抓取物体得方式,RigidbodyPosition:通过改变刚体得位置；Child：将物体作为手的子物体")]
        public GrabType grabType=GrabType.RigidbodyPosition;
        [Tooltip("可以被哪些控制器抓起来")]
        public ControllerType controllerType = ControllerType.Right;
        [Tooltip("通过触发按键抓起物体")]
        public KeyType keyType=KeyType.Grip;
        [Tooltip("抓起物体后，会执行哪些响应")]
        public GrabResponse grabResponse=GrabResponse.CloseGravity;
        public bool HideHand;
        [Tooltip("物理学抛出事件")]
        public PhysicsEvent physicsEvent;
        [Tooltip("指定ID的碰撞")]
        public Specific specificTrigger;
        public GrabEvent grabEvent;
        [ReadOnly]
        public Hand currentHand;
        [HideInInspector]
        public  Rigidbody rig;
        public bool DisableInteraction;//关闭交互

        [ReadOnly]
        [Tooltip("是否被手抓起")]
        public bool BeCaptured;//是否被抓取
        private void Awake()
        {
            rig = GetComponent<Rigidbody>();
            if (grabType == GrabType.Child)
            {
                rig.isKinematic = true;
            }
        }
        /// <summary>
        /// 被手抓起
        /// </summary>
        public void OnGrab()
        {
            if (grabResponse.ToString().Contains("SetRigidbodyIsTrigger")) 
            {
                if(this.GetComponent<Collider>()!=null)
                this.GetComponent<Collider>().isTrigger = true;
            }
            if (grabResponse.ToString().Contains("NonKinematic"))
                rig.isKinematic = true;
            if(grabResponse.ToString().Contains("CloseGravity"))
                rig.useGravity= false;
            if (grabResponse.ToString().Contains("DisEnableCollider")) 
            {
                if (this.GetComponent<Collider>() != null)
                    this.GetComponent<Collider>().enabled= false;
            }

            if (grabType == GrabType.RigidbodyPosition)
                rig.isKinematic = false;

            BeCaptured = true;
            grabEvent.GrabEnterEvent();
        }

        /// <summary>
        /// 脱离手并且被抛出
        /// </summary>
        public void Throw()
        {
            if (grabResponse.ToString().Contains("SetRigidbodyIsTrigger"))
            {
                if (this.GetComponent<Collider>() != null)
                    this.GetComponent<Collider>().isTrigger = false;
            }
            if (grabResponse.ToString().Contains("NonKinematic"))
                rig.isKinematic = false;
            if (grabResponse.ToString().Contains("CloseGravity"))
                rig.useGravity = true;
            if (grabResponse.ToString().Contains("DisEnableCollider"))
            {
                if (this.GetComponent<Collider>() != null)
                    this.GetComponent<Collider>().enabled = true;
            }
            if (grabType == GrabType.Child)
            {
                rig.isKinematic = false;
            }
            BeCaptured = false;
            grabEvent.FallEvent();
        }

        /// <summary>
        /// 脱离手部的束缚
        /// </summary>
        public void SlipTheCollar()
        {
            if (grabResponse.ToString().Contains("SetRigidbodyIsTrigger"))
            {
                if (this.GetComponent<Collider>() != null)
                    this.GetComponent<Collider>().isTrigger = false;
            }
            if (grabResponse.ToString().Contains("NonKinematic"))
                rig.isKinematic = false;
            if (grabResponse.ToString().Contains("CloseGravity"))
                rig.useGravity = true;
            if (grabResponse.ToString().Contains("DisEnableCollider"))
            {
                if (this.GetComponent<Collider>() != null)
                    this.GetComponent<Collider>().enabled = true;
            }
            if (grabType == GrabType.Child)
            {
                rig.isKinematic = false;
            }
        }


        public void OnTouch()
        {
            grabEvent.EnterTouchEvent();
        }


        public void ExitTouch()
        { 
            grabEvent.EixtTouchEvent();
 
        }

        private void OnTriggerEnter(Collider other)
        {
            Hand hand = other.GetComponent<Hand>();
            if (hand != null) 
            {
                if (hand.inputSource == controllerType || controllerType == ControllerType.All) 
                {
                    OnTouch();
                }         
            }
            GrabTriggerEvent GTE = other.GetComponent<GrabTriggerEvent>();
            if (GTE != null&&GTE.ID== specificTrigger.m_Id) 
            {
                if (!GTE.grabResponseEventGroup.ToString().Contains("ReleaseTrigger")) 
                {
                    if (currentHand != null) 
                    {
                        switch (currentHand.inputSource) 
                        {
                            case ControllerType.Left:
                                currentHand.ForcedDisposalLeft();
                                break;

                            case ControllerType.Right:
                                currentHand.ForcedDisposalRight();
                                break;
                        }
                    }
                }

                if (GTE.grabResponseEventGroup.ToString().Contains("SynchronizationPosition")) 
                {
                    transform.position = GTE.transform.position;
                }

                if (GTE.grabResponseEventGroup.ToString().Contains("SynchronizationRotation")) 
                {
                    transform.rotation = GTE.transform.rotation;
                }

                if (GTE.grabResponseEventGroup.ToString().Contains("EnableRigKinematic")) 
                {
                    rig.isKinematic = true;
                }

                if (GTE.grabResponseEventGroup.ToString().Contains("DisEnableCollider"))
                {
                    foreach (Collider c in GetComponentsInChildren<Collider>())
                    {
                        c.enabled = false;
                    }
                }

                if (GTE.grabResponseEventGroup.ToString().Contains("SynchronizationParent"))
                {
                    transform.parent = GTE.transform.parent ;
                }

                if (GTE.grabResponseEventGroup.ToString().Contains("DisableInteraction"))
                {
                    SetInteractionState(true);
                }

                GTE.Event(specificTrigger.m_Id);
                GTE.MultipleEvent(specificTrigger.m_Id);

                if (GTE.grabResponseEventGroup.ToString().Contains("CloseTriggerGameObject"))
                {
                    this.gameObject.SetActive(false);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Hand hand = other.GetComponent<Hand>();
            if (hand != null)
            {
                if (hand.inputSource == controllerType || controllerType == ControllerType.All) 
                {
                    ExitTouch();
                }           
            }
        }

        public void SetInteractionState(bool state)
        {
            DisableInteraction = state;
        }


        public void ForcedDisposal()
        {
            if (currentHand != null)
            {
                switch (currentHand.inputSource)
                {
                    case ControllerType.Left:
                        currentHand.ForcedDisposalLeft();
                        break;

                    case ControllerType.Right:
                        currentHand.ForcedDisposalRight();
                        break;
                }
            }
        }
    }

    /// <summary>
    /// 抓取响应设置
    /// </summary>
    [System.Flags]
    public enum GrabResponse
    {
        SetRigidbodyIsTrigger = 2,//设置刚体得碰撞盒为触发器
        NonKinematic = 4,//设置刚体为非运动学
        CloseGravity = 8,//关闭重力
        DisEnableCollider = 32,//关闭碰撞盒
        GrabPointAsCenter= 64,//以抓取点为中心
        DisEnablePosition=128,//关闭位置跟踪
        DisEnableRotation=256,//关闭旋转跟踪
        TriggerKeyOddOrEven=512,//触发一次按键,就会抓住物品，再此触发就会丢下物品
    }
}
