using UnityEngine;
using UnityEngine.EventSystems;



public class VRInputModel : BaseInputModule
{
    /// <summary>
    /// 事件摄像机
    /// </summary>
    public Camera eventCamera;
    /// <summary>
    /// 是否检测射线
    /// </summary>
    public bool isActive = true;
    /// <summary>
    /// 是否执行UI操作标志位
    /// </summary>
    public bool isExecute = false;
    /// <summary>
    /// 指针事件数据
    /// </summary>
    public PointerEventData Data { get; private set; } = null;
    //protected override void Awake()
    //{
    //    eventCamera = GetComponent<Camera>();
    //}

   
    protected override void Start()
    {
        Data = new PointerEventData(eventSystem);
        //设定射线的起始点为事件相机的视窗中心
        Data.position = eventCamera.transform.position;
      
    }
    public override void Process()
    {
        if (isActive)
        {
            //发射射线检测UI
            eventSystem.RaycastAll(Data, m_RaycastResultCache);
            //从由近到远的射线碰撞结果m_RaycastResultCache中获取第一个（最近）的碰撞结果对应的射线结果
            Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
            //先处理射线点进入或移出UI游戏物体（这个事件让继承IPointerEnterHandler和IPointerExitHandler中的事件触发）
            HandlePointerExitAndEnter(Data, Data.pointerCurrentRaycast.gameObject);
            Debug.Log(Data.pointerCurrentRaycast.gameObject);
            //按下点击按钮的标志位
            if (isExecute)
            {
                ProcessPress();
            }
            else
            {
                ProcessRelease();
            }
        }

    }
    private void ProcessPress()
    {
        print("process");
        //把当前的射线信息赋值给光标按下射线
        Data.pointerPressRaycast = Data.pointerCurrentRaycast;
        //把光标按下射线对应的游戏物体赋值给指针数据中的pointPress
        Data.pointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(Data.pointerPressRaycast.gameObject);
        //执行光标按下事件，该事件会让继承了IPointerClickHandler的派生类中的事件触发
        ExecuteEvents.Execute(Data.pointerPress, Data, ExecuteEvents.pointerDownHandler);
        //把光标按下射线对应的游戏物体赋值给指针数据中的pointDrag
        Data.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(Data.pointerPressRaycast.gameObject);
        //执行光标开始拖动事件，该事件会让继承了IIDragHandler的派生类中的事件触发
        // ExecuteEvents.Execute(Data.pointerDrag, Data, ExecuteEvents.beginDragHandler);
        ExecuteEvents.Execute(Data.pointerDrag, Data, ExecuteEvents.dragHandler);
    }
    private void ProcessRelease()
    {
        GameObject pointRelease = ExecuteEvents.GetEventHandler<IPointerClickHandler>(Data.pointerCurrentRaycast.gameObject);

        if (Data.pointerPress == pointRelease)
            ExecuteEvents.Execute(Data.pointerPress, Data, ExecuteEvents.pointerClickHandler);

        ExecuteEvents.Execute(Data.pointerPress, Data, ExecuteEvents.pointerUpHandler);
        ExecuteEvents.Execute(Data.pointerDrag, Data, ExecuteEvents.endDragHandler);

        Data.pointerPress = null;
        Data.pointerDrag = null;

        Data.pointerCurrentRaycast.Clear();
    }
}
