using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIStyleWareHouse : ScriptableObject
{
    public ImageStyle imageStyle;
    public Texture2D[] textures;
    public AudioClip[] audioClips;


    private void OnEnable()
    {
        for (int i = 0; i < imageStyle.uicolors.Length; i++)
        {
            imageStyle.uicolors[i] = new Color(1, 1, 1, 1);
        }
    }
}

[System.Serializable]
public struct ImageStyle 
{
    public Sprite[] sprites;
    public Color[] uicolors;
}
