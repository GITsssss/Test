using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Video;
using Unity.EditorCoroutines.Editor;
using Application = UnityEngine.Application;
using NPOI.SS.Formula.Functions;

[CustomEditor(typeof(VideoEvent))]
public class VideoEventEditor : Editor
{
    bool showOrigin;
    VideoEvent videoEvent;
    VideoPlayer editorVideoPlayer;
    Texture video;
    EditorCoroutine editorCoroutine;
    float progress;
    bool previewProgress;
    bool openAllLastRunEvent=true;
    public override void OnInspectorGUI()
    {
        videoEvent = (VideoEvent)target;
        serializedObject.Update();

        if (showOrigin = EditorGUILayout.Toggle("չʾԭ����", showOrigin))
            base.OnInspectorGUI();

        using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox)) 
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("��Ƶ������");
            GUILayout.Label("��ƵƬ��");
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("player"), GUIContent.none);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("clip"), GUIContent.none);
            GUILayout.EndHorizontal();
        }

        if (videoEvent.player != null&& videoEvent.clip) 
        {
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
              



                videoEvent.playSpeed = EditorGUILayout.Slider("�����ٶ�", videoEvent.playSpeed, 0, 10);
                GUILayout.BeginHorizontal();
                GUILayout.Label("�Զ�����", GUILayout.Width(100));
                videoEvent.autoplay = EditorGUILayout.Toggle(videoEvent.autoplay);
                GUILayout.Label("����ѭ��", GUILayout.Width(100));
                videoEvent.isloop = EditorGUILayout.Toggle(videoEvent.isloop);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                if (videoEvent.isloop) // isloopClamp
                {
                    GUILayout.Label("����ѭ��", GUILayout.Width(100));
                    videoEvent.isloopClamp = EditorGUILayout.Toggle(videoEvent.isloopClamp);
                    if (videoEvent.isloopClamp)
                    {
                        //GUILayout.Label("ѭ������", GUILayout.Width(100));
                        
                        //EditorGUILayout.LabelField(videoEvent.count.ToString(),GUILayout.Width(40));
                        videoEvent.loopCount = Mathf.Clamp(EditorGUILayout.IntField("ѭ������", videoEvent.loopCount), 1, 1000);
                    }
                    else
                    {
                        GUILayout.Label("����ѭ��");
                    }
                }
                GUILayout.EndHorizontal();


                GUILayout.BeginHorizontal();

                if (!videoEvent.finishDealyClose)
                {
                    GUILayout.Label("����ر�", GUILayout.Width(100));
                    videoEvent.finishClose = EditorGUILayout.Toggle(videoEvent.finishClose);
                }
                else
                {
                    EditorGUILayout.HelpBox("����رպ��ӳٹر�ֻ��ѡ��һ��", MessageType.Warning);
                }


                if (!videoEvent.finishClose)
                {
                    GUILayout.Label("�����ӳٹر�", GUILayout.Width(100));
                    videoEvent.finishDealyClose = EditorGUILayout.Toggle(videoEvent.finishDealyClose);
                }
                else
                {
                    EditorGUILayout.HelpBox("����رպ��ӳٹر�ֻ��ѡ��һ��", MessageType.Warning);
                }

                // GUILayout.Label("�ӳ�ʱ��", GUILayout.Width(100));

               
                GUILayout.EndHorizontal();

                if (videoEvent.isloop && videoEvent.isloopClamp) 
                {
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("������ѭ�������һ��ִ���¼�");
                    openAllLastRunEvent = EditorGUILayout.Toggle(openAllLastRunEvent);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginVertical();
                    using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
                    {
                        if (openAllLastRunEvent)
                        {
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("ѭ�����һ��ִ�п�ʼ�¼�");
                            videoEvent.starlast = EditorGUILayout.Toggle(videoEvent.starlast);
                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("ѭ�����һ��ִ�н����¼�");
                            videoEvent.playtimelast = EditorGUILayout.Toggle(videoEvent.playtimelast);
                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("ѭ�����һ��ִ�в����¼�");
                            videoEvent.finishlast = EditorGUILayout.Toggle(videoEvent.finishlast);
                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();

                            GUILayout.Label("ѭ�����һ��ִ�в����ӳ��¼�");
                            videoEvent.finishDealylast = EditorGUILayout.Toggle(videoEvent.finishDealylast);
                            GUILayout.EndHorizontal();
                        }
                    }

                    GUILayout.EndVertical();
                }
                
               
                videoEvent.delayTime = Mathf.Clamp(EditorGUILayout.FloatField("�ӳ�ʱ��", videoEvent.delayTime), 0, 1000);
                Rect width = EditorGUILayout.GetControlRect(GUILayout.ExpandWidth(true));
                progress = videoEvent.progress;
                EditorGUI.ProgressBar(width, progress, "��ǰ����:" +"["+ (progress*100).ToString("f2") + "%"+"]"+"��ʵʱ��:"+videoEvent.player.time.ToString("F2")+"��");
            }


            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                if (videoEvent.player != null && video != null)
                {
                    Rect r = EditorGUILayout.GetControlRect(GUILayout.Width(200), GUILayout.Height(200));
                    EditorGUI.DrawPreviewTexture(r, video);
                }
                else
                {
                    EditorGUILayout.HelpBox("���Ԥ����Ԥ��һ����Ƶ", MessageType.Info);
                }

                GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
                if (!Application.isPlaying)
                    if (GUILayout.Button("Ԥ����Ƶ", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true)))
                    {

                        if (editorVideoPlayer == null)
                        {
                            GameObject g = new GameObject();
                            g.name = "EditorPreviewVideoPlayer";
                            editorVideoPlayer = g.AddComponent<VideoPlayer>();
                            editorVideoPlayer.clip = videoEvent.clip;
                            //editorVideoPlayer.renderMode = VideoRenderMode.CameraFarPlane;
                            //editorVideoPlayer.targetCamera = Camera.main;
                            editorVideoPlayer.loopPointReached += Stop;
                            editorCoroutine = EditorCoroutineUtility.StartCoroutine(PlayVideoInEditor(), this);
                            if (video == null)
                            {
                                video = new Texture2D(256, 256);
                            }
                        }
                    }
                if (GUILayout.Button("������Ƶ", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true))) 
                {
                    videoEvent.player.Play();   
                }
                GUILayout.EndHorizontal();  
            }



            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_StarEvent"));

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Ԥ������", GUILayout.Width(60)))
                {
                    if (editorVideoPlayer == null)
                    {
                        GameObject g = new GameObject();
                        g.name = "EditorPreviewVideoPlayer";
                        editorVideoPlayer = g.AddComponent<VideoPlayer>();
                        editorVideoPlayer.clip = videoEvent.clip;
                        //editorVideoPlayer.renderMode = VideoRenderMode.CameraFarPlane;
                        //editorVideoPlayer.targetCamera = Camera.main;

                        editorCoroutine = EditorCoroutineUtility.StartCoroutine(PlayVideoInEditor(), this);
                        EditorCoroutineUtility.StartCoroutine(PreviewVideoEditor(), this);
                        if (video == null)
                        {
                            video = new Texture2D(256, 256);
                        }
                    }
                }
                GUILayout.Label("���Ž���(��)", GUILayout.Width(80));
                videoEvent.timeEventpercentage = EditorGUILayout.Slider(videoEvent.timeEventpercentage, 0, (float)videoEvent.clip.length);
                GUILayout.EndHorizontal();
                EditorGUILayout.PropertyField(serializedObject.FindProperty("m_TimeEvent"));
            }


            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_FinishEvent"));//m_FinishDealyEvent
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_FinishDealyEvent"));
        }
       
        serializedObject.ApplyModifiedProperties();
        //EditorApplication.QueuePlayerLoopUpdate();
    }

    void Stop(VideoPlayer player)
    {
        EditorCoroutineUtility.StopCoroutine(editorCoroutine);
        if(editorVideoPlayer!=null)
        DestroyImmediate(editorVideoPlayer.gameObject);
    }
    IEnumerator PlayVideoInEditor()
    {
        while (true)
        {
            if (!editorVideoPlayer.isPlaying)
                editorVideoPlayer.Play();

            video = editorVideoPlayer.texture;
            progress = (float)editorVideoPlayer.time / (float)editorVideoPlayer.clip.length;
            Repaint();
            yield return null;
        }
        
    }

    IEnumerator PreviewVideoEditor() 
    {
        //videoEvent.timeEventpercentage
        yield return new WaitForSecondsRealtime(videoEvent.timeEventpercentage);
        Stop(editorVideoPlayer);
    }
}