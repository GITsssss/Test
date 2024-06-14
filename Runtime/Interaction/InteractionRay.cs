using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using HLVR.InputSystem;
using HLVR.Configuration;
using UnityEngine.InputSystem;

namespace HLVR.Interaction 
{
    [RequireComponent(typeof(LineRenderer))]
    [DisallowMultipleComponent]
    public class InteractionRay : MonoBehaviour
    {
        [Tooltip("MouseKeyboard 鼠标键盘；VR 虚拟现实设备 头盔、手柄；Touch 触屏；HandShank 游戏手柄")]
        public InputDevice inputDevice;
        [Tooltip("激活射线")]
        public  bool m_EnebledOnce = true;
        [ReadOnly]
        public bool m_Enebled = false;
        public InteractionRayData data;
        public ControllerType inputSource;
        ControllerType currentSource;
        public KeyType button;
        public KeyType m_TeleportButton;
        public bool m_EnebledRotate=true;
        public Ray ray;
        RaycastHit hit;
        public Transform head, leftHand, RightHand;
        [Header("HandModelPrefab")]
        [Tooltip("0:left 1:right")]
        public GameObject[] hands;
        [HideInInspector]
        public Rigidbody left, right;//左手 右手
        [HideInInspector]
        public Transform currentHand;
        [Tooltip("交互层级")]
        public LayerMask layerMask;
        [Tooltip("射线最大有效距离")]
        public float m_MaxCastDistance;
        [Tooltip("射线渲染")]
        public RenderArguments m_RenderRay;
        LineRenderer line;
        [HideInInspector]
        public InteractionObject m_IO;
        [HideInInspector]
        public InteractionUI m_IOUI;
        public GrabObjects m_IOGrab;
        [Tooltip("瞬移曲线设置")]
        public CurveLine curveline;
        [Tooltip("头部追踪数据")]
        public HeadInfoData headInfoData;
        [Tooltip("左手追踪数据")]
        public HandleInfoData lefthandInfoData;
        [Tooltip("右手追踪数据")]
        public HandleInfoData righthandInfoData;
        [HideInInspector]
        public bool grab; 
        bool onIO;
        CharacterController player;
       public InfoPanel infoPanel;
        public bool OnIO 
        {
            get 
            {
                return onIO;
            }
        }
        #region
        public void SetEnebledState(bool state)
        {
            m_EnebledOnce = state;
            m_Enebled = state;
        }

        public void OpenEnebled()
        {
            m_EnebledOnce = true;
            m_Enebled = true;
        }

        public void CloseEnebled()
        {
            m_EnebledOnce = false;
            m_Enebled = false;
         
        }


        #endregion

        private static InteractionRay interactionRay;
        public static InteractionRay Instance 
        {
            get 
            {
               return interactionRay;
            }
        }

        private void Awake()
        {
            if (interactionRay == null)
            {
                interactionRay = this;
            }
            else 
            {
               Destroy(this);
            }
           Initalize();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position,3);
        }

        public void SetEnebledMoveState(bool state)
        {
            curveline.SetEnebledMoveState(state);
        }

