//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//namespace HLVR.VRCameraRenderSmooth 
//{
//    public static class CSArithmetic
//    {
//        public static void Smooth_StabilizePosition(out Transform transform)
//        {
//            if (Vector3.Magnitude(currentPos - lastPos) >= stabilizePositionClampValue)
//            {
//                openpos = true;

//            }
//            if (openpos)
//            {
//                transform.position = Vector3.Lerp(transform.position, userControl.maincamera.transform.position, PositionLerp);
//                if (transform.position == userControl.maincamera.transform.position)
//                    openpos = false;
//            }

//            if (Vector3.Angle(transform.forward, userControl.maincamera.transform.forward) >= stabilizeRotaionClampValue)
//            {
//                openrot = true;

//            }

//            Debug.Log(Vector3.Angle(transform.forward, userControl.maincamera.transform.forward));
//            if (openrot)
//            {
               
//                transform.rotation = Quaternion.Slerp(transform.rotation, userControl.maincamera.transform.rotation, RotationLerp);              
//                if (Vector3.SqrMagnitude(transform.eulerAngles - userControl.maincamera.transform.eulerAngles) <= m_IgnoreValue) openrot = false;
//            }
//            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
//        }
//    }
//}

