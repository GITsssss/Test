using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using System.Runtime.ConstrainedExecution;


public class TmproSetWindow : EditorWindow
{

    public float font;
    public  TMP_FontAsset fontAsset;
    public GameObject parent;
    public Color fontcolor;
    SerializedProperty fontcolor_property;

    SerializedProperty parent_obj;
    SerializedProperty fontAsset_property;
    SerializedProperty fontsize;
    SerializedObject cur;

    public TextMeshProUGUI[] proUGUIs ;
    SerializedProperty proUGUIs_property;
    private void OnEnable()
    {
        cur = new SerializedObject(this);
        fontAsset_property=cur.FindProperty("fontAsset");
        fontsize = cur.FindProperty("font");
        parent_obj = cur.FindProperty("parent");
        proUGUIs_property = cur.FindProperty("proUGUIs");
        fontcolor_property = cur.FindProperty("fontcolor");
    }
    private void OnGUI()
    {
        cur.Update();
        EditorGUILayout.PropertyField(parent_obj);
        EditorGUILayout.PropertyField(fontAsset_property);
        EditorGUILayout.PropertyField(fontsize);
        EditorGUILayout.PropertyField(fontcolor_property);
        EditorGUILayout.PropertyField(proUGUIs_property);
       
        GUILayout.BeginHorizontal();    
        if (GUILayout.Button("获取TmPro"))
        {

            proUGUIs= parent.GetComponentsInChildren<TextMeshProUGUI>();
            Debug.Log(parent.GetComponentsInChildren<TextMeshProUGUI>().Length);
        }

        if (GUILayout.Button("设置字体"))
        {
            foreach (TextMeshProUGUI t in proUGUIs)
            {
                t.font = fontAsset;
            }
        }

        if (GUILayout.Button("设置颜色"))
        {
            foreach (TextMeshProUGUI t in proUGUIs)
            {
                t.color=fontcolor;
            }
        }

        if (GUILayout.Button("设置大小"))
        {
            foreach (TextMeshProUGUI t in proUGUIs)
            {
                t.fontSize = font;
            }
        }

        GUILayout.EndHorizontal();
        cur.ApplyModifiedProperties();
    }



    [MenuItem("HLVR/Tool/UI/TmproSet")]
    public static void Window()
    {
       TmproSetWindow window = EditorWindow.GetWindow<TmproSetWindow>(); 
    }
}
