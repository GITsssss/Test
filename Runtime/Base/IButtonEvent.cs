using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HLVR.EventSystems 
{
    public interface IButtonEventable
    {
        public void Button();
        public void ButtonOdd();
        public void ButtonEven();
        public void ButtonRelease();
        public void ButtonPress();
    }
}

