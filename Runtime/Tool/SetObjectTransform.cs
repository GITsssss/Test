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
    [Tooltip("设置物体变换的空间类型，World 基于世界坐标，Local 基于局部坐标")]
    public TransType transTypel;
    [Tooltip("开启后，同步完位置时，将会销毁该脚本及在乘物体")]
    public bool setPosFinishDestroy=true;
    [Tooltip("开启后，同步完位置时，将会把被设置物体设置为目标物体的子物体")]
    public bool setObjectToTarChild = true;
    [Tooltip("被设置的对象")]
    public Transform setobject;
    [Tooltip("变换信息目标")]
    public Transform targetObject;
    [Tooltip("开启位置同步")]
    public bool m_SetPos;
    [Tooltip("开启四元数同步")]
    public bool m_SetRotation;
    [Tooltip("开启缩放同步")]
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
