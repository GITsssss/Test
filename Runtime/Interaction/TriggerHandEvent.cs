using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HLVR.InputSystem;
using HLVR.EventSystems;
namespace HLVR.Interaction 
{
    public class TriggerHandEvent : MonoBehaviour
    {
        [Tooltip("���Ա���Щ����������")]
        public ControllerType TriggerType;
        [Tooltip("������Ӧģʽ TriggerPressDown:���°�������; Release:�ɿ��������� OnTrigger:�����ʹ���")]
        public HandTriggerResponseEventGroup grabResponse=HandTriggerResponseEventGroup.OnTrigger;
        [Tooltip("���ʹ���˰�������ģʽ����ʹ���ĸ��������д���")]
        public KeyType key;
        [Tooltip("��������ץȡ�����ʱ�򴥷��¼�")]
        public bool AllowHandGrabingTrigeer;
       
        [ReadOnly]
        public Hand hand;
        public InteractionEvent triggerEvent;
        [Tooltip("ִ�����¼��ر�����")]
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
        TriggerPressDown,//���°�������
        Release,//�ͷŰ�������
        OnTrigger,//��������
       //����������ץȡ����ʱ����
    }
}

