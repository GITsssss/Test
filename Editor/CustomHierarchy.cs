
# if UNITY_EDITOR_WIN
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;
using UnityEngine.AI;
using UnityEngine.UI;

public class CustomHierarchy
{


    // 总的开关用于开启或关闭 显示图标以及彩色文字
    public static bool EnableCustomHierarchy = true;
    public static bool EnableCustomHierarchyLabel = true;
  
    static CustomHierarchy()
    {
       
        EditorApplication.hierarchyWindowItemOnGUI += HierarchWindowOnGui;
    }

    // 用于覆盖原有文字的LabelStyle
    private static GUIStyle LabelStyle(Color color)
    {
        var style = new GUIStyle(((GUIStyle)"Label"))
        {
            padding =
            {
                left = EditorStyles.label.padding.left + 17,
                top = EditorStyles.label.padding.top
            },
            normal =
            {
                textColor = color
            }
        };
        return style;
    }

    // 绘制Rect
    private static Rect CreateRect(Rect selectionRect, int index)
    {
        var rect = new Rect(selectionRect);
        rect.x += rect.width - 20 - (20 * index);
        rect.width = 18;
        return rect;
    }

    // 绘制图标
    private static void DrawIcon<T>(Rect rect)
    {
        // 获得Unity内置的图标
        var icon = EditorGUIUtility.ObjectContent(null, typeof(T)).image;
        GUI.Label(rect, icon);
    }

    private static void DrawIcon2<T>(Rect rect)
    {
        // 获得Unity内置的图标
        var icon = Resources.Load("HLVR_Logo") as Texture;
        GUI.Label(rect, icon);
    }

    // 综合以上，根据类型，绘制图标和文字
    private static void DrawRectIcon<T>(Rect selectionRect, GameObject go, Color textColor, ref int order, ref GUIStyle style) where T : Component
    {
        //if (go.GetComponent<T>())
        if (go.HasComponent<T>(false)) // 使用扩展方法HasComponent
        {
            // 绘制新的Label覆盖原有名字
            if (EnableCustomHierarchyLabel)
            {
                // 字体样式
                style = LabelStyle(textColor);
            }

            // 图标的绘制排序
            order += 1;
            var rect = CreateRect(selectionRect, order);

            // 绘制图标
            DrawIcon<T>(rect);
        }
    }

    private static void DrawRectIcon2<T>(GameObject go, Color textColor, ref int order, ref GUIStyle style) where T : Component
    {
        //if (go.GetComponent<T>())
        if (go.HasComponent<T>(false)) // 使用扩展方法HasComponent
        {
            // 绘制新的Label覆盖原有名字
            if (EnableCustomHierarchyLabel)
            {
                // 字体样式
                style = LabelStyle(textColor);
            }

            // 图标的绘制排序
            order += 1;
            
            var rect = CreateRect(new Rect(1,1,100,100), order);

            // 绘制图标
            DrawIcon2<T>(rect);
        }
    }

