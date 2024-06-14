using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HLVR.Interaction
{
    public static class Curve
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PointCount">���ɵ���</param>
        /// <param name="Interval">����֮��ļ������</param>
        /// <param name="OriginPoint">ԭ��</param>
        /// <param name="Gravity">����</param>
        /// <param name="direction">���ߵ���������</param>
        public static Vector3[] StructcCurve(int PointCount, float Interval, Vector3 OriginPoint, float Gravity ,Vector3 direction)
        {
            Vector3[] points=new Vector3[PointCount];
            for (int i = 0; i < PointCount; i++) 
            {
                //if (i != 0)
                    points[i] = direction * i * Interval + new Vector3(0, 0.5f * Gravity * Mathf.Pow(i, 2), 0)+OriginPoint;
                //else
                //    points[i] = new Vector3(OriginPoint.x, OriginPoint.y, OriginPoint.z);
            }
            return points;
        }
    }
}



