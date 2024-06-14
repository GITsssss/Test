using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

namespace HLVR.InputSystem
{
    public static class VRInput
    {
        public static InputActionAsset inputs;

        public static bool GetKeyDown(KeyType key, ControllerType type)
        {
            if (inputs==null)
            inputs = VRInputManager.Instance.actions;
            switch (type)
            {
                case ControllerType.Left:
                    switch (key)
                    {
                        case KeyType.Trigger:
                            return inputs.FindAction("LeftTrigger").WasPressedThisFrame();
                        case KeyType.Grip:
                            return inputs.FindAction("LeftGrip").WasPressedThisFrame();
                        case KeyType.Rocker:
                            return inputs.FindAction("LeftRocker").WasPressedThisFrame();
                        case KeyType.XA:
                            if (inputs.FindAction("X").WasPressedThisFrame())
                                Debug.Log("X");
                            return inputs.FindAction("X").WasPressedThisFrame();
                        case KeyType.YB:
                            if (inputs.FindAction("Y").WasPressedThisFrame())
                                Debug.Log("Y");
                            return inputs.FindAction("Y").WasPressedThisFrame();
                        case KeyType.Menu:
                            if (inputs.FindAction("GameMenu").WasPressedThisFrame())
                                Debug.Log("GameMenu");
                            return inputs.FindAction("GameMenu").WasPressedThisFrame();
                    }
                    return false;
                case ControllerType.Right:
                    switch (key)
                    {
                        case KeyType.Trigger:
                            return inputs.FindAction("RightTrigger").WasPressedThisFrame();
                        case KeyType.Grip:
                            return inputs.FindAction("RightGrip").WasPressedThisFrame();
                        case KeyType.Rocker:
                            return inputs.FindAction("RightRocker").WasPressedThisFrame();
                        case KeyType.XA:
                            if (inputs.FindAction("A").WasPressedThisFrame())
                                Debug.Log("A");
                            return inputs.FindAction("A").WasPressedThisFrame();
                        case KeyType.YB:
                            if (inputs.FindAction("B").WasPressedThisFrame())
                                Debug.Log("B");
                            return inputs.FindAction("B").WasPressedThisFrame();
                        case KeyType.Menu:
                            if (inputs.FindAction("GameMenu").WasPressedThisFrame())
                                Debug.Log("GameMenu");
                            return inputs.FindAction("GameMenu").WasPressedThisFrame();
                    }
                    return false;
                case ControllerType.All:
                    switch (key)
                    {
                        case KeyType.Trigger:
                            if (inputs.FindAction("RightTrigger").WasPressedThisFrame())
                                return inputs.FindAction("RightTrigger").WasPressedThisFrame();
                            else if (inputs.FindAction("LeftTrigger").WasPressedThisFrame())
                                return inputs.FindAction("LeftTrigger").WasPressedThisFrame();
                            else return false;
                        case KeyType.Grip:
                            if (inputs.FindAction("RightGrip").WasPressedThisFrame())
                                return inputs.FindAction("RightGrip").WasPressedThisFrame();
                            else if (inputs.FindAction("LeftGrip").WasPressedThisFrame())
                                return inputs.FindAction("LeftGrip").WasPressedThisFrame();
                            else return false;


                        case KeyType.Rocker:
                            if (inputs.FindAction("RightRocker").WasPressedThisFrame())
                                return inputs.FindAction("RightRocker").WasPressedThisFrame();
                            else if (inputs.FindAction("LeftRocker").WasPressedThisFrame())
                                return inputs.FindAction("LeftRocker").WasPressedThisFrame();
                            else return false;
                        case KeyType.XA:
                            if (inputs.FindAction("A").WasPressedThisFrame())
                                return inputs.FindAction("A").WasPressedThisFrame();
                            else if (inputs.FindAction("X").WasPressedThisFrame())
                                return inputs.FindAction("X").WasPressedThisFrame();
                            else return false;
                        case KeyType.YB:
                            if (inputs.FindAction("B").WasPressedThisFrame())
                                return inputs.FindAction("B").WasPressedThisFrame();
                            else if (inputs.FindAction("B").WasPressedThisFrame())
                                return inputs.FindAction("B").WasPressedThisFrame();
                            else return false;
                    }
                    return false;
            }
            return false;
        }



