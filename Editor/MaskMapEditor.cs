using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class MaskMapEditor : EditorWindow//��չ�༭��Ҫ�̳�EditorWindow
{
    private Texture2D metallic, occlusion, detailmask, smoothness;//�ĸ������ֶ�
    private Texture2D _metallic, _occlusion, _detailmask, _smoothness;//���ĸ��ֶ�����Ԥ����ʵ��
    private Material mat;//����Shader�Ĳ���
    private bool changed;

    [MenuItem("HLVR/Tool/HDRP/CreateMaskMap")]
    static void OpenMaskMapEditor()//ʵ�ֵ���˵����İ�ť��������
    {
        MaskMapEditor maskMapEditor = GetWindow<MaskMapEditor>(false, "MaskMap Editor");//��������
        maskMapEditor.titleContent.text = "MaskMap Editor";//����������
        maskMapEditor.Init();//��ʼ��
    }

    private void Init()
    {
        if (mat == null)
        {
            mat = new Material(Shader.Find("Hidden/MaskMap"));//����MaskMap���Shader��������
            mat.hideFlags = HideFlags.HideAndDontSave;//���ʲ�����
        }
    }

    public void OnGUI()
    {
        GetInput();
        Preview();
        CreateMask();
    }

    private void CreateMask()
    {
        if (GUILayout.Button("Create Mask Map"))
        {
            GenPicture();

        }
    }

    private void GenPicture()
    {
        Texture2D output = GetTexture();

        string savePath = EditorUtility.SaveFilePanel("Save", Application.dataPath, "texture.png", "png");
        File.WriteAllBytes(savePath, output.EncodeToPNG());//��������Ϊpng��ʽ��Ҳ������jpg��exr�ȸ�ʽ
        AssetDatabase.Refresh();//���£�Ҫ��Ȼ��Unity���в��ῴ�����ɵ�ͼƬ����win���ļ��������п��Կ�����
    }

    private Texture2D GetTexture()
    {
        if (metallic != null)
        {
            mat.SetTexture("_Metallic", metallic);//��Shader�����Ը�ֵ
        }

        if (occlusion != null)
        {
            mat.SetTexture("_Occlusion", occlusion);
        }

        if (detailmask != null)
        {
            mat.SetTexture("_DetailMask", detailmask);
        }

        if (smoothness != null)
        {
            mat.SetTexture("_Smoothness", smoothness);
        }

        RenderTexture tempRT = new RenderTexture(2048, 2048, 32, RenderTextureFormat.ARGB32);//���������ֱ��ʿ����Լ���Ϊ1024�ģ�Ҳ�����Լ��ڱ༭������������ɹ�ѡ��ķֱ��ʣ�����ʵ��
        tempRT.Create();
        Texture2D temp2 = new Texture2D(tempRT.width, tempRT.height, TextureFormat.ARGB32, false);
        Graphics.Blit(temp2, tempRT, mat);//��temp2�����ֵͨ��mat��ֵ��tempRT�����ĵĴ��룬���Ժúÿ�������������Ľ���
        RenderTexture prev = RenderTexture.active;
        RenderTexture.active = tempRT;//���õ�ǰactive������

        Texture2D output = new Texture2D(tempRT.width, tempRT.height, TextureFormat.ARGB32, false);//����һ��RGBA��ʽ������
        output.ReadPixels(new Rect(0, 0, tempRT.width, tempRT.height), 0, 0);//��ȡ��ǰactive������            
        output.Apply();//apply���ı�д��
        RenderTexture.active = prev;

        return output;
    }


    private void GetInput()
    {
        EditorGUILayout.BeginHorizontal("Box");
        GUILayout.Label("Input Textures");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal("Box");
        _metallic = TextureField("metallic", metallic);
        if (_metallic != metallic)
        {
            metallic = _metallic;
            changed = true;
        }
        _occlusion = TextureField("occlusion", occlusion);
        if (_occlusion != occlusion)
        {
            occlusion = _occlusion;
            changed = true;
        }
        _detailmask = TextureField("detailmask", detailmask);
        if (_detailmask != detailmask)
        {
            detailmask = _detailmask;
            changed = true;
        }
        _smoothness = TextureField("smoothness", smoothness);
        if (_smoothness != smoothness)
        {
            smoothness = _smoothness;
            changed = true;
        }
        EditorGUILayout.EndHorizontal();

    }

    private Texture2D preview = null;
    private void Preview()
    {
        EditorGUILayout.BeginHorizontal("Box");
        GUILayout.Label("Preview Output");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal("Box");

        Rect previewRect = new Rect(this.position.width / 2 - 75, 250, 150, 150);
        if (preview == null)
        {
            preview = Texture2D.blackTexture;
        }

        if (changed)
        {
            preview = GetTexture();
            changed = false;
        }
        EditorGUI.DrawPreviewTexture(previewRect, preview);
        EditorGUILayout.Space(160);
        EditorGUILayout.EndHorizontal();
    }

    private Texture2D TextureField(string name, Texture2D texture)
    {
        EditorGUILayout.BeginVertical();
        var style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.MiddleCenter;
        style.fixedWidth = 150;
        GUILayout.Label(name, style);
        Texture2D result = EditorGUILayout.ObjectField(texture, typeof(Texture2D), false, GUILayout.Width(150), GUILayout.Height(150)) as Texture2D;
        EditorGUILayout.EndHorizontal();
        return result;
    }
}