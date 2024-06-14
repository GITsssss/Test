using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

namespace HLVR.RenderPipeline 
{
    public class SetTextureEditor : EditorWindow
    {
        public enum MaterialType
        {
            StandardOrStandardSpecular,
            UniversalRenderPipelineLit,
            HDRenderPipelineLit,
        }
        public enum TransitionMode
        {
            URPConversionBuiltIn,
            URPConversionHDRP,
            BuiltInConversionURP,
            BuiltInConversionHDRP,
            HDRPConversionURP,
            HDRPConversionBuiltIn,
        }
        public enum Platform
        {
            Android,
            iphone,
            Standalone,
        }
        public enum TextureSize
        {
            S32 = 32,
            S64 = 64,
            S128 = 128,
            S256 = 256,
            S512 = 512,
            S1024 = 1024,
            S2048 = 2048,
            S4096 = 4096,
            S8192 = 8192,
            S16384 = 16384,
        }


        public RenderPipelineAsset renderPipelineAsset;
        [Header("��Ⱦ���߱�׼��ɫ������")]
        public MaterialType RenderingPipelineType;
        [Header("��ɫ��ת")]
        public TransitionMode transitionMode;
        [Header("����������")]
        public List<Material> MaterialList;
        [Header("������������ͼ����")]
        public TextureSize textureSize = TextureSize.S1024;
        public Platform platform = Platform.Standalone;
        public TextureImporterShape ImporterShape = TextureImporterShape.Texture2D;
        public TextureImporterFormat m_TextureImporterFormat = TextureImporterFormat.Automatic;
        public List<Texture> TextureList;
        private SerializedProperty TextureListProperty;
        public List<Material> materialList;
        private SerializedProperty materialListProperty;
        SerializedObject settexture;
        TextureImporter textureImporter;
        string BaseColorName;
        string BaseName;
        string MetallicOrSpecularName;
        string NormalMapName;
        string HeightName;
        string OcclusionName;
        string EmissionName;
        string MaterialListkMapName;

        Vector2 sc1pos;
        Vector2 sc2pos;
        int SymbolIndex(string str)//��ȡ�ض����                  
        {
            return str.IndexOf("_", 0) + 1;
        }

        string GetName(string texname) //��ȡ����
        {
            return texname.Remove(0, SymbolIndex(texname));
        }