        public static bool GetKey( KeyType key, ControllerType type)
        {
            if (inputs == null)
                inputs = VRInputManager.Instance.actions;
            switch (type)
            {
                case ControllerType.Left:
                    switch (key)
                    {
                        case KeyType.Trigger:
                            return inputs.FindAction("LeftTrigger").IsPressed();
                        case KeyType.Grip:
                            return inputs.FindAction("LeftGrip").IsPressed();
                        case KeyType.Rocker:
                            if (inputs.FindAction("LeftRocker").IsPressed())
                                Debug.Log("LeftRocker");
                            return inputs.FindAction("LeftRocker").IsPressed();
                        case KeyType.XA:
                            if (inputs.FindAction("X").IsPressed())
                                Debug.Log("X");
                            return inputs.FindAction("X").IsPressed();
                        case KeyType.YB:
                            if (inputs.FindAction("Y").IsPressed())
                                Debug.Log("Y");
                            return inputs.FindAction("Y").IsPressed();

                    }
                    return false;
                case ControllerType.Right:
                    switch (key)
                    {
                        case KeyType.Trigger:
                            return inputs.FindAction("RightTrigger").IsPressed();
                        case KeyType.Grip:
                            return inputs.FindAction("RightGrip").IsPressed();
                        case KeyType.Rocker:
                            if (inputs.FindAction("RightRocker").IsPressed())
                                Debug.Log("RightRocker");
                            return inputs.FindAction("RightRocker").IsPressed();
                        case KeyType.XA:
                            if (inputs.FindAction("A").IsPressed())
                                Debug.Log("A");
                            return inputs.FindAction("A").IsPressed();
                        case KeyType.YB:
                            if (inputs.FindAction("B").IsPressed())
                                Debug.Log("B");
                            return inputs.FindAction("B").IsPressed();
                    }
                    return false;
                case ControllerType.All:
                    switch (key)
                    {
                        case KeyType.Trigger:
                            if (inputs.FindAction("RightTrigger").IsPressed())
                                return inputs.FindAction("RightTrigger").IsPressed();
                            else if (inputs.FindAction("LeftTrigger").IsPressed())
                                return inputs.FindAction("LeftTrigger").IsPressed();
                            else return false;
                        case KeyType.Grip:
                            if (inputs.FindAction("RightGrip").IsPressed())
                                return inputs.FindAction("RightGrip").IsPressed();
                            else if (inputs.FindAction("LeftGrip").IsPressed())
                                return inputs.FindAction("LeftGrip").IsPressed();
                            else return false;


                        case KeyType.Rocker:
                            if (inputs.FindAction("RightRocker").IsPressed())
                                return inputs.FindAction("RightRocker").IsPressed();
                            else if (inputs.FindAction("LeftRocker").IsPressed())
                                return inputs.FindAction("LeftRocker").IsPressed();
                            else return false;
                        case KeyType.XA:
                            if (inputs.FindAction("A").IsPressed())
                                return inputs.FindAction("A").IsPressed();
                            else if (inputs.FindAction("X").IsPressed())
                                return inputs.FindAction("X").IsPressed();
                            else return false;
                        case KeyType.YB:
                            if (inputs.FindAction("B").IsPressed())
                                return inputs.FindAction("B").IsPressed();
                            else if (inputs.FindAction("B").IsPressed())
                                return inputs.FindAction("B").IsPressed();
                            else return false;
                    }
                    break;
            }
            return false;
        }