        public void SetDefaultValue()
        {

            #region 
            //inputSource = ControllerType.All;
            //button= KeyType.Trigger ;
            //m_TeleportButton = KeyType.Rocker;
            //m_MaxCastDistance = 10;
            //m_RenderRay.starwidth = 0.01f;
            //m_RenderRay.endwidth = 0.01f;
            //m_RenderRay.bezier.BezierPointCount = 20;
            //m_RenderRay.bezier.m_Distance = 0.6f;
            //m_RenderRay.normalC = new Color(Color.red.r, Color.red.g, Color.red.b,1);
            //m_RenderRay.hitIOC = new Color(0f, 0.9568628f, 0.9568628f, 1f);
            //m_RenderRay.material = Resources.Load("Ray") as Material;
            //m_RenderRay.shaderColorName = "_Color";
            //curveline.m_EnebledMove = true;
            //curveline.PointCount = 60;
            //curveline.Interval = 0.4f;
            //curveline.Gravity = -0.02f;
            //curveline.width = 0.02f;
            //curveline.layerMask.value = LayerMask.NameToLayer("Everything");
            #endregion
            hands = new GameObject[2];
            lefthandInfoData.m_EnableRotation= true;
            lefthandInfoData.m_EnablePosition = true;
            righthandInfoData.m_EnablePosition = true;
            righthandInfoData.m_EnableRotation = true;
            leftHand =  transform.Find("LeftController");
            RightHand = transform.GetChild(2);
            hands[0] = data.handControllerPrefabs[0];
            hands[1] = data.handControllerPrefabs[1];
            inputSource = data.handtype;
            button = data.button ;
            layerMask.value = data.rayLayerMask.value ;
            m_TeleportButton = data.TeleportButton;
            m_MaxCastDistance = data.m_MaxCastDistance ;
            m_RenderRay.starwidth = data.starwidth;
            m_RenderRay.endwidth = data.endwidth;
            m_RenderRay.bezier.BezierPointCount = data.BezierPointCount;
            m_RenderRay.bezier.m_Distance = data.m_Distance;
            m_RenderRay.normalC = data.normalC;
            m_RenderRay.hitIOC = data.hitIOC;
            m_RenderRay.material = data.material;
            m_RenderRay.shaderColorName = data.shaderColorName;
            curveline.m_EnebledMove = data.m_EnebledMove;
            curveline.PointCount = data.PointCount;
            curveline.Interval = data.Interval;
            curveline.Gravity = data.Gravity;
            curveline.width = data.width;
            curveline.layerMask.value = data.CurvelayerMask.value;
        }

        public void SaveDefaultValue()
        {
            data.handControllerPrefabs = new GameObject[2];
            data.handControllerPrefabs[0]= hands[0];
            data.handControllerPrefabs[1]= hands[1];
            data.handtype= inputSource ;
            data.button = button ;
            data.TeleportButton = m_TeleportButton ;
            data.m_MaxCastDistance = m_MaxCastDistance ;
            data.starwidth= m_RenderRay.starwidth ;
            data.endwidth = m_RenderRay.endwidth ;
            data.BezierPointCount = m_RenderRay.bezier.BezierPointCount;
            data.m_Distance= m_RenderRay.bezier.m_Distance;
            data.normalC = m_RenderRay.normalC ;
            data.hitIOC = m_RenderRay.hitIOC;
            data.material = m_RenderRay.material;
            data.shaderColorName= m_RenderRay.shaderColorName ;
            data.m_EnebledMove= curveline.m_EnebledMove;
            data.PointCount= curveline.PointCount ;
            data.Interval = curveline.Interval;
            data.Gravity= curveline.Gravity ;
            data.width= curveline.width;
            data.rayLayerMask= layerMask.value;
            data.CurvelayerMask=curveline.layerMask.value;
            //data.Save();
        }

