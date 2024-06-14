using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using HLVR.Interaction;
using UnityEngine.UI;
namespace HLVR.EventSystems
{
    [CustomEditor(typeof(InteractionEventElement))]
    public class InteractionEventElementEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            InteractionEventElement interactionUI = (InteractionEventElement)target;
            if (GUILayout.Button("添加/刷新响应链接"))
            {
                interactionUI.AddLinkResponse();
            }
        }
    }

}
