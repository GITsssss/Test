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

        //按照父级的宽高全屏填充
        this.StretchToParentSize();
        // 允许对Graph进行Zoom in/out
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        // 允许拖拽Content
        this.AddManipulator(new ContentDragger());
        // 允许Selection里的内容
        this.AddManipulator(new SelectionDragger());
        // GraphView允许进行框选
        this.AddManipulator(new RectangleSelector());
        AddElement(new DialogueNode());
    }
}

public class DialogueNode : Node
{
    public float Speed;
    public DialogueNode()
    {
        title = "人物移动速度";
    }

}
