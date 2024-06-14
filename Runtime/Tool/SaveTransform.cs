using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  为了让游戏道具可以匹配人物动画状态时候的位置。在场景中运行程序，将道具与人物动画匹配好位置、旋转、缩放，点击保存，下次程序初始化的时候，将会使用这个数据
/// </summary>
public class SaveTransform : MonoBehaviour
{
    public bool usepos;
    public bool usequaternion;
    public bool usescale;
    [Tooltip("是否开启使用指定名字的Key值")]
    public bool useotherdate;
    [Tooltip("使用指定名字的Key值")]
    public string names;
    Vector3 pos;
    Quaternion quaternion;
    Vector3 scale;
    string thename;
    public void SetDate()
    {
        thename = this.gameObject.name;
        if (usepos == true)
            this.transform.gameObject.transform.localPosition = new Vector3(PlayerPrefs.GetFloat("xpos" + thename), PlayerPrefs.GetFloat("ypos" + thename), PlayerPrefs.GetFloat("zpos" + thename)) ;
        if (usequaternion == true)
            this.transform.gameObject.transform.localRotation = new Quaternion(PlayerPrefs.GetFloat("xq" + thename), PlayerPrefs.GetFloat("yq" + thename), PlayerPrefs.GetFloat("zq" + thename), PlayerPrefs.GetFloat("wq" + thename));
        if (usescale == true)
            this.transform.gameObject.transform.localScale = new Vector3(PlayerPrefs.GetFloat("xs" + thename), PlayerPrefs.GetFloat("ys" + thename), PlayerPrefs.GetFloat("zs" + thename));
        print(PlayerPrefs.GetFloat("xpos" + thename));
        print(thename);
    }


    public void SetThisOfOtherDate() 
    {
        if (usepos == true)
            this.transform.gameObject.transform.localPosition = new Vector3(PlayerPrefs.GetFloat("xpos" + names), PlayerPrefs.GetFloat("ypos" + names), PlayerPrefs.GetFloat("zpos" + names));
        if (usequaternion == true)
            this.transform.gameObject.transform.localRotation = new Quaternion(PlayerPrefs.GetFloat("xq" + names), PlayerPrefs.GetFloat("yq" + names), PlayerPrefs.GetFloat("zq" + names), PlayerPrefs.GetFloat("wq" + names));
        if (usescale == true)
            this.transform.gameObject.transform.localScale = new Vector3(PlayerPrefs.GetFloat("xs" + names), PlayerPrefs.GetFloat("ys" + names), PlayerPrefs.GetFloat("zs" + names));

    }
    void GetDate() 
    {
        pos = this.transform.gameObject.transform.localPosition;
        print(pos);
        print(this.transform.localPosition);
        quaternion = this.transform.gameObject.transform.localRotation;
        scale = this.transform.gameObject.transform.localScale;
    }
   public void SaveDate()
    {
            thename = this.gameObject.name;
            GetDate();
            PlayerPrefs.SetFloat("xpos" + thename, pos.x);
            PlayerPrefs.SetFloat("ypos" + thename, pos.y);
            PlayerPrefs.SetFloat("zpos" + thename, pos.z);
           print(PlayerPrefs.GetFloat("xpos" + thename));

            PlayerPrefs.SetFloat("xq" + thename, quaternion.x);
            PlayerPrefs.SetFloat("yq" + thename, quaternion.y);
            PlayerPrefs.SetFloat("zq" + thename, quaternion.z);
            PlayerPrefs.SetFloat("wq" + thename, quaternion.w);
        

       

            PlayerPrefs.SetFloat("xs" + thename, scale.x);
            PlayerPrefs.SetFloat("ys" + thename, scale.y);
            PlayerPrefs.SetFloat("zs" + thename, scale.z);


       


        Debug.Log("保存完成！"+thename);
      
    }
}
