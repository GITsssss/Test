using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HLVR.Interaction;
using HLVR.EventSystems;
using log4net.Core;

[DisallowMultipleComponent]
public class ImageResponse : MonoBehaviour
{
    public ResponseColor[] rcs;
    Color[] normalColor;
    Sprite[] normalSprite;
    Sprite[] lastSprite;
    public bool selectAll;
    public Color responseColor=Color.white;
    public Color lockColor = new Color(0.2f,0.2f,0.2f,0.2f);
    InteractionEventElement io;

    bool go;
    bool back;

    private void Awake()
    {
        GetIOGroundImage();
        io = GetComponent<InteractionEventElement>();
        normalColor = new Color[rcs.Length];
        normalSprite=new Sprite[rcs.Length];
        lastSprite=new Sprite[rcs.Length];  
        for (int i = 0; i < rcs.Length; i++) 
        {
            normalColor[i] = rcs[i].images.color;
            normalSprite[i] = rcs[i].images.sprite;
            lastSprite[i] = normalSprite[i];
            if (rcs[i].images.type == Image.Type.Sliced) 
            {
                rcs[i].responseAnimation.orippum = rcs[i].images.pixelsPerUnitMultiplier;
                rcs[i].responseAnimation.tarppum = rcs[i].responseAnimation.orippum + rcs[i].responseAnimation.tarppum;
            }

        }

        Lock();
    }

    public void Lock()
    {
        if (io!=null&&io.m_LockInteraction)
        {
            for (int i = 0; i < rcs.Length; i++) 
            {
                rcs[i].images.color = lockColor;
            }
                //if (rcs[i].response)
               
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
            {
                rcs[i].images.color = normalColor[i];
            }
              
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
        io.triggerEvent.Enter.m_Events.AddListener(SetResponseEnterSprite);
        io.buttonEvent.m_ButtonOdd.m_Events.AddListener(SetResponseClickOddSprite);
        io.buttonEvent.m_ButtonEven.m_Events.AddListener(SetResponseClickEvenSprite);
        io.triggerEvent.Exit.m_Events.AddListener(SetNormalColor);
        io.triggerEvent.Exit.m_Events.AddListener(SetNormalSprite);
        io.triggerEvent.Enter.m_Events.AddListener(Go);
        io.triggerEvent.Exit.m_Events.AddListener(Back);
    }

    private void OnDisable()
    {
        // Lock();
        io.triggerEvent.Enter.m_Events.RemoveListener(SetResponseEnterSprite);
        io.triggerEvent.Enter.m_Events.RemoveListener(SetResponseColor);
        io.buttonEvent.m_ButtonOdd.m_Events.RemoveListener(SetResponseClickOddSprite);
        io.buttonEvent.m_ButtonEven.m_Events.RemoveListener(SetResponseClickEvenSprite);
        io.triggerEvent.Exit.m_Events.RemoveListener(SetNormalColor);
        io.triggerEvent.Exit.m_Events.RemoveListener(SetNormalSprite);
        io.triggerEvent.Enter.m_Events.RemoveListener(Go);
        io.triggerEvent.Exit.m_Events.RemoveListener(Back);
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
            if (rcs[i].response.ToString().Contains("Color")|| rcs[i].response.ToString()=="-1") 
            {
                if (selectAll) 
                {
                    rcs[i].color = responseColor;
                }
                
                rcs[i].images.color = rcs[i].color;
            }       
        }      
    }


    void SetNormalSprite()
    {
        for (int i = 0; i < rcs.Length; i++)
        {
            if (!rcs[i].response.ToString().Contains("ClickOdd") && !rcs[i].response.ToString().Contains("ClickEven")&& rcs[i].response.ToString()!="-1")
                rcs[i].images.sprite = normalSprite[i];
            else 
            {
                rcs[i].images.sprite= lastSprite[i];
            }
        }
    }
    void SetResponseEnterSprite()
    {
        for (int i = 0; i < rcs.Length; i++)
        {
            if (rcs[i].response.ToString().Contains("Enter")|| rcs[i].response.ToString() == "-1")
            {
                lastSprite[i] = rcs[i].images.sprite;
                rcs[i].images.sprite = rcs[i].enter;
            }
        }
    }

    void SetResponseClickOddSprite()
    {
        for (int i = 0; i < rcs.Length; i++)
        {
            if (rcs[i].response.ToString().Contains("ClickOdd")|| rcs[i].response.ToString() == "-1")
            {  
                rcs[i].images.sprite = rcs[i].clickodd;
                lastSprite[i] = rcs[i].images.sprite;
            }
        }
    }

    void SetResponseClickEvenSprite()
    {
        for (int i = 0; i < rcs.Length; i++)
        {
            if (rcs[i].response.ToString().Contains("ClickEven")|| rcs[i].response.ToString() == "-1")
            {
                
                rcs[i].images.sprite = rcs[i].clickeven;
                lastSprite[i] = rcs[i].images.sprite;
            }
        }
    }




    float lerp;
   

    private void Update()
    {
        Animation();
    }
    void Animation()
    {
        for (int i = 0; i < rcs.Length; i++)
        {
            if (rcs[i].responseAnimation.uiaFlags.ToString().Contains("PixelsPerUnitMultiplier") || rcs[i].responseAnimation.uiaFlags.ToString() == "-1")
            {
               rcs[i].images.pixelsPerUnitMultiplier = Mathf.Lerp(rcs[i].responseAnimation.orippum, rcs[i].responseAnimation.tarppum, LerpValue(rcs[i].responseAnimation.speed));
            }
        }
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

        return Mathf.Clamp(lerp, 0, 1);
    }

}

[System.Serializable]
public struct ResponseColor 
{
    public Image images;
    public Sprite enter;
    public Sprite clickodd;
    public Sprite clickeven;
    public Color color;
    [Tooltip("响应功能混合层")]
    public ResponseFlags response;
    public ResponseAnimation responseAnimation;

}

[System.Serializable]
public struct ResponseAnimation 
{
    public UIImageAnimationFlags uiaFlags;
    public float speed;
    [HideInInspector]
    public  float orippum;
    public float tarppum;
}