        public static bool GetKeyUp(KeyType key, ControllerType type)
        {
            if (inputs == null)
                inputs = VRInputManager.Instance.actions;
            switch (type)
            {
                case ControllerType.Left:
                    switch (key)
                    {
                        case KeyType.Trigger:
                            return inputs.FindAction("LeftTrigger").WasReleasedThisFrame();
                        case KeyType.Grip:
                            return inputs.FindAction("LeftGrip").WasReleasedThisFrame();
                        case KeyType.Rocker:
                            if (inputs.FindAction("LeftRocker").WasReleasedThisFrame())
                                Debug.Log("LeftRocker");
                            return inputs.FindAction("LeftRocker").WasReleasedThisFrame();
                        case KeyType.XA:
                            if (inputs.FindAction("X").WasReleasedThisFrame())
                                Debug.Log("X");
                            return inputs.FindAction("X").WasReleasedThisFrame();
                        case KeyType.YB:
                            if (inputs.FindAction("Y").WasReleasedThisFrame())
                                Debug.Log("Y");
                            return inputs.FindAction("Y").WasReleasedThisFrame();
                        case KeyType.Menu:
                            if (inputs.FindAction("Menu").WasReleasedThisFrame())
                                Debug.Log("Menu");
                            return inputs.FindAction("Menu").WasReleasedThisFrame();
                    }
                    return false;
                case ControllerType.Right:
                    switch (key)
                    {
                        case KeyType.Trigger:
                            return inputs.FindAction("RightTrigger").WasReleasedThisFrame();
                        case KeyType.Grip:
                            return inputs.FindAction("RightGrip").WasReleasedThisFrame();
                        case KeyType.Rocker:
                            if (inputs.FindAction("RightRocker").WasReleasedThisFrame())
                                Debug.Log("RightRocker");
                            return inputs.FindAction("RightRocker").WasReleasedThisFrame();
                        case KeyType.XA:
                            if (inputs.FindAction("A").WasReleasedThisFrame())
                                Debug.Log("A");
                            return inputs.FindAction("A").WasReleasedThisFrame();
                        case KeyType.YB:
                            if (inputs.FindAction("B").WasReleasedThisFrame())
                                Debug.Log("B");
                            return inputs.FindAction("B").WasReleasedThisFrame();
                        case KeyType.Menu:
                            if (inputs.FindAction("GameMenu").WasReleasedThisFrame())
                                Debug.Log("GameMenu");
                            return inputs.FindAction("GameMenu").WasReleasedThisFrame();
                    }
                    return false;
                case ControllerType.All:
                    switch (key)
                    {
                        case KeyType.Trigger:
                            if (inputs.FindAction("RightTrigger").WasReleasedThisFrame())
                                return inputs.FindAction("RightTrigger").WasReleasedThisFrame();
                            else if (inputs.FindAction("LeftTrigger").WasReleasedThisFrame())
                                return inputs.FindAction("LeftTrigger").WasReleasedThisFrame();
                            else return false;
                        case KeyType.Grip:
                            if (inputs.FindAction("RightGrip").WasReleasedThisFrame())
                                return inputs.FindAction("RightGrip").WasReleasedThisFrame();
                            else if (inputs.FindAction("LeftGrip").WasReleasedThisFrame())
                                return inputs.FindAction("LeftGrip").WasReleasedThisFrame();
                            else return false;


                        case KeyType.Rocker:
                            if (inputs.FindAction("RightRocker").WasReleasedThisFrame())
                                return inputs.FindAction("RightRocker").WasReleasedThisFrame();
                            else if (inputs.FindAction("LeftRocker").WasReleasedThisFrame())
                                return inputs.FindAction("LeftRocker").WasReleasedThisFrame();
                            else return false;
                        case KeyType.XA:
                            if (inputs.FindAction("A").WasReleasedThisFrame())
                                return inputs.FindAction("A").WasReleasedThisFrame();
                            else if (inputs.FindAction("X").WasReleasedThisFrame())
                                return inputs.FindAction("X").WasReleasedThisFrame();
                            else return false;
                        case KeyType.YB:
                            if (inputs.FindAction("B").WasReleasedThisFrame())
                                return inputs.FindAction("B").WasReleasedThisFrame();
                            else if (inputs.FindAction("B").WasReleasedThisFrame())
                                return inputs.FindAction("B").WasReleasedThisFrame();
                            else return false;
                    }
                    return false;
            }
            return false;
        }

        public static bool GetRocker(KeyType key, ControllerType type)
        {
            if (inputs == null)
                inputs = VRInputManager.Instance.actions;
            switch (type)
            {
                case ControllerType.Left:
                  return  inputs.FindAction("LeftRockerAxis").WasPressedThisFrame();

                case ControllerType.Right:
                    return inputs.FindAction("RightRockerAxis").WasPressedThisFrame();

                case ControllerType.All:
                    if (inputs.FindAction("LeftRockerAxis").WasPressedThisFrame())
                        return inputs.FindAction("LeftRockerAxis").WasPressedThisFrame();
                    else if (inputs.FindAction("RightRockerAxis").WasPressedThisFrame())
                        return inputs.FindAction("RightRockerAxis").WasPressedThisFrame();
                    else
                        return false;

            }

            return false;
        }


