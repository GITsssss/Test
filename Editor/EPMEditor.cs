using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Events;
using UnityEngine.Events;
using UnityEditorInternal;
using UnityEngine.TextCore.Text;
using HLVR.EventSystems;
using System;

[CustomEditor(typeof(EventPointManager),true)]
public class EPMEditor : Editor
{
    EventPointManager pointManager;
    SerializedProperty multimasterEventPort;
    SerializedProperty lifecycleEventPort;
    SerializedProperty audioEventPort;
    SerializedProperty elements;
    bool foldout;
    private ReorderableList list;
    int count;
    private void OnEnable()
    {
       
        multimasterEventPort = serializedObject.FindProperty("multimasterEventPort");
        lifecycleEventPort = serializedObject.FindProperty("lifecycleEventPort");
        audioEventPort = serializedObject.FindProperty("audioEventPort");
        elements = serializedObject.FindProperty("eventPoint");

        list = new ReorderableList(serializedObject, elements, true, true, true, true);
        list.headerHeight = 50;
        list.elementHeight = 500;


        list.drawHeaderCallback = (Rect rect) =>
        {
          
        };

        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
          

            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 5;
            GUILayout.BeginHorizontal();
            EditorGUI.LabelField(new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight), "事件点"+ (index+1));
            EditorGUI.PropertyField(new Rect(rect.x+60, rect.y, 340, 40), element.FindPropertyRelative("lable"), GUIContent.none);
            EditorGUI.PropertyField(new Rect(rect.x , rect.y+45, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("elementType"), GUIContent.none);
            int indexEmun = element.FindPropertyRelative("elementType").enumValueIndex;
            switch (indexEmun)
            {
                case 0:
                    EditorGUI.LabelField(new Rect(rect.x+120, rect.y+45, 60, EditorGUIUtility.singleLineHeight),"音频事件");
                    EditorGUI.PropertyField(new Rect(rect.x + 200, rect.y+45, 200, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("audioEvent"), GUIContent.none);
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight + 60, 400, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("audioEventPort"), GUIContent.none,true);
                    break;
                case 1:
                    EditorGUI.LabelField(new Rect(rect.x + 120, rect.y + 45, 60, EditorGUIUtility.singleLineHeight), "生命周期事件");
                    EditorGUI.PropertyField(new Rect(rect.x + 200, rect.y + 45, 200, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("lifecycleEvent"), GUIContent.none);
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight + 60, 400, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("lifecycleEventPort"), GUIContent.none,true);
                    break;
                case 2:
                    EditorGUI.LabelField(new Rect(rect.x + 120, rect.y + 45, 60, EditorGUIUtility.singleLineHeight), "多控事件");
                    EditorGUI.PropertyField(new Rect(rect.x + 200, rect.y + 45, 200, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("multimasterEvent"), GUIContent.none);
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight + 60, 400, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("m_TrueEvent"), GUIContent.none);
                    break;

            }
            GUILayout.EndHorizontal();
        };

        list.elementHeightCallback = (int index) =>
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            SerializedProperty property = element.FindPropertyRelative("elementType");
            float height = EditorGUI.GetPropertyHeight(property);
            int indexEmun = element.FindPropertyRelative("elementType").enumValueIndex;
            switch (indexEmun) 
            {
                case 0:
                    height = EditorGUI.GetPropertyHeight(element.FindPropertyRelative("audioEventPort"));
                  
                    break;
                case 1:
                    height = EditorGUI.GetPropertyHeight(element.FindPropertyRelative("lifecycleEventPort"));
                    break;
                case 2:
                    height = EditorGUI.GetPropertyHeight(element.FindPropertyRelative("m_TrueEvent"));
                    break;
            }
            return height + 80;
        };

        
    }


    public override void OnInspectorGUI()
    {
        pointManager = target as EventPointManager;
        base.OnInspectorGUI();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("创建生命周期事件")) 
        {
            string n = pointManager.CreateLifiEvent();
            GameObject go = GameObject.Find(n);
            EditorGUIUtility.PingObject(go);
            Selection.activeGameObject = go;
        }

        if (GUILayout.Button("创建音频事件"))
        {
            string n= pointManager.CreateAudioEvent();
            GameObject go = GameObject.Find(n);
            EditorGUIUtility.PingObject(go);
            Selection.activeGameObject = go;

        }

        if (GUILayout.Button("创建多控事件"))
        {
            string n = pointManager.CreateMultimasterEvent();
            GameObject go = GameObject.Find(n);
            EditorGUIUtility.PingObject(go);
            Selection.activeGameObject = go;
        }
      
    
        GUILayout.EndHorizontal();
        serializedObject.Update();
        GUILayout.BeginHorizontal();
        foldout= EditorGUILayout.Foldout(foldout,"事件管理中心");
        count= EditorGUILayout.IntField(count,GUILayout.Width(40));
        GUILayout.EndHorizontal();

        if(foldout)
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();


    }

   
}


//[CustomPropertyDrawer(typeof(EventElement))]
//public class EventElementDrawer : PropertyDrawer 
//{
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        position.height = 500;
//        var lable = new Rect(position.x, position.y, 222, position.height);
//        var elementType = new Rect(position.x + 222, position.y, 222, position.height);
//        float height = EditorGUI.GetPropertyHeight(property.FindPropertyRelative("audioEventPort"));
       
//        EditorGUIUtility.labelWidth = 100;
//        EditorGUI.PropertyField(lable, property.FindPropertyRelative("lable"));
//        EditorGUI.PropertyField(elementType, property.FindPropertyRelative("elementType"));
       
//        var audioEventPort = new Rect(position.x + 480, position.y, 222, height);
//        EditorGUI.PropertyField(audioEventPort, property.FindPropertyRelative("audioEventPort"),true);
   
//    }
//}


