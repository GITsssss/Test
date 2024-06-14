using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class SetPropData : MonoBehaviour
{
    public Transform[] transforms;
    public PropData propData;
    public bool setPos;
    public bool setRot;
    public void Set()
    {
        for(int i=0;i<transforms.Length;i++)
        {
            if(setRot)
                transforms[i].localRotation = propData.OriginRotation[i];
            if (setPos)
                transforms[i].localPosition = propData.OriginPosition[i];
        }
        DebugInfo.DebugWarning(OutColor.Green, "设置完成！");
    }

    public void Save()
    {
        propData.OriginRotation = new Quaternion[transforms.Length];
        propData.OriginPosition = new Vector3[transforms.Length];
        for (int i = 0; i < transforms.Length; i++)
        {
            if (setRot)
                propData.OriginRotation[i]=  transforms[i].localRotation;
            if (setPos)
                propData.OriginPosition[i]= transforms[i].localPosition;
        }

       
        //AssetDatabase.SaveAssets();
        DebugInfo.DebugWarning(OutColor.Green, "保存完成！");
    }
}
