using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HLVR.InputSystem;
using HLVR.EventSystems;
namespace HLVR.Interaction 
{
    public class TriggerHandEvent : MonoBehaviour
    {
        [Tooltip("可以被哪些控制器触发")]
        public ControllerType TriggerType;
        [Tooltip("触发响应模式 TriggerPressDown:按下按键触发; Release:松开按键触发 OnTrigger:触碰就触发")]
        public HandTriggerResponseEventGroup grabResponse=HandTriggerResponseEventGroup.OnTrigger;
        [Tooltip("如果使用了按键触发模式，则使用哪个按键进行触发")]
        public KeyType key;
        [Tooltip("允许手在抓取物体的时候触发事件")]
        public bool AllowHandGrabingTrigeer;
       
        [ReadOnly]
        public Hand hand;
        public InteractionEvent triggerEvent;
        [Tooltip("执行完事件关闭自身")]
        public bool close;
        
        public bool once=true;
        private void OnEnable()
        {
            triggerEvent.AddListener(SetOnceFalse);
            triggerEvent.AddListener(Close);
        }

        private void OnDisable()
        {
            once = true;
            hand = null;
            triggerEvent.RemoveListener(SetOnceFalse);
            triggerEvent.RemoveListener(Close);
        }

        void Close()
        {
            transform.gameObject.SetActive(!close);
        }

        void SetOnceTrue() 
        {
            once = true;
        }

        void SetOnceFalse()
        {
            once = false;
        }
        void SetOnce(bool state)
        {
            once = state;
        }

        private void Update()
        {
            if (hand != null && once)
            {
                switch (TriggerType)
                {
                    case ControllerType.Left:
                        if (hand.inputSource == TriggerType)
                        {
                            if (!AllowHandGrabingTrigeer)
                            {
                                if (hand.current == null)
                                {
                                    switch (grabResponse)
                                    {
                                        case HandTriggerResponseEventGroup.OnTrigger:
                                            triggerEvent?.Invoke();
                                            break;
                                        case HandTriggerResponseEventGroup.TriggerPressDown:
                                            if (VRInput.GetKeyDown(key, hand.inputSource))
                                            {
                                                triggerEvent?.Invoke();
                                            }
                                            break;
                                        case HandTriggerResponseEventGroup.Release:
                                            if (VRInput.GetKeyUp(key, hand.inputSource))
                                            {
                                                triggerEvent?.Invoke();
                                            }
                                            break;

                                    }
                                }
                            }
                            else
                            {
                                switch (grabResponse)
                                {
                                    case HandTriggerResponseEventGroup.OnTrigger:
                                        triggerEvent?.Invoke();
                                        break;
                                    case HandTriggerResponseEventGroup.TriggerPressDown:
                                        if (VRInput.GetKeyDown(key, hand.inputSource))
                                        {
                                            triggerEvent?.Invoke();
                                        }
                                        break;
                                    case HandTriggerResponseEventGroup.Release:
                                        if (VRInput.GetKeyUp(key, hand.inputSource))
                                        {
                                            triggerEvent?.Invoke();
                                        }
                                        break;

                                }
                            }
                        }
                        break;
                    case ControllerType.Right:
                        if (hand.inputSource == TriggerType)
                        {
                            if (!AllowHandGrabingTrigeer)
                            {
                                if (hand.current == null)
                                {
                                    switch (grabResponse)
                                    {
                                        case HandTriggerResponseEventGroup.OnTrigger:
                                            triggerEvent?.Invoke();
                                            break;
                                        case HandTriggerResponseEventGroup.TriggerPressDown:
                                            if (VRInput.GetKeyDown(key, hand.inputSource))
                                            {
                                                triggerEvent?.Invoke();
                                            }
                                            break;
                                        case HandTriggerResponseEventGroup.Release:
                                            if (VRInput.GetKeyUp(key, hand.inputSource))
                                            {
                                                triggerEvent?.Invoke();
                                            }
                                            break;

                                    }
                                }
                            }
                            else
                            {
                                switch (grabResponse)
                                {
                                    case HandTriggerResponseEventGroup.OnTrigger:
                                        triggerEvent?.Invoke();
                                        break;
                                    case HandTriggerResponseEventGroup.TriggerPressDown:
                                        if (VRInput.GetKeyDown(key, hand.inputSource))
                                        {
                                            triggerEvent?.Invoke();
                                        }
                                        break;
                                    case HandTriggerResponseEventGroup.Release:
                                        if (VRInput.GetKeyUp(key, hand.inputSource))
                                        {
                                            triggerEvent?.Invoke();
                                        }
                                        break;

                                }
                            }
                        }
                        break;
                    case ControllerType.All:

                        if (!AllowHandGrabingTrigeer)
                        {
                            if (hand.current == null)
                            {
                                switch (grabResponse)
                                {
                                    case HandTriggerResponseEventGroup.OnTrigger:
                                        triggerEvent?.Invoke();
                                        break;
                                    case HandTriggerResponseEventGroup.TriggerPressDown:
                                        if (VRInput.GetKeyDown(key, hand.inputSource))
                                        {
                                            triggerEvent?.Invoke();
                                        }
                                        break;
                                    case HandTriggerResponseEventGroup.Release:
                                        if (VRInput.GetKeyUp(key, hand.inputSource))
                                        {
                                            triggerEvent?.Invoke();
                                        }
                                        break;

                                }
                            }
                        }
                        else
                        {
                            switch (grabResponse)
                            {
                                case HandTriggerResponseEventGroup.OnTrigger:
                                    triggerEvent?.Invoke();
                                    break;
                                case HandTriggerResponseEventGroup.TriggerPressDown:
                                    if (VRInput.GetKeyDown(key, hand.inputSource))
                                    {
                                        triggerEvent?.Invoke();
                                    }
                                    break;
                                case HandTriggerResponseEventGroup.Release:
                                    if (VRInput.GetKeyUp(key, hand.inputSource))
                                    {
                                        triggerEvent?.Invoke();
                                    }
                                    break;

                            }
                        }

                        break;
                }

            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<Hand>()!=null)
                hand = other.GetComponent<Hand>();
           
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Hand>() != null)
                hand = null;
        }
    }
    public enum HandTriggerResponseEventGroup
    {
        TriggerPressDown,//按下按键触发
        Release,//释放按键触发
        OnTrigger,//触碰触发
       //允许手正在抓取物体时触发
    }
}

