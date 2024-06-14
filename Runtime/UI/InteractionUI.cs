using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HLVR.EventSystems;
using UnityEngine.UI;
using Unity.VisualScripting;
using HLVR.UI;

namespace HLVR.Interaction
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioSource))]
    public class InteractionUI : MonoBehaviour
    {
        public InteractionEventElement m_Event;
        bool enter=true;
        bool exit;
        private void Reset()
        {
            if (transform.GetComponent<RectTransform>() == null)
            {
                DestroyImmediate(this);
            }
            else 
            {
                RectTransform rectTransform = GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    if (transform.GetComponent<BoxCollider>() == null)
                    {
                        transform.gameObject.AddComponent<BoxCollider>();
                        transform.GetComponent<BoxCollider>().size = new Vector3(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y, 0);
                        transform.GetComponent<BoxCollider>().isTrigger = true;
                    }
                }
            }
            m_Event = GetComponent<InteractionEventElement>();
        }

        public void Enter()
        {
            if (enter)
            {
                m_Event.Enter(); 
                Debug.LogWarning("射线进入了" + transform.name);
                enter = false;
                exit = true;
                m_Event.ASEEnter();
            }
        }
        public void Stay(bool keystate, bool upkey,bool press,bool mousekey ,bool upmousekey,bool mousepress)
        {
            m_Event.Stay();
            if (keystate|| mousekey)
            {
                m_Event.Button();
                m_Event.ASEPress();
                m_Event.OE();
            }
            else if (upkey|| upmousekey)
            {
                m_Event.ButtonRelease();
            }

            if (press || mousepress)
            {
                m_Event.ButtonPress();
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

