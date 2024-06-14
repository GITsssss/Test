using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "PropData", menuName = "HLVR/PropData", order = 1)]
public class PropData : ScriptableObject
{
    public Vector3[]    OriginPosition;
    public Quaternion[] OriginRotation;
//# if UNITY_EDITOR_WIN
//    public void Save()
//    {
//        EditorUtility.SetDirty(this);
//        AssetDatabase.SaveAssets();
//        DebugInfo.DebugWarning(OutColor.Green,"±£´æÍê³É£¡");
//    }
//#endif
}
