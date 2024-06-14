using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HLVR.EventSystems;
using UnityEngine.UI;
using Unity.VisualScripting;
using HLVR.UI;

namespace HLVR.Interaction
{
    [RequireComponent(typeof(AudioSource))]
    public class InteractionObject : MonoBehaviour
    {
        public InteractionEventElement  m_Event;
        bool enter = true;
        bool exit;
        RectTransform rectTransform;
        RectTransformLerp r;
        private void Start()
        {
          
            rectTransform = GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                if (transform.GetComponent<BoxCollider>() == null) 
                {
                    transform.gameObject.AddComponent<BoxCollider>();
                    transform.GetComponent<BoxCollider>().size = new Vector3(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y, 0);
                    transform.GetComponent<BoxCollider>().isTrigger = true;
                }        
            }
          


            if (LayerMask.NameToLayer("IO") > 5) 
            {
                transform.gameObject.layer = LayerMask.NameToLayer("IO");
            }
            else
            {
                Debug.LogWarning("请您创建一个名字叫“IO”的层级");
            }
        }
        public void ResetState()
        {
            enter = true;
            exit = false;
        }
        public void Enter()
        {
            if (enter)
            {
                m_Event.Enter();
                Debug.LogWarning("射线进入了"+transform.name);
                enter = false;
                exit = true;
                m_Event.ASEEnter();
            }
        }

        
        public void Stay(bool keystate,bool upkey,bool mousekey,bool mouseUpkey)
        {
            m_Event.Stay();
            if (keystate|| mousekey)
            {
                m_Event.Button();
                m_Event.ASEPress();
                //audios?.PlayOneShot(iOAudio.buttonKey);
                m_Event.OE();
            }
            else if(upkey || mouseUpkey)
            {
                m_Event.ButtonRelease();
                m_Event.ASERelease();
            }
            
        }

        public void Exit()
        {

            Debug.LogWarning("射线离开了" + transform.name);
            if (exit)
            {
                m_Event.Exit();
                exit = false;
                enter = true;
                m_Event.ASEExit();
            }
        }
    }


   
}