        [MenuItem("HLVR/Tool/RenderPipeline/MaterialTextureAutomation[�����Զ���������ͼ����]")]
        public static void Windows()
        {
            SetTextureEditor se = EditorWindow.CreateWindow<SetTextureEditor>();
            se.minSize = new Vector2(800, 550);
            se.maxSize = new Vector2(800, 550);
        }
        private void OnEnable()
        {
            settexture = new SerializedObject(this);
            TextureListProperty = settexture.FindProperty("TextureList");
            materialListProperty = settexture.FindProperty("materialList");
        }
        private void OnGUI()
        {
            settexture.Update();
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            RenderingPipelineType = (MaterialType)EditorGUILayout.EnumPopup("��Ⱦ���߱�׼��ɫ������", RenderingPipelineType, GUILayout.Width(300));
            transitionMode = (TransitionMode)EditorGUILayout.EnumPopup("��ɫ��ת��", transitionMode, GUILayout.Width(300));
            textureSize = (TextureSize)EditorGUILayout.EnumPopup("��ͼ��С", textureSize, GUILayout.Width(300));
            platform = (Platform)EditorGUILayout.EnumPopup("ƽ̨����", platform, GUILayout.Width(300));
            ImporterShape = (TextureImporterShape)EditorGUILayout.EnumPopup("��ͼ����", ImporterShape, GUILayout.Width(300));
            m_TextureImporterFormat = (TextureImporterFormat)EditorGUILayout.EnumPopup("��ͼ��ʽ", m_TextureImporterFormat, GUILayout.Width(300));
            GUILayout.EndVertical();
            renderPipelineAsset = (RenderPipelineAsset)EditorGUILayout.ObjectField(renderPipelineAsset, typeof(RenderPipelineAsset));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            using (var horizontalScope = new GUILayout.VerticalScope("box"))
            {

                if (GUILayout.Button("���ò�����ͼ", GUILayout.Width(100), GUILayout.Height(40)))
                {
                    GetTextrueToMaterialName();

                }

                if (GUILayout.Button("����/���� ���", GUILayout.Width(100), GUILayout.Height(40)))
                {
                    ToggleInspectorLock();

                }

            }
            using (var horizontalScope = new GUILayout.VerticalScope("box"))
            {


                if (GUILayout.Button("��ɫ����", GUILayout.Width(100), GUILayout.Height(40)))
                {
                    AllTextureWhile();
                }

                if (GUILayout.Button("�������������", GUILayout.Width(100), GUILayout.Height(40)))
                {
                    ClearAllTexure();
                }

            }

            using (var horizontalScope = new GUILayout.VerticalScope("box"))
            {

                if (GUILayout.Button("���������", GUILayout.Width(100), GUILayout.Height(40)))
                {
                    ClearMaterialData();
                }

                if (GUILayout.Button("���������ͼ", GUILayout.Width(100), GUILayout.Height(40)))
                {
                    ClearTexturesData();
                }
            }
            using (var horizontalScope = new GUILayout.VerticalScope("box"))
            {

                if (GUILayout.Button("������ͼ��С", GUILayout.Width(100), GUILayout.Height(40)))
                {
                    SetTextureSize();
                }

                if (GUILayout.Button("ת����ɫ��", GUILayout.Width(100), GUILayout.Height(40)))
                {
                    SetShader();
                }
            }

            using (var horizontalScope = new GUILayout.VerticalScope("box"))
            {

                if (GUILayout.Button("ת����ͼ", GUILayout.Width(100), GUILayout.Height(40)))
                {
                    TransitionMap();
                }
                //TransitionRenderPipelineAsset()
                if (GUILayout.Button("ת����Ⱦ����", GUILayout.Width(100), GUILayout.Height(40)))
                {
                    TransitionRenderPipelineAsset();
                }
            }
            GUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            sc1pos = EditorGUILayout.BeginScrollView(sc1pos, GUILayout.Width(400), GUILayout.Height(300));
            GUILayout.Label("��ͼ�б�");
            EditorGUILayout.PropertyField(TextureListProperty);
            EditorGUILayout.EndScrollView();
            
            EditorGUILayout.Space(50);
            
            sc2pos = EditorGUILayout.BeginScrollView(sc2pos, GUILayout.Width(400), GUILayout.Height(300));
            GUILayout.Label("�����б�");
            EditorGUILayout.PropertyField(materialListProperty);
            EditorGUILayout.EndScrollView();

            EditorGUILayout.EndHorizontal();
            settexture.ApplyModifiedProperties();
        }

