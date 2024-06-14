using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HLVR.EventSystems;
using HLVR.InputSystem;
using UnityEngine.Animations.Rigging;
using System.Linq;

namespace HLVR.Interaction 
{
    [DisallowMultipleComponent]//��ֹ�ظ����������
    [RequireComponent(typeof(Rigidbody))]
    public class GrabObjects : MonoBehaviour
    {
        [Tooltip("ץȡ����÷�ʽ,RigidbodyPosition:ͨ���ı�����λ�ã�Child����������Ϊ�ֵ�������")]
        public GrabType grabType=GrabType.RigidbodyPosition;
        [Tooltip("���Ա���Щ������ץ����")]
        public ControllerType controllerType = ControllerType.Right;
        [Tooltip("ͨ����������ץ������")]
        public KeyType keyType=KeyType.Grip;
        [Tooltip("ץ������󣬻�ִ����Щ��Ӧ")]
        public GrabResponse grabResponse=GrabResponse.CloseGravity;
        public bool HideHand;
        [Tooltip("����ѧ�׳��¼�")]
        public PhysicsEvent physicsEvent;
        [Tooltip("ָ��ID����ײ")]
        public Specific specificTrigger;
        public GrabEvent grabEvent;
        [ReadOnly]
        public Hand currentHand;
        [HideInInspector]
        public  Rigidbody rig;
        public bool DisableInteraction;//�رս���

        [ReadOnly]
        [Tooltip("�Ƿ���ץ��")]
        public bool BeCaptured;//�Ƿ�ץȡ
        private void Awake()
        {
            rig = GetComponent<Rigidbody>();
            if (grabType == GrabType.Child)
            {
                rig.isKinematic = true;
            }
        }
        /// <summary>
        /// ����ץ��
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
        /// �����ֲ��ұ��׳�
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
        /// �����ֲ�������
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
    /// ץȡ��Ӧ����
    /// </summary>
    [System.Flags]
    public enum GrabResponse
    {
        SetRigidbodyIsTrigger = 2,//���ø������ײ��Ϊ������
        NonKinematic = 4,//���ø���Ϊ���˶�ѧ
        CloseGravity = 8,//�ر�����
        DisEnableCollider = 32,//�ر���ײ��
        GrabPointAsCenter= 64,//��ץȡ��Ϊ����
        DisEnablePosition=128,//�ر�λ�ø���
        DisEnableRotation=256,//�ر���ת����
        TriggerKeyOddOrEven=512,//����һ�ΰ���,�ͻ�ץס��Ʒ���ٴ˴����ͻᶪ����Ʒ
    }
}
