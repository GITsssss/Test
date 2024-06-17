using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HLVR.Interaction;
using HLVR.EventSystems;
namespace HLVR.UI 
{
    [DisallowMultipleComponent]
    public class RectTransformLerp : MonoBehaviour
    {
        public RectTransform rectTransform;
        Vector3 oriScale;
        Vector3 oriAngle;
        Vector2 orisizeDelta;

        public Vector3 tar;
        public Vector3 tarangle;
        public Vector2 tarsizeDelta;
        
        [HideInInspector]
        public Vector3 oriOffset;
        [HideInInspector]
        public Vector3 oriAngleOffset;
        [HideInInspector]
        public Vector2 orisizeDeltaOffset;

        public float speed=0.2f;
        public float speedangle = 0.2f;
        public float speedsizeDelta = 0.2f;
        float lerp;
        bool go;
        bool back;
        InteractionEventElement io;



        private void Awake()
        {
            if(rectTransform==null)
            rectTransform = GetComponent<RectTransform>();
            oriScale = rectTransform.localScale;
            oriAngle = rectTransform.eulerAngles;
            orisizeDelta = rectTransform.sizeDelta;
            io = GetComponent<InteractionEventElement>();
        }
        private void Start()
        {
            tar = oriScale + tar + oriOffset;
            tarangle = oriAngleOffset + tarangle + oriAngle;
            tarsizeDelta= orisizeDelta + tarsizeDelta+orisizeDeltaOffset;
        }
        private void Update()
        {
            Lerp();
        }

        void Lerp()
        {
            if (io.UIAnimation.m_UseRectScale)
                rectTransform.localScale = Vector3.Lerp(oriScale, tar, LerpValue(speed));
            if(io.UIAnimation.m_UseRectAngle)
            rectTransform.eulerAngles =Vector3.Lerp(oriAngle, tarangle, LerpValue(speedangle)) ;
            if (io.UIAnimation.m_UseRectSizeDelta)
                rectTransform.sizeDelta = Vector3.Lerp(orisizeDelta, tarsizeDelta, LerpValue(speedsizeDelta));
        }

        private void OnEnable()
        {
            io.triggerEvent.Enter.m_Events.AddListener(Go);
            io.triggerEvent.Exit.m_Events.AddListener(Back);
        }


        private void OnDisable()
        {
            rectTransform.localScale = oriScale;
            //Back();
            io.triggerEvent.Enter.m_Events.RemoveListener(Go);
            io.triggerEvent.Exit.m_Events.RemoveListener(Back);
        }

        public void Go()
        {
            go = true;
            back = false;
        }

        public void Back()
        {
            go = false;
            back = true;
        }

        float LerpValue(float speed)
        {
            if (go) 
            {
                lerp += Time.deltaTime * speed;
                if (lerp >= 1) go = false;
            }

            if (back) 
            {
                lerp -= Time.deltaTime * speed;
                if (lerp <= 0) back = false;
            }
           
            return Mathf.Clamp(lerp,0,1);
        }

    }
}


