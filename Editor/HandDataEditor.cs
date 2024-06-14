using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(HandData))]
public class HandDataEditor : Editor
{
    HandData handData;
    public override void OnInspectorGUI()
    {
        handData = target as HandData;
        base.OnInspectorGUI();
    }
}
