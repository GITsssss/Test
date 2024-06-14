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
        [Tooltip("������ť�¼���ÿ�ΰ��¶������һ��")]
        public EventElement m_Button;
        [Tooltip("������ť�¼���ÿ�ΰ���Ϊ�����������һ��")]
        public EventElement m_ButtonOdd;
        [Tooltip("������ť�¼���ÿ�ΰ���Ϊż���������һ��")]
        public EventElement m_ButtonEven;
        [Tooltip("������ť�¼����ɿ���ťʱ�����һ��")]
        public EventElement m_Release;
        [Tooltip("������ť�¼������ڰ���״̬ʱ��һֱ����")]
        public EventElement m_press;
    }

    [System.Serializable]//grabTriggerEvent �ṹ
    public struct MultipleIDEvent
    {
        public int ID;
        public InteractionEvent m_ResponseEvent;
    }

    [System.Serializable]
    public struct TriggerEvent 
    {
        [Tooltip("���������һ��")]
        public EventElement Enter;
        [Tooltip("����������������")]
        public EventElement Stay;
        [Tooltip("�������������һ��")]
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
        //[Tooltip("�Ƿ񽫴��¼���ӵ�Unity Button ���¼���")]
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

