using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SetObjectTransform : MonoBehaviour
{
    public enum TransType 
    {
       World,
       Local
    }
    [Tooltip("��������任�Ŀռ����ͣ�World �����������꣬Local ���ھֲ�����")]
    public TransType transTypel;
    [Tooltip("������ͬ����λ��ʱ���������ٸýű����ڳ�����")]
    public bool setPosFinishDestroy=true;
    [Tooltip("������ͬ����λ��ʱ������ѱ�������������ΪĿ�������������")]
    public bool setObjectToTarChild = true;
    [Tooltip("�����õĶ���")]
    public Transform setobject;
    [Tooltip("�任��ϢĿ��")]
    public Transform targetObject;
    [Tooltip("����λ��ͬ��")]
    public bool m_SetPos;
    [Tooltip("������Ԫ��ͬ��")]
    public bool m_SetRotation;
    [Tooltip("��������ͬ��")]
    public bool m_SetScale;
    public bool openEnable;

    private void Awake()
    {
        if (targetObject == null) targetObject = this.gameObject.transform;
    }

    private void OnEnable()
    {
        if (openEnable) 
        {
            SetTransform();
        }
    }

    public void SetTransform() 
    {
        switch (transTypel) 
        {
            case TransType.World:
                if (m_SetPos) setobject.position = targetObject.position;
                if (m_SetRotation) setobject.rotation = targetObject.rotation;
                if (m_SetScale) setobject.localScale = targetObject.localScale;
                if(setPosFinishDestroy)
                Destroy(this.gameObject);
                if (setObjectToTarChild)
                    setobject.parent = targetObject;
                FinishEvent.Invoke();
                break;
            case TransType.Local:
                if (m_SetPos) setobject.localPosition = targetObject.position;
                if (m_SetRotation) setobject.localRotation= targetObject.rotation;
                if (m_SetScale) setobject.localScale = targetObject.localScale;
                if (setPosFinishDestroy)
                    Destroy(this.gameObject);
                if (setObjectToTarChild)
                    setobject.parent = targetObject;
                FinishEvent.Invoke();
                break;
        }
      
    }

    public void ClearSetObjectParent() 
    {
        if (setobject.parent != null) setobject.parent = null;
    }

    public void ResetPosition(Transform obj) 
    {
        obj.position = new Vector3(0,0,0);
    }
    public void ResetRotation(Transform obj) {
        obj.rotation = Quaternion.identity;
    }

    public SetTransformFinishEvent FinishEvent;
    [System.Serializable]
    public class SetTransformFinishEvent : UnityEvent { }
}
