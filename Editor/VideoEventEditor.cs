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

        if (showOrigin = EditorGUILayout.Toggle("展示原属性", showOrigin))
            base.OnInspectorGUI();

        using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox)) 
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("视频播放器");
            GUILayout.Label("视频片段");
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
              



                videoEvent.playSpeed = EditorGUILayout.Slider("播放速度", videoEvent.playSpeed, 0, 10);
                GUILayout.BeginHorizontal();
                GUILayout.Label("自动播放", GUILayout.Width(100));
                videoEvent.autoplay = EditorGUILayout.Toggle(videoEvent.autoplay);
                GUILayout.Label("开启循环", GUILayout.Width(100));
                videoEvent.isloop = EditorGUILayout.Toggle(videoEvent.isloop);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                if (videoEvent.isloop) // isloopClamp
                {
                    GUILayout.Label("限制循环", GUILayout.Width(100));
                    videoEvent.isloopClamp = EditorGUILayout.Toggle(videoEvent.isloopClamp);
                    if (videoEvent.isloopClamp)
                    {
                        //GUILayout.Label("循环次数", GUILayout.Width(100));
                        
                        //EditorGUILayout.LabelField(videoEvent.count.ToString(),GUILayout.Width(40));
                        videoEvent.loopCount = Mathf.Clamp(EditorGUILayout.IntField("循环次数", videoEvent.loopCount), 1, 1000);
                    }
                    else
                    {
                        GUILayout.Label("无限循环");
                    }
                }
                GUILayout.EndHorizontal();


                GUILayout.BeginHorizontal();

                if (!videoEvent.finishDealyClose)
                {
                    GUILayout.Label("播完关闭", GUILayout.Width(100));
                    videoEvent.finishClose = EditorGUILayout.Toggle(videoEvent.finishClose);
                }
                else
                {
                    EditorGUILayout.HelpBox("播完关闭和延迟关闭只能选择一个", MessageType.Warning);
                }


                if (!videoEvent.finishClose)
                {
                    GUILayout.Label("播完延迟关闭", GUILayout.Width(100));
                    videoEvent.finishDealyClose = EditorGUILayout.Toggle(videoEvent.finishDealyClose);
                }
                else
                {
                    EditorGUILayout.HelpBox("播完关闭和延迟关闭只能选择一个", MessageType.Warning);
                }

                // GUILayout.Label("延迟时间", GUILayout.Width(100));

               
                GUILayout.EndHorizontal();

                if (videoEvent.isloop && videoEvent.isloopClamp) 
                {
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("激活在循环的最后一次执行事件");
                    openAllLastRunEvent = EditorGUILayout.Toggle(openAllLastRunEvent);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginVertical();
                    using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
                    {
                        if (openAllLastRunEvent)
                        {
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("循环最后一次执行开始事件");
                            videoEvent.starlast = EditorGUILayout.Toggle(videoEvent.starlast);
                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("循环最后一次执行进度事件");
                            videoEvent.playtimelast = EditorGUILayout.Toggle(videoEvent.playtimelast);
                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("循环最后一次执行播完事件");
                            videoEvent.finishlast = EditorGUILayout.Toggle(videoEvent.finishlast);
                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();

                            GUILayout.Label("循环最后一次执行播完延迟事件");
                            videoEvent.finishDealylast = EditorGUILayout.Toggle(videoEvent.finishDealylast);
                            GUILayout.EndHorizontal();
                        }
                    }

                    GUILayout.EndVertical();
                }
                
               
                videoEvent.delayTime = Mathf.Clamp(EditorGUILayout.FloatField("延迟时间", videoEvent.delayTime), 0, 1000);
                Rect width = EditorGUILayout.GetControlRect(GUILayout.ExpandWidth(true));
                progress = videoEvent.progress;
                EditorGUI.ProgressBar(width, progress, "当前进度:" +"["+ (progress*100).ToString("f2") + "%"+"]"+"真实时间:"+videoEvent.player.time.ToString("F2")+"秒");
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
                    EditorGUILayout.HelpBox("点击预览可预览一次视频", MessageType.Info);
                }

                GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
                if (!Application.isPlaying)
                    if (GUILayout.Button("预览视频", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true)))
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
                if (GUILayout.Button("播放视频", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true))) 
                {
                    videoEvent.player.Play();   
                }
                GUILayout.EndHorizontal();  
            }



            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_StarEvent"));

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("预览进度", GUILayout.Width(60)))
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
                GUILayout.Label("播放进度(秒)", GUILayout.Width(80));
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