using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Linq;

[CustomEditor(typeof(EventPointManager),true)]
public class EPMEditor : Editor
{
    EventPointManager pointManager;
    SerializedProperty elements;
    bool foldout;
    private ReorderableList list;
    int count;
    bool openbase;
    List<bool> openlable=new List<bool>();
    Texture bt1;
    Texture bt2;
    Texture bt3;
    private void OnEnable()
    {
        Draw();
    }

   

    void Draw()
    {
      
        elements = serializedObject.FindProperty("eventPoint");
        

        list = new ReorderableList(serializedObject, elements, true, true, true, true);
        list.headerHeight = 50;
        list.elementHeight = 10;


        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "");
        };

        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {  
          // list.onCanAddCallback.;
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
           
            rect.y += 5;
           
            GUILayout.BeginHorizontal();
            EditorGUI.LabelField(new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight), "事件点" + (index + 1));

           
            EditorGUI.PropertyField(new Rect(rect.x, rect.y + 45, 100, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("elementType"), GUIContent.none);
            int indexEmun = element.FindPropertyRelative("elementType").enumValueIndex;
            switch (indexEmun)
            {
                case 0:


                    openlable[index] = EditorGUI.Toggle(new Rect(rect.x + 75, rect.y, 30, 30), openlable[index]);
                    
                    if (openlable[index])
                    {
                        EditorGUI.PropertyField(new Rect(rect.x + 95, rect.y, 340, 40), element.FindPropertyRelative("lable"), GUIContent.none);
                    }

                    EditorGUI.LabelField(new Rect(rect.x + 120, rect.y + 45, 60, EditorGUIUtility.singleLineHeight), "音频事件");
                    if (GUI.Button(new Rect(rect.x, rect.y + 20, 30, EditorGUIUtility.singleLineHeight), new GUIContent(bt1, "音频注入")))
                    {
                        // 点击自定义按钮的处理逻辑
                        pointManager.eventPoint[index].SetAudioEvent();
                    }

                    if (GUI.Button(new Rect(rect.x+35, rect.y + 20, 30, EditorGUIUtility.singleLineHeight), new GUIContent(bt2, "撤销注入")))
                    {
                        // 点击自定义按钮的处理逻辑
                        pointManager.eventPoint[index].ClearAudioEvent();
                    }
                    EditorGUI.PropertyField(new Rect(rect.x + 200, rect.y + 45, 200, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("audioEvent"), GUIContent.none);
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight + 60, 400, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("audioEventPort"), GUIContent.none, true);
                    break;
                case 1:

                    openlable[index] = EditorGUI.Toggle(new Rect(rect.x + 75, rect.y, 30, 30), openlable[index]);

                    if (openlable[index])
                    {
                        EditorGUI.PropertyField(new Rect(rect.x + 95, rect.y, 340, 40), element.FindPropertyRelative("lable"), GUIContent.none);
                    }

                    if (GUI.Button(new Rect(rect.x, rect.y + 20, 30, EditorGUIUtility.singleLineHeight), new GUIContent(bt1,"周期注入")))
                    {
                        // 点击自定义按钮的处理逻辑
                        pointManager.eventPoint[index].SetLifecyleEvent();
                    }
                    if (GUI.Button(new Rect(rect.x+35, rect.y + 20, 30, EditorGUIUtility.singleLineHeight), new GUIContent(bt2,"撤销注入")))
                    {
                        // 点击自定义按钮的处理逻辑
                        pointManager.eventPoint[index].ClearLifecyleEvent();   
                    }
                    EditorGUI.LabelField(new Rect(rect.x + 120, rect.y + 45, 60, EditorGUIUtility.singleLineHeight), "生命周期事件");
                    EditorGUI.PropertyField(new Rect(rect.x + 200, rect.y + 45, 200, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("lifecycleEvent"), GUIContent.none);
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight + 60, 400, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("lifecycleEventPort"), GUIContent.none, true);
                    break;
                case 2:

                    openlable[index] = EditorGUI.Toggle(new Rect(rect.x + 75, rect.y, 30, 30), openlable[index]);

                    if (openlable[index])
                    {
                        EditorGUI.PropertyField(new Rect(rect.x + 95, rect.y, 340, 40), element.FindPropertyRelative("lable"), GUIContent.none);
                    }

                    if (GUI.Button(new Rect(rect.x, rect.y + 20, 30, EditorGUIUtility.singleLineHeight),new GUIContent(bt1,"多控注入")))
                    {
                        // 点击自定义按钮的处理逻辑
                        pointManager.eventPoint[index].SetMultimasterEvent();
                    }
                    if (GUI.Button(new Rect(rect.x+35, rect.y + 20, 30, EditorGUIUtility.singleLineHeight), new GUIContent(bt2,"撤销注入")))
                    {
                        // 点击自定义按钮的处理逻辑
                        pointManager.eventPoint[index].ClearMultimasterEvent();
                    }
                    EditorGUI.LabelField(new Rect(rect.x + 120, rect.y + 45, 60, EditorGUIUtility.singleLineHeight), "多控事件");
                    EditorGUI.PropertyField(new Rect(rect.x + 200, rect.y + 45, 200, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("multimasterEvent"), GUIContent.none);
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight + 60, 400, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("m_TrueEvents"), GUIContent.none);
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
                    height = EditorGUI.GetPropertyHeight(element.FindPropertyRelative("m_TrueEvents"));

                    break;
            }
            return height + 80;
        };

    }


    public override void OnInspectorGUI()
    {
        pointManager = target as EventPointManager;
        openlable.Add(new bool());
        // EditorGUI.DrawRect(EditorGUILayout.BeginHorizontal(new GUIStyle(), GUILayout.Width(100)), new Color(0.5f, 0.5f, 0.5f, 0.5f));

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("显示源属性");
        openbase = EditorGUILayout.Toggle(openbase);
        GUILayout.EndHorizontal();
        if (openbase)
            base.OnInspectorGUI();

        string btpath = "Packages/com.hlvr/EditorButton";
        string[] btnames = AssetDatabase.FindAssets("", new[] { btpath });
        bt1 = AssetDatabase.LoadAssetAtPath<Texture>(AssetDatabase.GUIDToAssetPath(btnames[0]));
        bt2 = AssetDatabase.LoadAssetAtPath<Texture>(AssetDatabase.GUIDToAssetPath(btnames[1]));
        bt3 = AssetDatabase.LoadAssetAtPath<Texture>(AssetDatabase.GUIDToAssetPath(btnames[2]));
        if (GUILayout.Button("注入所有事件"))
        {
            for (int i = 0; i < pointManager.eventPoint.Count; i++)
            {
                pointManager.eventPoint[i].SetLifecyleEvent();
                pointManager.eventPoint[i].SetAudioEvent();
                pointManager.eventPoint[i].SetMultimasterEvent();
            }
        }
        GUILayout.BeginHorizontal();
        if (GUILayout.Button(new GUIContent(bt1, "注入所有生命周期事件"), GUILayout.Width(30), GUILayout.Height(30)))
        {
            for (int i = 0; i < pointManager.eventPoint.Count; i++)
            {
                pointManager.eventPoint[i].SetLifecyleEvent();
            }
        }
        if (GUILayout.Button("创建生命周期事件", GUILayout.Height(30))) 
        {

            pointManager.eventPoint.Add(new EventElement(EventPointElementType.LifecycleEvent));
            count = pointManager.eventPoint.Count;
            EventElement[] array = pointManager.eventPoint.ToArray();
            string n = pointManager.CreateLifiEvent(ref array[count - 1].lifecycleEvent);
            pointManager.eventPoint = array.ToList();
            GameObject go = GameObject.Find(n);
            EditorGUIUtility.PingObject(go);
            Selection.activeGameObject = go;
            
        
        }

        if (GUILayout.Button(new GUIContent(bt2, "撤销所有生命周期事件"), GUILayout.Width(30), GUILayout.Height(30)))
        {
            for (int i = 0; i < pointManager.eventPoint.Count; i++)
            {
                pointManager.eventPoint[i].ClearLifecyleEvent();
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button(new GUIContent(bt1, "注入所有音频事件"), GUILayout.Width(30), GUILayout.Height(30)))
        {

            for (int i = 0; i < pointManager.eventPoint.Count; i++)
            {
                pointManager.eventPoint[i].SetAudioEvent();
            }
        }
        if (GUILayout.Button("创建音频事件", GUILayout.Height(30)))
        {
            pointManager.eventPoint.Add(new EventElement(EventPointElementType.AudioEvent));
            count = pointManager.eventPoint.Count;
            EventElement[] array = pointManager.eventPoint.ToArray();  
            string n = pointManager.CreateAudioEvent(ref array[count - 1].audioEvent);
            pointManager.eventPoint = array.ToList();
           
            GameObject go = GameObject.Find(n);
            EditorGUIUtility.PingObject(go);
            Selection.activeGameObject = go;

        }
        if (GUILayout.Button(new GUIContent(bt2, "撤销所有音频事件"), GUILayout.Width(30), GUILayout.Height(30)))
        {

            for (int i = 0; i < pointManager.eventPoint.Count; i++)
            {
                pointManager.eventPoint[i].ClearAudioEvent();
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button(new GUIContent(bt1, "注入所有多控事件"), GUILayout.Width(30),GUILayout.Height(30)))
        {
           
            for(int i=0;i < pointManager.eventPoint.Count; i++)
            {
                pointManager.eventPoint[i].SetMultimasterEvent();
            }
        }
        if (GUILayout.Button("创建多控事件", GUILayout.Height(30)))
        {
            pointManager.eventPoint.Add(new EventElement(EventPointElementType.MultimasterEvent));
            count = pointManager.eventPoint.Count;
            EventElement[] array = pointManager.eventPoint.ToArray();
            string n = pointManager.CreateMultimasterEvent(ref array[count - 1].multimasterEvent);
            pointManager.eventPoint = array.ToList();
            GameObject go = GameObject.Find(n);
            EditorGUIUtility.PingObject(go);
            Selection.activeGameObject = go;
        }
        if (GUILayout.Button(new GUIContent(bt2, "撤销所有多控事件"), GUILayout.Width(30), GUILayout.Height(30)))
        {

            for (int i = 0; i < pointManager.eventPoint.Count; i++)
            {
                pointManager.eventPoint[i].ClearMultimasterEvent();
            }
        }

        GUILayout.EndHorizontal();

        if (GUILayout.Button("撤销所有事件"))
        {
            for (int i = 0; i < pointManager.eventPoint.Count; i++)
            {
                pointManager.eventPoint[i].ClearAudioEvent();
                pointManager.eventPoint[i].ClearLifecyleEvent();
                pointManager.eventPoint[i].ClearMultimasterEvent();
            }
        }

        serializedObject.Update();
        GUILayout.BeginHorizontal();
        foldout= EditorGUILayout.Foldout(foldout,"事件管理中心",true);
        int listcount;
        listcount = EditorGUILayout.IntField(pointManager.eventPoint.Count, GUILayout.Width(40));
        GUILayout.EndHorizontal();

        if (foldout)
            list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}

