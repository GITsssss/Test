using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using HLVR.AudioSystem;
using Unity.EditorCoroutines.Editor;


[CustomEditor(typeof(AudioEvent))]
public class AudioEventEditor : Editor
{
    AudioEvent ae;
    bool showBase;

    bool Foldou;
    bool openLastAllEvent;
    AudioSource source;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        if (showBase=GUILayout.Toggle(showBase,GUIContent.none))
        base.OnInspectorGUI();
        ae= (AudioEvent)target;
        EditorGUILayout.BeginHorizontal();

        using (new EditorGUILayout.HorizontalScope(EditorStyles.helpBox))
        {
   
            EditorGUILayout.LabelField("DebugInfo",GUILayout.Width(50));
            ae.m_OpenDebug= EditorGUILayout.Toggle(GUIContent.none,ae.m_OpenDebug,GUILayout.Width(20));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_audio"), GUIContent.none, GUILayout.ExpandWidth(true));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("clip"), GUIContent.none, GUILayout.ExpandWidth(true));
        }

        EditorGUILayout.EndHorizontal();

        if (ae.m_audio != null)
        {

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox)) 
            {
                using (new EditorGUILayout.HorizontalScope(EditorStyles.helpBox))
                {
                    ae.m_IsPlay = EditorGUILayout.Toggle("�Զ�����", ae.m_IsPlay);
                    ae.m_IsLoop = EditorGUILayout.Toggle("ѭ������", ae.m_IsLoop);
                }
                if (ae.m_IsLoop)
                {
                    using (new EditorGUILayout.HorizontalScope(EditorStyles.helpBox))
                    {
                        ae.loopCountClamp = EditorGUILayout.Toggle("ѭ������", ae.loopCountClamp, GUILayout.ExpandWidth(true));
                        if (ae.loopCountClamp)
                            ae.countPlay = Mathf.Clamp(EditorGUILayout.IntField("���ƴ���", ae.countPlay, GUILayout.ExpandWidth(true)), 1, 100);
                        else EditorGUILayout.LabelField("����ѭ��");
                    }



                    if (ae.loopCountClamp)
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.Space(10);
                        Rect ff = EditorGUILayout.GetControlRect(true, GUILayout.ExpandWidth(true));
                        Foldou = EditorGUI.Foldout(ff, Foldou, "�Ƿ�����ѭ�����һ��ִ���¼�");
                        openLastAllEvent = GUILayout.Button("��������");
                        if (openLastAllEvent)
                        {
                            Foldou = true;
                        }
                        GUILayout.EndHorizontal();
                        if (Foldou)
                        {
                            using (new EditorGUILayout.HorizontalScope(EditorStyles.helpBox, GUILayout.ExpandHeight(true)))
                            {
                                GUILayout.BeginVertical();

                                GUILayout.BeginHorizontal();
                                EditorGUILayout.LabelField("����StartEventѭ�����һ��ִ��");
                                GUILayout.Space(30);
                                ae.openStartEventlastInvoke = EditorGUILayout.Toggle(ae.openStartEventlastInvoke, GUILayout.ExpandWidth(true));
                                GUILayout.EndHorizontal();
                                GUILayout.Space(10);
                                GUILayout.BeginHorizontal();
                                EditorGUILayout.LabelField("����PlayTimeEventѭ�����һ��ִ��");
                                GUILayout.Space(30);
                                ae.openPlayTimeEventlastInvoke = EditorGUILayout.Toggle(ae.openPlayTimeEventlastInvoke, GUILayout.ExpandWidth(true));
                                GUILayout.EndHorizontal();
                             
                                GUILayout.Space(10);
                                
                                GUILayout.BeginHorizontal();
                                EditorGUILayout.LabelField("����FinishEventѭ�����һ��ִ��");
                                GUILayout.Space(30);
                                ae.openFinishEventlastInvoke = EditorGUILayout.Toggle(ae.openFinishEventlastInvoke, GUILayout.ExpandWidth(true));
                                GUILayout.EndHorizontal();


                                GUILayout.Space(10);

                                GUILayout.BeginHorizontal();
                                EditorGUILayout.LabelField("����FinishDelayEventѭ�����һ��ִ��");
                                GUILayout.Space(30);
                                ae.openFinishDelayEventlastInvoke = EditorGUILayout.Toggle(ae.openFinishDelayEventlastInvoke, GUILayout.ExpandWidth(true));
                                GUILayout.EndHorizontal();
                                GUILayout.EndVertical();
                            }
                        }
                    }

                    if (ae.loopCountClamp)
                    {
                        if (openLastAllEvent)
                        {
                            ae.openStartEventlastInvoke = true;
                            ae.openPlayTimeEventlastInvoke = true;
                            ae.openFinishEventlastInvoke = true;
                            ae.openFinishDelayEventlastInvoke = true;
                        }
                    }
                    else
                    {
                        openLastAllEvent = false;
                        ae.openStartEventlastInvoke = false;
                        ae.openPlayTimeEventlastInvoke = false;
                        ae.openFinishEventlastInvoke = false;
                        ae.openFinishDelayEventlastInvoke = false;
                    }
                }
                else 
                {
                    openLastAllEvent = false;
                    ae.openStartEventlastInvoke = false;
                    ae.openPlayTimeEventlastInvoke = false;
                    ae.openFinishEventlastInvoke = false;
                    ae.openFinishDelayEventlastInvoke = false;
                }

            }



           
            using (new EditorGUILayout.HorizontalScope(EditorStyles.helpBox))
            {
                // ae.m_PlayFinishResetData = EditorGUILayout.Toggle("��������", ae.m_PlayFinishResetData);
                if (!ae.m_FinishDelayCloseThis)
                    ae.m_FinishCloseThis = EditorGUILayout.Toggle("����ر�", ae.m_FinishCloseThis);
                else EditorGUILayout.HelpBox("�����ӳ��벥��ֻ��ѡ��һ��", MessageType.Info);
            }

           
            using (new EditorGUILayout.HorizontalScope(EditorStyles.helpBox))
            {
                if (!ae.m_FinishCloseThis)
                    ae.m_FinishDelayCloseThis = EditorGUILayout.Toggle("�����ӳٹر�", ae.m_FinishDelayCloseThis);
                else EditorGUILayout.HelpBox("ע��ע�ᵽFinishDelayEvent�е��¼����ܲ���ִ�У�", MessageType.Warning);
                ae.m_DelayTime = Mathf.Clamp(EditorGUILayout.FloatField("�ӳ�ʱ��(��)", ae.m_DelayTime), 0, 10000);
            }
         



            EditorGUILayout.BeginHorizontal();

        
            if (ae.clip != null) 
            {
                using (new EditorGUILayout.HorizontalScope(EditorStyles.helpBox)) 
                {
                    GUILayout.BeginVertical();
                    EditorGUILayout.LabelField(ae.PlayTimeValue.ToString("f2") + "/" + ae.clip.length.ToString(), GUILayout.Width(50));
                    Rect width = EditorGUILayout.GetControlRect(GUILayout.ExpandWidth(true));
                    EditorGUI.ProgressBar(width, (ae.PlayTimeValue * 1f) / (ae.clip.length * 1f), "��ǰ����");
                    Color bc = GUI.backgroundColor;
                    GUI.backgroundColor = Color.green;
                    Rect width0 = EditorGUILayout.GetControlRect(false,10f, GUILayout.ExpandWidth(true));
                    EditorGUI.ProgressBar(width0, (ae.m_PlayTime*1f)/100f, "");
                    GUI.backgroundColor = bc* Color.white;
                    GUILayout.EndVertical();
                }
             
            }
            else
            {
                EditorGUILayout.HelpBox("��ǰ�����AudioClipΪ�գ�", MessageType.Warning,true);
            }

            EditorGUILayout.EndHorizontal();



            float height;
            if (ae.m_StartEvent.GetPersistentEventCount() <= 1)
                height = 115;
            else height = ae.m_StartEvent.GetPersistentEventCount() * 60 + 55;
            Rect width1 = EditorGUILayout.GetControlRect(GUILayout.ExpandWidth(true), GUILayout.Height(height));
            EditorGUI.PropertyField(width1, serializedObject.FindProperty("m_StartEvent"));


            float height2;
            if (ae.m_PlayTimeEvent.GetPersistentEventCount() <= 1)
                height2 = 115;
            else height2 = ae.m_PlayTimeEvent.GetPersistentEventCount() * 55 + 55;
            Rect width22 = EditorGUILayout.GetControlRect(GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true), GUILayout.MinHeight(50));


            GUILayout.Label("Audio plays to a specified percentage");
            GUILayout.BeginHorizontal();
            using (new EditorGUILayout.HorizontalScope(EditorStyles.helpBox))
            {
               

                if (ae.clip != null) 
                {
                    if (GUILayout.Button("����", GUILayout.Width(40)))
                    {
                        source = EditorUtility.CreateGameObjectWithHideFlags("Audio Player", HideFlags.HideAndDontSave).AddComponent<AudioSource>();
                        source.clip = ae.clip;
                        if (!source.isPlaying) 
                        {
                            source.Play();
                            EditorCoroutineUtility.StartCoroutine(DesPlay(),this);
                        }
                           
                    }
                 
                  
                }
                EditorApplication.QueuePlayerLoopUpdate();
                GUILayout.BeginVertical();
                Color bc = GUI.backgroundColor;
                GUI.backgroundColor = Color.yellow;
                Rect width0v = EditorGUILayout.GetControlRect(false, 5f, GUILayout.ExpandWidth(true));
               if(source!=null)
                EditorGUI.ProgressBar(width0v, (source.time * 1f) / (source.clip.length * 1f), "");
                GUI.backgroundColor = bc * Color.white;
                //  EditorGUI.PropertyField(width22, serializedObject.FindProperty("m_PlayTime"));

                ae.m_PlayTime = GUILayout.HorizontalSlider(ae.m_PlayTime, 0, 100);
                GUILayout.EndVertical();
                EditorGUILayout.LabelField("[" + ae.m_PlayTime.ToString("f1") + "%" + "]", GUILayout.Width(50));
               
            }

            GUILayout.EndHorizontal();

            Rect width2 = EditorGUILayout.GetControlRect(GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(false), GUILayout.Height(height2));
            EditorGUI.PropertyField(width2, serializedObject.FindProperty("m_PlayTimeEvent"));

            float height3;
            if (ae.m_FinishEvent.GetPersistentEventCount() <= 1)
                height3 = 115;
            else height3 = ae.m_FinishEvent.GetPersistentEventCount() * 60 + 55;
            Rect width3 = EditorGUILayout.GetControlRect(GUILayout.ExpandWidth(true), GUILayout.Height(height3));
            EditorGUI.PropertyField(width3, serializedObject.FindProperty("m_FinishEvent"));


            float height4;
            if (ae.m_FinishEvent.GetPersistentEventCount() <= 1)
                height4 = 115;
            else height4 = ae.m_FinishEvent.GetPersistentEventCount() * 60 + 55;
            Rect width4 = EditorGUILayout.GetControlRect(GUILayout.ExpandWidth(true), GUILayout.Height(height4));
            EditorGUI.PropertyField(width4, serializedObject.FindProperty("m_FinishDelayEvent"));
        }
       
      
        

        serializedObject.ApplyModifiedProperties(); 
    }


    IEnumerator DesPlay()
    {

       float time= source.clip.length*((ae.m_PlayTime * 1f) / 100f)*1f;
        yield return new WaitForSecondsRealtime(time);
        DestroyImmediate(source);
    }
}
