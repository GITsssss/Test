using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HLVR.EventSystems;
public class DebugEvent : MonoBehaviour
{
    public int index;
    public CustomEvents[] events;
    public void Test()
    {
        events[index].m_Events?.Invoke();
    }
}
