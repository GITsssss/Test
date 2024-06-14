using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HLVR.Interaction;
//using Knife.HDRPOutline.Core;

namespace HLVR.InputSystem 
{
    public class GetKeyState : MonoBehaviour
    {
        public Transform trigger;
        public Transform triggerTargetRot;
        Quaternion originTrigger;

        public Transform grab;
        public Transform grabTargetRot;
        Quaternion originGrab;
        public ControllerType handType;
        [ReadOnly]
        public Vector2 areaAxis;
        public AxisDisRotation axis;
        public Transform rocker;
        Vector3 orirockerpos;
        public float angle = 28f;

        public Transform XA;
        public Transform XATargetPos;
        Vector3 originXApos;
        public Transform YB;
        public Transform YBTargetPos;
        Vector3 originYBpos;

        private void Awake()
        {
            originTrigger =  trigger.localRotation;
            originGrab    =  grab.localRotation;
            originXApos   =  XA.localPosition;
            originYBpos   =  YB.localPosition;
            orirockerpos  =  rocker.localPosition;
        }

        private void Update()
        {
            trigger.localRotation = Quaternion.Lerp(originTrigger, triggerTargetRot.localRotation, VRInput.GetKeyValue(KeyType.Trigger, handType));
            grab.localRotation = Quaternion.Lerp(originGrab, grabTargetRot.localRotation, VRInput.GetKeyValue(KeyType.Grip, handType));

            if(VRInput.GetKeyDown(KeyType.XA,handType))
                XA.localPosition = Vector3.Lerp(originXApos, XATargetPos.localPosition, 1);
            else if(VRInput.GetKeyUp(KeyType.XA, handType))
                XA.localPosition = Vector3.Lerp(originXApos, XATargetPos.localPosition, 0);

            if (VRInput.GetKeyDown(KeyType.YB, handType))
                YB.localPosition = Vector3.Lerp(originYBpos, YBTargetPos.localPosition, 1);
            else if (VRInput.GetKeyUp(KeyType.YB, handType))
                YB.localPosition = Vector3.Lerp(originYBpos, YBTargetPos.localPosition, 0);


            if (VRInput.GetKeyDown(KeyType.Rocker, handType))
                rocker.localPosition = Vector3.Lerp(orirockerpos,orirockerpos+XA.transform.up*0.002f*-1f,1);
            else if(VRInput.GetKeyUp(KeyType.Rocker, handType))
                rocker.localPosition = Vector3.Lerp(orirockerpos, orirockerpos+XA.transform.up * 0.002f, 0);

            areaAxis.x= VRInput.GetAxis(Axis.Horizontal, handType);
            areaAxis.y= VRInput.GetAxis(Axis.Vertical, handType);
            float x = areaAxis.x;
            float y = areaAxis.y;
            switch (handType)
            {
                case ControllerType.Left:
                    switch (axis)
                    {
                            case AxisDisRotation.X:
                            rocker.localEulerAngles = new Vector3(0, -1 * x * angle, -1 * y * angle);
                            break;

                            case AxisDisRotation.Y:
                            rocker.localEulerAngles = new Vector3(y * angle,0, -1 * x * angle);
                            break;

                            case AxisDisRotation.Z:
                            rocker.localEulerAngles = new Vector3(-1 * y * angle, -1 * x * angle, 0);
                            break;
                    }
                  
                    break;
                case ControllerType.Right:
                   
                    switch (axis)
                    {
                        case AxisDisRotation.X:
                            rocker.localEulerAngles = new Vector3(0, x * angle, -1 * y * angle);
                            break;

                        case AxisDisRotation.Y:
                            rocker.localEulerAngles = new Vector3(-1 * y * angle, 0, x * angle);
                            break;

                        case AxisDisRotation.Z:
                            rocker.localEulerAngles = new Vector3(-1 * y * angle, x * angle, 0);
                            break;
                    }
                    break;

            }
        }
        public enum AxisDisRotation
        {
          X,
          Y,
          Z
        }
    }

    

}
