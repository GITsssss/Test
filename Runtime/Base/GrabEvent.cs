using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HLVR.EventSystems
{
    [System.Serializable]
    public struct GrabEvent
    {
        [Tooltip("�����¼�")]
        public bool m_Enable;

        public FallEvent fallEvent;
        [HideInInspector]
        public bool once;

        public InteractionEvent EnterTouch;
        public InteractionEvent EixtTouch;
        public InteractionEvent grabEnter;
        public InteractionEvent Fall;

        public void EnterTouchEvent()
        {
            if (m_Enable)
                EnterTouch?.Invoke();
        }

        public void EixtTouchEvent()
        {
            if (m_Enable)
                EixtTouch?.Invoke();
        }

        public void GrabEnterEvent()
        {
            if (m_Enable && !once)
            {
                Debug.LogWarning(6666666);
                grabEnter?.Invoke();
                once = true;
            }
        }

        public void FallEvent()
        {
            if (m_Enable)
            {
                Fall?.Invoke();
                once = false;
            }
        }
    }

    [System.Serializable]
    public struct PhysicsEvent
    {
        [Tooltip("����")]
        public bool m_Enable;
        [Tooltip("�׳�������ٶ�����;Auto�����ݿ������ĻӶ��ٶȺʹ�С�����׳���Value:����valueֵ�Ĵ�С���ſ������Ӷ��ķ�������׳�")]
        public ThrowVelocityType velocityType;
        [Tooltip("�׳�������ٶȴ�С")]
        public float value;
        public void ThrowObject(Transform transform, Vector3 velocity)
        {
            if (m_Enable)
            {
                switch (velocityType)
                {
                    case ThrowVelocityType.Auto:
                        transform.GetComponent<Rigidbody>().velocity = velocity;
                        break;
                    case ThrowVelocityType.Value:
                        transform.GetComponent<Rigidbody>().velocity = velocity.normalized * value;
                        break;
                }

            }
        }

        public enum ThrowVelocityType
        {
            Auto,//���ݿ������ĻӶ��ٶȣ��Զ�������ٶȷ���ʹ�С��
            Value//���������ֵ�����ſ������Ӷ��ķ����׳�
        }
    }

    [System.Serializable]
    public struct Specific
    {
        [Tooltip("ȷ��Ϊָ��ID�����¼�")]
        public bool specific;
        [Tooltip("��ײID����")]
        [ClampInt(0, 9999)]
        public int m_Id;
    }

    [System.Serializable]
    public struct FallEvent
    {
        [Tooltip("�ɿ������ɹ������¼��󣬹ر�����")]
        public bool FallHandIDClose;
        [Tooltip("�ɿ������ɹ������¼��󣬿������徲̬")]
        public bool FallHandIDRigStatic;

    }

    /// <summary>
    /// Transform ׷���ֲ���������� Child ��Ϊ�ֵ�������
    /// </summary>
    public enum GrabType
    {
        RigidbodyPosition,
        Child,
    }

    
}


