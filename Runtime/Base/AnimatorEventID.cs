using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HLVR.EventSystems;
namespace HLVR.Animations 
{
    [System.Serializable]
    public struct AnimatorEventID
    {
        [Tooltip("�����¼��ӿ�")]
        public InteractionEvent Event;
        [Tooltip("�������ż���֮��ִ�ж����¼��ӿ��е��¼�")]
        public int m_CountRun;
        [HideInInspector]
        [Tooltip("�����¼�������")]
        public int countPlay;
    }
}
