using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using HLVR.Interaction;
using HLVR.EventSystems;
[DisallowMultipleComponent]
public class TextTMPUIIO : MonoBehaviour
{
    public TextMeshProUGUIColor[] tmpugcs;
     Color[] normalColor;
    public Color ioColor;
    public Color lockColor = new Color(0.2f, 0.2f, 0.2f, 0.2f);
    InteractionEventElement io;
    private void Awake()
    {
        GetIOGroundImage();
        io = GetComponent<InteractionEventElement>();
        normalColor = new Color[tmpugcs.Length];
        for (int i = 0; i < tmpugcs.Length; i++)
            normalColor[i] = tmpugcs[i].textMeshPro.color;
    }
    
    private void OnEnable()
    {
        io.triggerEvent.Exit.m_Events.AddListener(NormalColor);
        io.triggerEvent.Enter.m_Events.AddListener(IOColor);
    }

    private void OnDisable()
    {
        io.triggerEvent.Exit.m_Events.RemoveListener(NormalColor);
        io.triggerEvent.Enter.m_Events.RemoveListener(IOColor);
    }

    public void NormalColor()
    {
        for (int i = 0; i < tmpugcs.Length; i++)
            if (tmpugcs[i].response)
                tmpugcs[i].textMeshPro.color = normalColor[i];
    }

    public void IOColor()
    {
        for (int i = 0; i < tmpugcs.Length; i++)
            if (tmpugcs[i].response)
            tmpugcs[i].textMeshPro.color = ioColor;
    }

    void GetIOGroundImage()
    {
        TextMeshProUGUI[] tmp = GetComponentsInChildren<TextMeshProUGUI>();
        for (int i = 0; i < tmp.Length; i++)
        {
            tmpugcs[i].textMeshPro = tmp[i];
        }
    }
    public void Lock()
    {
        if (io.m_LockInteraction)
        {
            for (int i = 0; i < tmpugcs.Length; i++)
                //if (rcs[i].response)
                tmpugcs[i].textMeshPro.color = lockColor;
        }
    }

    public void UnLock()
    {
        if (!io.m_LockInteraction)
        {
            for (int i = 0; i < tmpugcs.Length; i++)
                tmpugcs[i].textMeshPro.color = normalColor[i];
        }
    }
}

[System.Serializable]
public struct TextMeshProUGUIColor
{
    public TextMeshProUGUI textMeshPro;
    public bool response;
}
