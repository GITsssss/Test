using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HLVR.Interaction 
{
    [System.Serializable]
    public struct RectTransformSet
    {
        [Tooltip("开启响应")]
        public bool m_UseRect;
        [Tooltip("开启缩放响应")]
        public bool m_UseRectScale;
        [Tooltip("目标缩放")]
        public Vector3 tar;
        [Tooltip("缩放响应速度")]
        public float speed;
        [Tooltip("开启旋转响应")]
        public bool m_UseRectAngle;
        [Tooltip("目标角度")]
        public Vector3 tarAngle;
        [Tooltip("旋转响应速度")]
        public float speedangle;

        [Tooltip("开启尺寸变化响应")]
        public bool m_UseRectSizeDelta;
        public Vector2 tarsizeDelta;
        public float speedtarsizeDelta;
    }

    [System.Serializable]
    public struct ImageResponseSet
    {
        [Tooltip("响应颜色")]
        public Color responseColor;
        [Tooltip("锁定颜色")]
        public Color lockColor;
    }

    [System.Serializable]
    public struct TextTMPUIIOSet
    {
        [Tooltip("响应颜色")]
        public Color responseColor;
        [Tooltip("锁定颜色")]
        public Color lockColor;
    }

    [System.Flags]
    public enum LinkResponse
    {
        RectTransformLerp = 2,
        ImageResponse = 4,
        TextTMPUIIO = 8,
    }

    public class EnumFlags : PropertyAttribute
    {

    }


    [System.Flags]
    public enum ResponseFlags
    {
        Color = 2,
        Enter = 4,
        ClickOdd = 8,
        ClickEven = 16,
    }

    [System.Flags]
    public enum UIImageAnimationFlags
    {
        PixelsPerUnitMultiplier=2,
    }
}