        public void SetTextureSize()
        {
            foreach (Texture2D texture in TextureList)
            {
                string path = AssetDatabase.GetAssetPath(texture);
                TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
                textureImporter.textureShape = ImporterShape;
                textureImporter.maxTextureSize = (int)textureSize;
                textureImporter.SetPlatformTextureSettings(platform.ToString(), (int)textureSize, m_TextureImporterFormat);
                AssetDatabase.ImportAsset(path);
            }

        }
        public void GetTextrueToMaterialName()//��ȡ��������������Ӧ�Ĳ�����ͼ
        {
            SetShader2();
            ChannelType(RenderingPipelineType);
            if (MaterialList.Count > 0 && TextureList.Count > 0)
            {
                for (int i = 0; i < MaterialList.Count; i++)
                {
                    for (int n = 0; n < TextureList.Count; n++)
                    {
                        if (MaterialList[i].name == GetName(TextureList[n].name).Remove(GetName(TextureList[n].name).IndexOf("_", 0)))
                        {
                            switch (RenderingPipelineType)
                            {
                                case MaterialType.StandardOrStandardSpecular:
                                    if (MaterialList[i].shader.name == "Standard (Specular setup)")
                                    {
                                        switch (GetName(GetName(TextureList[n].name)))
                                        {
                                            //SpecularSmoothness
                                            case "AlbedoTransparency":
                                                MaterialList[i].SetTexture(BaseName, TextureList[n]);

                                                break;
                                            case "SpecularSmoothness": MaterialList[i].SetTexture("_SpecGlossMap", TextureList[n]); break;
                                            case "Normal":
                                                MaterialList[i].SetTexture(NormalMapName, TextureList[n]);
                                                string path = AssetDatabase.GetAssetPath(TextureList[n]);
                                                TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
                                                importer.textureType = TextureImporterType.NormalMap;
                                                break;
                                            case "Height": MaterialList[i].SetTexture(HeightName, TextureList[n]); break;
                                            case "AO": MaterialList[i].SetTexture(OcclusionName, TextureList[n]); break;
                                            case "_EmissionMap": MaterialList[i].SetTexture(EmissionName, TextureList[n]); break;
                                        }

                                    }
                                    else if (MaterialList[i].shader.name == "Standard")
                                    {

                                        switch (GetName(GetName(TextureList[n].name)))
                                        {
                                            case "AlbedoTransparency": MaterialList[i].SetTexture(BaseName, TextureList[n]); break;
                                            case "MetallicSmoothness": MaterialList[i].SetTexture("_MetallicGlossMap", TextureList[n]); break;
                                            case "Normal":
                                                MaterialList[i].SetTexture(NormalMapName, TextureList[n]);

                                                string path = AssetDatabase.GetAssetPath(TextureList[n]);
                                                TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
                                                importer.textureType = TextureImporterType.NormalMap;
                                                break;
                                            case "Height": MaterialList[i].SetTexture(HeightName, TextureList[n]); break;
                                            case "AO": MaterialList[i].SetTexture(OcclusionName, TextureList[n]); break;
                                            case "_EmissionMap": MaterialList[i].SetTexture(EmissionName, TextureList[n]); break;
                                        }


                                    }

                                    break;
                                case MaterialType.UniversalRenderPipelineLit:

                                    if (MaterialList[i].GetFloat("_WorkflowMode") == 0)
                                    {
                                        switch (GetName(GetName(TextureList[n].name)))
                                        {
                                            case "AlbedoTransparency": MaterialList[i].SetTexture(BaseName, TextureList[n]); break;
                                            case "SpecularSmoothness": MaterialList[i].SetTexture("_SpecGlossMap", TextureList[n]); break;
                                            case "Normal":
                                                MaterialList[i].SetTexture(NormalMapName, TextureList[n]);
                                                string path = AssetDatabase.GetAssetPath(TextureList[n]);
                                                TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
                                                importer.textureType = TextureImporterType.NormalMap;
                                                break;
                                            case "Height": MaterialList[i].SetTexture(HeightName, TextureList[n]); break;
                                            case "AO": MaterialList[i].SetTexture(OcclusionName, TextureList[n]); break;
                                            case "_EmissionMap": MaterialList[i].SetTexture(EmissionName, TextureList[n]); break;
                                        }


                                    }
                                    else if (MaterialList[i].GetFloat("_WorkflowMode") == 1)
                                    {
                                        switch (GetName(GetName(TextureList[n].name)))
                                        {
                                            case "AlbedoTransparency": MaterialList[i].SetTexture(BaseName, TextureList[n]); break;
                                            case "MetallicSmoothness": MaterialList[i].SetTexture("_MetallicGlossMap", TextureList[n]); break;
                                            case "Normal":
                                                MaterialList[i].SetTexture(NormalMapName, TextureList[n]);
                                                string path = AssetDatabase.GetAssetPath(TextureList[n]);
                                                TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
                                                importer.textureType = TextureImporterType.NormalMap;

                                                break;
                                            case "Height": MaterialList[i].SetTexture(HeightName, TextureList[n]); break;
                                            case "AO": MaterialList[i].SetTexture(OcclusionName, TextureList[n]); break;
                                            case "_EmissionMap": MaterialList[i].SetTexture(EmissionName, TextureList[n]); break;
                                        }

                                    }
                                    break;

                                case MaterialType.HDRenderPipelineLit:
                                    switch (MaterialList[i].GetInt("_MaterialID"))
                                    {
                                        case 1:
                                            switch (GetName(GetName(TextureList[n].name)))
                                            {
                                                case "BaseMap": MaterialList[i].SetTexture(BaseName, TextureList[n]); break;
                                                case "MaterialListkMap": MaterialList[i].SetTexture(MaterialListkMapName, TextureList[n]); break;
                                                case "MaskMap": MaterialList[i].SetTexture(MaterialListkMapName, TextureList[n]); break;
                                                case "Normal":
                                                    MaterialList[i].SetTexture(NormalMapName, TextureList[n]);
                                                    string path = AssetDatabase.GetAssetPath(TextureList[n]);
                                                    TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter; ;
                                                    importer.textureType = TextureImporterType.NormalMap;
                                                    break;
                                            }
                                            break;

                                        case 4:
                                            switch (GetName(GetName(TextureList[n].name)))
                                            {
                                                case "BaseMap": MaterialList[i].SetTexture(BaseName, TextureList[n]); break;
                                                case "MaterialListkMap": MaterialList[i].SetTexture(MaterialListkMapName, TextureList[n]); break;
                                                //MaskMap
                                                case "MaskMap": MaterialList[i].SetTexture(MaterialListkMapName, TextureList[n]); break;
                                                case "Specular": MaterialList[i].SetTexture("_SpecularColorMap", TextureList[n]); break;
                                                case "Normal":
                                                    MaterialList[i].SetTexture(NormalMapName, TextureList[n]);
                                                    string path = AssetDatabase.GetAssetPath(TextureList[n]);
                                                    TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter; ;
                                                    importer.textureType = TextureImporterType.NormalMap;
                                                    break;
                                            }
                                            break;

                                    }

                                    break;
                            }

                        }
                    }
                }
            }
            else if (MaterialList.Count > 0 && TextureList.Count == 0)
                Debug.LogError("��ǰ��ͼ������Ϊ��");
            else if (MaterialList.Count == 0 && TextureList.Count > 0)
                Debug.LogError("��ǰ�����������Ϊ��");
            else if (MaterialList.Count == 0 && TextureList.Count == 0)
                Debug.LogError("��ǰ�Ĳ�������������ͼ���ݶ�Ϊ��");
        }

