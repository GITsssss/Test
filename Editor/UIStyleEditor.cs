
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
[CustomEditor(typeof(UIStyle))]
[CanEditMultipleObjects]
public class UIStyleEditor : Editor
{
    ImageStyle[] imageStyles;
    TMproStyle[] tmprostyle;
    UIStyle style;
    public override void OnInspectorGUI()
    {
       
        style = (UIStyle)target;
    //    base.OnInspectorGUI();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Update", GUILayout.Width(70)))
        {
            style.GetUIStyle();
        }

        EditorGUILayout.BeginHorizontal(new GUIStyle());
        GUILayout.Label("UI风格元素资产库",GUILayout.Width(100));
        style.uswh=  (UIStyleWareHouse)EditorGUILayout.ObjectField(style.uswh,typeof(UIStyleWareHouse));
        EditorGUILayout.EndHorizontal();

       
        GUILayout.EndHorizontal();
        if (style.GetComponentValue<Image>())
            DrawImageButton();
        else
            EditorGUILayout.HelpBox("当前游戏对象不具有Image组件!", MessageType.Warning);

        GUILayout.Space(10);
        EditorGUILayout.BeginHorizontal(new GUIStyle(), GUILayout.Width(100));
        GUILayout.Label("自定义文本",GUILayout.Width(70));
        style.cusTmpro = EditorGUILayout.Toggle(style.cusTmpro, GUILayout.Width(25));
        if (style.cusTmpro)
        {
            style.custmprougui = (TextMeshProUGUI)EditorGUILayout.ObjectField(style.custmprougui, typeof(TextMeshProUGUI), GUILayout.Width(120));
            if (style.custmprougui == null) EditorGUILayout.HelpBox("请添加TMPro组件!", MessageType.Warning);     
        }
        EditorGUILayout.EndHorizontal();
       
        if (style.uitext!=null)
            DrawTMProTextButton();   
        else if(!style.cusTmpro&& !style.GetComponentValue<TextMeshProUGUI>())
            EditorGUILayout.HelpBox("当前游戏对象不具有TextMeshProUGUI组件!", MessageType.Warning);
        serializedObject.Update();
        serializedObject.ApplyModifiedProperties();

    }


