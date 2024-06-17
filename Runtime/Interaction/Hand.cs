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
        [Tooltip("������")]
        public ControllerType inputSource;
        public Hand aotherHand;
        [ReadOnly]
        [Tooltip("��ǰץס������")]
        public GrabObjects current;
        public float speed = 85f;
        public float threshold;
        [HideInInspector]
        public GameObject grabpoint;
        [HideInInspector]
        public Vector3 CollideredgePoint;//����ץȡ������ײ���Ͼ���ץȡ������ĵ�
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
        /// �ɿ�ץȡ������
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
        /// ������һֻ�����������
        /// </summary>
        /// <param name="other"></param>

        void FromLootOtherHand(Collider other) //������һֱ������������
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
        /// ץȡ��
        /// </summary>
        public void Grabing()
        {
            if (current != null)
            {
                Vector3 vector = Vector3.one;
                switch (current.grabType)
                {
                    case GrabType.RigidbodyPosition://����λ�ø���
                        if (current.grabResponse.ToString().Contains("GrabPointAsCenter"))
                        {
                             vector = grabpoint.transform.position - current.rig.position;
                        }
                        else
                        {
                             vector = transform.position - current.rig.position;
                        }

                        Vector3 targetVelocity =vector  * speed;


                        //�ɰ��������߼�-Start
                        ////if(Teleport.Instance.)
                        ////current.rig.position = transform.position;

                        //if ((current.rig.velocity - targetVelocity).magnitude >= 0.2f && !current.grabResponse.ToString().Contains("DisEnablePosition"))
                        //    current.rig.velocity = Vector3.MoveTowards(current.rig.velocity, targetVelocity, 100 * Time.fixedDeltaTime);
                        //else
                        //    current.rig.velocity = Vector3.zero;
                        //�ɰ��������߼�-End


                        //�°��������߼�-Start
                        if (Vector3.Magnitude(current.rig.velocity - targetVelocity) <= threshold)
                        {

                            current.rig.velocity= vector * Vector3.Distance(transform.position, current.rig.position);
                        }

                        else
                            current.rig.velocity= vector * speed * Vector3.Distance(transform.position, current.rig.position);
                        //�°��������߼�-End


                        if (!current.grabResponse.ToString().Contains("DisEnableRotation"))
                            current.rig.rotation = transform.rotation;
                     
                            current.rig.angularVelocity = Vector3.zero;
                        break;

                    case GrabType.Child://��Ϊ�ֵ�������
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
        /// ��������һֻ��
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
        /// ������е�ץȡ�����岻Ϊ�գ���ǿ��ʹ����������
        /// </summary>
        /// <param name="controllerType">�ֲ�����</param>
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
        /// ������
        /// </summary>
        public void HideHand() 
        {
            if(inputSource==ControllerType.Left)
            InteractionRay.Instance.left.gameObject.SetActive(false);
            else if(inputSource == ControllerType.Right)
            InteractionRay.Instance.right.gameObject.SetActive(false);
        }

        /// <summary>
        /// ��ʾ��
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
        /// �����ǰ����û��GrabObjects������ͻ�����汲�����ȡGrabObjects���
        /// </summary>
        /// <param name="self">����</param>
        /// <param name="grabObjects">���ڵ���GrabObjectsd�Ĵ�����</param>
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
