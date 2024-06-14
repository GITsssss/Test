using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HLVR.EventSystems;

namespace HLVR.Interaction 
{
    public class OnTriggerEvent : MonoBehaviour
    {
        [Tooltip("����")]
        public bool m_OnEnable;
        [Tooltip("�����ǩ���ݲ�Ϊ�գ�ֻ�з�������������ָ����ǩ���������ִ�д�������")]
        public string tags;
        [Tooltip("���˲�Ƶ�ʱ���Ƿ�ر�����")]
        public bool close;
        public InteractionEvent m_OnTriggerEnter;
        public InteractionEvent m_OnTriggerExit;
        public void OnTriggerEnter(Collider other)
        {
            if (m_OnEnable)
            {
                if (other.tag == tags)
                {
                    m_OnTriggerEnter?.Invoke();
                    this.gameObject.SetActive(!close);
                }
                else if (other.tag == "Player")
                {
                    m_OnTriggerEnter?.Invoke();
                    this.gameObject.SetActive(!close);
                }
            }
           
        }

        public void OnTriggerExit(Collider other)
        {
            if (m_OnEnable) 
            {
                if (other.tag == tags)
                {
                    m_OnTriggerExit?.Invoke();
                    this.gameObject.SetActive(!close);
                }
                else if (other.tag == "Player")
                {
                    m_OnTriggerExit?.Invoke();
                    this.gameObject.SetActive(!close);
                }
            }    
        }
    }
}
