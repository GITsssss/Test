using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class MaskMapEditor : EditorWindow//扩展编辑器要继承EditorWindow
{
    private Texture2D metallic, occlusion, detailmask, smoothness;//四个纹理字段
    private Texture2D _metallic, _occlusion, _detailmask, _smoothness;//这四个字段用于预览的实现
    private Material mat;//带有Shader的材质
    private bool changed;

    [MenuItem("HLVR/Tool/HDRP/CreateMaskMap")]
    static void OpenMaskMapEditor()//实现点击菜单栏的按钮弹出窗口
    {
        MaskMapEditor maskMapEditor = GetWindow<MaskMapEditor>(false, "MaskMap Editor");//创建窗口
        maskMapEditor.titleContent.text = "MaskMap Editor";//给窗口命名
        maskMapEditor.Init();//初始化
    }

    private void Init()
    {
        if (mat == null)
        {
            mat = new Material(Shader.Find("Hidden/MaskMap"));//根据MaskMap这个Shader创建材质
            mat.hideFlags = HideFlags.HideAndDontSave;//材质不保存
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
        File.WriteAllBytes(savePath, output.EncodeToPNG());//将纹理保存为png格式，也可以是jpg、exr等格式
        AssetDatabase.Refresh();//更新，要不然在Unity当中不会看到生成的图片（在win的文件管理器中可以看到）
    }

    private Texture2D GetTexture()
    {
        if (metallic != null)
        {
            mat.SetTexture("_Metallic", metallic);//给Shader的属性赋值
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

        RenderTexture tempRT = new RenderTexture(2048, 2048, 32, RenderTextureFormat.ARGB32);//生成纹理，分辨率可以自己改为1024的，也可以自己在编辑器上做出多个可供选择的分辨率，容易实现
        tempRT.Create();
        Texture2D temp2 = new Texture2D(tempRT.width, tempRT.height, TextureFormat.ARGB32, false);
        Graphics.Blit(temp2, tempRT, mat);//将temp2纹理的值通过mat赋值到tempRT，核心的代码，可以好好看看对这个方法的解释
        RenderTexture prev = RenderTexture.active;
        RenderTexture.active = tempRT;//设置当前active的纹理

        Texture2D output = new Texture2D(tempRT.width, tempRT.height, TextureFormat.ARGB32, false);//创建一个RGBA格式的纹理
        output.ReadPixels(new Rect(0, 0, tempRT.width, tempRT.height), 0, 0);//读取当前active的纹理            
        output.Apply();//apply将改变写入
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