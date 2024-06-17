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
        [Header("渲染管线标准着色器类型")]
        public MaterialType RenderingPipelineType;
        [Header("着色互转")]
        public TransitionMode transitionMode;
        [Header("材质球纹理贴图数据")]
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
        int SymbolIndex(string str)//获取截断序号                  
        {
            return str.IndexOf("_", 0) + 1;
        }

        string GetName(string texname) //获取名字
        {
            return texname.Remove(0, SymbolIndex(texname));
        }

        [MenuItem("HLVR/Tool/RenderPipeline/MaterialTextureAutomation[创建自动化材质贴图工具]")]
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
                RenderingPipelineType = (MaterialType)EditorGUILayout.EnumPopup("渲染管线标准着色器类型", RenderingPipelineType, GUILayout.Width(300));
                GUILayout.BeginHorizontal();
                using (var horizontalScopes = new GUILayout.VerticalScope(EditorStyles.helpBox, GUILayout.Width(102)))
                {
                    if (GUILayout.Button("设置材质贴图", GUILayout.Width(100), GUILayout.Height(20)))
                    {
                        GetTextrueToMaterialName();
                    }
                    if (GUILayout.Button("颜色重置", GUILayout.Width(100), GUILayout.Height(20)))
                    {
                        AllTextureWhile();
                    }

                    if (GUILayout.Button("清除材质球纹理", GUILayout.Width(100), GUILayout.Height(20)))
                    {
                        ClearAllTexure();
                    }

                }
                using (var horizontalScopes = new GUILayout.VerticalScope(EditorStyles.helpBox, GUILayout.Width(102)))
                {

                    if (GUILayout.Button("清除材质列表", GUILayout.Width(100), GUILayout.Height(20)))
                    {
                        ClearMaterialData();
                    }

                    if (GUILayout.Button("清除贴图列表", GUILayout.Width(100), GUILayout.Height(20)))
                    {
                        ClearTexturesData();
                    }
                }
                GUILayout.EndHorizontal();

            }
            GUILayout.BeginHorizontal();
            using (var horizontalScope = new GUILayout.VerticalScope(EditorStyles.helpBox, GUILayout.Width(102)))
            {

                textureSize = (TextureSize)EditorGUILayout.EnumPopup("贴图大小", textureSize, GUILayout.Width(300));
                platform = (Platform)EditorGUILayout.EnumPopup("平台设置", platform, GUILayout.Width(300));
                ImporterShape = (TextureImporterShape)EditorGUILayout.EnumPopup("贴图类型", ImporterShape, GUILayout.Width(300));
                m_TextureImporterFormat = (TextureImporterFormat)EditorGUILayout.EnumPopup("贴图格式", m_TextureImporterFormat, GUILayout.Width(300));
                if (GUILayout.Button("设置贴图类型", GUILayout.Width(300), GUILayout.Height(20)))
                {
                    SetTextureSize();
                }
                //if (GUILayout.Button("锁定/解锁 面板", GUILayout.Width(100), GUILayout.Height(40)))
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
                    if (GUILayout.Button("转换渲染管线", GUILayout.Width(100), GUILayout.Height(20)))
                    {
                        TransitionRenderPipelineAsset();
                    }
                    if (GUILayout.Button("获取管线文件", GUILayout.Width(100), GUILayout.Height(20)))
                    {
                        renderPipelineAsset=GraphicsSettings.renderPipelineAsset;
                    }
                    GUILayout.EndHorizontal();
                }
                using (var horizontalScope = new GUILayout.VerticalScope(EditorStyles.helpBox, GUILayout.Width(102)))
                {
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("转换着色器", GUILayout.Width(100), GUILayout.Height(20)))
                    {
                        SetShader();
                        
                    }

                    if (GUILayout.Button("转换着色器和贴图", GUILayout.Width(120), GUILayout.Height(20)))
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
            //    EditorGUILayout.HelpBox("列表贴图为空", MessageType.Warning);
            //}
                EditorGUILayout.Space(5);
            EditorGUILayout.BeginHorizontal();
            using (var horizontalScope = new GUILayout.VerticalScope(EditorStyles.helpBox, GUILayout.Width(102))) 
            {
                sc1pos = EditorGUILayout.BeginScrollView(sc1pos, GUILayout.Width(320), GUILayout.Height(300));
                GUILayout.Label("贴图列表");
                EditorGUILayout.PropertyField(TextureListProperty, GUILayout.Width(300));
                EditorGUILayout.EndScrollView();
            }


            using (var horizontalScope = new GUILayout.VerticalScope(EditorStyles.helpBox, GUILayout.Width(102))) 
            {
                sc2pos = EditorGUILayout.BeginScrollView(sc2pos, GUILayout.Width(320), GUILayout.Height(300));
                GUILayout.Label("材质列表");
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
        public void GetTextrueToMaterialName()//获取与材质球名称相对应的材质贴图
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
                Debug.LogError("当前贴图的数据为空");
            else if (materialList.Count == 0 && TextureList.Count > 0)
                Debug.LogError("当前材质球的数据为空");
            else if (materialList.Count == 0 && TextureList.Count == 0)
                Debug.LogError("当前的材质球数据与贴图数据都为空");
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
            else Debug.LogError("当前材质球数据为零，请添加要删除贴图的材质球");
        }

        public void ClearMaterialData() //清除材质球数据
        {
            if (materialList.Count > 0)
                materialList.Clear();
            else
                Debug.LogError("没有材质球可清除");
        }
        public void ClearTexturesData() //清除纹理贴图数据
        {
            if (materialList.Count > 0)
                TextureList.Clear();
            else
                Debug.LogError("没有贴图可清除");
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
                Debug.LogError("没有贴图可清除");


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

        void ChannelType(MaterialType ty) //得到输出通道类型
        {

            switch (ty)
            {
                case MaterialType.StandardOrStandardSpecular:
                    Debug.LogWarning("当前选择的渲染管线为旧版内置管线！");
                    BaseColorName = "_Color";
                    BaseName = "_MainTex";
                    // MetallicOrSpecularName = "_MetallicGlossMap";
                    NormalMapName = "_BumpMap";
                    HeightName = "_ParallaxMap";
                    OcclusionName = "_OcclusionMap";
                    EmissionName = "_EmissionMap";
                    break;

                case MaterialType.UniversalRenderPipelineLit:
                    Debug.LogWarning("当前选择的渲染管线为SRP_URP通用渲染管线！");
                    BaseColorName = "_BaseColor";
                    BaseName = "_BaseMap";
                    // MetallicOrSpecularName = "_MetallicGlossMap";
                    NormalMapName = "_BumpMap";
                    HeightName = "_ParallaxMap";
                    OcclusionName = "_OcclusionMap";
                    EmissionName = "_EmissionMap";
                    break;
                case MaterialType.HDRenderPipelineLit:
                    Debug.LogWarning("当前选择的渲染管线为SRP_HDRP高清渲染管线！");
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

        public void TransitionMap() //直接转换贴图
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