        public void ClearAllTexure()
        {
            ChannelType(RenderingPipelineType);
            if (MaterialList.Count > 0)
                for (int i = 0; i < MaterialList.Count; i++)
                {
                    MaterialList[i].SetTexture(BaseName, null);
                    MaterialList[i].SetTexture(NormalMapName, null);
                    MaterialList[i].SetTexture(HeightName, null);
                    MaterialList[i].SetTexture(OcclusionName, null);
                    MaterialList[i].SetTexture(EmissionName, null);
                    switch (RenderingPipelineType)
                    {
                        case MaterialType.StandardOrStandardSpecular:
                            if (MaterialList[i].shader.name == "Standard (Specular setup)")
                                MaterialList[i].SetTexture("_SpecGlossMap", null);
                            else if (MaterialList[i].shader.name == "Standard")
                                MaterialList[i].SetTexture("_MetallicGlossMap", null);
                            break;
                        case MaterialType.UniversalRenderPipelineLit:
                            if (MaterialList[i].GetFloat("_WorkflowMode") == 0)
                                MaterialList[i].SetTexture("_SpecGlossMap", null);
                            else if (MaterialList[i].GetFloat("_WorkflowMode") == 1)
                                MaterialList[i].SetTexture("_MetallicGlossMap", null);
                            break;
                        case MaterialType.HDRenderPipelineLit:
                            MaterialList[i].SetTexture("_MaterialListkMap", null);
                            break;
                    }

                }
            else Debug.LogError("��ǰ����������Ϊ�㣬�����Ҫɾ����ͼ�Ĳ�����");
        }

        public void ClearMaterialData() //�������������
        {
            if (MaterialList.Count > 0)
                MaterialList.Clear();
            else
                Debug.LogError("û�в���������");
        }
        public void ClearTexturesData() //���������ͼ����
        {
            if (MaterialList.Count > 0)
                TextureList.Clear();
            else
                Debug.LogError("û����ͼ�����");
        }
        public void AllTextureWhile()
        {
            SetShader2();
            ChannelType(RenderingPipelineType);
            if (MaterialList.Count > 0)
            {
                for (int i = 0; i < MaterialList.Count; i++)
                {
                    MaterialList[i].SetColor(BaseColorName, Color.white);
                }
            }
            else
                Debug.LogError("û����ͼ�����");


        }

