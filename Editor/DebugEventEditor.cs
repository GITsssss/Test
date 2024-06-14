using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using HLVR.EventSystems;

[CustomEditor(typeof(DebugEvent))]
public class DebugEventEditor :Editor
{
    DebugEvent de; 
    public override void OnInspectorGUI()
    {
        de =target as  DebugEvent;
        base.OnInspectorGUI();
        if (GUILayout.Button("ต๗สิ")) 
        {
            de.Test();
        }
    }
}