    // 绘制Hiercrch
    static void HierarchWindowOnGui(int instanceId, Rect selectionRect)
    {
        if (!EnableCustomHierarchy) return;
        try
        {
            // CheckBox // -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --
            var rectCheck = new Rect(selectionRect);
            rectCheck.x += rectCheck.width - 20;
            rectCheck.width = 18;

            // 通过ID获得Obj
            var obj = EditorUtility.InstanceIDToObject(instanceId);
            var go = (GameObject)obj;// as GameObject;

            // 绘制Checkbox 
            go.SetActive(GUI.Toggle(rectCheck, go.activeSelf, string.Empty));

            // -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- 
            // 图标的序列号
            var index = 0;
            GUIStyle style = null;

            // is Static 
            if (go.isStatic)
            {
                index += 1;
                var rectIcon = CreateRect(selectionRect, index);
                GUI.Label(rectIcon, "S");
            }

            // 文字颜色定义 
            var colorMesh = new Color(42 / 255f, 210 / 255f, 235 / 255f);
            var colorSkinMesh = new Color(0.78f, 0.35f, 0.78f);
            var colorLight = new Color(251 / 255f, 244 / 255f, 124 / 255f);
            var colorPhysic = new Color(0.35f, 0.75f, 0f);
            var colorCollider = new Color(0.35f, 0.75f, 0.196f);
            var colorAnimation = new Color(175 / 255f, 175 / 255f, 218 / 255f);
            var colorCamera = new Color(111 / 255f, 121 / 255f, 212 / 255f);
            var colorParticle = new Color(130 / 255f, 124 / 255f, 251 / 255f);
            var colorNav = new Color(217 / 255f, 80 / 255f, 62 / 255f);
            var colorAudio = new Color(255 / 255f, 126 / 255f, 0f);
             
            var colorGraphic = new Color(255 / 255f, 0f, 0f);

            // Draw //  -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- 
            // 可以在此修改，根据需要删减需要绘制的内容
            // Renderer
            DrawRectIcon<MeshRenderer>(selectionRect, go, colorMesh, ref index, ref style);
            DrawRectIcon<SkinnedMeshRenderer>(selectionRect, go, colorSkinMesh, ref index, ref style);
            //HLVR
          //  DrawRectIcon2<InteractionRay>(go,Color.yellow, ref index, ref style);
            // Colliders
            DrawRectIcon<BoxCollider>(selectionRect, go, colorCollider, ref index, ref style);
            DrawRectIcon<SphereCollider>(selectionRect, go, colorCollider, ref index, ref style);
            DrawRectIcon<CapsuleCollider>(selectionRect, go, colorCollider, ref index, ref style);
            DrawRectIcon<MeshCollider>(selectionRect, go, colorCollider, ref index, ref style);
            DrawRectIcon<CharacterController>(selectionRect, go, colorCollider, ref index, ref style);
            // RigidBody
            DrawRectIcon<Rigidbody>(selectionRect, go, colorPhysic, ref index, ref style);
            // Lights
            DrawRectIcon<Light>(selectionRect, go, colorLight, ref index, ref style);
            // Joints

            // Animation / Animator
            DrawRectIcon<Animator>(selectionRect, go, colorAnimation, ref index, ref style);
            DrawRectIcon<Animation>(selectionRect, go, colorAnimation, ref index, ref style);
            // Camera / Projector
            DrawRectIcon<Camera>(selectionRect, go, colorCamera, ref index, ref style);
            DrawRectIcon<Projector>(selectionRect, go, colorCamera, ref index, ref style);
            // NavAgent
            DrawRectIcon<NavMeshAgent>(selectionRect, go, colorNav, ref index, ref style);
            DrawRectIcon<NavMeshObstacle>(selectionRect, go, colorNav, ref index, ref style);
            // Network
            // Particle
            DrawRectIcon<ParticleSystem>(selectionRect, go, colorParticle, ref index, ref style);
            // Audio
            DrawRectIcon<AudioSource>(selectionRect, go, colorAudio, ref index, ref style);

            DrawRectIcon<Image>(selectionRect, go, colorGraphic, ref index, ref style);
            DrawRectIcon<RawImage>(selectionRect, go, colorGraphic, ref index, ref style);
            DrawRectIcon<Text>(selectionRect, go, colorGraphic, ref index, ref style);

            // 绘制Label来覆盖原有的名字
            if (style != null && go.activeInHierarchy)
            {
                var labelRect = new Rect(selectionRect);
                // labelRect.x = labelRect.x + 16;
                // labelRect.y = labelRect.y - 1;
                GUI.Label(labelRect, go.name, style);
            }
        }
        catch (Exception)
        {
        }
    }
}

public static class ExtensionMethods
{
    /// <summary>
    /// 检测是否含有组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="go"></param>
    /// <param name="checkChildren">是否检测子层级</param>
    /// <returns></returns>
    public static bool HasComponent<T>(this GameObject go, bool checkChildren) where T : Component
    {
        if (!checkChildren)
        {
            return go.GetComponent<T>();
        }
        else
        {
            return go.GetComponentsInChildren<T>().FirstOrDefault() != null;
        }
    }
}

#endif
// EndScript // 