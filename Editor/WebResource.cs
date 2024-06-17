using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class WebResource : EditorWindow
{

    [MenuItem("HLVR/WebResource/游戏模型资源网站WebsiteResources")]
    public static void GameMode()
    {
        Application.OpenURL("https://www.textures.com/browse/3d-objects/114553");
    }


    [MenuItem("HLVR/WebResource/Unity商场")]
    public static void UnityStore()
    {
        Application.OpenURL("https://assetstore.unity.com/zh");
    }


    [MenuItem("HLVR/WebResource/阿里云图标")]
    public static void ALY()
    {
        Application.OpenURL("https://www.iconfont.cn/");
    }

    [MenuItem("HLVR/WebResource/觅元素")]
    public static void MYS()
    {
        Application.OpenURL("https://m.51yuansu.com/");
    }

    [MenuItem("HLVR/WebResource/全景图")]
    public static void CubeMap()
    {
        Application.OpenURL("https://hdri-haven.com/category/outdoor?page=11");
    }
    [MenuItem("HLVR/WebResource/材质球")]
    public static void Material()
    {
        Application.OpenURL("https://ambientcg.com/view?id=Tiles074");
    }

    [MenuItem("HLVR/WebResource/爱给网")]
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
