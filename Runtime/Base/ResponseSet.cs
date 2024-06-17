using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HLVR.Interaction 
{
    [System.Serializable]
    public struct RectTransformSet
    {
        [Tooltip("������Ӧ")]
        public bool m_UseRect;
        [Tooltip("����������Ӧ")]
        public bool m_UseRectScale;
        [Tooltip("Ŀ������")]
        public Vector3 tar;
        [Tooltip("������Ӧ�ٶ�")]
        public float speed;
        [Tooltip("������ת��Ӧ")]
        public bool m_UseRectAngle;
        [Tooltip("Ŀ��Ƕ�")]
        public Vector3 tarAngle;
        [Tooltip("��ת��Ӧ�ٶ�")]
        public float speedangle;

        [Tooltip("�����ߴ�仯��Ӧ")]
        public bool m_UseRectSizeDelta;
        public Vector2 tarsizeDelta;
        public float speedtarsizeDelta;
    }

    [System.Serializable]
    public struct ImageResponseSet
    {
        [Tooltip("��Ӧ��ɫ")]
        public Color responseColor;
        [Tooltip("������ɫ")]
        public Color lockColor;
    }

    [System.Serializable]
    public struct TextTMPUIIOSet
    {
        [Tooltip("��Ӧ��ɫ")]
        public Color responseColor;
        [Tooltip("������ɫ")]
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
