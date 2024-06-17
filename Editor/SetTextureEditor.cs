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
        string materialListkMapName;

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
            se.minSize = new Vector2(670, 510);
            se.maxSize = new Vector2(670, 510);
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
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox,GUILayout.Width(300))) 
            {  
                RenderingPipelineType = (MaterialType)EditorGUILayout.EnumPopup("��Ⱦ���߱�׼��ɫ������", RenderingPipelineType, GUILayout.Width(300));
                GUILayout.BeginHorizontal();
                using (var horizontalScopes = new GUILayout.VerticalScope(EditorStyles.helpBox, GUILayout.Width(102)))
                {
                    if (GUILayout.Button("���ò�����ͼ", GUILayout.Width(100), GUILayout.Height(20)))
                    {
                        GetTextrueToMaterialName();
                    }
                    if (GUILayout.Button("��ɫ����", GUILayout.Width(100), GUILayout.Height(20)))
                    {
                        AllTextureWhile();
                    }

                    if (GUILayout.Button("�������������", GUILayout.Width(100), GUILayout.Height(20)))
                    {
                        ClearAllTexure();
                    }

                }
                using (var horizontalScopes = new GUILayout.VerticalScope(EditorStyles.helpBox, GUILayout.Width(102)))
                {

                    if (GUILayout.Button("��������б�", GUILayout.Width(100), GUILayout.Height(20)))
                    {
                        ClearMaterialData();
                    }

                    if (GUILayout.Button("�����ͼ�б�", GUILayout.Width(100), GUILayout.Height(20)))
                    {
                        ClearTexturesData();
                    }
                }
                GUILayout.EndHorizontal();

            }
            GUILayout.BeginHorizontal();
            using (var horizontalScope = new GUILayout.VerticalScope(EditorStyles.helpBox, GUILayout.Width(102)))
            {

                textureSize = (TextureSize)EditorGUILayout.EnumPopup("��ͼ��С", textureSize, GUILayout.Width(300));
                platform = (Platform)EditorGUILayout.EnumPopup("ƽ̨����", platform, GUILayout.Width(300));
                ImporterShape = (TextureImporterShape)EditorGUILayout.EnumPopup("��ͼ����", ImporterShape, GUILayout.Width(300));
                m_TextureImporterFormat = (TextureImporterFormat)EditorGUILayout.EnumPopup("��ͼ��ʽ", m_TextureImporterFormat, GUILayout.Width(300));
                if (GUILayout.Button("������ͼ����", GUILayout.Width(300), GUILayout.Height(20)))
                {
                    SetTextureSize();
                }
                //if (GUILayout.Button("����/���� ���", GUILayout.Width(100), GUILayout.Height(40)))
                //{
                //    ToggleInspectorLock();
                //}
            }

           
           

           
            GUILayout.EndHorizontal();
            GUILayout.EndHorizontal();

         

            using (var horizontalScopes = new GUILayout.VerticalScope(EditorStyles.helpBox, GUILayout.Width(600)))
            {
                GUILayout.BeginHorizontal();
               
              
                using (var horizontalScope = new GUILayout.VerticalScope(EditorStyles.helpBox, GUILayout.Width(300)))
                {
                    GUILayout.BeginHorizontal();
                    transitionMode = (TransitionMode)EditorGUILayout.EnumPopup(GUIContent.none, transitionMode, GUILayout.Width(300),GUILayout.Width(180));
                    if (GUILayout.Button("ת����Ⱦ����", GUILayout.Width(100), GUILayout.Height(20)))
                    {
                        TransitionRenderPipelineAsset();
                    }
                    if (GUILayout.Button("��ȡ�����ļ�", GUILayout.Width(100), GUILayout.Height(20)))
                    {
                        renderPipelineAsset=GraphicsSettings.renderPipelineAsset;
                    }
                    GUILayout.EndHorizontal();
                }
                using (var horizontalScope = new GUILayout.VerticalScope(EditorStyles.helpBox, GUILayout.Width(102)))
                {
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("ת����ɫ��", GUILayout.Width(100), GUILayout.Height(20)))
                    {
                        SetShader();
                        
                    }

                    if (GUILayout.Button("ת����ɫ������ͼ", GUILayout.Width(120), GUILayout.Height(20)))
                    {
                        TransitionMap();
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndHorizontal();
            }
         
          
            renderPipelineAsset = (RenderPipelineAsset)EditorGUILayout.ObjectField(GUIContent.none, renderPipelineAsset, typeof(RenderPipelineAsset),GUILayout.Width(650));
            //using (var horizontalScope = new GUILayout.VerticalScope(EditorStyles.helpBox, GUILayout.Width(600))) 
            //{
            //    EditorGUILayout.HelpBox("�б���ͼΪ��", MessageType.Warning);
            //}
                EditorGUILayout.Space(5);
            EditorGUILayout.BeginHorizontal();
            using (var horizontalScope = new GUILayout.VerticalScope(EditorStyles.helpBox, GUILayout.Width(102))) 
            {
                sc1pos = EditorGUILayout.BeginScrollView(sc1pos, GUILayout.Width(320), GUILayout.Height(300));
                GUILayout.Label("��ͼ�б�");
                EditorGUILayout.PropertyField(TextureListProperty, GUILayout.Width(300));
                EditorGUILayout.EndScrollView();
            }


            using (var horizontalScope = new GUILayout.VerticalScope(EditorStyles.helpBox, GUILayout.Width(102))) 
            {
                sc2pos = EditorGUILayout.BeginScrollView(sc2pos, GUILayout.Width(320), GUILayout.Height(300));
                GUILayout.Label("�����б�");
                EditorGUILayout.PropertyField(materialListProperty, GUILayout.Width(300));
                EditorGUILayout.EndScrollView();
            }
          

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
            if (materialList.Count > 0 && TextureList.Count > 0)
            {
                for (int i = 0; i < materialList.Count; i++)
                {
                    for (int n = 0; n < TextureList.Count; n++)
                    {
                        if (materialList[i].name == GetName(TextureList[n].name).Remove(GetName(TextureList[n].name).IndexOf("_", 0)))
                        {
                            switch (RenderingPipelineType)
                            {
                                case MaterialType.StandardOrStandardSpecular:
                                    if (materialList[i].shader.name == "Standard (Specular setup)")
                                    {
                                        switch (GetName(GetName(TextureList[n].name)))
                                        {
                                            //SpecularSmoothness
                                            case "AlbedoTransparency":
                                                materialList[i].SetTexture(BaseName, TextureList[n]);

                                                break;
                                            case "SpecularSmoothness": materialList[i].SetTexture("_SpecGlossMap", TextureList[n]); break;
                                            case "Normal":
                                                materialList[i].SetTexture(NormalMapName, TextureList[n]);
                                                string path = AssetDatabase.GetAssetPath(TextureList[n]);
                                                TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
                                                importer.textureType = TextureImporterType.NormalMap;
                                                break;
                                            case "Height": materialList[i].SetTexture(HeightName, TextureList[n]); break;
                                            case "AO": materialList[i].SetTexture(OcclusionName, TextureList[n]); break;
                                            case "_EmissionMap": materialList[i].SetTexture(EmissionName, TextureList[n]); break;
                                        }

                                    }
                                    else if (materialList[i].shader.name == "Standard")
                                    {

                                        switch (GetName(GetName(TextureList[n].name)))
                                        {
                                            case "AlbedoTransparency": materialList[i].SetTexture(BaseName, TextureList[n]); break;
                                            case "MetallicSmoothness": materialList[i].SetTexture("_MetallicGlossMap", TextureList[n]); break;
                                            case "Normal":
                                                materialList[i].SetTexture(NormalMapName, TextureList[n]);

                                                string path = AssetDatabase.GetAssetPath(TextureList[n]);
                                                TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
                                                importer.textureType = TextureImporterType.NormalMap;
                                                break;
                                            case "Height": materialList[i].SetTexture(HeightName, TextureList[n]); break;
                                            case "AO": materialList[i].SetTexture(OcclusionName, TextureList[n]); break;
                                            case "_EmissionMap": materialList[i].SetTexture(EmissionName, TextureList[n]); break;
                                        }


                                    }

                                    break;
                                case MaterialType.UniversalRenderPipelineLit:

                                    if (materialList[i].GetFloat("_WorkflowMode") == 0)
                                    {
                                        switch (GetName(GetName(TextureList[n].name)))
                                        {
                                            case "AlbedoTransparency": materialList[i].SetTexture(BaseName, TextureList[n]); break;
                                            case "SpecularSmoothness": materialList[i].SetTexture("_SpecGlossMap", TextureList[n]); break;
                                            case "Normal":
                                                materialList[i].SetTexture(NormalMapName, TextureList[n]);
                                                string path = AssetDatabase.GetAssetPath(TextureList[n]);
                                                TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
                                                importer.textureType = TextureImporterType.NormalMap;
                                                break;
                                            case "Height": materialList[i].SetTexture(HeightName, TextureList[n]); break;
                                            case "AO": materialList[i].SetTexture(OcclusionName, TextureList[n]); break;
                                            case "_EmissionMap": materialList[i].SetTexture(EmissionName, TextureList[n]); break;
                                        }


                                    }
                                    else if (materialList[i].GetFloat("_WorkflowMode") == 1)
                                    {
                                        switch (GetName(GetName(TextureList[n].name)))
                                        {
                                            case "AlbedoTransparency": materialList[i].SetTexture(BaseName, TextureList[n]); break;
                                            case "MetallicSmoothness": materialList[i].SetTexture("_MetallicGlossMap", TextureList[n]); break;
                                            case "Normal":
                                                materialList[i].SetTexture(NormalMapName, TextureList[n]);
                                                string path = AssetDatabase.GetAssetPath(TextureList[n]);
                                                TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
                                                importer.textureType = TextureImporterType.NormalMap;

                                                break;
                                            case "Height": materialList[i].SetTexture(HeightName, TextureList[n]); break;
                                            case "AO": materialList[i].SetTexture(OcclusionName, TextureList[n]); break;
                                            case "_EmissionMap": materialList[i].SetTexture(EmissionName, TextureList[n]); break;
                                        }

                                    }
                                    break;

                                case MaterialType.HDRenderPipelineLit:
                                    switch (materialList[i].GetInt("_MaterialID"))
                                    {
                                        case 1:
                                            switch (GetName(GetName(TextureList[n].name)))
                                            {
                                                case "BaseMap": materialList[i].SetTexture(BaseName, TextureList[n]); break;
                                                case "materialListkMap": materialList[i].SetTexture(materialListkMapName, TextureList[n]); break;
                                                case "MaskMap": materialList[i].SetTexture(materialListkMapName, TextureList[n]); break;
                                                case "Normal":
                                                    materialList[i].SetTexture(NormalMapName, TextureList[n]);
                                                    string path = AssetDatabase.GetAssetPath(TextureList[n]);
                                                    TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter; ;
                                                    importer.textureType = TextureImporterType.NormalMap;
                                                    break;
                                            }
                                            break;

                                        case 4:
                                            switch (GetName(GetName(TextureList[n].name)))
                                            {
                                                case "BaseMap": materialList[i].SetTexture(BaseName, TextureList[n]); break;
                                                case "materialListkMap": materialList[i].SetTexture(materialListkMapName, TextureList[n]); break;
                                                //MaskMap
                                                case "MaskMap": materialList[i].SetTexture(materialListkMapName, TextureList[n]); break;
                                                case "Specular": materialList[i].SetTexture("_SpecularColorMap", TextureList[n]); break;
                                                case "Normal":
                                                    materialList[i].SetTexture(NormalMapName, TextureList[n]);
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
            else if (materialList.Count > 0 && TextureList.Count == 0)
                Debug.LogError("��ǰ��ͼ������Ϊ��");
            else if (materialList.Count == 0 && TextureList.Count > 0)
                Debug.LogError("��ǰ�����������Ϊ��");
            else if (materialList.Count == 0 && TextureList.Count == 0)
                Debug.LogError("��ǰ�Ĳ�������������ͼ���ݶ�Ϊ��");
        }

        public void ClearAllTexure()
        {
            ChannelType(RenderingPipelineType);
            if (materialList.Count > 0)
                for (int i = 0; i < materialList.Count; i++)
                {
                    materialList[i].SetTexture(BaseName, null);
                    materialList[i].SetTexture(NormalMapName, null);
                    materialList[i].SetTexture(HeightName, null);
                    materialList[i].SetTexture(OcclusionName, null);
                    materialList[i].SetTexture(EmissionName, null);
                    switch (RenderingPipelineType)
                    {
                        case MaterialType.StandardOrStandardSpecular:
                            if (materialList[i].shader.name == "Standard (Specular setup)")
                                materialList[i].SetTexture("_SpecGlossMap", null);
                            else if (materialList[i].shader.name == "Standard")
                                materialList[i].SetTexture("_MetallicGlossMap", null);
                            break;
                        case MaterialType.UniversalRenderPipelineLit:
                            if (materialList[i].GetFloat("_WorkflowMode") == 0)
                                materialList[i].SetTexture("_SpecGlossMap", null);
                            else if (materialList[i].GetFloat("_WorkflowMode") == 1)
                                materialList[i].SetTexture("_MetallicGlossMap", null);
                            break;
                        case MaterialType.HDRenderPipelineLit:
                            materialList[i].SetTexture("_materialListkMap", null);
                            break;
                    }

                }
            else Debug.LogError("��ǰ����������Ϊ�㣬�����Ҫɾ����ͼ�Ĳ�����");
        }

        public void ClearMaterialData() //�������������
        {
            if (materialList.Count > 0)
                materialList.Clear();
            else
                Debug.LogError("û�в���������");
        }
        public void ClearTexturesData() //���������ͼ����
        {
            if (materialList.Count > 0)
                TextureList.Clear();
            else
                Debug.LogError("û����ͼ�����");
        }
        public void AllTextureWhile()
        {
            SetShader2();
            ChannelType(RenderingPipelineType);
            if (materialList.Count > 0)
            {
                for (int i = 0; i < materialList.Count; i++)
                {
                    materialList[i].SetColor(BaseColorName, Color.white);
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
                    for (int i = 0; i < materialList.Count; i++)
                    {
                        materialList[i].shader = Shader.Find("Standard");
                    };
                    break;

                case MaterialType.UniversalRenderPipelineLit:
                    for (int i = 0; i < materialList.Count; i++)
                    {
                        materialList[i].shader = Shader.Find("Universal Render Pipeline/Lit");
                    };
                    break;
                case MaterialType.HDRenderPipelineLit:
                    for (int i = 0; i < materialList.Count; i++)
                    {
                        materialList[i].shader = Shader.Find("HDRP/Lit");
                    };
                    break;
            }


        }

        public void SetShader()
        {


            if (transitionMode == TransitionMode.BuiltInConversionURP || transitionMode == TransitionMode.HDRPConversionURP)
            {
                for (int i = 0; i < materialList.Count; i++)
                {
                    materialList[i].shader = Shader.Find("Universal Render Pipeline/Lit");
                }
            }
            else if (transitionMode == TransitionMode.BuiltInConversionHDRP || transitionMode == TransitionMode.URPConversionHDRP)
            {
                for (int i = 0; i < materialList.Count; i++)
                {
                    materialList[i].shader = Shader.Find("HDRP/Lit");
                }
            }
            else if(transitionMode == TransitionMode.URPConversionBuiltIn|| transitionMode==TransitionMode.HDRPConversionBuiltIn)
            {
                Debug.Log(materialList.Count);
                for (int i = 0; i < materialList.Count; i++)
                {
                    materialList[i].shader = Shader.Find("Standard");
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
                    //materialListkMapName = "_materialListkMap"; MaskMap
                    materialListkMapName = "_MaskMap";
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
                QualitySettings.renderPipeline = null;
            }
            else
            {
                GraphicsSettings.renderPipelineAsset = renderPipelineAsset;
                QualitySettings.renderPipeline = renderPipelineAsset;
            }
        }

        public void TransitionMap() //ֱ��ת����ͼ
        {
            ChannelType(RenderingPipelineType);
            switch (transitionMode)
            {
                case TransitionMode.HDRPConversionBuiltIn:
                    for (int i = 0; i < materialList.Count; i++)
                    {
                        Texture basemap = materialList[i].GetTexture(BaseName);
                        Texture normalmap = materialList[i].GetTexture(NormalMapName);
                        materialList[i].shader = GetShader(transitionMode);
                        materialList[i].SetTexture("_BumpMap", normalmap);
                        materialList[i].SetTexture("_MainTex", basemap);
                    }
                    break;

                case TransitionMode.URPConversionBuiltIn:
                    for (int i = 0; i < materialList.Count; i++)
                    {
                        Texture basemap = materialList[i].GetTexture(BaseName);
                        Texture normalmap = materialList[i].GetTexture(NormalMapName);
                        materialList[i].shader = GetShader(transitionMode);
                        materialList[i].SetTexture("_BumpMap", normalmap);
                        materialList[i].SetTexture("_MainTex", basemap);
                    }
                    break;

                case TransitionMode.HDRPConversionURP:

                    for (int i = 0; i < materialList.Count; i++)
                    {
                        Texture basemap = materialList[i].GetTexture(BaseName);
                        Texture normalmap = materialList[i].GetTexture(NormalMapName);
                        materialList[i].shader = GetShader(transitionMode);
                        materialList[i].SetTexture("_BumpMap", normalmap);
                        materialList[i].SetTexture("_BaseMap", basemap);
                    }

                    break;
                case TransitionMode.BuiltInConversionURP:
                    for (int i = 0; i < materialList.Count; i++)
                    {
                        Texture basemap = materialList[i].GetTexture(BaseName);
                        Texture normalmap = materialList[i].GetTexture(NormalMapName);
                        materialList[i].shader = GetShader(transitionMode);
                        materialList[i].SetTexture("_BumpMap", normalmap);
                        materialList[i].SetTexture("_BaseMap", basemap);
                    }
                    break;
                case TransitionMode.BuiltInConversionHDRP:
                    for (int i = 0; i < materialList.Count; i++)
                    {
                        Texture basemap = materialList[i].GetTexture(BaseName);
                        Texture normalmap = materialList[i].GetTexture(NormalMapName);
                        materialList[i].shader = GetShader(transitionMode);
                        materialList[i].SetTexture("_NormalMap", normalmap);
                        materialList[i].SetTexture("_BaseColorMap", basemap);
                    }
                    break;
                case TransitionMode.URPConversionHDRP:
                    for (int i = 0; i < materialList.Count; i++)
                    {
                        Texture basemap = materialList[i].GetTexture(BaseName);
                        Texture normalmap = materialList[i].GetTexture(NormalMapName);
                        materialList[i].shader = GetShader(transitionMode);
                        materialList[i].SetTexture("_NormalMap", normalmap);
                        materialList[i].SetTexture("_BaseColorMap", basemap);
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
