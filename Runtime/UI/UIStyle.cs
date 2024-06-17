using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NPOI.SS.UserModel;

public class UIStyle : MonoBehaviour
{
    [SerializeField]
    public UIStyleWareHouse uswh;
    [HideInInspector]
    public Image image;
    [HideInInspector]
    public ImageStyle[] imageStyle;
    [HideInInspector]
    public TextMeshProUGUI uitext;
    [HideInInspector]
    public bool cusTmpro;
    [HideInInspector]
    public TextMeshProUGUI custmprougui;
    [HideInInspector]
    public TMproStyle[] tmprostyle;
    private void Reset()
    {
        image = GetComponent<Image>();
        uitext=GetComponent<TextMeshProUGUI>();   
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
            image.sprite = imageStyle[index].sprites;
            image.color= imageStyle[index].uicolors;
        }
    }

    public void SetTmrpoText(int index)
    {
        if (uitext == null)
            uitext = GetComponent<TextMeshProUGUI>();
        else
        {
            uitext.text = tmprostyle[index].content ;
            uitext.color = tmprostyle[index].fontColor;
            uitext.font = tmprostyle[index].font;
            uitext.fontSize = tmprostyle[index].fontsize;  
        }
    }




    public void GetUIStyle()
    {
        image = GetComponent<Image>();
        if(cusTmpro)
        uitext = custmprougui;
        else
        uitext = GetComponent<TextMeshProUGUI>();
        if (uswh != null) 
        {
            imageStyle = uswh.imageStyle;
            tmprostyle = uswh.tmproStyle;
        }
    }


    public bool GetComponentValue<T>()
    { 
       if (GetComponent<T>()!=null)return true;
       else return false;
    }
          
}
