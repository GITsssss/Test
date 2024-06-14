
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using HLVR.Configuration;
public class CreateAsset : Editor
{

    // �ڲ˵�������������
    [MenuItem("HLVR/CreateAsset/GestureVRHand")]
   
    public static void Create()
    {
        // ʵ������  Bullet
        ScriptableObject handdata = ScriptableObject.CreateInstance<HandData>();

        // ���ʵ���� Bullet ��Ϊ�գ�����
        if (!handdata)
        {
            Debug.LogWarning("GestureVRHand not found");
            return;
        }

        // �Զ�����Դ����·��
        string path = "Assets/HLVR1.0/Configuration";

        // �����Ŀ�ܲ�������·��������һ��
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        //������ Bullet ת��Ϊ�ַ���
        //ƴ�ӱ����Զ�����Դ��.asset�� ·��
        path = string.Format("Assets/HLVR1.0/Configuration/{0}.asset", (typeof(HandData).ToString()));

        // �����Զ�����Դ��ָ��·��
        AssetDatabase.CreateAsset(handdata, path);
    }

    [MenuItem("HLVR/CreateAsset/InteractionRayData")]

    public static void Create2()
    {
        // ʵ������  Bullet
        ScriptableObject prop = ScriptableObject.CreateInstance<PropData>();

        // ���ʵ���� Bullet ��Ϊ�գ�����
        if (!prop)
        {
            Debug.LogWarning("PropData not found");
            return;
        }

        // �Զ�����Դ����·��
        string path = "Assets/HLVR1.0/Configuration";

        // �����Ŀ�ܲ�������·��������һ��
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        //������ Bullet ת��Ϊ�ַ���
        //ƴ�ӱ����Զ�����Դ��.asset�� ·��
        path = string.Format("Assets/HLVR1.0/Configuration/{0}.asset", (typeof(PropData).ToString()));

        // �����Զ�����Դ��ָ��·��
        AssetDatabase.CreateAsset(prop, path);
    }

    [MenuItem("HLVR/CreateAsset/PropData")]

    public static void Create3()
    {
        // ʵ������  Bullet
        ScriptableObject prop = ScriptableObject.CreateInstance<PropData>();

        // ���ʵ���� Bullet ��Ϊ�գ�����
        if (!prop)
        {
            Debug.LogWarning("PropData not found");
            return;
        }

        // �Զ�����Դ����·��
        string path = "Assets/HLVR1.0/Configuration";

        // �����Ŀ�ܲ�������·��������һ��
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        //������ Bullet ת��Ϊ�ַ���
        //ƴ�ӱ����Զ�����Դ��.asset�� ·��
        path = string.Format("Assets/HLVR1.0/Configuration/{0}.asset", (typeof(PropData).ToString()));

        // �����Զ�����Դ��ָ��·��
        AssetDatabase.CreateAsset(prop, path);
    }

    // �ڲ˵�������������
    [MenuItem("HLVR/CreateAsset/HandVisualization")]

    public static void CreateHandVisualization()
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
        string path = "Assets/HLVR/Configuration";

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


    [MenuItem("HLVR/CreateAsset/UIStyleWareHouse")]

    public static void CreateUIStyleWareHouse()
    {
        // ʵ������  
        ScriptableObject handdata = ScriptableObject.CreateInstance<UIStyleWareHouse>();

        // ���ʵ���� ��Ϊ�գ�����
        if (!handdata)
        {
            Debug.LogWarning("��ǰUIStyleWareHouse����Ϊ�գ�");
            return;
        }

        // �Զ�����Դ����·��
        string path = "Assets/HLVR/Configuration";

        // �����Ŀ�ܲ�������·��������һ��
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        //������ Bullet ת��Ϊ�ַ���
        //ƴ�ӱ����Զ�����Դ��.asset�� ·��
        path = string.Format("Assets/HLVR/Configuration/{0}.asset", (typeof(UIStyleWareHouse).ToString()));

        // �����Զ�����Դ��ָ��·��
        AssetDatabase.CreateAsset(handdata, path);
    }

  
}