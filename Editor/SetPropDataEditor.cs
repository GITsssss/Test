using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(SetPropData))]
public class SetPropDataEditor : Editor
{
    SetPropData setPropData;
    public override void OnInspectorGUI()
    {
        setPropData = target as SetPropData;
        base.OnInspectorGUI();
        GUILayout.BeginHorizontal("box");

        if (GUILayout.Button("设置旋转")) 
        {
            setPropData.Set();
        }

        if (GUILayout.Button("保存数据")) 
        {
            setPropData.Save();
            EditorUtility.SetDirty(setPropData.propData);
        }
  
        GUILayout.EndHorizontal();
    }

}
