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
        if (GUILayout.Button(new GUIContent(bt1, "编辑左手手势"),GUILayout.Width(40),GUILayout.Height(40)))  
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

        if (GUILayout.Button("保存左手手势", GUILayout.Width(80), GUILayout.Height(40)))
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
        if (GUILayout.Button(new GUIContent(bt4, "创建配置文件"), GUILayout.Width(40), GUILayout.Height(40)))
        {
            CreateHandVisualization();
        }
        GUILayout.Space(8);
        if (GUILayout.Button(new GUIContent(bt2, "编辑右手手手势"), GUILayout.Width(40), GUILayout.Height(40)))
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
        if (GUILayout.Button("保存右手手势", GUILayout.Width(80), GUILayout.Height(40)))
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
        // 实例化类  
        ScriptableObject handdata = ScriptableObject.CreateInstance<HandData>();

        // 如果实例化 类为空，返回
        if (!handdata)
        {
            Debug.LogWarning("当前HandData类型为空！");
            return;
        }

        // 自定义资源保存路径
        string path = Application.dataPath + "HLVR/Configuration";

        // 如果项目总不包含该路径，创建一个
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        //将类名 Bullet 转换为字符串
        //拼接保存自定义资源（.asset） 路径
        path = string.Format("Assets/HLVR/Configuration/{0}.asset", (typeof(HandData).ToString()));

        // 生成自定义资源到指定路径
        AssetDatabase.CreateAsset(handdata, path);
    }
}
