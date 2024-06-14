using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HLVR.EventSystems
{
    [System.Serializable]
    public struct GrabEvent
    {
        [Tooltip("启用事件")]
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
        [Tooltip("启用")]
        public bool m_Enable;
        [Tooltip("抛出物体的速度类型;Auto：根据控制器的挥动速度和大小进行抛出，Value:根据value值的大小沿着控制器挥动的方向进行抛出")]
        public ThrowVelocityType velocityType;
        [Tooltip("抛出物体的速度大小")]
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
            Auto,//根据控制器的挥动速度，自动计算出速度方向和大小。
            Value//根据输入的值，沿着控制器挥动的方向抛出
        }
    }

    [System.Serializable]
    public struct Specific
    {
        [Tooltip("确立为指定ID触发事件")]
        public bool specific;
        [Tooltip("碰撞ID号码")]
        [ClampInt(0, 9999)]
        public int m_Id;
    }

    [System.Serializable]
    public struct FallEvent
    {
        [Tooltip("松开按键成功触发事件后，关闭自身")]
        public bool FallHandIDClose;
        [Tooltip("松开按键成功触发事件后，开启刚体静态")]
        public bool FallHandIDRigStatic;

    }

    /// <summary>
    /// Transform 追踪手部输入的数据 Child 作为手的子物体
    /// </summary>
    public enum GrabType
    {
        RigidbodyPosition,
        Child,
    }

    
}


