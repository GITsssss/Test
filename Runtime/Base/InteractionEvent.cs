using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
namespace HLVR.EventSystems
{
    [System.Serializable]
    public class InteractionEvent:UnityEvent
    {

    }

    [System.Serializable]
    public struct ButtonEvent
    {
        [Tooltip("按击按钮事件，每次按下都会调用一次")]
        public EventElement m_Button;
        [Tooltip("按击按钮事件，每次按下为单数都会调用一次")]
        public EventElement m_ButtonOdd;
        [Tooltip("按击按钮事件，每次按下为偶数都会调用一次")]
        public EventElement m_ButtonEven;
        [Tooltip("按击按钮事件，松开按钮时会调用一次")]
        public EventElement m_Release;
        [Tooltip("按击按钮事件，处于按下状态时会一直调用")]
        public EventElement m_press;
    }

    [System.Serializable]//grabTriggerEvent 结构
    public struct MultipleIDEvent
    {
        public int ID;
        public InteractionEvent m_ResponseEvent;
    }

    [System.Serializable]
    public struct TriggerEvent 
    {
        [Tooltip("触发后调用一次")]
        public EventElement Enter;
        [Tooltip("持续触发持续调用")]
        public EventElement Stay;
        [Tooltip("触发结束后调用一次")]
        public EventElement Exit;
    }

    [System.Serializable]
    public struct CustomEvents
    {
        public string tag;
        public InteractionEvent m_Events;
    }


    [System.Serializable]
    public struct EventElement
    {
        //[Tooltip("是否将此事件添加到Unity Button 的事件下")]
        //public bool m_IsAddListenerUGUIButton;
        public bool debug;
        public InteractionEvent m_Events;

        public void DebugData(string info)
        {
            if (debug)
            Debug.LogWarning(info);
        }
    }


    [System.Serializable]
    public struct OnceEvent 
    {
        public InteractionEvent m_Event;
        [ReadOnly]
        public bool once;
        [ReadOnly]
        public float timer;
        public float time;
        public void Event()
        {
            if (!once) 
            {
                m_Event?.Invoke();
                once = true;
            }
        }

        public void EventInterval()
        {
            timer += Time.deltaTime;
            if (timer>= time)
            {
                m_Event?.Invoke();
                timer = 0;
            }
        }

        public void Reset()
        {
            if(once)
            once = false;
        }
    }

   
}

