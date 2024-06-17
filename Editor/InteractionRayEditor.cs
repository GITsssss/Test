using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using HLVR.Interaction;

[CustomEditor(typeof(InteractionRay))]
public class InteractionRayEditor : Editor
{
    InteractionRay interactionRay;
    public bool m_EnableXRInitializeXROnStartUP;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        interactionRay = target as InteractionRay;
       

        GUILayout.BeginHorizontal("box");

        if (GUILayout.Button("����Ĭ������"))
        {
            interactionRay.SetDefaultValue();
        }

        if (GUILayout.Button("������������"))
        {
            interactionRay.SaveDefaultValue();
            EditorUtility.SetDirty(interactionRay.data);
        }

       

        GUILayout.EndHorizontal();
        //m_EnableXRInitializeXROnStartUP = GUILayout.Toggle(m_EnableXRInitializeXROnStartUP, "����XR��ʼ��", GUILayout.Width(300), GUILayout.Width(40));
        //if (m_EnableXRInitializeXROnStartUP)
        //{

        //}
    }

   
}
