using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class MenuEdior : Editor
{
    [MenuItem("HLVR/Record/SmoothCamera")]
    public static void SmoothCamera()
    {
        string path = "Packages/com.hlvr/Prefabs";
        string[] guids = AssetDatabase.FindAssets("", new[] { path });
        for (int i = 0; i < guids.Length; i++) 
        {
            string str = AssetDatabase.GUIDToAssetPath(guids[i]);
            string s = str.Remove(0, str.LastIndexOf("/") + 1);
            GameObject pre = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guids[i]));
            string names = s;
            s = s.Replace(".prefab", "");
            GameObject g=null;
            if (s == "SmothCamera")
            g= Instantiate(pre);

            if (g != null)
            {
                EditorGUIUtility.PingObject(g);
                Selection.activeGameObject = g;
            }
          
        }
    }
}
