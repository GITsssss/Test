using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SetGesture))]
public class SetGestureEditor :Editor
{
    SetGesture setGesture;
    public override void OnInspectorGUI()
    {
        setGesture = target as SetGesture;
        base.OnInspectorGUI();
        if (GUILayout.Button("保存数据")) 
        {
            setGesture.Save();
            EditorUtility.SetDirty(setGesture.handData);
        }

        if (GUILayout.Button("重置手势"))
        {
            setGesture.ResetGesture();
        }
    }

    [MenuItem("HLVR/Tool/GestureAnimation")]
    public static void SetGestureTool()
    {
        GameObject tool=new GameObject();
        tool.AddComponent<SetGesture>();
        tool.name = "SetGestureTool";
        GameObject m_HandEditor = new GameObject();
        m_HandEditor.name = "HandEditor";
        m_HandEditor.transform.parent = tool.transform;
        m_HandEditor.transform.localPosition = Vector3.zero;
        m_HandEditor.transform.parent = null;
        tool.transform.parent = m_HandEditor.transform;
        tool.GetComponent<SetGesture>().lefthand.transform.parent= m_HandEditor.transform;
        tool.GetComponent<SetGesture>().righthand.transform.parent = m_HandEditor.transform;
    }
}