        private void Initalize()
        {
             line = GetComponent<LineRenderer>();
             player = GetComponent<CharacterController>();
             currentHand = RightHand;
           // m_RenderRay.material.name = "RayColor";
            curveline.teleport = GameObject.FindObjectOfType<Teleport>();
            if (hands[0].gameObject != null)
            {      
                   // left = Instantiate(hands[0], leftHand);
                left = Instantiate(hands[0],this.gameObject.transform).GetComponent<Rigidbody>();
            }
            if (hands[1].gameObject != null)
            {    
                  //  right = Instantiate(hands[1], RightHand);
                right = Instantiate(hands[1],this.gameObject.transform).GetComponent<Rigidbody>();
            }
            switch (inputDevice)
            {
                case InputDevice.MouseKeyboard:
                    lefthandInfoData.m_UseNoTraceTransformData = true;
                    lefthandInfoData.m_EnablePosition = false;
                    lefthandInfoData.m_EnableRotation = false;
                    righthandInfoData.m_UseNoTraceTransformData = true;
                    righthandInfoData.m_EnablePosition = false;
                    righthandInfoData.m_EnableRotation = false;
                    headInfoData.m_UseNoTraceTransformData = true;
                    headInfoData.m_EnablePosition = false;
                    headInfoData.m_EnableRotation = false;
                    player.enabled = true;
                    break;
                case InputDevice.VR:
                    lefthandInfoData.m_UseNoTraceTransformData = false;
                    lefthandInfoData.m_EnablePosition = true;
                    lefthandInfoData.m_EnableRotation = true;
                    righthandInfoData.m_UseNoTraceTransformData = false;
                    righthandInfoData.m_EnablePosition = true;
                    righthandInfoData.m_EnableRotation = true;
                    headInfoData.m_UseNoTraceTransformData = false;
                    headInfoData.m_EnablePosition = true;
                    headInfoData.m_EnableRotation = true;
                    player.enabled = false;
                  //  Camera.main.GetComponent<UnityEngine.SpatialTracking.>().enabled = true;
                    break;

                case InputDevice.Touch:
                    lefthandInfoData.m_UseNoTraceTransformData = true;
                    lefthandInfoData.m_EnablePosition = false;
                    lefthandInfoData.m_EnableRotation = false;
                    righthandInfoData.m_UseNoTraceTransformData = true;
                    righthandInfoData.m_EnablePosition = false;
                    righthandInfoData.m_EnableRotation = false;
                    headInfoData.m_UseNoTraceTransformData = true;
                    headInfoData.m_EnablePosition = false;
                    headInfoData.m_EnableRotation = false;
                 
                    player.enabled = true;
                  //  Camera.main.GetComponent<UnityEngine.InputSystem.XR.TrackedPoseDriver>().enabled = false;
                    break;

                case InputDevice.HandShank:

                    break;
            }
        }

    

        private void LauchRay(Transform hand)//���߷���
        {
            ray = new Ray(hand.position+hand.forward*0.2f, hand.forward);
        }

        Vector3 lastvelocit_left, lastvelocit_right;
        void HandModelRenderer()//�ֲ�ģ����Ⱦ
        {
          
            if (hands[0].gameObject != null) 
            {
                if (left != null) 
                {
                    //Vector3 vector = leftHand.position - left.position;

                    //if ((left.velocity - lastvelocit_left).magnitude < 2f)
                    //    left.velocity = Vector3.MoveTowards(left.velocity, vector * 1000f*Time.fixedDeltaTime, 100f);
                    //else
                    //    left.velocity = Vector3.zero;
                    //lastvelocit_left = left.velocity;
                    left.transform.localPosition = leftHand.localPosition;
                    left.transform.localRotation = leftHand.localRotation;
                }         
            }
            if (hands[1].gameObject != null) 
            {
                if (right != null) 
                {
                    //Vector3 vector = RightHand.position - right.position;
                    //if ((right.velocity - lastvelocit_right).magnitude < 2f)
                    //    right.velocity = Vector3.MoveTowards(right.velocity, vector * 1000f * Time.fixedDeltaTime, 100f);
                    //else 
                    //{
                    //    right.velocity = Vector3.zero;
                    //}
                    //lastvelocit_right = right.velocity;
                    right.transform.localPosition = RightHand.localPosition;
                    right.transform.localRotation = RightHand.localRotation;
                }          
            }  
        }

        List<XRDisplaySubsystem> displaySubsystems = new List<XRDisplaySubsystem>();

        private void OnEnable()
        {
            if (m_Enebled)
                Render.LineRenderState(line, true);
            Application.onBeforeRender += Head;
           
           
          
           // infoPanel.XRName = inputDevice[0].name;
        }

        private void OnDisable()
        {
            Render.LineRenderState(line, false);
            Application.onBeforeRender -= Head;
        }

        private void FixedUpdate()
        {
            headInfoData.TracePhysics();
            lefthandInfoData.Trace(ControllerType.Left);
            righthandInfoData.Trace(ControllerType.Right);
            HandModelRenderer();
        }


