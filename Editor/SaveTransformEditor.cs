using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(SaveTransform))]
public class SaveTransformEditor : Editor
{
    SaveTransform saveTransform;
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        saveTransform = target as SaveTransform;
        if (GUILayout.Button("保存数据")) 
        {
            saveTransform.SaveDate();
        }
        if (GUILayout.Button("初始数据")) {
            if (!saveTransform.useotherdate)
                saveTransform.SetDate();
            else
                saveTransform.SetThisOfOtherDate();
        }
    }
}
