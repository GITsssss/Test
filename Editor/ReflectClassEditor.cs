using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEditorInternal;
using System.Xml.Linq;
using System.Reflection;
using NPOI.SS.Formula.Functions;

[CustomEditor(typeof(ReflectClass))]
public class ReflectClassEditor :Editor
{
    ReflectClass reflectClass;
    [SerializeField]
   
    SerializedProperty obj_sp;
    SerializedObject so;
    int[] index;
    int[] index2;
    private ReorderableList list;
    SerializedProperty elements;
    public bool Foldout;
    private void OnEnable()
    {
     
        Draw();
        this.index = new int[list.count];
        index2 = new int[list.count];
    }
    void Draw()
    {
        elements = serializedObject.FindProperty("reflectGroup");
        list = new ReorderableList(serializedObject, elements, true, true, true, true);
        list.headerHeight = 50;
        list.elementHeight = 10;


        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "");
        };

        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
           
            var element = list.serializedProperty.GetArrayElementAtIndex(index);

            rect.y += 5;
            this.index2[index] = EditorGUI.Popup(new Rect(rect.x, rect.y, 200, EditorGUIUtility.singleLineHeight),this.index2[index], reflectClass.className.ToArray());
            this.index[index] = EditorGUI.Popup(new Rect(rect.x + 205, rect.y, 300, EditorGUIUtility.singleLineHeight), this.index[index], reflectClass.menames.ToArray());
            //EditorGUI.PropertyField(new Rect(rect.x, rect.y +20, 200, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("gameobject"), GUIContent.none);
            //Debug.Log(reflectClass.methodInfos);
            //if (reflectClass.methodInfos[this.index].GetParameters().Length > 0)
            //{
            //    Debug.Log(reflectClass.methodInfos[this.index].GetParameters().Length);
            //    switch (reflectClass.methodInfos[this.index].GetParameters().Length)
            //    {
            //        case 1:
            //            object o = reflectClass.methodInfos[this.index].GetParameters().GetValue(0);

            //         if(o.ToString().Contains("Transform"))
            //            EditorGUI.ObjectField(new Rect(rect.x+ 205, rect.y + 20, 300, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Transform"),GUIContent.none);
            //            break;

            //        case 2:
            //            object o2 = reflectClass.methodInfos[this.index].GetParameters().GetValue(0);
            //            if (o2.ToString().Contains("Transform"))
            //                EditorGUI.ObjectField(new Rect(rect.x + 205, rect.y + 20, 300, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Transform"), GUIContent.none);
            //            object o21 = reflectClass.methodInfos[this.index].GetParameters().GetValue(1);
            //            //if (o21.ToString().Contains("Int32"))
            //            //    EditorGUI.IntField(new Rect(rect.x + 205, rect.y + 20, 300, EditorGUIUtility.singleLineHeight), reflectClass.reflectGroup[this.index2].intvalue);
            //            break;

            //        case 3:
            //            object o3 = reflectClass.methodInfos[this.index].GetParameters().GetValue(0);
            //            if (o3.ToString().Contains("Transform"))
            //                EditorGUI.ObjectField(new Rect(rect.x + 205, rect.y + 20, 300, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Transform"), GUIContent.none);
            //            if (o3.ToString().Contains("Int32"))
            //                EditorGUI.ObjectField(new Rect(rect.x + 205, rect.y + 40, 40, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("intvalue"), GUIContent.none);
            //            if (o3.ToString().Contains("Boolean"))
            //                EditorGUI.ObjectField(new Rect(rect.x + 245, rect.y + 40, 40, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("boolvalue"), GUIContent.none);
            //            break;
            //    }

            //}

        };

        list.elementHeightCallback = (int index) =>
        {
            //var element = list.serializedProperty.GetArrayElementAtIndex(index);
            //SerializedProperty property = element.FindPropertyRelative("elementType")
            //float height = EditorGUI.GetPropertyHeight(property);
            //int indexEmun = element.FindPropertyRelative("elementType").enumValueIndex;
           
            return  100;
        };

    }

      int id;
      int  id2;
    public override void OnInspectorGUI()
    {
        serializedObject.Update(); 
        obj_sp = serializedObject.FindProperty("obj");
        reflectClass = target as ReflectClass;
        base.OnInspectorGUI();
        if (GUILayout.Button("Get")) 
        {
            reflectClass.Get(reflectClass.className[2]);
        }
       
        if (GUILayout.Button("GetM"))
        {
            reflectClass.GetObjectClassName(reflectClass.obj);
        }
       
        EditorGUILayout.PropertyField(obj_sp);
        id = EditorGUILayout.Popup(id, reflectClass.menames.ToArray());
        id2 = EditorGUILayout.Popup(id2, reflectClass.className.ToArray());
        //  if(Foldout=EditorGUILayout.Foldout(Foldout,GUIContent.none) )
        // list.DoLayoutList(); 
        using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
        {
            using (new EditorGUILayout.HorizontalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("MultipleEvent");
            }
        }
        serializedObject.ApplyModifiedProperties();   
    }
}
