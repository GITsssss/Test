using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HLVR.EventSystems;

namespace HLVR.Interaction 
{
    public class OnTriggerEvent : MonoBehaviour
    {
        [Tooltip("启用")]
        public bool m_OnEnable;
        [Tooltip("如果标签内容不为空，只有发生触发物体是指定标签的物体才能执行触发函数")]
        public string tags;
        [Tooltip("完成瞬移的时候是否关闭自身")]
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