        public static float GetKeyValue(KeyType key, ControllerType type)
        {
            if (inputs == null)
                inputs = VRInputManager.Instance.actions;
            switch (type)
            {
                case ControllerType.Left:
                    switch (key)
                    {
                        case KeyType.Trigger:
                            return inputs.FindAction("LeftTrigger").ReadValue<float>();
                        case KeyType.Grip:
                            return inputs.FindAction("LeftGrip").ReadValue<float>();
                        case KeyType.Rocker:
                            return 0;
                        case KeyType.XA:
                            return 0;
                        case KeyType.YB:
                            return 0;
                    }
                    return 0;
                case ControllerType.Right:
                    switch (key)
                    {
                        case KeyType.Trigger:
                            return inputs.FindAction("RightTrigger").ReadValue<float>();
                        case KeyType.Grip:
                            return inputs.FindAction("RightGrip").ReadValue<float>();
                        case KeyType.Rocker:
                            return 0;
                        case KeyType.XA:
                            return 0;
                        case KeyType.YB:
                            return 0;
                    }
                    return 0;
                case ControllerType.All:
                    switch (key)
                    {
                        case KeyType.Trigger:
                            if (inputs.FindAction("RightTrigger").ReadValue<float>() != 0)
                                return inputs.FindAction("RightTrigger").ReadValue<float>();
                            else if (inputs.FindAction("LeftTrigger").ReadValue<float>() != 0)
                                return inputs.FindAction("LeftTrigger").ReadValue<float>();
                            else return 0;
                        case KeyType.Grip:
                            if (inputs.FindAction("RightGrip").ReadValue<float>() != 0)
                                return inputs.FindAction("RightGrip").ReadValue<float>();
                            else if (inputs.FindAction("LeftGrip").ReadValue<float>() != 0)
                                return inputs.FindAction("LeftGrip").ReadValue<float>();
                            else return 0;
                        case KeyType.Rocker:
                            return 0;
                        case KeyType.XA:
                            return 0;
                        case KeyType.YB:
                            return 0;
                    }
                    return 0;
            }
            return 0;
        }


        private static float horizontal_Left;
        public static float LeftHorizontal 
        {
            get { return horizontal_Left; }
        }

        private static float horizontal_Right;
        public static float RightHorizontal
        {
            get { return horizontal_Right; }
        }


        private static float vertical_Left;  
        public static float LeftVertical 
        {
            get { return vertical_Left; }    
        }

        private static float vertical_Right;
        public static float RightVertical
        {
            get { return vertical_Right; }
        }

        public static void LeftAxisEvent(InputAction.CallbackContext callback)
        {
            horizontal_Left = callback.ReadValue<Vector2>().x;
            vertical_Left =callback.ReadValue<Vector2>().y;   
        }

        public static void LeftAxisEventRelease(InputAction.CallbackContext callback)
        {
            horizontal_Left = 0;
            vertical_Left = 0;
        }

        public static void RightAxisEvent(InputAction.CallbackContext callback)
        {
            horizontal_Right = callback.ReadValue<Vector2>().x;
            vertical_Right = callback.ReadValue<Vector2>().y;
        }
        public static void RightAxisEventRelease(InputAction.CallbackContext callback)
        {
            horizontal_Right = 0;
            vertical_Right =0;
        }

        /// <summary>
        /// 获取手柄摇杆输入轴
        /// </summary>
        /// <param name="inputAxis"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static float GetAxis(Axis inputAxis, ControllerType type)
        {
            if (inputs == null)
                inputs = VRInputManager.Instance.actions;
            switch (type)
            {
                case ControllerType.Left:
                    switch (inputAxis) 
                    {
                        case Axis.Vertical:
                            return LeftVertical;

                        case Axis.Horizontal:
                            return LeftHorizontal;
                    }
                    break;
                case ControllerType.Right:
                    switch (inputAxis)
                    {
                        case Axis.Vertical:
                            return RightVertical;
                        case Axis.Horizontal:
                            return RightHorizontal;
                    }
                    break;
                case ControllerType.All:
                    switch (inputAxis)
                    {
                        case Axis.Vertical:
                            if (RightVertical != 0)
                                return RightVertical;
                            else if (LeftVertical != 0)
                                return LeftVertical;
                            else return 0;
                                
                        case Axis.Horizontal:
                            if (RightHorizontal != 0)
                                return RightHorizontal;
                            else if (LeftHorizontal != 0)
                                return LeftHorizontal;
                            else return 0;
                    }
                    break;
            }
            return 0;
        }

       
    }

    /// <summary>
    ///  控制类型 ： 左手或是右手或者左右都可以
    ///  Left 左手 
    ///  Right 右手
    ///  All 左手和右手
    ///  HandTracking_Left  左手势追踪识别
    ///  HandTracking_Right  右手手势追踪识别
    ///  HandTracking_All   左手和右手手势追踪识别
    /// </summary>
    /// 
    public enum ControllerType
    {
        Left,
        Right,
        All,
        HandTracking_Left,
        HandTracking_Right,
        HandTracking_All,
    }



    /// <summary>
    /// 输入轴  横轴和纵轴  对应手柄摇杆左右和前后推动
    /// </summary>

    public enum Axis
    {
        Horizontal,
        Vertical,
    }


    /// <summary>
    /// 手柄控制器的输入按键类型
    /// </summary>
    public enum KeyType
    {
        Trigger,
        Rocker,
        Grip,
        XA,
        YB,
        Menu
    }
}
