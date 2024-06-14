
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using HLVR.Configuration;
public class CreateAsset : Editor
{

    // 在菜单栏创建功能项
    [MenuItem("HLVR/CreateAsset/GestureVRHand")]
   
    public static void Create()
    {
        // 实例化类  Bullet
        ScriptableObject handdata = ScriptableObject.CreateInstance<HandData>();

        // 如果实例化 Bullet 类为空，返回
        if (!handdata)
        {
            Debug.LogWarning("GestureVRHand not found");
            return;
        }

        // 自定义资源保存路径
        string path = "Assets/HLVR1.0/Configuration";

        // 如果项目总不包含该路径，创建一个
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        //将类名 Bullet 转换为字符串
        //拼接保存自定义资源（.asset） 路径
        path = string.Format("Assets/HLVR1.0/Configuration/{0}.asset", (typeof(HandData).ToString()));

        // 生成自定义资源到指定路径
        AssetDatabase.CreateAsset(handdata, path);
    }

    [MenuItem("HLVR/CreateAsset/InteractionRayData")]

    public static void Create2()
    {
        // 实例化类  Bullet
        ScriptableObject prop = ScriptableObject.CreateInstance<PropData>();

        // 如果实例化 Bullet 类为空，返回
        if (!prop)
        {
            Debug.LogWarning("PropData not found");
            return;
        }

        // 自定义资源保存路径
        string path = "Assets/HLVR1.0/Configuration";

        // 如果项目总不包含该路径，创建一个
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        //将类名 Bullet 转换为字符串
        //拼接保存自定义资源（.asset） 路径
        path = string.Format("Assets/HLVR1.0/Configuration/{0}.asset", (typeof(PropData).ToString()));

        // 生成自定义资源到指定路径
        AssetDatabase.CreateAsset(prop, path);
    }

    [MenuItem("HLVR/CreateAsset/PropData")]

    public static void Create3()
    {
        // 实例化类  Bullet
        ScriptableObject prop = ScriptableObject.CreateInstance<PropData>();

        // 如果实例化 Bullet 类为空，返回
        if (!prop)
        {
            Debug.LogWarning("PropData not found");
            return;
        }

        // 自定义资源保存路径
        string path = "Assets/HLVR1.0/Configuration";

        // 如果项目总不包含该路径，创建一个
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        //将类名 Bullet 转换为字符串
        //拼接保存自定义资源（.asset） 路径
        path = string.Format("Assets/HLVR1.0/Configuration/{0}.asset", (typeof(PropData).ToString()));

        // 生成自定义资源到指定路径
        AssetDatabase.CreateAsset(prop, path);
    }

    // 在菜单栏创建功能项
    [MenuItem("HLVR/CreateAsset/HandVisualization")]

    public static void CreateHandVisualization()
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
        string path = "Assets/HLVR/Configuration";

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


    [MenuItem("HLVR/CreateAsset/UIStyleWareHouse")]

    public static void CreateUIStyleWareHouse()
    {
        // 实例化类  
        ScriptableObject handdata = ScriptableObject.CreateInstance<UIStyleWareHouse>();

        // 如果实例化 类为空，返回
        if (!handdata)
        {
            Debug.LogWarning("当前UIStyleWareHouse类型为空！");
            return;
        }

        // 自定义资源保存路径
        string path = "Assets/HLVR/Configuration";

        // 如果项目总不包含该路径，创建一个
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        //将类名 Bullet 转换为字符串
        //拼接保存自定义资源（.asset） 路径
        path = string.Format("Assets/HLVR/Configuration/{0}.asset", (typeof(UIStyleWareHouse).ToString()));

        // 生成自定义资源到指定路径
        AssetDatabase.CreateAsset(handdata, path);
    }

  
}