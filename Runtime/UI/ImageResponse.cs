using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HLVR.Interaction;
using HLVR.EventSystems;
[DisallowMultipleComponent]
public class ImageResponse : MonoBehaviour
{
    public ResponseColor[] rcs;
    Color[] normalColor;
    public Color responseColor=Color.white;
    public Color lockColor = new Color(0.2f,0.2f,0.2f,0.2f);
    InteractionEventElement io;
   
    private void Awake()
    {
        GetIOGroundImage();
        io = GetComponent<InteractionEventElement>();
        normalColor = new Color[rcs.Length]; 
        for (int i = 0; i < rcs.Length; i++)
            normalColor[i] = rcs[i].images.color;

        Lock();
    }

    public void Lock()
    {
        if (io!=null&&io.m_LockInteraction)
        {
            for (int i = 0; i < rcs.Length; i++)
                //if (rcs[i].response)
                rcs[i].images.color = lockColor;
        }
        else if (io == null)
        {
            DebugInfo.DebugLog(OutColor.Red, this.name);
        }
    }

    public void UnLock()
    {
        if (io != null && !io.m_LockInteraction)
        {
            for (int i = 0; i < rcs.Length; i++)
                rcs[i].images.color = normalColor[i];
        } else if (io == null)
        {
            DebugInfo.DebugLog(OutColor.Red,this.name);
        }
    }


    void GetIOGroundImage()
    {
        Image[] img = GetComponentsInChildren<Image>();
        for (int i = 0; i < img.Length; i++)
        {
            rcs[i].images= img[i];
        }
    }

    private void OnEnable()
    {
        //UnLock();
        io.triggerEvent.Enter.m_Events.AddListener(SetResponseColor);
        io.triggerEvent.Exit.m_Events.AddListener(SetNormalColor);
    }

    private void OnDisable()
    {
        // Lock();
        io.triggerEvent.Enter.m_Events.RemoveListener(SetResponseColor);
        io.triggerEvent.Exit.m_Events.RemoveListener(SetNormalColor);
    }


    void SetNormalColor()
    {
        for (int i = 0; i < rcs.Length; i++)
        {
            rcs[i].images.color = normalColor[i];
        }
       
    }


    void SetResponseColor()
    {
        for (int i = 0; i < rcs.Length; i++)
        {
            if (rcs[i].response)
            rcs[i].images.color = responseColor;
        }      
    }


}

[System.Serializable]
public struct ResponseColor 
{
    public Image images;
    public bool response;
}

//[System.Serializable]
//public struct ImageResponseCom
//{
//    public bool m_LockInteraction;
//    public ImageResponse ir;
//}