        private void Update()
        {
            switch (inputDevice)
            {
                case InputDevice.MouseKeyboard:
                     if (Input.GetMouseButton(1))
                    {
                        m_Enebled = false;
                        if (curveline.teleport != null && curveline.m_EnebledMove)
                            curveline.teleport.enabled = true;
                        m_EnebledOnce = true;
                    }
                    else
                    {
                        if (grab)
                        {
                            m_Enebled = false;
                            if (m_IO != null)
                            {
                                m_IO.Exit();
                                m_IO = null;
                            }
                            m_EnebledOnce = true;
                        }
                        else if (m_EnebledOnce)
                        {
                            m_Enebled = true;
                            m_EnebledOnce = false;
                        }

                    }
                    break;
                case InputDevice.VR:
                    if (VRInput.GetKey(m_TeleportButton, currentSource) && VRInput.RightVertical>0.7f)
                    {
                        m_Enebled = false;
                        if (curveline.teleport != null && curveline.m_EnebledMove)
                            curveline.teleport.enabled = true;
                        m_EnebledOnce = true;
                    }
                    else
                    {
                        if (grab)
                        {
                            m_Enebled = false;
                            if (m_IO != null)
                            {
                                m_IO.Exit();
                                m_IO = null;
                            }
                            m_EnebledOnce = true;
                        }
                        else if (m_EnebledOnce)
                        {
                            m_Enebled = true;
                            m_EnebledOnce = false;
                        }

                    }
                    break;

                case InputDevice.Touch:
                    if (Input.GetMouseButton(1))
                    {
                        m_Enebled = false;
                        if (curveline.teleport != null && curveline.m_EnebledMove)
                            curveline.teleport.enabled = true;
                        m_EnebledOnce = true;
                    }
                    else
                    {
                        if (grab)
                        {
                            m_Enebled = false;
                            if (m_IO != null)
                            {
                                m_IO.Exit();
                                m_IO = null;
                            }
                            m_EnebledOnce = true;
                        }
                        else if (m_EnebledOnce)
                        {
                            m_Enebled = true;
                            m_EnebledOnce = false;
                        }

                    }
                    break;

                case InputDevice.HandShank:

                    break;
            }
            Run();
        }




        private void Head()
        {
            headInfoData.TracePosAndRot();
            //头盔追踪数据
            if (!headInfoData.m_UseNoTraceTransformData)
            {
                if (headInfoData.m_EnablePosition)
                {
                    head.localPosition = headInfoData.position;
                }

                if (headInfoData.m_EnableRotation)
                {
                    head.localRotation = headInfoData.rotation;
                }
            }
            else
            {
                head.localPosition = headInfoData.m_NoVRTracePosition;
            }
        }

