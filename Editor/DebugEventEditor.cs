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
        // base.OnInspectorGUI();
        serializedObject.Update();

        using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("events_a"));
            if (GUILayout.Button("ต๗สิ"))
            {
                de.events_a?.Invoke();
            }
        }

        using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("events_b"));
            if (GUILayout.Button("ต๗สิ"))
            {
                de.events_b?.Invoke();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
