using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FixedOnHand))]
public class FixedOnHandEditor : Editor
{

    FixedOnHand fixedOnHand;
    public override void OnInspectorGUI()
    {
        fixedOnHand = target as FixedOnHand;
        base.OnInspectorGUI();
        GUILayout.BeginHorizontal("box");
        if (GUILayout.Button("��������"))
        {
            fixedOnHand.Save();
        }

        if (GUILayout.Button("��������"))
        {
            fixedOnHand.Set();
        }
        GUILayout.EndHorizontal();
    }
}