        Vector3 lastpos;
        private void Run()
        {
           
            infoPanel.VRInfo();
            //左手手柄追踪数据
            if (lefthandInfoData.m_EnablePosition)
            {
                leftHand.localPosition = lefthandInfoData.position;
            
            }
            else if (lefthandInfoData.m_UseNoTraceTransformData)
            {
                if(Input.GetKey(KeyCode.Q))
                lefthandInfoData.m_NoVRTracePosition += leftHand.transform.forward*2f*Time.deltaTime;
                else if(Input.GetKey(KeyCode.Alpha1))
                lefthandInfoData.m_NoVRTracePosition -= leftHand.transform.forward * 2f * Time.deltaTime;
                leftHand.localPosition = lefthandInfoData.m_NoVRTracePosition;
            }

            if (lefthandInfoData.m_EnableRotation)
            {
                leftHand.localRotation = lefthandInfoData.rotation;
            } 
            else if (lefthandInfoData.m_UseNoTraceTransformData)
            {
                leftHand.forward = Camera.main.transform.forward;
            }


            //右手手柄追踪数据
            if (righthandInfoData.m_EnablePosition) 
            {
               // righthandInfoData.position +=Input.GetAxis("Mouse ScrollWheel") * Camera.main.transform.forward;
                RightHand.localPosition = righthandInfoData.position;
                lefthandInfoData.GetDir(lastpos, lefthandInfoData.position);

            }
            else if(righthandInfoData.m_UseNoTraceTransformData)
            {

                RightHand.localPosition = righthandInfoData.m_NoVRTracePosition;
            }

            if (righthandInfoData.m_EnableRotation) 
            {
                RightHand.localRotation = righthandInfoData.rotation;
            }
            else if (righthandInfoData.m_UseNoTraceTransformData)
            {
                // righthandInfoData.m_NoVRTraceRotation += RightHand.right * Input.GetAxis("Mouse ScrollWheel") * 10f;
                //RightHand.localEulerAngles = righthandInfoData.m_NoVRTraceRotation;
                RightHand.forward = Camera.main.transform.forward;
            }
            lastpos = righthandInfoData.position;
            if (m_Enebled)
            {
                LauchRay(GetHand());
                RayCastEvent();             
                Render.LineRenderState(line, true);
                if (m_RenderRay.bezier.m_UseBezier&&RayCastState()&& m_IOUI != null)
                {
                    if (line.positionCount != m_RenderRay.bezier.BezierPointCount)
                        line.positionCount = m_RenderRay.bezier.BezierPointCount;

                    for (int i = 0; i < m_RenderRay.bezier.BezierPointCount; i++)
                    {
                        line.SetPosition(i, Render.Bezier(ray.origin, GetHitObjectPosition(), ray.GetPoint(Mathf.Clamp(m_RenderRay.bezier.m_Distance*Vector3.Distance(ray.origin, GetHitObjectPosition()), 0, m_MaxCastDistance)), i * 1f / m_RenderRay.bezier.BezierPointCount * 1f));
                        //    Debug.Log("////"+i+"///"+Render.Bezier(Vector3.zero, new Vector3(5,5,5),new Vector3(2,3,6), 1));
                        Render.RenderSegment(line, m_RenderRay.starwidth, m_RenderRay.material, m_RenderRay.hitIO);
                    }
                }else
                {
                    if (line.positionCount != 2) line.positionCount = 2;
                    Render.RenderTwoPointSegment(line, ray.origin, GetRayEndPoint(), m_RenderRay.starwidth, m_RenderRay.endwidth, m_RenderRay.material, m_RenderRay.normal);
                }




            }
            else
            {
                Render.LineRenderState(line, false);
            }
            Turn();
            if(inputDevice==InputDevice.MouseKeyboard)
            PlayerMove();//PC

        }

     
        void PlayerMove()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 dir = horizontal * player.transform.right*5*Time.deltaTime+vertical*player.transform.forward*5*Time.deltaTime;
            player.Move(dir+new Vector3(0,-0.2f,0));
            Camera.main.transform.Rotate(Input.GetAxis("Mouse Y")*-1f, 0,0);
            player.transform.Rotate(0,Input.GetAxis("Mouse X"),0);
        }


        /// <summary>
        /// 角色控制器旋转
        /// </summary>
        void Turn()
        {
            if (m_EnebledRotate) 
            {
               transform.Rotate(Vector3.up, 45 * GetDir());
            }       
        }

       /// <summary>
       /// 获取手柄输入方向
       /// </summary>
       /// <returns></returns>
        int GetDir()
        {
            if(VRInput.GetKeyDown(m_TeleportButton,currentSource))
            {
                if (VRInput.LeftHorizontal > 0.2f || VRInput.RightHorizontal > 0.2f) return 1;
                else if (VRInput.LeftHorizontal < -0.2f || VRInput.RightHorizontal < -0.2f) return -1;
                else return 0;
            }
            else return 0;
        }


       /// <summary>
       /// 设置手柄设备输入源
       /// </summary>
       /// <returns></returns>
        Transform GetHand()
        {
            switch (inputSource) 
            {
                case ControllerType.Left:
                    currentHand = leftHand; 
                    currentSource = ControllerType.Left;
                    return currentHand;
                case ControllerType.Right:
                    currentHand = RightHand;
                    currentSource = ControllerType.Right;
                    return currentHand;
                case ControllerType.All:
                    if (VRInput.GetKeyDown(KeyType.Trigger, ControllerType.Left))
                    {
                        currentHand = leftHand;
                        currentSource = ControllerType.Left;
                    }
                    else if (VRInput.GetKeyDown(KeyType.Trigger, ControllerType.Right)) 
                    {
                        currentHand = RightHand;
                        currentSource = ControllerType.Right;
                    }
                    return currentHand;
            }
            return RightHand;
        }

        private void RayCastEvent()
        {
            if (RayCastState())
            {
                if (m_IOUI != null)
                {
                    if (m_IOUI.gameObject != hit.collider.gameObject)
                    {
                        m_IOUI.Exit();
                        m_IOUI = RayCastIOUI();
                    }
                    else
                    {
                        m_IOUI.Enter();
                        m_RenderRay.material.SetColor(m_RenderRay.shaderColorName, m_RenderRay.hitIOC);
                          line.colorGradient = m_RenderRay.hitIO;      
                        m_IOUI.Stay(VRInput.GetKeyDown( button, currentSource), VRInput.GetKeyUp( button, currentSource), VRInput.GetKey(button, currentSource),Input.GetMouseButtonDown(0),Input.GetMouseButtonUp(0),Input.GetMouseButton(0));
                    }

                 
                }
                else 
                {
                    if (m_IO != null)
                    {
                        if (m_IO.gameObject != hit.collider.gameObject)
                        {
                            m_IO.Exit();
                            m_IO = RayCastIO();
                        }
                        else
                        {
                            m_IO.Enter();
                            m_RenderRay.material.SetColor(m_RenderRay.shaderColorName, m_RenderRay.hitIOC);
                            line.colorGradient = m_RenderRay.hitIO;                           
                            m_IO.Stay(VRInput.GetKeyDown(button, currentSource), VRInput.GetKeyUp( button, currentSource),Input.GetMouseButtonDown(0),Input.GetMouseButtonUp(0));
                        }
                    }
                    else
                    {
                        m_RenderRay.material.SetColor(m_RenderRay.shaderColorName, m_RenderRay.normalC);
                        line.colorGradient = m_RenderRay.normal;
                    }
                  
                }


                if (m_IOGrab!=null)
                {
                    m_RenderRay.material.SetColor(m_RenderRay.shaderColorName, m_RenderRay.hitIOC);
                    line.colorGradient = m_RenderRay.hitIO;
                }
             
                    m_IO = RayCastIO();
                m_IOUI = RayCastIOUI();
                m_IOGrab = RayCastGrab();
            }
            else
            {
                if (m_IOUI != null)
                {
                    m_IOUI.Exit();
                    m_IOUI = null;
                }
                if (m_IO != null)
                {
                    m_IO.Exit();
                    m_IO = null;
                }

                if (m_IOGrab!=null)
                {
                    m_IOGrab = null;
                }

                line.colorGradient = m_RenderRay.normal;
                m_RenderRay.material.SetColor(m_RenderRay.shaderColorName, m_RenderRay.normalC);
            }
        }

        private bool RayCastState()//������ײ״̬
        {
            if (Physics.Raycast(ray, out hit, m_MaxCastDistance, layerMask.value))
                return true;
            else return false;
        }

        private InteractionObject RayCastIO()//������ɽ�������Ľ���״̬
        {
            if (RayCastState())
            {
                if (hit.collider.gameObject.GetComponent<InteractionObject>() != null)
                {
                    onIO = true;
                    return hit.collider.gameObject.GetComponent<InteractionObject>();

                }
                else
                {
                    onIO = false;
                    return null;
                }
            }
            else 
            {
                onIO = false;
                return null;
            } 
        }

        //Ѱ�����ո�����
        [ReadOnly]
        public Transform par;
        void FindParentGrabObjects(GameObject obj)
        {
            if (obj.transform.GetComponent<GrabObjects>() != null)
            {
                par = obj.transform;

                //return;
            }
            else if (obj.transform.parent != null)
            {
                Debug.LogWarning(obj.name);
                FindParentGrabObjects(obj.transform.parent.transform.gameObject);
            }
            else
            {
                par = null;
            }
        }

        private  GrabObjects RayCastGrab()
        {
            if (RayCastState()) 
            {
                if (hit.collider.gameObject.GetComponent<GrabObjects>() != null)
                {
                    onIO = true;
                    return hit.collider.gameObject.GetComponent<GrabObjects>();
                }
                else if (hit.collider.transform.parent!=null)
                {
                    FindParentGrabObjects(hit.collider.gameObject);
                    if (par!=null&& par.GetComponent<GrabObjects>() != null)
                    {
                           onIO = true;
                           return par.GetComponent<GrabObjects>();
                    }
                    else 
                    {
                        onIO = false;
                        return null;
                    }
                }
                else
                {
                    onIO = false;
                    return null;
                }
            }
            else
            {
                onIO = false;
                return null;
            }
        }


        private InteractionUI RayCastIOUI()//������ɽ�������Ľ���״̬
        {
            if (RayCastState())
            {
                if (hit.collider.gameObject.GetComponent<InteractionUI>() != null)
                {
                    onIO = true;
                    return hit.collider.gameObject.GetComponent<InteractionUI>();

                }
                else
                {
                    onIO = false;
                    return null;
                }
            }
            else
            {
                onIO = false;
                return null;
            }
        }
        public Vector3 GetRayEndPoint()
        {
            //HLVR:��ȡ���ߵ��յ�����
            if (RayCastState())
                return hit.point;
            else
                return ray.GetPoint(m_MaxCastDistance*m_RenderRay.m_NoHitLength);
        }

        public GameObject GetRayGameObject()
        {
            if (RayCastState())
                return hit.collider.gameObject;
            else
                return null;
        }

        public Vector3 GetHitObjectPosition()
        {
            if (RayCastState())
                Debug.LogWarning(hit.collider.gameObject.transform.position);
            if (RayCastState())
                return hit.collider.gameObject.transform.position;
            else
                return new Vector3(0,0,0);
        }
    }
    /// <summary>
    ///  MouseKeyboard 鼠标键盘；VR 虚拟现实设备 头盔、手柄；Touch 触屏；HandShank 游戏手柄, HandTracking 手势追踪
    /// </summary>
    public enum InputDevice 
    {
        MouseKeyboard,
        VR,
        HandTracking,
        Touch,
        HandShank,
    }

    [System.Serializable]
    public struct RenderArguments
    {
        [Tooltip("���߿�ʼ����")]
        public float starwidth;
        [Tooltip("����β������")]
        public float endwidth;
        [Tooltip("���߲�����")]
        public Material material;
        [Tooltip("���߽���ɫ")]
        // [HideInInspector]
        public string shaderColorName; 
        public Gradient normal;
        public Gradient hitIO;
        public Color normalC;
        public Color hitIOC;
        // [ColorUsage(true, true)]
        //[Tooltip("����������ɫ")]
        // public Color normal;
        //[Tooltip("���߽�����ɫ")]
        //[ColorUsage(true, true)]
        // public Color hitIO;
        [Tooltip("��������ײ�³���")]
        public float m_NoHitLength;
        public Bezier bezier;
    }

    [System.Serializable]
    public struct CurveLine 
    {
        public bool m_EnebledMove;
        public LayerMask layerMask;
        public int PointCount;
        public float Interval;
        public float Gravity;
        public float width;
        public float StarPointdistance;
        public Teleport teleport;
        [Tooltip("����������ɫ")]
       // [ColorUsage(true, true)]
        public Gradient normal;
        [Tooltip("���߽�����ɫ")]
        //[ColorUsage(true, true)]
        public Gradient hitIO;
        public void SetEnebledMoveState(bool state)
        {
            m_EnebledMove = state;
        }
    }

    [System.Serializable]
    public struct Bezier 
    {
        [Tooltip("�Ƿ�ʹ�ñ�����Ч��")]
        public bool m_UseBezier;
        [Tooltip("ʹ�ñ��������߹��ɵ���")]
        public int BezierPointCount;
        public float m_Distance;
    }

    //[System.Serializable]
    //public struct Simulator
    //{
    //    public float moveSpeed;
    //    public float rotateSpeed;
    //}

    [System.Serializable]
    public struct HeadInfoData 
    {
        [Tooltip("启用")]
        public bool m_EnablePosition;
        [Tooltip("启用")]
        public bool m_EnableRotation;
        [Tooltip("不使用追踪数据，是否使用指定数据")]
        public bool m_UseNoTraceTransformData;
        [Tooltip("当无不使用追踪数据的时候使用指定的位置数据")]
        public Vector3 m_NoVRTracePosition;
        [ReadOnly]
        [Tooltip("空间追踪位置")]
        public Vector3 position;
        [Tooltip("空间追踪角度")]
        [ReadOnly]
        public Quaternion rotation;
        [Tooltip("运动速度")]
        [ReadOnly]
        public Vector3 velocity;
        [Tooltip("运动角速度")]
        [ReadOnly]
        public Vector3 AngularVelocity;
        public void TracePosAndRot()
        {
            UnityEngine.XR.InputDevices.GetDeviceAtXRNode(XRNode.Head).TryGetFeatureValue(UnityEngine.XR.CommonUsages.devicePosition, out position);
            UnityEngine.XR.InputDevices.GetDeviceAtXRNode(XRNode.Head).TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceRotation, out rotation);
         
        }

        public void TracePhysics()
        {
            UnityEngine.XR.InputDevices.GetDeviceAtXRNode(XRNode.Head).TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out velocity);
            UnityEngine.XR.InputDevices.GetDeviceAtXRNode(XRNode.Head).TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceAngularVelocity, out AngularVelocity);
        }
        public void SetPositonEnable(bool state)
        {
            m_EnablePosition = state;
        }

        public void SetRoatationEnable(bool state)
        {
            m_EnableRotation = state;
        }
    }



    [System.Serializable]
    public struct HandleInfoData
    {
        [Tooltip("启用")]
        public bool m_EnablePosition;
        [Tooltip("启用")]
        public bool m_EnableRotation;
        [Tooltip("不使用追踪数据，是否使用指定数据")]
        public bool m_UseNoTraceTransformData;
        [Tooltip("当无不使用追踪数据的时候使用指定的位置数据")]
        public Vector3 m_NoVRTracePosition;
        //[Tooltip("当无不使用追踪数据的时候使用指定的角度数据")]
        //public Vector3 m_NoVRTraceRotation;
        [ReadOnly]
        [Tooltip("空间追踪位置")]
        public Vector3 position;
        [Tooltip("空间追踪角度")]
        [ReadOnly]
        public Quaternion rotation;
        [Tooltip("运动速度")]
        [ReadOnly]
        public Vector3 velocity;
        [Tooltip("运动角速度")]
        [ReadOnly]
        public Vector3 AngularVelocity;

        [ReadOnly]
        [Tooltip("手柄挥动的方向")]
        public Vector3 direction;
        public void Trace(ControllerType type) 
        {
            switch (type)
            {
                case ControllerType.Left:
                    UnityEngine.XR.InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.devicePosition, out position);
                    UnityEngine.XR.InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceRotation, out rotation);
                    UnityEngine.XR.InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out velocity);
                    UnityEngine.XR.InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceAngularVelocity, out AngularVelocity);
                  
                    break;
                case ControllerType.Right:
                    UnityEngine.XR.InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.devicePosition, out position);
                    UnityEngine.XR.InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceRotation, out rotation);
                    UnityEngine.XR.InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out velocity);
                    UnityEngine.XR.InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceAngularVelocity, out AngularVelocity);
                    break;
            }   
        }


        public Vector3 GetDir(Vector3 lastPos,Vector3 currentPos)
        {
            return (currentPos - lastPos).normalized;
        }


        public void SetPositonEnable(bool state)
        {
            m_EnablePosition = state;
        }

        public void SetRoatationEnable(bool state)
        {
           m_EnableRotation = state;
        }
    }

}

[System.Serializable]
public struct InfoPanel 
{
    [Tooltip("设备名称")]
    [ReadOnly]
    public string[] XRName;
    public void VRInfo()
    {
        XRName = XRSettings.supportedDevices;
    }
}



