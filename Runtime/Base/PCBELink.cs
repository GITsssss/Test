using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace HLVR.EventSystems
{
    [DisallowMultipleComponent]
    public class PCBELink : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler,IPointerDownHandler,IPointerUpHandler
    {
        InteractionEventElement eventElement;
        bool oddoreven=true;
        private void Reset()
        {
            if (GetComponent<InteractionEventElement>() == null) 
            {
                Debug.LogWarning("不可以向InteractionEventElement为空的对象添加PCBELink");
                DestroyImmediate(this);
            }
        }

        private void Awake()
        {
            eventElement = GetComponent<InteractionEventElement>();
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            eventElement.Button();
            if (oddoreven)
            {
                eventElement.ButtonOdd();
                oddoreven = !oddoreven;
            }
            else 
            {
                eventElement.ButtonEven();
                oddoreven = !oddoreven;
            }

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            eventElement.Enter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            eventElement.Exit();
        }

        public void OnPointerDown(PointerEventData eventData) 
        {
            eventElement.ButtonPress();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            eventElement.ButtonRelease();
        }
    }
}

