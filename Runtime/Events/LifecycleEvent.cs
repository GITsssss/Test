using UnityEngine;


namespace HLVR.EventSystems
{
    public class LifecycleEvent : MonoBehaviour
    {

        public enum ExecuteType
        {
            Delay,//延迟
            RightAway,//立刻
        }
        public ExecuteType m_ExecuteType = ExecuteType.RightAway;
        [Tooltip("执行完成任意一个生命周期事件后关闭这个物体")]
        public bool m_FinishEventCloseThis;
        [Header("如果选择了Delay模式，那么这个值决定延迟多久去执行事件方法[延迟时间的大小]")]
        public float m_DelayTime;
        public InteractionEvent AwakeEvent;
        public InteractionEvent StartEvent;
        public InteractionEvent OnEnableEvent;
        public InteractionEvent OnDisableEvent;
        public InteractionEvent OnDestroyEvent;
        private void Awake()
        {

            OnDestroyEvent.AddListener(StartEventFunc);
            switch (m_ExecuteType)
            {
                case ExecuteType.Delay:
                    Invoke("AwakeEventFunc", m_DelayTime);
                    break;
                case ExecuteType.RightAway: AwakeEventFunc(); break;
            }
        }

        private void Start()
        {

            switch (m_ExecuteType)
            {
                case ExecuteType.Delay: Invoke("StartEventFunc", m_DelayTime); break;
                case ExecuteType.RightAway: StartEventFunc(); break;
            }

        }

        private void OnEnable()
        {
            switch (m_ExecuteType)
            {
                case ExecuteType.Delay: Invoke("OnEnableEventFunc", m_DelayTime); break;
                case ExecuteType.RightAway: OnEnableEventFunc(); break;
            }
        }

        private void OnDisable()
        {
            switch (m_ExecuteType)
            {
                case ExecuteType.Delay: Invoke("OnDisableEventFunc", m_DelayTime); break;
                case ExecuteType.RightAway: OnDisableEventFunc(); break;
            }
        }   
        private void OnDestroy()
        {
            switch (m_ExecuteType)
            {
                case ExecuteType.Delay: Invoke("OnDestroyEventFunc", m_DelayTime); break;
                case ExecuteType.RightAway: OnDestroyEventFunc(); break;
            }
        }

        void AwakeEventFunc()
        {
            if (AwakeEvent != null) 
            {
                if (m_FinishEventCloseThis) AwakeEvent.AddListener(RunFinishClose);
                AwakeEvent.Invoke();

               
            }
        }

        public void SetEventInvokeState(InteractionEvent interaction, UnityEngine.Events.UnityEventCallState state)
        {
            for (int i=0;i<interaction.GetPersistentEventCount();i++)
            {
                interaction.SetPersistentListenerState(i, state);
            }       
        }

        void StartEventFunc()
        {
            if (StartEvent != null) 
            {
                if (m_FinishEventCloseThis) StartEvent.AddListener(RunFinishClose);
                StartEvent.Invoke();
            }
               
        }
        void OnEnableEventFunc()
        {
            if (OnEnableEvent != null) 
            {
                if (m_FinishEventCloseThis) OnEnableEvent.AddListener(RunFinishClose);
                OnEnableEvent.Invoke();
            }  
        }

        void OnDisableEventFunc()
        {
            if (OnDisableEvent != null) 
            {
                if (m_FinishEventCloseThis) OnDisableEvent.AddListener(RunFinishClose);
                OnDisableEvent.Invoke();
            }
            
        }


        void OnDestroyEventFunc()
        {
            if (OnDestroyEvent != null) 
            {
                if (m_FinishEventCloseThis) OnDestroyEvent.AddListener(RunFinishClose);
                OnDestroyEvent?.Invoke();          
            }           
        }

        private void RunFinishClose()
        {
            this.gameObject.SetActive(false);
        }
    }

}
