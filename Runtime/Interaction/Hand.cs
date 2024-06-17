using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HLVR.Animations;
using HLVR.InputSystem;
using HLVR.EventSystems;
using HLVR.Arithmetic;
using UnityEngine.UIElements;
using UnityEngine.Animations.Rigging;
using static Codice.CM.Common.CmCallContext;

namespace HLVR.Interaction
{
    public class Hand : MonoBehaviour
    {
        [Tooltip("控制器")]
        public ControllerType inputSource;
        public Hand aotherHand;
        [ReadOnly]
        [Tooltip("当前抓住的物体")]
        public GrabObjects current;
        public float speed = 85f;
        public float threshold;
        [HideInInspector]
        public GameObject grabpoint;
        [HideInInspector]
        public Vector3 CollideredgePoint;//返回抓取刚体碰撞盒上距离抓取点最近的点
        [ReadOnly]
        public GrabObjects grabOBJ;
        bool oddeven=true;
        private void Reset()
        {
            LoadOtherHand();
        }

        private void Awake()
        {
            grabpoint = new GameObject("GrabPoint");
            grabpoint.transform.parent = transform;
            grabpoint.transform.localPosition = Vector3.zero;
        }
        /// <summary>
        /// 松开抓取的物体
        /// </summary>
        private void Release()
        {
            if (current != null)
            {

                if (VRInput.GetKeyUp(current.keyType, inputSource)&&!current.grabResponse.ToString().Contains("TriggerKeyOddOrEven"))
                {
                    if (current.controllerType == inputSource || current.controllerType == ControllerType.All)
                    {
                        if (current.transform.parent == this.transform.parent)
                            current.transform.parent = null;
                        current.rig.velocity = Vector3.zero;
                        Vector3 vs = InteractionRay.Instance.righthandInfoData.velocity;
                        Vector3 worldAngularVelocity = InteractionRay.Instance.righthandInfoData.AngularVelocity;
                        if (current.physicsEvent.m_Enable)
                        {
                            switch (current.physicsEvent.velocityType)
                            {
                                case PhysicsEvent.ThrowVelocityType.Auto:
                                    //current.rig.velocity = transform.TransformDirection(vs);
                                    //current.rig.angularVelocity=transform.TransformDirection(worldAngularVelocity);
                                    Vector3 rotationVelocity = Vector3.Cross(worldAngularVelocity, transform.localPosition);
                                    current.rig.velocity = transform.TransformDirection(vs) + rotationVelocity;

                                    break;
                                case PhysicsEvent.ThrowVelocityType.Value:
                                    current.rig.velocity = transform.TransformDirection(vs.normalized * current.physicsEvent.value);
                                    break;
                            }

                        }

                        current.Throw();
                        current.currentHand = null;
                        grabOBJ = null;
                        current = null;
                        ShowHand();
                    }

                }
                else if (VRInput.GetKeyUp(current.keyType,inputSource) && current.grabResponse.ToString().Contains("TriggerKeyOddOrEven")) 
                {
                    oddeven = !oddeven;
                    if (current.BeCaptured&& oddeven) 
                    {
                        if (current.controllerType == inputSource || current.controllerType == ControllerType.All)
                        {
                            if (current.transform.parent == this.transform.parent)
                                current.transform.parent = null;
                            current.rig.velocity = Vector3.zero;
                            Vector3 vs = InteractionRay.Instance.righthandInfoData.direction;
                            if (current.physicsEvent.m_Enable)
                            {
                                switch (current.physicsEvent.velocityType)
                                {
                                    case PhysicsEvent.ThrowVelocityType.Auto:
                                        current.rig.velocity = vs;
                                        break;
                                    case PhysicsEvent.ThrowVelocityType.Value:
                                        current.rig.velocity = vs.normalized * current.physicsEvent.value;
                                        break;
                                }

                            }

                            current.Throw();
                            current.currentHand = null;
                            grabOBJ = null;
                            current = null;
                            ShowHand();
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 从另外一只手里夺走物体
        /// </summary>
        /// <param name="other"></param>

        void FromLootOtherHand(Collider other) //从另外一直手中拿走物体
        {
            if (aotherHand.current != null && current.gameObject == aotherHand.current.gameObject)
            {
                if (VRInput.GetKeyDown(aotherHand.current.keyType, inputSource))
                {
                    if (aotherHand.current.controllerType == inputSource || aotherHand.current.controllerType == ControllerType.All)
                    {
                        aotherHand.current.rig.velocity = Vector3.zero;
                        aotherHand.oddeven = true;
                        aotherHand.current.currentHand = null;
                        aotherHand. ShowHand();
                        HideHand();
                        current.currentHand = this;
                        grabpoint.transform.position = other.ClosestPoint(current.transform.position);
                        CollideredgePoint = other.ClosestPointOnBounds(grabpoint.transform.position);
                        current?.OnGrab();
                        aotherHand.grabOBJ = null;
                        aotherHand.current = null;
                    }
                }
            }
        }



        /// <summary>
        /// 抓取中
        /// </summary>
        public void Grabing()
        {
            if (current != null)
            {
                Vector3 vector = Vector3.one;
                switch (current.grabType)
                {
                    case GrabType.RigidbodyPosition://刚体位置跟踪
                        if (current.grabResponse.ToString().Contains("GrabPointAsCenter"))
                        {
                             vector = grabpoint.transform.position - current.rig.position;
                        }
                        else
                        {
                             vector = transform.position - current.rig.position;
                        }

                        Vector3 targetVelocity =vector  * speed;


                        //旧版刚体跟随逻辑-Start
                        ////if(Teleport.Instance.)
                        ////current.rig.position = transform.position;

                        //if ((current.rig.velocity - targetVelocity).magnitude >= 0.2f && !current.grabResponse.ToString().Contains("DisEnablePosition"))
                        //    current.rig.velocity = Vector3.MoveTowards(current.rig.velocity, targetVelocity, 100 * Time.fixedDeltaTime);
                        //else
                        //    current.rig.velocity = Vector3.zero;
                        //旧版刚体跟随逻辑-End


                        //新版刚体跟随逻辑-Start
                        if (Vector3.Magnitude(current.rig.velocity - targetVelocity) <= threshold)
                        {

                            current.rig.velocity= vector * Vector3.Distance(transform.position, current.rig.position);
                        }

                        else
                            current.rig.velocity= vector * speed * Vector3.Distance(transform.position, current.rig.position);
                        //新版刚体跟随逻辑-End


                        if (!current.grabResponse.ToString().Contains("DisEnableRotation"))
                            current.rig.rotation = transform.rotation;
                     
                            current.rig.angularVelocity = Vector3.zero;
                        break;

                    case GrabType.Child://作为手的子物体
                        current.transform.parent = this.transform.parent;

                        if (current.grabResponse.ToString().Contains("GrabPointAsCenter"))
                        {
                            if (!current.grabResponse.ToString().Contains("DisEnablePosition"))
                            {
                                current.transform.position = grabpoint.transform.position;
                            }    
                            if (!current.grabResponse.ToString().Contains("DisEnableRotation"))
                                current.transform.rotation = grabpoint.transform.rotation;
                        }
                        else
                        {
                            if (!current.grabResponse.ToString().Contains("DisEnablePosition"))
                                current.transform.localPosition = Vector3.zero;
                            if (!current.grabResponse.ToString().Contains("DisEnableRotation"))
                                current.transform.localRotation = Quaternion.identity;
                        }
                        break;
                }

            }
        }

        /// <summary>
        /// 加载另外一只手
        /// </summary>
        void LoadOtherHand()
        {
            Hand[] hands = FindObjectsOfType<Hand>();
            for (int i = 0; i < 2; i++)
            {
                if (this.gameObject != hands[i].gameObject)
                    aotherHand = hands[i];
            }
        }


        /// <summary>
        /// 如果手中的抓取的物体不为空，则强制使物体脱离手
        /// </summary>
        /// <param name="controllerType">手部类型</param>
        public void ForcedDisposalHand(ControllerType controllerType)
        {
            if (current != null&&inputSource== controllerType)
            {
                if (current.transform.parent == this.transform.parent)
                    current.transform.parent = null;
                current.rig.velocity = Vector3.zero;
                current.SlipTheCollar();
                current.currentHand = null;
                grabOBJ = null;
                current = null;
                ShowHand();
            }
        }
        /// <summary>
        /// 隐藏手
        /// </summary>
        public void HideHand() 
        {
            if(inputSource==ControllerType.Left)
            InteractionRay.Instance.left.gameObject.SetActive(false);
            else if(inputSource == ControllerType.Right)
            InteractionRay.Instance.right.gameObject.SetActive(false);
        }

        /// <summary>
        /// 显示手
        /// </summary>
        public void ShowHand()
        {
            if (inputSource == ControllerType.Left)
                InteractionRay.Instance.left.gameObject.SetActive(true);
            else if (inputSource == ControllerType.Right)
                InteractionRay.Instance.right.gameObject.SetActive(true);
        }
        private void FixedUpdate()
        {
            Grabing();
        }

        private void Update()
        {
            Release();         
        }
        private void OnTriggerStay(Collider other)
        {
            if (current == null) 
            {
                if (other.GetComponent<GrabObjects>() != null)
                {
                    if (VRInput.GetKeyDown(other.GetComponent<GrabObjects>().keyType, inputSource))
                    {
                        current = other.GetComponent<GrabObjects>();
                      
                        if (current != null) 
                        {
                            if (current.controllerType == inputSource || current.controllerType == ControllerType.All) 
                            {
                                if (current.DisableInteraction)
                                {
                                    current = null;
                                    return;
                                }
                                else 
                                {
                                    if (current.currentHand != null)
                                    {
                                        FromLootOtherHand(other);
                                    }
                                    else
                                    {
                                        current.currentHand = this;
                                        grabpoint.transform.position = other.ClosestPoint(current.transform.position);
                                        CollideredgePoint = other.ClosestPointOnBounds(grabpoint.transform.position);
                                        current.OnGrab();
                                     
                                        if (current.HideHand)
                                        {
                                            HideHand();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                current = null;
                            }

                        }
                       
                    }
                }
                else 
                {
                    
                    if (grabOBJ == null&& current==null)
                        GetParentGrabObjects(other.transform, out grabOBJ);
                    if (grabOBJ != null&& VRInput.GetKeyDown(grabOBJ.keyType, inputSource))
                    {
                        current = grabOBJ;
                       
                        if (current != null) 
                        {
                            if (current.controllerType == inputSource || current.controllerType == ControllerType.All)
                            {
                                if (current.DisableInteraction)
                                {
                                    current = null;
                                    return;
                                }
                                else
                                {
                                    if (current.currentHand != null)
                                    {
                                        FromLootOtherHand(other);
                                    }
                                    else
                                    {
                                        current.currentHand = this;
                                        grabpoint.transform.position = other.ClosestPoint(current.transform.position);
                                        CollideredgePoint = other.ClosestPointOnBounds(grabpoint.transform.position);
                                     
                                        current.OnGrab();
                                       
                                      
                                        if (current.HideHand)
                                        {
                                            HideHand();
                                        }
                                    }
                                }
                            }
                            else 
                            {
                                current = null;
                            }
                           
                        }
                      
                    }
                }
                
            }
        }


        /// <summary>
        /// 如果当前对象没有GrabObjects组件，就会从其祖辈对象获取GrabObjects组件
        /// </summary>
        /// <param name="self">自身</param>
        /// <param name="grabObjects">用于当作GrabObjectsd的储存器</param>
        void GetParentGrabObjects(Transform self,out GrabObjects grabObjects)
        {
            if (self.transform.parent != null)
            {
                Transform parent = self.parent ;
                GrabObjects go = parent.GetComponent<GrabObjects>();
                if (go == null && parent.transform.parent != null)
                {
                    GetParentGrabObjects(parent, out grabObjects);
                }
                else
                {
                    if (go!=null&&!go.DisableInteraction)
                        grabObjects = go;
                    else
                        grabObjects = null;
                }

            }
            else 
            {
                grabObjects = null;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (grabOBJ != null)grabOBJ = null;
        }

    }


    
}

[System.Serializable]
public struct HandGrabEvent 
{
    public InteractionEvent OnGrab;
    public InteractionEvent Release;
    public InteractionEvent OnTouch;
    public InteractionEvent ExitTouch;
}
