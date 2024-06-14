using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HLVR.Interaction;
using HLVR.EventSystems;


namespace HLVR.Interaction 
{
    public class TeleportPoint : MonoBehaviour
    {
        public bool m_OnEnable;
        public TriggerEvent m_MoveEvent;
        [Tooltip("�Ƿ�ر�����")]
        public bool close = true;
        InteractionRay ir;
        public bool synchronizationRotation;
        private void Awake()
        {
            ir = GameObject.FindObjectOfType<InteractionRay>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.parent!=null&&other.transform.parent.GetComponent<InteractionRay>() != null && m_OnEnable)
            {
                m_MoveEvent.Enter.m_Events?.Invoke();
                m_MoveEvent.Enter.DebugData("��������");
                if (close) transform.gameObject.SetActive(false);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.transform.parent != null && other.transform.parent.GetComponent<InteractionRay>() != null && m_OnEnable)
            {
                m_MoveEvent.Exit.m_Events?.Invoke();
                m_MoveEvent.Exit.DebugData("�����뿪");
                if (close) transform.gameObject.SetActive(false);
            }        
        }

        public void SetVRPlayerPos(Transform tar)
        {
            ir.transform.position = tar.position;
        }

    }

}
