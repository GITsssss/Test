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

        // ��graphView��������Editor����

        // ������ӵ�EditorWindow�Ŀ��ӻ�RootԪ������
        rootVisualElement.Add(graphViewTest);
    }

    // �رմ���ʱ����graphView
    private void OnDisable()
    {
        rootVisualElement.Remove(graphViewTest);
    }
}



