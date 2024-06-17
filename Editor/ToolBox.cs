using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using System.IO;
using System;
using UnityEngine.UIElements;

public class ToolBox : EditorWindow
{
    Vector2 scrollPosition;
    static ToolBox toolBox;
    [MenuItem("HLVR/Tool/ToolKit")]
    public static void OpenWindow()
    {   
        toolBox= EditorWindow.GetWindow<ToolBox>("VR资产工具箱",false);
        toolBox.minSize = new Vector2(400,200);
        toolBox.maxSize = new Vector2(1920,1080);
    }
    private void OnGUI()
    {
        scrollPosition= GUILayout.BeginScrollView(scrollPosition);
        string path= "Packages/com.hlvr/Prefabs";
        string[] guids = AssetDatabase.FindAssets("", new[] { path });
        Texture2D[] thumbnail=new Texture2D[guids.Length];
        GUILayout.Label("VR交互模型");
       
        using (var horizontalScope = new GUILayout.VerticalScope(EditorStyles.helpBox))
        {
            GUILayout.BeginHorizontal(GUILayout.Width(200), GUILayout.Height(100));
            for (int i = 0; i < guids.Length; i++)
            {
                string str = AssetDatabase.GUIDToAssetPath(guids[i]);
                thumbnail[i] = AssetPreview.GetAssetPreview(AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guids[i])));

                GUILayout.BeginVertical(GUILayout.Height(100));
                string s = str.Remove(0, str.LastIndexOf("/") + 1);
                GameObject pre = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guids[i]));
                string names = s;
                s = s.Replace(".prefab", "");
                GUIStyle style = new GUIStyle();
                style.normal.textColor = Color.white;
                style.fontStyle = FontStyle.Bold;
                style.fixedWidth = 100;
                style.fontSize = 15;
                style.alignment = TextAnchor.MiddleCenter;
                style.clipping = TextClipping.Clip;
                if (GUILayout.Button(thumbnail[i], GUILayout.Width(100), GUILayout.Height(100)))
                {
                    if (s == names.Replace(".prefab", ""))
                        Instantiate(pre);
                }

                GUILayout.Label(s, style, GUILayout.Width(100));
                GUILayout.EndVertical();

                GUILayout.Space(12);
            }
            GUILayout.EndHorizontal();
        }
       
       
    
      
        GUILayout.EndScrollView();
    }

    

  
}
