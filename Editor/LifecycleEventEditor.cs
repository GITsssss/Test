using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using HLVR.EventSystems;
using System;

[CustomEditor(typeof(LifecycleEvent))]
public class LifecycleEventEditor : Editor
{
    bool showOrigin;
    string ty = "";
    public OnEnableLifeEvent oele;
    GUIStyle font = new GUIStyle();
    [Flags]
    public enum OnEnableLifeEvent
    {
      AwakeEvent=2,
      StartEvent=4,
      OnEnableEvent=8,
      OnDisableEvent=16,
      OnDestroyEvent=32,
    }

    private void OnEnable()
    {
        font.fontSize = 20;
        font.fontStyle = FontStyle.Bold;
        font.normal.textColor = Color.blue;
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        if (showOrigin=EditorGUILayout.Toggle("绘制源属性",showOrigin))
        base.OnInspectorGUI();
        LifecycleEvent lifecycleEvent = (LifecycleEvent)target;

       // EditorGUILayout.LabelField("Script life cycle 脚本生命周期事件 ", font, GUILayout.Height(50)) ;

        using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox)) 
        {

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("执行类型" + ty, GUILayout.Width(110));

            lifecycleEvent.m_ExecuteType = (LifecycleEvent.ExecuteType)EditorGUILayout.EnumPopup(lifecycleEvent.m_ExecuteType);

            GUILayout.EndHorizontal();

            switch (lifecycleEvent.m_ExecuteType)
            {
                case LifecycleEvent.ExecuteType.Delay:
                    ty = "：延迟执行";
                    lifecycleEvent.m_DelayTime = Mathf.Clamp(EditorGUILayout.FloatField("延迟时间", lifecycleEvent.m_DelayTime), 0, 1000);
                    break;

                case LifecycleEvent.ExecuteType.RightAway:
                    ty = "：立即执行";
                    break;

            }
            lifecycleEvent.m_FinishEventCloseThis = EditorGUILayout.Toggle("执行完是否关闭本身", lifecycleEvent.m_FinishEventCloseThis, GUILayout.Width(60));
            oele = (OnEnableLifeEvent)EditorGUILayout.EnumFlagsField("启用事件接口", oele);
        }


        if (oele.ToString().Contains("AwakeEvent") || oele.ToString() == "-1") 
        {
            lifecycleEvent.SetEventInvokeState(lifecycleEvent.AwakeEvent, UnityEngine.Events.UnityEventCallState.RuntimeOnly);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("AwakeEvent"), GUILayout.ExpandWidth(true));
        } 
        else 
        {
            lifecycleEvent.SetEventInvokeState(lifecycleEvent.AwakeEvent,UnityEngine.Events.UnityEventCallState.Off);
        }

        if (oele.ToString().Contains("StartEvent") || oele.ToString() == "-1")
        {
            lifecycleEvent.SetEventInvokeState(lifecycleEvent.StartEvent, UnityEngine.Events.UnityEventCallState.RuntimeOnly);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("StartEvent"), GUILayout.ExpandWidth(true));
        }
        else 
        {
            lifecycleEvent.SetEventInvokeState(lifecycleEvent.StartEvent, UnityEngine.Events.UnityEventCallState.Off);
        }

        if (oele.ToString().Contains("OnEnableEvent") || oele.ToString() == "-1")
        {
            lifecycleEvent.SetEventInvokeState(lifecycleEvent.OnEnableEvent, UnityEngine.Events.UnityEventCallState.RuntimeOnly);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("OnEnableEvent"), GUILayout.ExpandWidth(true));
        }
        else 
        {
            lifecycleEvent.SetEventInvokeState(lifecycleEvent.OnEnableEvent, UnityEngine.Events.UnityEventCallState.Off);
        }

        if (oele.ToString().Contains("OnDisableEvent") || oele.ToString() == "-1")
        {
            lifecycleEvent.SetEventInvokeState(lifecycleEvent.OnDisableEvent, UnityEngine.Events.UnityEventCallState.RuntimeOnly);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("OnDisableEvent"), GUILayout.ExpandWidth(true));
        }
        else 
        {
            lifecycleEvent.SetEventInvokeState(lifecycleEvent.OnDisableEvent, UnityEngine.Events.UnityEventCallState.Off);
        }

        if (oele.ToString().Contains("OnDestroyEvent") || oele.ToString() == "-1") 
        {
            lifecycleEvent.SetEventInvokeState(lifecycleEvent.OnDestroyEvent, UnityEngine.Events.UnityEventCallState.RuntimeOnly);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("OnDestroyEvent"), GUILayout.ExpandWidth(true));
        }
        else 
        {
            lifecycleEvent.SetEventInvokeState(lifecycleEvent.OnDestroyEvent, UnityEngine.Events.UnityEventCallState.Off);
        }
           

        serializedObject.ApplyModifiedProperties();
    }
}
