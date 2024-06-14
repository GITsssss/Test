using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FixedOnHand : MonoBehaviour
{
    public Transform m_FixedPoint;
    public PropData propData;


    public void SetParent()
    {
        transform.parent = m_FixedPoint;
    }

    public void Set()
    {
        transform.localPosition = propData.OriginPosition[0];
        transform.localRotation = propData.OriginRotation[0];
        DebugInfo.DebugLog("数据同步完成");
    }

    public void Save()
    {
        propData.OriginPosition[0]  = transform.localPosition;
        propData.OriginRotation[0]= transform.localRotation;
        DebugInfo.DebugLog("保存数据完成");
    }
}
