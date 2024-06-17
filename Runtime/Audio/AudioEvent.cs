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
        public float PlayTimeValue;
        public AudioSource m_audio;
        public AudioClip clip;
        [Tooltip("���clip��ֵ��Ϊ�գ����ڸ�AU��ֵ��ִ����Ƶ���Ź���")]
        public bool m_IsPlay=true;
        public bool m_IsLoop;
        public bool m_OpenDebug;
        [Header("Audio Play Star Invoke Once")]
        public InteractionEvent m_StartEvent;

        [Header("����Ƶ���Ŵﵽָ���ٷֱȵ���һ�δ��¼���0%-100%[PlayTime]")]
        [Range(0f, 100f)]
        public float m_PlayTime;
        public InteractionEvent m_PlayTimeEvent;

        [Header("��������Ƶ�Ƿ���������")]
        public bool m_PlayFinishResetData;

        [Header("Audio Play Finish Invoke Once")]
        public InteractionEvent m_FinishEvent;
        [Tooltip("����Ƶ������ɺ�ر�����")]
        public bool m_FinishCloseThis=true;

        [Header("Audio Play Finish delay Invoke Once")]
        public InteractionEvent m_FinishDelayEvent;

       
        
        public float m_DelayTime;
        [Tooltip("����Ƶ������ɺ�ر�����")]
        public bool m_FinishDelayCloseThis;
        [Tooltip("������Ƶ���ų�ʼ���һ�β��ŵ���")]
        public bool openStartEventlastInvoke;
        [Tooltip("����ѭ���������һ�β��ŵ���")]
        public bool openPlayTimeEventlastInvoke;
        [Tooltip("����ѭ���������һ�β��ŵ���")]
        public bool openFinishEventlastInvoke;
        [Tooltip("����ѭ���������һ�β��ŵ���")]
        public bool openFinishDelayEventlastInvoke;

        [Tooltip("����ѭ�����ŵĴ���")]
        public bool loopCountClamp;
        [Tooltip("ѭ�����ŵĴ���")]
        public int countPlay=1;

        public AudioPlayLastOnce[] audioPlayLastOnces;  

        bool starPlayOnce = true;
        bool percentumOnce = true;
        bool finishOnce = false;
        int count=1;
        bool delay;
        float tiemr;
        Coroutine playCountIE;
        private void Reset()
        {
            m_audio = this.GetComponent<AudioSource>();
        }
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
            //finishOnce = true;
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

                
                starPlayOnce = false;
                       
                if (m_IsLoop && loopCountClamp&&openStartEventlastInvoke) 
                {
                    if (count == countPlay) 
                    {
                        m_StartEvent?.Invoke();
                        if (m_OpenDebug)
                            Debug.Log("��Ƶ���ſ�ʼִ��һ��");
                    }
              
                }
                else 
                {
                    m_StartEvent?.Invoke();
                    if (m_OpenDebug)
                        Debug.Log("��Ƶ���ſ�ʼִ��һ��");
                }
                playCountIE = StartCoroutine("PlayerCount");
            }



            if (percentumOnce && m_audio.clip != null)
            {
                if (m_audio.time >= (m_audio.clip.length * (m_PlayTime / 100)))
                {

                  
                    if (m_IsLoop && loopCountClamp&&openPlayTimeEventlastInvoke)
                    {
                        if (count == countPlay)
                        {
                            m_PlayTimeEvent?.Invoke();
                            percentumOnce = false;
                            if (m_OpenDebug)
                                Debug.Log("��Ƶ���ŵ�ָ���İٷֱ�ִ��һ��");
                        }

                    }
                    else
                    {
                        m_PlayTimeEvent?.Invoke();
                        percentumOnce = false;
                        if (m_OpenDebug)
                            Debug.Log("��Ƶ���ŵ�ָ���İٷֱ�ִ��һ��");
                    }
                  
                }
            }

            /// Debug.Log(m_audio.time+"///"+ m_audio.clip.length);

            if (m_IsLoop)
            {
                if (finishOnce&&!starPlayOnce)
                {
                    if (loopCountClamp)
                    {
                        if (ae.openFinishEventlastInvoke)
                        {
                            if (count == countPlay + 1)
                            {
                                m_FinishEvent?.Invoke();
                                delay = true;
                                if (m_OpenDebug)
                                 Debug.Log("��Ƶ������ִ��һ��");
                                if (m_FinishCloseThis)
                                    this.gameObject.SetActive(false);
                            }
                        }
                        else 
                        {
                            m_FinishEvent?.Invoke();
                            delay = true;
                            if (m_OpenDebug)
                                Debug.Log("��Ƶ������ִ��һ��");
                            if (m_FinishCloseThis)
                                this.gameObject.SetActive(false);
                        }
                        if (count >= countPlay + 1)
                        {
                            ae.Stop();         
                            count = 0;
                        
                        }
                     
                    }
                    else 
                    {
                        if (!ae.openFinishEventlastInvoke)
                        {
                            m_FinishEvent?.Invoke();
                            delay = true;
                            if (m_OpenDebug)
                                Debug.Log("��Ƶ������ִ��һ��");
                            if (m_FinishCloseThis)
                                this.gameObject.SetActive(false);
                        }
                    }

                   

             
                    finishOnce = false;
                
                }
            }
            else 
            {
                if (finishOnce&& !m_audio.isPlaying && !starPlayOnce)
                {

                    m_FinishEvent?.Invoke();
                    if (m_OpenDebug)
                        Debug.Log("��Ƶ������ִ��һ��");
                    delay = true;

                    if (m_PlayFinishResetData)
                        ResetData();

                    if (m_FinishCloseThis)
                        this.gameObject.SetActive(false);
                    finishOnce = false;
                }
            }

            Debug.LogWarning(delay);

            if (delay)
            {
                tiemr += Time.deltaTime;
                if (tiemr >= m_DelayTime)
                {
                
                    if (m_IsLoop && loopCountClamp&&openFinishDelayEventlastInvoke)
                    {
                        if (count == 0)
                        {
                            m_FinishDelayEvent?.Invoke();
                            if (m_OpenDebug)
                                Debug.Log("��Ƶ�������ӳ�ִ��һ��");
                            if (m_FinishDelayCloseThis)
                                this.gameObject.SetActive(false);
                        }

                    }
                    else
                    {
                        m_FinishDelayEvent?.Invoke();
                        if (m_OpenDebug)
                            Debug.Log("��Ƶ�������ӳ�ִ��һ��");
                        if (m_FinishDelayCloseThis)
                            this.gameObject.SetActive(false);
                    }

                 

                    if (m_PlayFinishResetData)
                        ResetData();

                    delay = false;
                    tiemr = 0;
                }
            }
        }


        IEnumerator PlayerCount()
        {
           
            yield return new WaitForSeconds(clip.length);
            count++;       
       
            //Debug.Log(count+"///"+finishOnce);
            if (count == countPlay+1)
            {
                StopCoroutine(playCountIE);
               // count = 1;
            }
            else 
            {
                if (m_IsLoop) 
                {
                    percentumOnce = true;
                    starPlayOnce = true;        
                }     
            }
            finishOnce = true;
        }
    }

    [System.Serializable]
    public struct AudioPlayLastOnce 
    {
        public AudioPlayLastInvokeType playLastInvokeType;
        public InteractionEvent m_LastOnceEvent;
    }
    public enum AudioPlayLastInvokeType
    {
       Star,
       Time,
       Finish,
       FinishDelay,
    }
}


