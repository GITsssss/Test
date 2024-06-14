using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[CustomEditor(typeof(UIStyle))]
public class UIStyleEditor : Editor
{
    Color[] color1;

    public override void OnInspectorGUI()
    {
        UIStyle style = (UIStyle)target;
        base.OnInspectorGUI();
        if (GUILayout.Button("Update",GUILayout.Width(70)))
        {
            style.GetUIStyle();
         
        }
        //style.GetUIStyle();
        SerializedProperty imageListProperty = serializedObject.FindProperty("sprites");
        color1 = style.colors ;
        int rows=0;
        if (imageListProperty.arraySize < 4)
            rows = imageListProperty.arraySize;
        else if (imageListProperty.arraySize == 4)
            rows = 4;
        else if (imageListProperty.arraySize > 4) 
        {
            if (imageListProperty.arraySize % 4 != 0)
                rows = (imageListProperty.arraySize / 4) + 1;
            else rows = (imageListProperty.arraySize / 4);
        }
         

        int remainder = imageListProperty.arraySize % 4;
        if (remainder == 0) remainder = 4;



        if (imageListProperty.arraySize > 4)
        {
            EditorGUILayout.BeginVertical();
            for (int i = 0; i < rows; i++)
            {
                EditorGUILayout.BeginHorizontal();
                if (i == rows - 1&& remainder!=4)
                {
                    for (int n = 0; n < remainder; n++)
                    {
                        SerializedProperty imageProperty = imageListProperty.GetArrayElementAtIndex(i * 4 + n);
                        Sprite sp = imageProperty.objectReferenceValue as Sprite;
                        Texture image = sp.texture;

                        GUILayout.BeginVertical();
                        if (GUILayout.Button(new GUIContent(image), GUILayout.Width(80), GUILayout.Height(80)))
                        {
                            style.SetImageSprites(i * 4 + n);
                        }
                        //  SerializedProperty color = serializedObject.FindProperty("colors").GetArrayElementAtIndex(i * 4 + n);

                        color1[i * 4 + n] = EditorGUILayout.ColorField(color1[i*4+n], GUILayout.Width(80), GUILayout.Height(20));
                        GUILayout.EndVertical();
                    }
                }
                else 
                {
                    for (int n = 0; n < 4; n++)
                    {
                       
                        SerializedProperty imageProperty = imageListProperty.GetArrayElementAtIndex(i * 4 + n);
                        Sprite sp = imageProperty.objectReferenceValue as Sprite;



                        Texture image = sp.texture;
                        GUILayout.BeginVertical();
                       
                        if (GUILayout.Button(new GUIContent(image), GUILayout.Width(80), GUILayout.Height(80)))
                        {
                            style.SetImageSprites(i * 4 + n);
                        }

                        // SerializedProperty color = serializedObject.FindProperty("colors").GetArrayElementAtIndex(i * 4 + n);

                        color1[i * 4 + n] = EditorGUILayout.ColorField(color1[i * 4 + n], GUILayout.Width(80), GUILayout.Height(20));

                        GUILayout.EndVertical();
                    }
                }
                EditorGUILayout.EndHorizontal();

            }
            EditorGUILayout.EndVertical();
        }
        else 
        {
            EditorGUILayout.BeginHorizontal();
            for (int i = 0; i < rows; i++)
            {
                
                SerializedProperty imageProperty = imageListProperty.GetArrayElementAtIndex(i);
                Sprite sp = imageProperty.objectReferenceValue as Sprite;
                Texture image = sp.texture;

                GUILayout.BeginVertical();

                if (GUILayout.Button(new GUIContent(image), GUILayout.Width(80), GUILayout.Height(80)))
                {
                    style.SetImageSprites(i);
                }
                //  SerializedProperty color = serializedObject.FindProperty("colors").GetArrayElementAtIndex(i );
                color1[i]= EditorGUILayout.ColorField(color1[i],GUILayout.Width(80),GUILayout.Height(20));
               // EditorGUILayout.PropertyField(color, GUILayout.Width(80), GUILayout.Height(40));
                GUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
        }
       


      

    

       
     
        serializedObject.Update();
        serializedObject.ApplyModifiedProperties();

    }
}