        void SetShader2()
        {
            switch (RenderingPipelineType)
            {
                case MaterialType.StandardOrStandardSpecular:
                    for (int i = 0; i < MaterialList.Count; i++)
                    {
                        MaterialList[i].shader = Shader.Find("Standard");
                    };
                    break;

                case MaterialType.UniversalRenderPipelineLit:
                    for (int i = 0; i < MaterialList.Count; i++)
                    {
                        MaterialList[i].shader = Shader.Find("Universal Render Pipeline/Lit");
                    };
                    break;
                case MaterialType.HDRenderPipelineLit:
                    for (int i = 0; i < MaterialList.Count; i++)
                    {
                        MaterialList[i].shader = Shader.Find("HDRP/Lit");
                    };
                    break;
            }


        }

        public void SetShader()
        {


            if (transitionMode == TransitionMode.BuiltInConversionURP || transitionMode == TransitionMode.HDRPConversionURP)
            {
                for (int i = 0; i < MaterialList.Count; i++)
                {
                    MaterialList[i].shader = Shader.Find("Universal Render Pipeline/Lit");
                }
            }
            else if (transitionMode == TransitionMode.BuiltInConversionHDRP || transitionMode == TransitionMode.URPConversionHDRP)
            {
                for (int i = 0; i < MaterialList.Count; i++)
                {
                    MaterialList[i].shader = Shader.Find("HDRP/Lit");
                }
            }
            else
            {
                for (int i = 0; i < MaterialList.Count; i++)
                {
                    MaterialList[i].shader = Shader.Find("Standard");
                    Debug.Log(666);
                }
            }



        }

        Shader GetShader(MaterialType ty)
        {
            switch (RenderingPipelineType)
            {
                case MaterialType.StandardOrStandardSpecular:
                    return Shader.Find("Standard");
                case MaterialType.UniversalRenderPipelineLit:
                    return Shader.Find("Universal Render Pipeline/Lit");
                case MaterialType.HDRenderPipelineLit:
                    return Shader.Find("HDRP/Lit");
            }
            return null;


        }

        Shader GetShader(TransitionMode ty)
        {
            switch (ty)
            {
                case TransitionMode.HDRPConversionBuiltIn:
                    return Shader.Find("Standard");
                case TransitionMode.HDRPConversionURP:
                    return Shader.Find("Universal Render Pipeline/Lit");
                case TransitionMode.BuiltInConversionHDRP:
                    return Shader.Find("HDRP/Lit");
                case TransitionMode.BuiltInConversionURP:
                    return Shader.Find("Universal Render Pipeline/Lit");
                case TransitionMode.URPConversionBuiltIn:
                    return Shader.Find("Standard");
                case TransitionMode.URPConversionHDRP:
                    return Shader.Find("HDRP/Lit");
            }
            return null;
        }

        void ChannelType(MaterialType ty) //�õ����ͨ������
        {

            switch (ty)
            {
                case MaterialType.StandardOrStandardSpecular:
                    Debug.LogWarning("��ǰѡ�����Ⱦ����Ϊ�ɰ����ù��ߣ�");
                    BaseColorName = "_Color";
                    BaseName = "_MainTex";
                    // MetallicOrSpecularName = "_MetallicGlossMap";
                    NormalMapName = "_BumpMap";
                    HeightName = "_ParallaxMap";
                    OcclusionName = "_OcclusionMap";
                    EmissionName = "_EmissionMap";
                    break;

                case MaterialType.UniversalRenderPipelineLit:
                    Debug.LogWarning("��ǰѡ�����Ⱦ����ΪSRP_URPͨ����Ⱦ���ߣ�");
                    BaseColorName = "_BaseColor";
                    BaseName = "_BaseMap";
                    // MetallicOrSpecularName = "_MetallicGlossMap";
                    NormalMapName = "_BumpMap";
                    HeightName = "_ParallaxMap";
                    OcclusionName = "_OcclusionMap";
                    EmissionName = "_EmissionMap";
                    break;
                case MaterialType.HDRenderPipelineLit:
                    Debug.LogWarning("��ǰѡ�����Ⱦ����ΪSRP_HDRP������Ⱦ���ߣ�");
                    BaseColorName = "_BaseColor";
                    BaseName = "_BaseColorMap";
                    //MaterialListkMapName = "_MaterialListkMap"; MaskMap
                    MaterialListkMapName = "_MaskMap";
                    //_MaskMap
                    NormalMapName = "_NormalMap";
                    break;
            }
        }

