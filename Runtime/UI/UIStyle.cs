using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static log4net.Appender.ColoredConsoleAppender;

public class UIStyle : MonoBehaviour
{
    public UIStyleWareHouse uswh;
    [HideInInspector]
    public Sprite[] sprites;
    [HideInInspector]
    public Color[] colors;
    Image image;

    private void Reset()
    {
        if (GetComponent<Image>() == null) 
        {
            DestroyImmediate(this);
            Debug.LogError("此脚本智能添加到具有Image组件的游戏对象上");
        }
    }
    private void Awake()
    {
        image = GetComponent<Image>();  
    }

    public void SetImageSprites(int index)
    {
        if (image == null)
            image = GetComponent<Image>();
        else 
        {
            image.sprite = sprites[index];
            image.color= colors[index];
        }
    }
    public void GetUIStyle() 
    {
        if (uswh != null) 
        {
            colors = uswh.imageStyle.uicolors;
            sprites = uswh.imageStyle.sprites;
        }
    }
          
}
