using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HLVR.InputSystem;
using HLVR.Interaction;
//手势动画


namespace HLVR.Animations 
{
    public class GestureAnimation : MonoBehaviour
    {
        public HandData handTriggerData;
        public HandData handGripData;
        public ControllerType handType;
        public Transform[] bones;
        Quaternion[] originRotation;

      
        InteractionRay ioray;
        public GameObject m_HandModel;
      //  [ReadOnly]
        [Tooltip("手柄模型使用ID号")]
        public int m_Index;
        [Tooltip("手柄模型控制器")]
        public GameObject[] m_ControllerModels;
        [Tooltip("如果值为真则渲染手模型，否则渲染手柄模型")]
        public bool RenderHand;
        public bool m_HideAll;
        private void Awake()
        {
            bones = new Transform[25];
            bones = transform.GetChild(0).GetChild(0).GetChild(5).GetComponentsInChildren<Transform>();
            originRotation = new Quaternion[bones.Length];
            for (int i = 0; i < bones.Length; i++)
            {
                originRotation[i] = bones[i].localRotation;
            }
            ioray = GameObject.FindObjectOfType<InteractionRay>();
            RenderModel();
        }

        public void RenderModel()
        {
            if (m_HideAll) 
            {
                m_HandModel.SetActive(false);
                foreach (GameObject g in m_ControllerModels)
                    g.SetActive(false);
            }
            else
            if (RenderHand)
            {
                m_HandModel.SetActive(true);
                foreach(GameObject g in m_ControllerModels)
                    g.SetActive(false);
            }
            else
            {
                m_HandModel.SetActive(false);

                for (int i=0;i<m_ControllerModels.Length;i++)
                {
                    if(m_Index== i)
                    m_ControllerModels[i].SetActive(true);
                    else
                    m_ControllerModels[i].SetActive(false);
                }
              
            }
        }


        /// <summary>
        /// 渲染控制器模型
        /// </summary>
        public void RenderControllerModel()
        {
            RenderHand = false;
            m_HideAll = false;
            RenderModel();
        }

        /// <summary>
        /// 渲染手部皮肤
        /// </summary>
        public void RenderHandSkin()
        {
            RenderHand = true;
            m_HideAll = false;
            RenderModel();
        }


        /// <summary>
        /// 隐藏所有控制器和手部皮肤
        /// </summary>
        public void HideAll()
        {
            m_HideAll = true;
            RenderHand= false;
            RenderModel();
        }




        private void Update()
        {
            if(RenderHand)
            AnimationGesture();
        }


        /// <summary>
        /// 手部皮肤骨骼动画
        /// </summary>
        public void AnimationGesture()
        {
           
            switch (handType)
            {
                case ControllerType.Left:
                    for (int i = 0; i < bones.Length; i++)
                    {
                        if (VRInput.GetKeyValue(KeyType.Trigger, ControllerType.Left) > 0)
                        {
                           
                            bones[i].localRotation = Quaternion.Lerp(originRotation[i], handTriggerData.lefthand[i], VRInput.GetKeyValue(KeyType.Trigger,ControllerType.Left));
                        }
                        else if (VRInput.GetKeyValue(KeyType.Grip, ControllerType.Left) > 0 && VRInput.GetKeyValue(KeyType.Trigger, ControllerType.Left) == 0)
                        {
                            bones[i].localRotation = Quaternion.Lerp(originRotation[i], handGripData.lefthand[i], VRInput.GetKeyValue(KeyType.Grip, ControllerType.Left));
                        }
                        else 
                        {
                            bones[i].localRotation= Quaternion.Lerp(originRotation[i], handTriggerData.righthand[i], 0);
                        }



                    }
                    break;

                case ControllerType.Right:
                    for (int i = 0; i < bones.Length; i++)
                    {
                        if (VRInput.GetKeyValue(KeyType.Trigger, ControllerType.Right) > 0.1f)
                        {
                            bones[i].localRotation = Quaternion.Lerp(originRotation[i], handTriggerData.righthand[i], VRInput.GetKeyValue(KeyType.Trigger, ControllerType.Right));
                        }
                        else if (VRInput.GetKeyValue(KeyType.Grip, ControllerType.Right) > 0.1f)
                        {
                            bones[i].localRotation = Quaternion.Lerp(originRotation[i], handGripData.righthand[i], VRInput.GetKeyValue(KeyType.Grip, ControllerType.Right));
                        }
                        else 
                        {
                            bones[i].localRotation = Quaternion.Lerp(originRotation[i], handTriggerData.righthand[i], 0);
                        }


                    }
                    break;
            }
        }
    }

}

