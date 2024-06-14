using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HLVR.EventSystems;
namespace HLVR.Animations 
{
    [System.Serializable]
    public struct AnimatorEventID
    {
        [Tooltip("动画事件接口")]
        public InteractionEvent Event;
        [Tooltip("动画播放几次之后执行动画事件接口中的事件")]
        public int m_CountRun;
        [HideInInspector]
        [Tooltip("动画事件计数器")]
        public int countPlay;
    }
}
