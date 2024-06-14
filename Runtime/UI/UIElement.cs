using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HLVR.EventSystems;
public class UIElement : MonoBehaviour
{
    public InteractionEvent m_OnEnable;
    public InteractionEvent m_OnDisable;
    private void OnEnable()
    {
        if (!UIManager.Instance.stack.Contains(this.gameObject))
        UIManager.Instance.AddElementStack(this.gameObject);
        m_OnEnable?.Invoke();
    }

    private void OnDisable()
    {
        m_OnDisable?.Invoke();
    }
}
