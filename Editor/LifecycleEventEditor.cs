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
        if (showOrigin=EditorGUILayout.Toggle("����Դ����",showOrigin))
        base.OnInspectorGUI();
        LifecycleEvent lifecycleEvent = (LifecycleEvent)target;

       // EditorGUILayout.LabelField("Script life cycle �ű����������¼� ", font, GUILayout.Height(50)) ;

        using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox)) 
        {

            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("ִ������" + ty, GUILayout.Width(110));

            lifecycleEvent.m_ExecuteType = (LifecycleEvent.ExecuteType)EditorGUILayout.EnumPopup(lifecycleEvent.m_ExecuteType);

            GUILayout.EndHorizontal();

            switch (lifecycleEvent.m_ExecuteType)
            {
                case LifecycleEvent.ExecuteType.Delay:
                    ty = "���ӳ�ִ��";
                    lifecycleEvent.m_DelayTime = Mathf.Clamp(EditorGUILayout.FloatField("�ӳ�ʱ��", lifecycleEvent.m_DelayTime), 0, 1000);
                    break;

                case LifecycleEvent.ExecuteType.RightAway:
                    ty = "������ִ��";
                    break;

            }
            lifecycleEvent.m_FinishEventCloseThis = EditorGUILayout.Toggle("ִ�����Ƿ�رձ���", lifecycleEvent.m_FinishEventCloseThis, GUILayout.Width(60));
            oele = (OnEnableLifeEvent)EditorGUILayout.EnumFlagsField("�����¼��ӿ�", oele);
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
