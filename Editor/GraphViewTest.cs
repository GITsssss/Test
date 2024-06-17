using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor;

public class GraphViewTest : GraphView
{
    public GraphViewTest(EditorWindow editorWindow)
    {

        //���ո����Ŀ��ȫ�����
        this.StretchToParentSize();
        // �����Graph����Zoom in/out
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        // ������קContent
        this.AddManipulator(new ContentDragger());
        // ����Selection�������
        this.AddManipulator(new SelectionDragger());
        // GraphView������п�ѡ
        this.AddManipulator(new RectangleSelector());
        AddElement(new DialogueNode());
    }
}

public class DialogueNode : Node
{
    public float Speed;
    public DialogueNode()
    {
        title = "�����ƶ��ٶ�";
    }

}
