using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GraphWindow : EditorWindow
{
    [MenuItem("HLVR/Graph/Open Dialogue Graph View")]
    public static void OpenDialogueGraphWindow()
    {
        
        EditorWindow window = GetWindow<GraphWindow>();
        window.titleContent = new GUIContent("Dialogue Graph");
    }

    private GraphViewTest graphViewTest;


    private void OnEnable()
    {
       
        graphViewTest = new GraphViewTest(this);

        // 让graphView铺满整个Editor窗口

        // 把它添加到EditorWindow的可视化Root元素下面
        rootVisualElement.Add(graphViewTest);
    }

    // 关闭窗口时销毁graphView
    private void OnDisable()
    {
        rootVisualElement.Remove(graphViewTest);
    }
}



