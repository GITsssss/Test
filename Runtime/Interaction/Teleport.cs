using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HLVR.InputSystem;
namespace HLVR.Interaction 
{
    //[RequireComponent(typeof(LineRenderer))]
    public class Teleport : MonoBehaviour
    {
        KeyType keyType;
        ControllerType  LRType;
        [ReadOnly]
        public LineRenderer line;
       // InteractionRay.InstanceInteractionRay.Instance;
        Vector3[] posints;
        public GameObject m_TeleportPooint;
        public GameObject m_TeleportDestinationInvalid;
        public  Material lineMaterial;
        public AudioClip m_TeleportAE;
        public AudioClip m_TeleportDestinationInvalidAE;
        [ReadOnly]
        GameObject currentTeleporPooint;
        [ReadOnly]
        GameObject currentTeleportDestinationInvalid;
        bool isNoIO = true;
        bool isNOMovePoint = false;
        public int hitID;
        [ReadOnly]
        public bool isNoHit;
        Vector3 hitPoint;
        [HideInInspector]
        public bool IsNoMove;
        RaycastHit[] raycastHits;
        Gradient normal;
        Gradient hitIO;
        TeleportArea teleportArea;
        TeleportPoint teleportPoint;
        GameObject hitObject;

        private static Teleport teleport;
        public static Teleport Instance 
        {
            get 
            {
                return teleport;
            }
        }

        private void Reset()
        {
            if(teleport==null)
                teleport = this;
            else Destroy(this);
            if (FindObjectsOfType<Teleport>().Length > 1)
            {
                DestroyImmediate(this.gameObject);
                return;
            }
            this.gameObject.AddComponent<LineRenderer>();
            this.transform.gameObject.name = "Teleport";
            lineMaterial = new Material(Shader.Find("Universal Render Pipeline/Particles/Unlit"));
            lineMaterial.SetFloat("_Surface",1);
            
        }

        private void Awake()
        {
            TryGetComponent<LineRenderer>(out line);
            line.enabled = false;
            line.material = lineMaterial;
            line.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;

            //  teleportArea = GameObject.FindObjectsOfType<TeleportArea>();
            currentTeleporPooint = Instantiate(m_TeleportPooint);
            currentTeleportDestinationInvalid = Instantiate(m_TeleportDestinationInvalid);
            currentTeleporPooint.gameObject.SetActive(false);
            currentTeleportDestinationInvalid.gameObject.SetActive(false);
            this.enabled = false;
        }

        private void Start()
        {

            keyType = InteractionRay.Instance.m_TeleportButton;
            line.startWidth = InteractionRay.Instance.curveline.width;
            line.endWidth = InteractionRay.Instance.curveline.width;
            normal = InteractionRay.Instance.curveline.normal;
            hitIO = InteractionRay.Instance.curveline.hitIO;
           
            Vector3 oripos = InteractionRay.Instance.leftHand.position;



            line.enabled = true;
            currentTeleporPooint.SetActive(true);
            currentTeleporPooint.gameObject.SetActive(false);
            currentTeleportDestinationInvalid.gameObject.SetActive(false);
            //start 初始化线段的显示位置
            int count = InteractionRay.Instance.curveline.PointCount;
            float interval = InteractionRay.Instance.curveline.Interval;
            Vector3 oripos2 = InteractionRay.Instance.currentHand.position;
            float gravity = InteractionRay.Instance.curveline.Gravity;
            Vector3 dir = InteractionRay.Instance.currentHand.forward;
            raycastHits = new RaycastHit[count - 1];
            posints = Curve.StructcCurve(count, interval, oripos2, gravity, dir);
            line.positionCount = posints.Length;
            for (int i = 0; i < posints.Length; i++)
            {
                line.SetPosition(i, posints[i]);
            }
        }

        private void OnEnable()
        {
            line.enabled = true;//end
        }

        private void OnDisable()
        {
            line.enabled = false;
            isNoHit = false;
            currentTeleporPooint.SetActive(false);
            currentTeleportDestinationInvalid.SetActive(false);
            if (teleportArea != null)
            {
                teleportArea.current.SetInt("showColor", 0);
                teleportArea = null;
            }
        }

