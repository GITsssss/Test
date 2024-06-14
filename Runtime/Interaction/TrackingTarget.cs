using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingTarget : MonoBehaviour
{
    public Vector3 grabOffsetPostion;
    public Vector3 grabOffsetAngle;
    public bool tracking;
    /// <summary>
    /// 追踪目标对象的位置和角度
    /// </summary>
    public void Tracking(Transform target)
    {
        if (tracking) 
        {
            TrackingPosition(target);
            TrackingAngle(target);
        }    
    }
    /// <summary>
    /// 追踪目标角度
    /// </summary>
    /// <param name="target">目标对象</param>
    public void TrackingAngle(Transform target)
    {
        if (tracking) 
        {
            Vector3 angle = grabOffsetAngle + target.transform.eulerAngles;
            transform.rotation = Quaternion.Euler(angle);
        }
  
    }

    /// <summary>
    /// 追踪目标位置
    /// </summary>
    /// <param name="target">目标对象</param>

    public void TrackingPosition(Transform target)
    {
        if (tracking) 
        {
            transform.position = grabOffsetPostion + target.transform.position;
        }
    }
    /// <summary>
    /// 设置目标物追踪状态
    /// </summary>
    /// <param name="state">True 开启追踪， false 关闭追踪</param>
    public void SetTrackingState(bool state)
    { 
      tracking= state;
    }
}