    void DrawImageButton()
    {

        //style.GetUIStyle();
        using (var v = new EditorGUILayout.VerticalScope(EditorStyles.helpBox)) 
        {
            SerializedProperty imageListProperty = serializedObject.FindProperty("imageStyle");
            imageStyles = style.imageStyle;
            int rows = 0;
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
                EditorGUILayout.BeginVertical(GUILayout.Width(25));
                for (int i = 0; i < rows; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    if (i == rows - 1 && remainder != 4)
                    {
                        for (int n = 0; n < remainder; n++)
                        {
                            SerializedProperty imageProperty = imageListProperty.GetArrayElementAtIndex(i * 4 + n);
                            Sprite sp = imageProperty.FindPropertyRelative("sprites").objectReferenceValue as Sprite;
                            Texture image = sp.texture;

                            EditorGUILayout.BeginVertical(new GUIStyle(), GUILayout.Width(100));
                            if (GUILayout.Button(new GUIContent(image), GUILayout.Width(80), GUILayout.Height(80)))
                            {
                                style.SetImageSprites(i * 4 + n);
                            }
                            //  SerializedProperty color = serializedObject.FindProperty("colors").GetArrayElementAtIndex(i * 4 + n);

                            imageStyles[i * 4 + n].uicolors = EditorGUILayout.ColorField(imageStyles[i * 4 + n].uicolors, GUILayout.Width(80), GUILayout.Height(20));
                            GUILayout.EndVertical();
                        }
                    }
                    else
                    {
                        for (int n = 0; n < 4; n++)
                        {

                            SerializedProperty imageProperty = imageListProperty.GetArrayElementAtIndex(i * 4 + n);
                            Sprite sp = imageProperty.FindPropertyRelative("sprites").objectReferenceValue as Sprite;
                            Texture image = sp.texture;
                            EditorGUILayout.BeginVertical(new GUIStyle(), GUILayout.Width(100));

                            if (GUILayout.Button(new GUIContent(image), GUILayout.Width(80), GUILayout.Height(80)))
                            {
                                style.SetImageSprites(i * 4 + n);
                            }

                            // SerializedProperty color = serializedObject.FindProperty("colors").GetArrayElementAtIndex(i * 4 + n);

                            imageStyles[i * 4 + n].uicolors = EditorGUILayout.ColorField(imageStyles[i * 4 + n].uicolors, GUILayout.Width(80), GUILayout.Height(20));

                            GUILayout.EndVertical();
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                }
                EditorGUILayout.EndVertical();
            }
            else
            {
                EditorGUILayout.BeginHorizontal(GUILayout.Width(25));
                for (int i = 0; i < rows; i++)
                {

                    SerializedProperty imageProperty = imageListProperty.GetArrayElementAtIndex(i);
                    Sprite sp = imageProperty.FindPropertyRelative("sprites").objectReferenceValue as Sprite;
                    Texture image = sp.texture;

                    EditorGUILayout.BeginVertical(new GUIStyle(), GUILayout.Width(100));

                    if (GUILayout.Button(new GUIContent(image), GUILayout.Width(80), GUILayout.Height(80)))
                    {
                        style.SetImageSprites(i);
                    }
                    //  SerializedProperty color = serializedObject.FindProperty("colors").GetArrayElementAtIndex(i );
                    imageStyles[i].uicolors = EditorGUILayout.ColorField(imageStyles[i].uicolors, GUILayout.Width(80), GUILayout.Height(20));
                    // EditorGUILayout.PropertyField(color, GUILayout.Width(80), GUILayout.Height(40));
                    GUILayout.EndVertical();
                }
                EditorGUILayout.EndHorizontal();
            }
        }
     
    }



    void DrawTMProTextButton()
    {

        using (var v = new EditorGUILayout.VerticalScope(EditorStyles.helpBox)) 
        {

            SerializedProperty imageListProperty = serializedObject.FindProperty("tmprostyle");
            tmprostyle = style.tmprostyle;
            int rows = 0;
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
                EditorGUILayout.BeginVertical(GUILayout.Width(25));
                for (int i = 0; i < rows; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    if (i == rows - 1 && remainder != 4)
                    {
                        for (int n = 0; n < remainder; n++)
                        {
                            SerializedProperty imageProperty = imageListProperty.GetArrayElementAtIndex(i * 4 + n);

                            EditorGUILayout.BeginVertical(new GUIStyle(), GUILayout.Width(100));
                            if (GUILayout.Button("UseElement", GUILayout.Width(80), GUILayout.Height(20)))
                            {
                                style.SetTmrpoText(i * 4 + n);
                            }
                            tmprostyle[i * 4 + n].content = EditorGUILayout.TextArea(tmprostyle[i * 4 + n].content, GUILayout.Width(80), GUILayout.Height(25));
                            tmprostyle[i * 4 + n].font = (TMP_FontAsset)EditorGUILayout.ObjectField(tmprostyle[i * 4 + n].font, typeof(TMP_FontAsset), GUILayout.Width(80));
                            tmprostyle[i * 4 + n].fontsize = EditorGUILayout.FloatField(tmprostyle[i * 4 + n].fontsize, GUILayout.Width(80));
                            tmprostyle[i * 4 + n].fontColor = EditorGUILayout.ColorField(tmprostyle[i * 4 + n].fontColor, GUILayout.Width(80), GUILayout.Height(20));
                            GUILayout.EndVertical();
                        }
                    }
                    else
                    {
                        for (int n = 0; n < 4; n++)
                        {

                            SerializedProperty imageProperty = imageListProperty.GetArrayElementAtIndex(i * 4 + n);
                            EditorGUILayout.BeginVertical(new GUIStyle(), GUILayout.Width(100));

                            if (GUILayout.Button("UseElement", GUILayout.Width(80), GUILayout.Height(20)))
                            {
                                style.SetTmrpoText(i * 4 + n);
                            }

                            // SerializedProperty color = serializedObject.FindProperty("colors").GetArrayElementAtIndex(i * 4 + n);
                            tmprostyle[i * 4 + n].content = EditorGUILayout.TextArea(tmprostyle[i * 4 + n].content, GUILayout.Width(80), GUILayout.Height(25));
                            tmprostyle[i * 4 + n].font = (TMP_FontAsset)EditorGUILayout.ObjectField(tmprostyle[i * 4 + n].font, typeof(TMP_FontAsset), GUILayout.Width(80));
                            tmprostyle[i * 4 + n].fontsize = EditorGUILayout.FloatField(tmprostyle[i * 4 + n].fontsize, GUILayout.Width(80));
                            tmprostyle[i * 4 + n].fontColor = EditorGUILayout.ColorField(tmprostyle[i * 4 + n].fontColor, GUILayout.Width(80), GUILayout.Height(20));


                            GUILayout.EndVertical();
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                }
                EditorGUILayout.EndVertical();
            }
            else
            {
                EditorGUILayout.BeginHorizontal(GUILayout.Width(25));

                for (int i = 0; i < rows; i++)
                {

                    SerializedProperty imageProperty = imageListProperty.GetArrayElementAtIndex(i);
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.Space(10);
                    EditorGUILayout.BeginVertical(new GUIStyle(), GUILayout.Width(100));
                    //GUILayout.BeginVertical();
                    if (GUILayout.Button("UseElement", GUILayout.Width(80), GUILayout.Height(20)))
                    {
                        style.SetTmrpoText(i);
                    }

                    tmprostyle[i].content = EditorGUILayout.TextArea(tmprostyle[i].content, GUILayout.Width(80), GUILayout.Height(25));
                    tmprostyle[i].font = (TMP_FontAsset)EditorGUILayout.ObjectField(tmprostyle[i].font, typeof(TMP_FontAsset), GUILayout.Width(80));
                    tmprostyle[i].fontsize = EditorGUILayout.FloatField(tmprostyle[i].fontsize, GUILayout.Width(80));
                    tmprostyle[i].fontColor = EditorGUILayout.ColorField(tmprostyle[i].fontColor, GUILayout.Width(80), GUILayout.Height(20));
                    GUILayout.EndVertical();
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndHorizontal();
            }
        }

    }

}
