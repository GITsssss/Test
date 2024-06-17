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
    string[] normalString;
    string[] laststring;
    public Color ioColor;
    public Color lockColor = new Color(0.2f, 0.2f, 0.2f, 0.2f);
    InteractionEventElement io;
    private void Awake()
    {
        GetIOGroundImage();
        io = GetComponent<InteractionEventElement>();
        normalColor = new Color[tmpugcs.Length];
        normalString= new string[tmpugcs.Length];
        laststring=new string[tmpugcs.Length];
        for (int i = 0; i < tmpugcs.Length; i++) 
        {
            normalColor[i] = tmpugcs[i].textMeshPro.color;
            normalString[i] = tmpugcs[i].textMeshPro.text ;
            laststring[i]= tmpugcs[i].textMeshPro.text;
        }
       
    }
    
    private void OnEnable()
    {
        io.triggerEvent.Exit.m_Events.AddListener(NormalColor);
        io.buttonEvent.m_ButtonOdd.m_Events.AddListener(SetResponseClickOddString);
        io.buttonEvent.m_ButtonEven.m_Events.AddListener(SetResponseClickEvenString);
        io.triggerEvent.Enter.m_Events.AddListener(IOColor);
        io.triggerEvent.Enter.m_Events.AddListener(SetResponseEnterString);
    }

    private void OnDisable()
    {
        io.triggerEvent.Exit.m_Events.RemoveListener(NormalColor);
        io.buttonEvent.m_ButtonOdd.m_Events.RemoveListener(SetResponseClickOddString);
        io.buttonEvent.m_ButtonEven.m_Events.RemoveListener(SetResponseClickEvenString);
        io.triggerEvent.Enter.m_Events.RemoveListener(IOColor);
        io.triggerEvent.Enter.m_Events.RemoveListener(SetResponseEnterString);
    }

    public void NormalColor()
    {
        for (int i = 0; i < tmpugcs.Length; i++) 
        {
            tmpugcs[i].textMeshPro.color = normalColor[i];

            if (tmpugcs[i].textMeshPro.text == tmpugcs[i].enter)
            {
                tmpugcs[i].textMeshPro.text = laststring[i];
            }
            else
            if ((!tmpugcs[i].responseFlags.ToString().Contains("ClickEven") && !tmpugcs[i].responseFlags.ToString().Contains("ClickOdd")) && tmpugcs[i].responseFlags.ToString() != "-1")
                tmpugcs[i].textMeshPro.text = normalString[i];



        }     
    }

    public void IOColor()
    {
        for (int i = 0; i < tmpugcs.Length; i++) 
        {
            if (tmpugcs[i].allow)
                tmpugcs[i].textMeshPro.color = ioColor;
            else if (tmpugcs[i].m_Enable )
            {
                if(tmpugcs[i].responseFlags.ToString().Contains("Color")|| tmpugcs[i].responseFlags.ToString()=="-1")
                tmpugcs[i].textMeshPro.color = tmpugcs[i].color;
            }
        }
        
    }

    void SetResponseClickEvenString() 
    {
        for (int i = 0; i < tmpugcs.Length; i++)
        {
            if (tmpugcs[i].m_Enable)
            {
                if (tmpugcs[i].responseFlags.ToString().Contains("ClickEven") || tmpugcs[i].responseFlags.ToString() == "-1")
                    tmpugcs[i].textMeshPro.text = tmpugcs[i].even;
            }
        }
    }

    void SetResponseClickOddString()
    {
        for (int i = 0; i < tmpugcs.Length; i++)
        {
            if (tmpugcs[i].m_Enable)
            {
                if (tmpugcs[i].responseFlags.ToString().Contains("ClickOdd") || tmpugcs[i].responseFlags.ToString() == "-1")
                    tmpugcs[i].textMeshPro.text= tmpugcs[i].odd;
            }
        }
    }

    void SetResponseEnterString()
    {
        for (int i = 0; i < tmpugcs.Length; i++)
        {
            if (tmpugcs[i].m_Enable)
            {
                if (tmpugcs[i].responseFlags.ToString().Contains("Enter") || tmpugcs[i].responseFlags.ToString() == "-1") 
                {
                    laststring[i] = tmpugcs[i].textMeshPro.text;
                    tmpugcs[i].textMeshPro.text = tmpugcs[i].enter;          
                }
                 
            }
        }
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
    [Tooltip("开启响应")]
    public bool m_Enable;
    [Tooltip("允许统一管理")]
    public bool allow;
    public TextMeshProUGUI textMeshPro;
    public string enter;
    public string odd;
    public string even;
    public Color color;
    public ResponseFlags responseFlags;
}
