using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HLVR.EventSystems;

namespace HLVR.AudioSystem 
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioEvent : MonoBehaviour
    {
        AudioEvent ae;
        [Range(0,100)]
        public float PlayTimeValue;
        public AudioSource m_audio;
        public AudioClip clip;
        [Tooltip("如果clip的值不为空，将在给AU赋值后执行音频播放功能")]
        public bool m_IsPlay=true;
        public bool m_IsLoop;
        public bool m_OpenDebug;
        [Header("当音频播放一开始执行一次此事件")]
        public InteractionEvent m_StartEvent;

        [Header("当音频播放达到指定百分比调用一次此事件，0%-100%[PlayTime]")]
        [Range(0f, 100f)]
        public float m_PlayTime;
        public InteractionEvent m_PlayTimeEvent;

        [Header("播放完音频是否重置数据")]
        public bool m_PlayFinishResetData;

        [Header("当音频播放完成后调用一次此事件")]
        public InteractionEvent m_FinishEvent;
        [Tooltip("当音频播放完成后关闭自身")]
        public bool m_FinishCloseThis=true;

        [Header("当音频播放完成后延迟一定事件调用一次此事件")]
        public InteractionEvent m_FinishDelayEvent;
        public float m_DelayTime;
        [Tooltip("当音频播放完成后关闭自身")]
        public bool m_FinishDelayCloseThis;

        bool starPlayOnce = true;
        bool percentumOnce = true;
        bool finishOnce = true;
        bool delay;
        float tiemr;
        private void Awake()
        {
            if (m_audio == null)
                m_audio = this.GetComponent<AudioSource>();
            if (m_IsLoop) m_audio.loop = true;
            ae = GetComponent<AudioEvent>();
        }


        public void ResetData()
        {
            starPlayOnce = true;
            percentumOnce = true;
            finishOnce = true;
            delay = false;
            tiemr = 0;
        }


        public void Stop()
        {
            m_audio.Stop();
        }

        public void Play()
        { 
           m_audio.Play();  
        }

        public void Pause() 
        {
           m_audio.Pause();
        }

        public void ClearFinishEvent()
        {
            m_FinishEvent.RemoveAllListeners();
        }
        private void OnDisable()
        {
            starPlayOnce = true;
            percentumOnce = true;
            finishOnce = true;
            delay = false;
            tiemr = 0;
        }

        private void OnEnable()
        {
            starPlayOnce = true;
            percentumOnce = true;
            finishOnce = true;
            delay = false;
            tiemr = 0;
            AudioEventManager.Instance?.CloseOther(ae);
            if (clip != null && m_audio.clip == null)
            {
                m_audio.clip = clip;
                if (m_IsPlay) m_audio.Play();
            }
        }
        private void Update()
        {
            PlayTimeValue = m_audio.time;

            if (m_audio.isPlaying && starPlayOnce)
            {
                m_StartEvent?.Invoke();
                starPlayOnce = false;
                if (m_OpenDebug)
                    Debug.Log("音频播放开始执行一次");
            }

            if (percentumOnce&& m_audio.clip!=null)
            {
                if (m_audio.time >= (m_audio.clip.length * (m_PlayTime / 100)))
                {

                    m_PlayTimeEvent?.Invoke();
                    percentumOnce = false;
                    if (m_OpenDebug)
                        Debug.Log("音频播放到指定的百分比执行一次");
                }
            }

            if (finishOnce && m_audio.isPlaying == false && !starPlayOnce)
            {

                m_FinishEvent?.Invoke();

                finishOnce = false;
                if (m_FinishCloseThis)
                    this.gameObject.SetActive(false);
                if (m_OpenDebug)
                    Debug.Log("音频播放完执行一次");
                delay = true;

                if (m_PlayFinishResetData)
                    ResetData();
            }


            if (delay)
            {
                tiemr += Time.deltaTime;
                if (tiemr >= m_DelayTime)
                {
                    m_FinishDelayEvent?.Invoke();
                    delay = false;
                    tiemr = 0;

                    if (m_FinishDelayCloseThis)
                        this.gameObject.SetActive(false);

                    if (m_PlayFinishResetData)
                        ResetData();
                }
            }
        }
    }
}


