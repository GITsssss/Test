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
        [Tooltip("���clip��ֵ��Ϊ�գ����ڸ�AU��ֵ��ִ����Ƶ���Ź���")]
        public bool m_IsPlay=true;
        public bool m_IsLoop;
        public bool m_OpenDebug;
        [Header("����Ƶ����һ��ʼִ��һ�δ��¼�")]
        public InteractionEvent m_StartEvent;

        [Header("����Ƶ���Ŵﵽָ���ٷֱȵ���һ�δ��¼���0%-100%[PlayTime]")]
        [Range(0f, 100f)]
        public float m_PlayTime;
        public InteractionEvent m_PlayTimeEvent;

        [Header("��������Ƶ�Ƿ���������")]
        public bool m_PlayFinishResetData;

        [Header("����Ƶ������ɺ����һ�δ��¼�")]
        public InteractionEvent m_FinishEvent;
        [Tooltip("����Ƶ������ɺ�ر�����")]
        public bool m_FinishCloseThis=true;

        [Header("����Ƶ������ɺ��ӳ�һ���¼�����һ�δ��¼�")]
        public InteractionEvent m_FinishDelayEvent;
        public float m_DelayTime;
        [Tooltip("����Ƶ������ɺ�ر�����")]
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
                    Debug.Log("��Ƶ���ſ�ʼִ��һ��");
            }

            if (percentumOnce&& m_audio.clip!=null)
            {
                if (m_audio.time >= (m_audio.clip.length * (m_PlayTime / 100)))
                {

                    m_PlayTimeEvent?.Invoke();
                    percentumOnce = false;
                    if (m_OpenDebug)
                        Debug.Log("��Ƶ���ŵ�ָ���İٷֱ�ִ��һ��");
                }
            }

            if (finishOnce && m_audio.isPlaying == false && !starPlayOnce)
            {

                m_FinishEvent?.Invoke();

                finishOnce = false;
                if (m_FinishCloseThis)
                    this.gameObject.SetActive(false);
                if (m_OpenDebug)
                    Debug.Log("��Ƶ������ִ��һ��");
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


