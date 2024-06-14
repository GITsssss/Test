using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager.UI;

public class GestureEditorWindows : EditorWindow
{
    float thumb;
    float indexfinger;
    float middlefinger;
    float ringfinger;
    float littlefinger;
    [MenuItem("HLVR/Tool/GestureEditorWindow")]
    public static void Win()
    { 
        EditorWindow.GetWindow<GestureEditorWindows>();
    }

    private void OnGUI()
    {
        string btpath = "Packages/com.hlvr/ButtonIcon";
        string[] btnames = AssetDatabase.FindAssets("", new[] { btpath });
        Texture bt1 = AssetDatabase.LoadAssetAtPath<Texture>(AssetDatabase.GUIDToAssetPath(btnames[0]));
        Texture bt2 = AssetDatabase.LoadAssetAtPath<Texture>(AssetDatabase.GUIDToAssetPath(btnames[1]));
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical(GUILayout.Width(50));
        thumb = GUILayout.VerticalSlider(thumb, 0,1, GUILayout.Width(20),GUILayout.Height(300));
        GUILayout.Label("thumb");
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(GUILayout.Width(50));
        indexfinger = GUILayout.VerticalSlider(indexfinger, 0, 1, GUILayout.Width(20),GUILayout.Height(300));
        GUILayout.Label("index finger");
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(GUILayout.Width(50));
        middlefinger = GUILayout.VerticalSlider(middlefinger, 0, 1, GUILayout.Width(20),GUILayout.Height(300));
       GUILayout.Label("middle finger");
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(GUILayout.Width(50));
        ringfinger = GUILayout.VerticalSlider(ringfinger, 0, 1,  GUILayout.Width(20),GUILayout.Height(300));
        GUILayout.Label("the ring finger");
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(GUILayout.Width(50));
        littlefinger = GUILayout.VerticalSlider(littlefinger, 0, 1, GUILayout.Width(40), GUILayout.Height(300));
        GUILayout.Label("little finger");
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndHorizontal();
    }
}
