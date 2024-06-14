using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CreateAssetMenu(fileName = "Handdata", menuName = "HLVR/HandData", order = 1)]
public class HandData : ScriptableObject
{
    public Vector3 leftLocalPosition;
    public Quaternion leftLocalRotation;
    public Vector3 rightLocalPosition;
    public Quaternion rightLocalRotation;
    public Quaternion[] lefthand;
    public Quaternion[] righthand;
//# if UNITY_EDITOR_WIN
//    public void Save()
//    {
//        EditorUtility.SetDirty(this);
//        AssetDatabase.SaveAssets();
//    }
//#endif
}
