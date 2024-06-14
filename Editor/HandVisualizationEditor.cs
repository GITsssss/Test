using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;


[CustomEditor(typeof(HandVisualization))]
public class HandVisualizationEditor :Editor
{
    public override void OnInspectorGUI()
    {
        HandVisualization handVisualization;
        base.OnInspectorGUI();
        handVisualization=target as HandVisualization;
        GUILayout.BeginHorizontal();
        string btpath = "Packages/com.hlvr/ButtonIcon";
        string[] btnames = AssetDatabase.FindAssets("", new[] { btpath });
        Texture bt1= AssetDatabase.LoadAssetAtPath<Texture>(AssetDatabase.GUIDToAssetPath(btnames[0]));
        Texture bt2 = AssetDatabase.LoadAssetAtPath<Texture>(AssetDatabase.GUIDToAssetPath(btnames[1]));
        Texture bt3 = AssetDatabase.LoadAssetAtPath<Texture>(AssetDatabase.GUIDToAssetPath(btnames[2]));
        Texture bt4 = AssetDatabase.LoadAssetAtPath<Texture>(AssetDatabase.GUIDToAssetPath(btnames[3]));
        Texture bt5 = AssetDatabase.LoadAssetAtPath<Texture>(AssetDatabase.GUIDToAssetPath(btnames[4]));
        Texture bt6 = AssetDatabase.LoadAssetAtPath<Texture>(AssetDatabase.GUIDToAssetPath(btnames[5]));
        if (GUILayout.Button(new GUIContent(bt1, "�༭��������"),GUILayout.Width(40),GUILayout.Height(40)))  
        {
            string path = "Packages/com.hlvr/Resources";
            string[] names = AssetDatabase.FindAssets("", new[] { path });
            string name="";
          
            for (int i=0;i<names.Length;i++)
            {
                string str = AssetDatabase.GUIDToAssetPath(names[i]);
                string s = str.Remove(0, str.LastIndexOf("/") + 1);
                //Debug.Log(s.Remove(s.IndexOf(".")));
                if (s.Remove(s.IndexOf("."))== "LeftHandVis")
                name= names[i]; 
               
            }
           handVisualization. handleft = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(name)));
           handVisualization. handleft.name = "LeftHandVis";
           handVisualization. handleft.transform.parent = handVisualization.transform;
           handVisualization. handleft.transform.localPosition = Vector3.zero;
           handVisualization. handleft.transform.localRotation = Quaternion.identity;
         
         
           
            GameObject go = GameObject.Find(handVisualization.handleft.name);
            EditorGUIUtility.PingObject(go);
            Selection.activeGameObject = go;

        }

        if (GUILayout.Button("������������", GUILayout.Width(80), GUILayout.Height(40)))
        {
            handVisualization.handData.lefthand = new Quaternion[handVisualization.bonesEdior.Length];
            handVisualization.bonesEdior = handVisualization.handleft.transform.GetChild(0).GetChild(5).GetComponentsInChildren<Transform>();
            for (int i = 0; i < handVisualization.bonesEdior.Length; i++)
            {
                handVisualization.handData.leftLocalPosition = handVisualization.handleft.transform.localPosition;
                handVisualization.handData.leftLocalRotation = handVisualization.handleft.transform.localRotation;
                handVisualization.handData.lefthand[i] = handVisualization.bonesEdior[i].localRotation;
            }

            EditorUtility.SetDirty(handVisualization.handData);
            AssetDatabase.SaveAssets();
        }
        GUILayout.Space(8);
        if (GUILayout.Button(new GUIContent(bt4, "���������ļ�"), GUILayout.Width(40), GUILayout.Height(40)))
        {
            CreateHandVisualization();
        }
        GUILayout.Space(8);
        if (GUILayout.Button(new GUIContent(bt2, "�༭����������"), GUILayout.Width(40), GUILayout.Height(40)))
        {
            string path = "Packages/com.hlvr/Resources";
            string[] names = AssetDatabase.FindAssets("", new[] { path });
            string name = "";
            for (int i = 0; i < names.Length; i++)
            {
                string str = AssetDatabase.GUIDToAssetPath(names[i]);
                string s = str.Remove(0, str.LastIndexOf("/") + 1);
                //Debug.Log(s.Remove(s.IndexOf(".")));
                if (s.Remove(s.IndexOf(".")) == "RightHandVis")
                    name = names[i];

            }
            handVisualization. handright = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(name)));
            handVisualization. handright.name = "RightHandVis";
            handVisualization. handright.transform.parent = handVisualization.transform;
            handVisualization. handright.transform.localPosition = Vector3.zero;
            handVisualization. handright.transform.localRotation = Quaternion.identity;

            GameObject go = GameObject.Find(handVisualization. handright.name);
            EditorGUIUtility.PingObject(go);
            Selection.activeGameObject = go;
        }
        if (GUILayout.Button("������������", GUILayout.Width(80), GUILayout.Height(40)))
        {
            handVisualization.handData.righthand = new Quaternion[handVisualization.bonesEdior.Length];
            handVisualization.bonesEdior = handVisualization.handright.transform.GetChild(0).GetChild(5).GetComponentsInChildren<Transform>();
            for (int i = 0; i < handVisualization.bonesEdior.Length; i++)
            {
                handVisualization.handData.rightLocalPosition = handVisualization.handright.transform.localPosition;
                handVisualization.handData.rightLocalRotation = handVisualization.handright.transform.localRotation;
                handVisualization.handData.righthand[i] = handVisualization.bonesEdior[i].localRotation;
                EditorUtility.SetDirty(handVisualization.handData);
                AssetDatabase.SaveAssets();
            }
        }

        GUILayout.EndHorizontal();

      
    }


    public  void CreateHandVisualization()
    {
        // ʵ������  
        ScriptableObject handdata = ScriptableObject.CreateInstance<HandData>();

        // ���ʵ���� ��Ϊ�գ�����
        if (!handdata)
        {
            Debug.LogWarning("��ǰHandData����Ϊ�գ�");
            return;
        }

        // �Զ�����Դ����·��
        string path = Application.dataPath + "HLVR/Configuration";

        // �����Ŀ�ܲ�������·��������һ��
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        //������ Bullet ת��Ϊ�ַ���
        //ƴ�ӱ����Զ�����Դ��.asset�� ·��
        path = string.Format("Assets/HLVR/Configuration/{0}.asset", (typeof(HandData).ToString()));

        // �����Զ�����Դ��ָ��·��
        AssetDatabase.CreateAsset(handdata, path);
    }
}
