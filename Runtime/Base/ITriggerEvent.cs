using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HLVR.EventSystems 
{
    public interface ITriggerEvent
    {
        public void Enter();
        public void Stay();
        public void Exit();
    }
}