        private void Update()
        {
            CurveLaunch();
            DrawCurve(); 
        }


        public void DrawCurve()
        {
            line.positionCount = hitID;
            for (int i = 0; i < hitID; i++)
            {
               line.SetPosition(i, posints[i]);
            }
        }

        public void CurveLaunch()
        {
            int count = InteractionRay.Instance.curveline.PointCount;
            float interval = InteractionRay.Instance.curveline.Interval;
            Vector3 oripos = InteractionRay.Instance.currentHand.position+InteractionRay.Instance.currentHand.transform.forward* InteractionRay.Instance.curveline.StarPointdistance;
            float gravity = InteractionRay.Instance.curveline.Gravity;
            Vector3 dir = InteractionRay.Instance.currentHand.forward;
            raycastHits = new RaycastHit[count - 1];
            posints = Curve.StructcCurve(count, interval, oripos, gravity, dir);
            for (int i = 0; i < posints.Length; i++)
            {
                if(i< posints.Length-1)
                if (Physics.Linecast(posints[i], posints[i + 1], out raycastHits[i], InteractionRay.Instance.curveline.layerMask.value))
                {
                        if (raycastHits[i].collider.GetComponent<TeleportArea>() != null)
                        {
                            hitID = i;
                            isNoHit = true;
                            currentTeleporPooint.transform.position = raycastHits[i].point;
                            currentTeleporPooint.transform.up = raycastHits[i].normal;
                            hitPoint = raycastHits[i].point;

                            line.colorGradient = hitIO;
                           // line.endColor = InteractionRay.Instance.curveline.hitIO; 
                            teleportArea = raycastHits[i].collider.GetComponent<TeleportArea>();
                            if(teleportArea!=null)
                            teleportArea.current.SetInt("showColor", 1);
                            currentTeleporPooint.SetActive(true);
                            currentTeleportDestinationInvalid.SetActive(false);
                            IsNoMove = true;
                            teleportPoint = null;
                            break;
                        }
                        else  if (raycastHits[i].collider.GetComponent<TeleportPoint>() != null)
                        {
                            hitID = i;
                            isNoHit = true;
                            currentTeleporPooint.transform.position = raycastHits[i].collider.transform.position;
                            if (teleportArea != null) 
                            {
                                teleportArea.current.SetInt("showColor", 0);
                            } 
                            hitPoint = raycastHits[i].collider.transform.position;
                            line.colorGradient = hitIO;
                            currentTeleporPooint.SetActive(true);
                            hitObject = raycastHits[i].collider.transform.gameObject;
                            currentTeleportDestinationInvalid.SetActive(false);
                            IsNoMove = true;
                            teleportPoint = raycastHits[i].collider.GetComponent<TeleportPoint>();
                            break;
                        }
                        else
                        {
                            teleportPoint = null;
                            hitID = i;
                            isNoHit = true;
                            currentTeleportDestinationInvalid.SetActive(true);
                            currentTeleporPooint.SetActive(false);
                            if (teleportArea != null) 
                            {
                               teleportArea.current.SetInt("showColor", 0);
                            }
                          
                            currentTeleportDestinationInvalid.transform.position = raycastHits[i].point;
                            currentTeleportDestinationInvalid.transform.up = raycastHits[i].normal;
                            line.colorGradient = normal;
                            IsNoMove = false;
                            break;  
                        }
                }
            }

            if (VRInput.GetKeyUp(KeyType.Rocker, ControllerType.Right)||Input.GetMouseButtonUp(1))
            {
                if (IsNoMove)
                {

                    InteractionRay.Instance.transform.position = hitPoint;
                    if(teleportPoint != null)
                    if (teleportPoint.synchronizationRotation) 
                    {
                        InteractionRay.Instance.transform.rotation = hitObject.transform.rotation;
                        teleportPoint = null;
                    }
                    transform.GetComponent<Teleport>().enabled = false;
                }
                else 
                {
                    transform.GetComponent<Teleport>().enabled = false;
                }
            }

        }
    }
}