        public void TransitionRenderPipelineAsset()
        {
            if (transitionMode == TransitionMode.HDRPConversionBuiltIn || transitionMode == TransitionMode.URPConversionBuiltIn)
            {
                GraphicsSettings.renderPipelineAsset = null;
            }
            else
            {
                GraphicsSettings.renderPipelineAsset = renderPipelineAsset;
            }
        }

        public void TransitionMap() //ֱ��ת����ͼ
        {
            ChannelType(RenderingPipelineType);
            switch (transitionMode)
            {
                case TransitionMode.HDRPConversionBuiltIn:
                    for (int i = 0; i < MaterialList.Count; i++)
                    {
                        Texture basemap = MaterialList[i].GetTexture(BaseName);
                        Texture normalmap = MaterialList[i].GetTexture(NormalMapName);
                        MaterialList[i].shader = GetShader(transitionMode);
                        MaterialList[i].SetTexture("_BumpMap", normalmap);
                        MaterialList[i].SetTexture("_MainTex", basemap);
                    }
                    break;

                case TransitionMode.URPConversionBuiltIn:
                    for (int i = 0; i < MaterialList.Count; i++)
                    {
                        Texture basemap = MaterialList[i].GetTexture(BaseName);
                        Texture normalmap = MaterialList[i].GetTexture(NormalMapName);
                        MaterialList[i].shader = GetShader(transitionMode);
                        MaterialList[i].SetTexture("_BumpMap", normalmap);
                        MaterialList[i].SetTexture("_MainTex", basemap);
                    }
                    break;

                case TransitionMode.HDRPConversionURP:

                    for (int i = 0; i < MaterialList.Count; i++)
                    {
                        Texture basemap = MaterialList[i].GetTexture(BaseName);
                        Texture normalmap = MaterialList[i].GetTexture(NormalMapName);
                        MaterialList[i].shader = GetShader(transitionMode);
                        MaterialList[i].SetTexture("_BumpMap", normalmap);
                        MaterialList[i].SetTexture("_BaseMap", basemap);
                    }

                    break;
                case TransitionMode.BuiltInConversionURP:
                    for (int i = 0; i < MaterialList.Count; i++)
                    {
                        Texture basemap = MaterialList[i].GetTexture(BaseName);
                        Texture normalmap = MaterialList[i].GetTexture(NormalMapName);
                        MaterialList[i].shader = GetShader(transitionMode);
                        MaterialList[i].SetTexture("_BumpMap", normalmap);
                        MaterialList[i].SetTexture("_BaseMap", basemap);
                    }
                    break;
                case TransitionMode.BuiltInConversionHDRP:
                    for (int i = 0; i < MaterialList.Count; i++)
                    {
                        Texture basemap = MaterialList[i].GetTexture(BaseName);
                        Texture normalmap = MaterialList[i].GetTexture(NormalMapName);
                        MaterialList[i].shader = GetShader(transitionMode);
                        MaterialList[i].SetTexture("_NormalMap", normalmap);
                        MaterialList[i].SetTexture("_BaseColorMap", basemap);
                    }
                    break;
                case TransitionMode.URPConversionHDRP:
                    for (int i = 0; i < MaterialList.Count; i++)
                    {
                        Texture basemap = MaterialList[i].GetTexture(BaseName);
                        Texture normalmap = MaterialList[i].GetTexture(NormalMapName);
                        MaterialList[i].shader = GetShader(transitionMode);
                        MaterialList[i].SetTexture("_NormalMap", normalmap);
                        MaterialList[i].SetTexture("_BaseColorMap", basemap);
                    }
                    break;
            }
        }

        public static void ToggleInspectorLock()
        {
            var inspectorType = typeof(Editor).Assembly.GetType("UnityEditor.InspectorWindow");

            var isLocked = inspectorType.GetProperty("isLocked", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            var inspectorWindow = EditorWindow.GetWindow(inspectorType);

            var state = isLocked.GetGetMethod().Invoke(inspectorWindow, new object[] { });

            isLocked.GetSetMethod().Invoke(inspectorWindow, new object[] { !(bool)state });
        }
    }

}
