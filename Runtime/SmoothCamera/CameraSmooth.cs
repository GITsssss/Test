using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSmooth : MonoBehaviour
{
    //public Camera maincamera;
    //[Range(0, 1)]
    //public float value;
    //public SmoothType smoothType;
    //public KeyCode key;
    //public RawImage raw;
    Camera cameraCurrent;
    //public Slider slider;
    //
    public UserControl userControl;
    Vector3 lastVec;//上一帧向量
    Vector3 currentVec;//当前向量
    Vector3 lastPos;//上一帧位置
    Vector3 currentPos;//当前位置
    float timer;
    [Header("自动运算")]
    [Header("自动刷新帧率")]
    public float UpdateFrame= 0.05f;
    [Header("自动刷新角度，超出角度自动刷新")]
    public float autoClampAngle= 5f;

    [Range(0,1)]
    public float PositionLerp= 0.56f;
    public float stabilizePositionClampValue= 0.01f;
    [Range(0, 100)]
    public float RotationLerp= 0.25f;
    public float m_IgnoreValue=0.7f;
    public float stabilizeRotaionClampValue;
    [ReadOnly]
    public bool openpos;
    [ReadOnly]
    public bool openrot;
    private void Awake()
    {

        userControl.slider = transform.parent.GetChild(0).GetChild(1).GetComponent<Slider>();
        userControl.raw = transform.parent.GetChild(0).GetChild(0).GetComponent<RawImage>();
        if (userControl.maincamera == null)
            userControl.maincamera = Camera.main;

        cameraCurrent = transform.GetComponent<Camera>();
        cameraCurrent.nearClipPlane = 0.001f;
        transform.rotation = userControl. maincamera.transform.rotation;
        transform.position = userControl.maincamera.transform.position;
    }
    private void Update()
    {
        if (Input.GetKeyDown(userControl.key))
        {
            userControl. raw.gameObject.SetActive(!userControl.raw.gameObject.activeSelf);
            cameraCurrent.enabled = !cameraCurrent.enabled;
        }

        if (Input.GetKeyDown(KeyCode.F11))
        {
            userControl.slider.gameObject.SetActive(!userControl.slider.gameObject.activeSelf);
        }
    }
    private void FixedUpdate()
    {

        #region
        if (timer>=0.2f)
        {
            currentVec = userControl.maincamera.transform.forward;
            timer = 0;
        }
        currentVec = userControl.maincamera.transform.forward;
        currentPos = userControl.maincamera.transform.position;
        userControl.value = userControl.slider.value;
        switch (userControl.smoothType)
        {
            case SmoothType.SlerpUnclamped:
                transform.position = userControl.maincamera.transform.position;
                transform.rotation = Quaternion.SlerpUnclamped(transform.rotation, userControl.maincamera.transform.rotation, userControl.value);
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
                break;

            case SmoothType.Lerp:
                transform.position = userControl. maincamera.transform.position;
                transform.rotation = Quaternion.Lerp(transform.rotation, userControl.maincamera.transform.rotation, userControl.value);
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
                break;

            case SmoothType.LookCameraRay:
                if (Vector3.Magnitude(currentPos - lastPos) >= stabilizePositionClampValue)
                {
                    transform.position = userControl.maincamera.transform.position;

                }
                Ray ray = new Ray(userControl.maincamera.transform.position, userControl.maincamera.transform.forward);
                Vector3 point = ray.GetPoint(3);
                cameraCurrent.transform.LookAt(point);
                break;

            case SmoothType.UpdateFrame:
                transform.position = userControl. maincamera.transform.position;
                if (Vector3.Angle(currentVec, lastVec) <= autoClampAngle)
                {
                    userControl.slider.value = 0.2f;
                }
                else 
                {
                    userControl. slider.value = 1f;
                }
                transform.rotation = Quaternion.SlerpUnclamped(transform.rotation, userControl.maincamera.transform.rotation, userControl.value);
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
                break;
            case SmoothType.StabilizePosition:

               
                if (Vector3.Magnitude(currentPos - lastPos)>= stabilizePositionClampValue)
                {
                    openpos = true;
                                
                }
                if (openpos) 
                {
                    transform.position =Vector3.Lerp(transform.position, userControl.maincamera.transform.position, PositionLerp) ;
                    if (transform.position == userControl.maincamera.transform.position)
                        openpos = false;
                }

                if (Vector3.Angle(transform.forward,userControl.maincamera.transform.forward)>= stabilizeRotaionClampValue) 
                {
                    openrot = true;
                   
                }

            //    Debug.Log(Vector3.Angle(transform.forward, userControl.maincamera.transform.forward));
                if (openrot) 
                {
                    //    Vector3 an = Vector3.Slerp(transform.eulerAngles,userControl.maincamera.transform.eulerAngles,RotationLerp);
                    transform.rotation = Quaternion.Slerp(transform.rotation, userControl.maincamera.transform.rotation, RotationLerp);
                   // transform.rotation = Quaternion.RotateTowards(transform.rotation,userControl.maincamera.transform.rotation,RotationLerp);
                    //if (transform.eulerAngles== userControl.maincamera.transform.eulerAngles)
                    //    openrot = false;
                    if(Vector3.SqrMagnitude(transform.eulerAngles- userControl.maincamera.transform.eulerAngles)<= m_IgnoreValue) openrot = false;
                }
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
                break;
        }
        timer += Time.deltaTime;
        if (timer >= UpdateFrame)
        {
          
            lastVec = userControl.maincamera.transform.forward;
            lastPos = userControl.maincamera.transform.position;
            timer = 0;
        }

        #endregion

      

    }



}
public enum SmoothType
{
    SlerpUnclamped,
    Lerp,
    LookCameraRay,
    UpdateFrame,
    StabilizePosition,
}
[System.Serializable]
public struct UserControl
{
    public Camera maincamera;
    [Range(0, 1)]
    public float value;
    public SmoothType smoothType;
    public KeyCode key;
    [HideInInspector]
    public RawImage raw;
    //Camera cameraCurrent;
    [HideInInspector]
    public Slider slider;
}

