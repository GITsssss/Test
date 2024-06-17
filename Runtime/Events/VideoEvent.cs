using HLVR.EventSystems;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

[RequireComponent(typeof(VideoPlayer))]
public class VideoEvent : MonoBehaviour
{
    public VideoPlayer player;
    public VideoClip clip;
    public float progress;
    public float playSpeed=1f;
    public InteractionEvent m_StarEvent;
    public float timeEventpercentage;
    public InteractionEvent m_TimeEvent;
    public InteractionEvent m_FinishEvent;
    public bool finishClose;
    public bool finishDealyClose;
    public float delayTime;
    public InteractionEvent m_FinishDealyEvent;

    
    public bool starlast;
    public bool playtimelast;
    public bool finishlast;
    public bool finishDealylast;

    public int count=1;
    public bool autoplay;
    public int loopCount;
    public  bool isloop;
    public bool isloopClamp;

    bool starOnce=true;
    bool timeOnce = true;
    Coroutine coroutine;
    Coroutine coroutine_dealy;
    private void Awake()
    {
        if (player == null)
            player = GetComponent<VideoPlayer>();

        if (player != null)
        {
            player.clip = clip;
        }
    }

    private void OnEnable()
    {
        player.isLooping = isloop;
        if(autoplay)
        player.Play();
        player.playbackSpeed = playSpeed;
        player.loopPointReached += PlayVideoFinishEvemt;
    }

    private void OnDisable()
    {
        player.loopPointReached -= PlayVideoFinishEvemt;
    }

    private void Update()
    {
        if (starOnce&&player.isPlaying)
        {
            if (isloop && isloopClamp && starlast)
            {
                if (count == loopCount)
                {

                    m_StarEvent?.Invoke();
                    Debug.Log("开始播放事件");

                }

            }
            else
            {

                m_StarEvent?.Invoke();
                Debug.Log("开始播放事件");

            }

            starOnce = false;
            timeOnce = true;
            //coroutine = StartCoroutine(Fnish());
        }

        progress = (float)player.time / (float)clip.length;

        if (player.time >= timeEventpercentage && timeOnce)
        {
           
            if (isloop && isloopClamp&& playtimelast)
            {
                if (count == loopCount) 
                {
                    m_TimeEvent?.Invoke();
                    Debug.Log("进度事件");
                   
                }
            }
            else 
            {
                m_TimeEvent?.Invoke();
                Debug.Log("进度事件");
                
            }
            timeOnce = false;
        }
    }


 
    void PlayVideoFinishEvemt(VideoPlayer player)
    {
      
        if (isloop && isloopClamp && finishlast)
        {
            if (count == loopCount)
            {
                m_FinishEvent?.Invoke();
                Debug.Log("播放完成事件");
            }
        }
        else 
        {
            m_FinishEvent?.Invoke();
            Debug.Log("播放完成事件");
        }
        
        
        if (isloop && isloopClamp && finishDealylast)
        {
            if (count == loopCount)
            {
                Invoke("Dealy", delayTime);
            }

        }
        else
        {
            Invoke("Dealy", delayTime);
        }
        starOnce = true;
        if (count < loopCount)
            count++;
        else if (count == loopCount)
        { 
          player.Stop();
        }
    }

    void Dealy()
    {
        m_FinishEvent?.Invoke();
        Debug.Log("播放完成延迟事件");
     
    }
}
