using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class WebResource : EditorWindow
{

    [MenuItem("HLVR/WebResource/��Ϸģ����Դ��վWebsiteResources")]
    public static void GameMode()
    {
        Application.OpenURL("https://www.textures.com/browse/3d-objects/114553");
    }


    [MenuItem("HLVR/WebResource/Unity�̳�")]
    public static void UnityStore()
    {
        Application.OpenURL("https://assetstore.unity.com/zh");
    }


    [MenuItem("HLVR/WebResource/������ͼ��")]
    public static void ALY()
    {
        Application.OpenURL("https://www.iconfont.cn/");
    }

    [MenuItem("HLVR/WebResource/��Ԫ��")]
    public static void MYS()
    {
        Application.OpenURL("https://m.51yuansu.com/");
    }

    [MenuItem("HLVR/WebResource/ȫ��ͼ")]
    public static void CubeMap()
    {
        Application.OpenURL("https://hdri-haven.com/category/outdoor?page=11");
    }
    [MenuItem("HLVR/WebResource/������")]
    public static void Material()
    {
        Application.OpenURL("https://ambientcg.com/view?id=Tiles074");
    }

    [MenuItem("HLVR/WebResource/������")]
    public static void AiGei()
    {
        Application.OpenURL("https://www.aigei.com/");
    }

    [MenuItem("HLVR/WebResource/Free3D")]
    public static void Free3D()
    {
        Application.OpenURL("https://free3d.com/3d-models/fbx");
    }
}
