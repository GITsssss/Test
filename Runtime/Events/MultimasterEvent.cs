using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HLVR.EventSystems 
{
    public class MultimasterEvent : MonoBehaviour
    {
        public bool[] m_MS;
        public InteractionEvent m_TrueEvent;
        public bool close;
        private void Update()
        {
            if (MTS())
            {
                m_TrueEvent?.Invoke();
                if (close) gameObject.SetActive(false);
            }
        }

        public void SetSwichStateTrue(int index)
        {
            if (index >= 0 && index < m_MS.Length)
                m_MS[index] = true;
        }
        public void SetSwichStateFalse(int index)
        {
            if (index >= 0 && index < m_MS.Length)
                m_MS[index] = false;
        }

        private bool MTS()
        {
            for (int i = 0; i < m_MS.Length; i++)
            {
                if (!m_MS[i]) return false;
            }

            return true;
        }
    }

}
