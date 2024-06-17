using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using HLVR.AudioSystems;
using HLVR.Interaction;
using HLVR.UI;
using TMPro;
namespace HLVR.EventSystems 
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioSource))]
    public class InteractionEventElement : MonoBehaviour ,IButtonEventable,ITriggerEvent, ISoundEffectEventable
    {
        public  bool m_LockInteraction;
        public  LinkResponse linkResponse;
        public  RectTransformSet UIAnimation;
        public  ImageResponseSet ImageResponseSet;
        public  TextTMPUIIOSet TMPUIIOSet;
        public  SoundEffect IOSE; 
        public  ButtonEvent  buttonEvent;
        public  TriggerEvent triggerEvent;
        RectTransformLerp rect;
        private void Reset()
        {
            ImageResponseSet.lockColor = new Color(ImageResponseSet.responseColor.r, ImageResponseSet.responseColor.g, ImageResponseSet.responseColor.b, 0.4f);
        }

        private void Awake()
        {
            if (transform.GetComponent<BoxCollider>() == null)
                transform.gameObject.AddComponent<BoxCollider>();
            transform.GetComponent<BoxCollider>().size = new Vector3(transform.GetComponent<RectTransform>().sizeDelta.x, transform.GetComponent<RectTransform>().sizeDelta.y,1);
        }
        public void Lock()
        {
            m_LockInteraction = true;
            ImageResponse ir = GetComponent<ImageResponse>();
            if (ir != null)
            {
                ir.Lock();
            }

            TextTMPUIIO tMPUIIO = GetComponent<TextTMPUIIO>();
            if (tMPUIIO != null)
            {
                tMPUIIO.Lock();
            }

        }

        public void UnLock()
        {
            m_LockInteraction = false;
            ImageResponse ir = GetComponent<ImageResponse>();
            if (ir != null)
            {
                ir.UnLock();
            }
            TextTMPUIIO tMPUIIO = GetComponent<TextTMPUIIO>();
            if (tMPUIIO != null)
            {
                tMPUIIO.UnLock();
            }
        }


        public void Button() 
        {
            if (!m_LockInteraction) 
            {
                buttonEvent.m_Button.m_Events?.Invoke();
                ASEPress();
                buttonEvent.m_Button.DebugData("按下按键一次");
               
            }   
        }

        bool oe=true;
        public void OE()//奇偶调用
        {
            if (!m_LockInteraction) 
            {
                if (oe)
                {
                    ButtonOdd();
                    oe = !oe;
                }
                else
                {
                    ButtonEven();
                    oe = !oe;
                }
            }          
        }

        public void ButtonOdd()
        {
            buttonEvent. m_ButtonOdd.m_Events?.Invoke();
            buttonEvent.m_ButtonOdd.DebugData("按下按键奇数次一次");
          
        }
        
        public void ButtonEven() 
        {
            buttonEvent. m_ButtonEven.m_Events?.Invoke();
            buttonEvent.m_ButtonEven.DebugData("按下按键偶数次一次");
           
        }

        public void ButtonPress()
        {
            if (!m_LockInteraction) 
            {
                buttonEvent.m_press.m_Events?.Invoke();
                buttonEvent.m_ButtonEven.DebugData("持续按下按键中");
               
            }    
        }
        public void ButtonRelease()
        {
            if (!m_LockInteraction) 
            {
                buttonEvent.m_Release.m_Events?.Invoke();
                ASERelease();
                buttonEvent.m_ButtonEven.DebugData("松开按键一次");
               
            }    
        }

        public void Enter() 
        {
            if (!m_LockInteraction) 
            {
                triggerEvent.Enter.m_Events?.Invoke();
                ASEEnter();
                triggerEvent.Enter.DebugData("触发进入一次");
            }       
        }
        
        public void Stay() 
        {

            if (!m_LockInteraction) 
            {
                triggerEvent.Stay.m_Events?.Invoke();
                triggerEvent.Stay.DebugData("触发中持续执行");
            }       
        }
        
        public void Exit() 
        {
            if (!m_LockInteraction) 
            {
                triggerEvent.Exit.m_Events?.Invoke();
                ASEExit();
                triggerEvent.Exit.DebugData("离开触发一次");
            }
        }

        public void ASEEnter() //进入触发音效
        {
            if (!m_LockInteraction)
                IOSE.PlayAudio(SETriggerState.Enter);
        }
        public void ASEExit()//离开触发音效
        {
            if (!m_LockInteraction)
                IOSE.PlayAudio(SETriggerState.Exit);
        }
        public void ASEPress()//按下触发音效
        {
            if (!m_LockInteraction)
                IOSE.PlayAudio(SETriggerState.Press);
        }
        public void ASERelease()//松开触发音效
        {
            if (!m_LockInteraction)
                IOSE.PlayAudio(SETriggerState.Release);
        }


        public void AddLinkResponse()
        {
            Debug.Log(linkResponse.ToString());
            if (linkResponse.ToString() == "-1")
            {
                // if (linkResponse.ToString().ToLower().Contains("RectTransformLerp".ToLower()))
                AddRectTransformLerp();

                /// if (linkResponse.ToString().Contains("ImageResponse"))
                AddImageResponse();


                //if (linkResponse.ToString().Contains("TextTMPUIIO"))
                AddTextTMPUIIO();

            }
            else if (linkResponse.ToString() == "0")
            {
                if (transform.GetComponent<RectTransformLerp>() != null)
                    DestroyImmediate(transform.GetComponent<RectTransformLerp>());

                if (transform.GetComponent<ImageResponse>() != null)
                    DestroyImmediate(transform.GetComponent<ImageResponse>());


                if (transform.GetComponent<TextTMPUIIO>() != null)
                    DestroyImmediate(transform.GetComponent<TextTMPUIIO>());
            }
            else
            {
                if (linkResponse.ToString().ToLower().Contains("RectTransformLerp".ToLower()))
                    AddRectTransformLerp();
                else if (transform.GetComponent<RectTransformLerp>() != null)
                    DestroyImmediate(transform.GetComponent<RectTransformLerp>());
                if (linkResponse.ToString().Contains("ImageResponse"))
                    AddImageResponse();
                else if (transform.GetComponent<ImageResponse>() != null)
                    DestroyImmediate(transform.GetComponent<ImageResponse>());

                if (linkResponse.ToString().Contains("TextTMPUIIO")) AddTextTMPUIIO();
                else if (transform.GetComponent<TextTMPUIIO>() != null)
                    DestroyImmediate(transform.GetComponent<TextTMPUIIO>());
            }


            //初始化
            IOSE.audios = GetComponent<AudioSource>();
            if (UIAnimation.m_UseRect)
            {
                rect = transform.GetComponent<RectTransformLerp>();
            }
            if (rect != null) 
            {
                if (UIAnimation.m_UseRectScale)
                {
                    rect.oriOffset = UIAnimation.tar;
                    rect.speed = UIAnimation.speed;
                }
                if (UIAnimation.m_UseRectAngle)
                {
                    rect.oriAngleOffset = UIAnimation.tarAngle;
                    rect.speedangle = UIAnimation.speedangle;
                }

                if (UIAnimation.m_UseRectSizeDelta)
                {
                    rect.orisizeDeltaOffset = UIAnimation.tarsizeDelta;
                    rect.speedsizeDelta = UIAnimation.speedtarsizeDelta;
                }
            }
           

            ImageResponse ir = GetComponent<ImageResponse>();
            if (ir != null)
            {
                Image[] imgs = GetComponentsInChildren<Image>();
                ir.rcs = new ResponseColor[imgs.Length];
                for (int i=0;i<imgs.Length;i++)
                {
                    ir.rcs[i].images = imgs[i];
                    ir.rcs[i].response = ResponseFlags.Color;
                }
                ir.responseColor = ImageResponseSet.responseColor;
                ir.lockColor = ImageResponseSet.lockColor;
            }

            TextTMPUIIO tMPUIIO = GetComponent<TextTMPUIIO>();
            if (tMPUIIO != null)
            {
                TextMeshProUGUI[] texs = GetComponentsInChildren<TextMeshProUGUI>();
                tMPUIIO.tmpugcs = new TextMeshProUGUIColor[texs.Length];
                for (int i=0;i<texs.Length;i++)
                {
                    tMPUIIO.tmpugcs[i].textMeshPro = texs[i];
                    tMPUIIO.tmpugcs[i].m_Enable= true;
                    tMPUIIO.tmpugcs[i].allow= true;
                }
                tMPUIIO.ioColor = TMPUIIOSet.responseColor;
                tMPUIIO.lockColor = TMPUIIOSet.lockColor;
            }
        }

        void AddRectTransformLerp()
        {
            transform.gameObject.AddComponent<RectTransformLerp>();
        }

        void AddImageResponse()
        {
            transform.gameObject.AddComponent<ImageResponse>();
        }

        void AddTextTMPUIIO()
        {
            transform.gameObject.AddComponent<TextTMPUIIO>();
        }
    }
   
}

