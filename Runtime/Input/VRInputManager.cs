using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.DefaultInputActions;

namespace HLVR.InputSystem 
{
    public class VRInputManager : MonoBehaviour
    { 
        private static  VRInputManager inputManager;
        public static VRInputManager Instance 
        {
            get 
            {
                if (inputManager == null) 
                {
                    GameObject g = new GameObject("VRInputManager");
                    g.AddComponent<VRInputManager>();
                    inputManager = g.GetComponent<VRInputManager>();
                }
                return inputManager;
            }
        }


        public bool autoLoad;
        public InputActionAsset actions;


        private void Reset()
        {
            this.gameObject.name ="VRInputManager";
        }

        private void Awake()
        {
            if (inputManager == null)
            {
                inputManager = this;
            }
            else 
            {
               Destroy(inputManager);  
            }
            if(autoLoad)
            actions=Resources.Load<InputActionAsset>("Input");

            if (actions == null) 
            {
                Debug.LogError("输入配置文件为空！");
                this.enabled= false;   
            }
        }
        private void OnEnable()
        {
            actions.Enable();
            actions.FindAction("RightRockerAxis").performed += VRInput.RightAxisEvent;
            actions.FindAction("RightRockerAxis").canceled  += VRInput.RightAxisEventRelease;
            actions.FindAction("LeftRockerAxis").performed += VRInput.LeftAxisEvent;
            actions.FindAction("LeftRockerAxis").canceled += VRInput.LeftAxisEventRelease;
        }

        private void OnDisable()
        {
            actions.FindAction("RightRockerAxis").performed -= VRInput.RightAxisEvent;
            actions.FindAction("RightRockerAxis").canceled -= VRInput.RightAxisEventRelease;
            actions.FindAction("LeftRockerAxis").performed -= VRInput.LeftAxisEvent;
            actions.FindAction("LeftRockerAxis").canceled -= VRInput.LeftAxisEventRelease;
            actions.Disable();
        }

    }
}

