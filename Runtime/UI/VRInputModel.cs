using UnityEngine;
using UnityEngine.EventSystems;



public class VRInputModel : BaseInputModule
{
    /// <summary>
    /// �¼������
    /// </summary>
    public Camera eventCamera;
    /// <summary>
    /// �Ƿ�������
    /// </summary>
    public bool isActive = true;
    /// <summary>
    /// �Ƿ�ִ��UI������־λ
    /// </summary>
    public bool isExecute = false;
    /// <summary>
    /// ָ���¼�����
    /// </summary>
    public PointerEventData Data { get; private set; } = null;
    //protected override void Awake()
    //{
    //    eventCamera = GetComponent<Camera>();
    //}

   
    protected override void Start()
    {
        Data = new PointerEventData(eventSystem);
        //�趨���ߵ���ʼ��Ϊ�¼�������Ӵ�����
        Data.position = eventCamera.transform.position;
      
    }
    public override void Process()
    {
        if (isActive)
        {
            //�������߼��UI
            eventSystem.RaycastAll(Data, m_RaycastResultCache);
            //���ɽ���Զ��������ײ���m_RaycastResultCache�л�ȡ��һ�������������ײ�����Ӧ�����߽��
            Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
            //�ȴ������ߵ������Ƴ�UI��Ϸ���壨����¼��ü̳�IPointerEnterHandler��IPointerExitHandler�е��¼�������
            HandlePointerExitAndEnter(Data, Data.pointerCurrentRaycast.gameObject);
            Debug.Log(Data.pointerCurrentRaycast.gameObject);
            //���µ����ť�ı�־λ
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
        //�ѵ�ǰ��������Ϣ��ֵ����갴������
        Data.pointerPressRaycast = Data.pointerCurrentRaycast;
        //�ѹ�갴�����߶�Ӧ����Ϸ���帳ֵ��ָ�������е�pointPress
        Data.pointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(Data.pointerPressRaycast.gameObject);
        //ִ�й�갴���¼������¼����ü̳���IPointerClickHandler���������е��¼�����
        ExecuteEvents.Execute(Data.pointerPress, Data, ExecuteEvents.pointerDownHandler);
        //�ѹ�갴�����߶�Ӧ����Ϸ���帳ֵ��ָ�������е�pointDrag
        Data.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(Data.pointerPressRaycast.gameObject);
        //ִ�й�꿪ʼ�϶��¼������¼����ü̳���IIDragHandler���������е��¼�����
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
