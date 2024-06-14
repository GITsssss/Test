using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingTarget : MonoBehaviour
{
    public Vector3 grabOffsetPostion;
    public Vector3 grabOffsetAngle;
    public bool tracking;
    /// <summary>
    /// ׷��Ŀ������λ�úͽǶ�
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
    /// ׷��Ŀ��Ƕ�
    /// </summary>
    /// <param name="target">Ŀ�����</param>
    public void TrackingAngle(Transform target)
    {
        if (tracking) 
        {
            Vector3 angle = grabOffsetAngle + target.transform.eulerAngles;
            transform.rotation = Quaternion.Euler(angle);
        }
  
    }

    /// <summary>
    /// ׷��Ŀ��λ��
    /// </summary>
    /// <param name="target">Ŀ�����</param>

    public void TrackingPosition(Transform target)
    {
        if (tracking) 
        {
            transform.position = grabOffsetPostion + target.transform.position;
        }
    }
    /// <summary>
    /// ����Ŀ����׷��״̬
    /// </summary>
    /// <param name="state">True ����׷�٣� false �ر�׷��</param>
    public void SetTrackingState(bool state)
    { 
      tracking= state;
    }
}
