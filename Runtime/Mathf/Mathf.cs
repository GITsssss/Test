using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HLVR.Arithmetic
{
    public static class Mathf
    {

        /// <summary>
        /// ����һ��Vector3���͵ľ���ֵ
        /// </summary>
        /// <param name="valueV3"></param>
        /// <returns></returns>
        public static Vector3 Abs(Vector3 valueV3)
        {
            float x = UnityEngine.Mathf.Abs(valueV3.x);
            float y = UnityEngine.Mathf.Abs(valueV3.y);
            float z = UnityEngine.Mathf.Abs(valueV3.z);

            return new Vector3(x,y,z);
        }

        /// <summary>
        /// ����һ��Vector3���͵�x,y,z�����������Ļ���ֵ
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>

        public static Vector3 AngleToRadian(Transform transform1,Transform transform2)
        {
            float x = UnityEngine.Mathf.Deg2Rad * Vector3.Angle(transform1.right,transform2.right);
            float y = UnityEngine.Mathf.Deg2Rad * Vector3.Angle(transform1.up, transform2.up);
            float z = UnityEngine.Mathf.Deg2Rad * Vector3.Angle(transform1.forward, transform2.forward);

            return new Vector3(x,y,z);
        }
    }
}

