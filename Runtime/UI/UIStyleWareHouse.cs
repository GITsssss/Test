using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIStyleWareHouse : ScriptableObject
{
    public ImageStyle[] imageStyle;
    public TMproStyle[] tmproStyle;
    public Texture2D[] textures;
    public AudioClip[] audioClips;
}

[System.Serializable]
public struct ImageStyle 
{
    public Sprite sprites;
    public Color uicolors;
}

[System.Serializable]
public struct TMproStyle 
{
    [TextArea(1, 5)]
    public string content;
    public TMP_FontAsset font;
    public Color fontColor;
    [Range(1,100)]
    public float fontsize;
}
