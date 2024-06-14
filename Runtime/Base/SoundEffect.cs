using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HLVR.EventSystems;
namespace HLVR.AudioSystems
{
    public enum SETriggerState//��Ч����״̬
    {
        Enter,
        Exit,
        Press,
        Release,
    }
    [System.Serializable]
    public struct SoundEffect
    {
        public AudioSource audios;
        public AudioClip pressOnce;
        public AudioClip release;
        public AudioClip enter;
        public AudioClip exit;
        public void PlayAudio(SETriggerState state)
        {
            if (audios != null)
            {
                switch (state)
                {
                    case SETriggerState.Enter:
                        if (enter != null)
                            audios.PlayOneShot(enter);
                        break;
                    case SETriggerState.Exit:
                        if (exit != null)
                            audios.PlayOneShot(exit);
                        break;
                    case SETriggerState.Press:
                        if (pressOnce != null)
                            audios.PlayOneShot(pressOnce);
                        break;
                    case SETriggerState.Release:
                        if (release != null)
                            audios.PlayOneShot(release);
                        break;
                }
            }
        }
    }


    [System.Serializable]
    public struct ACE 
    {
        [Tooltip("������ǩ")]
        [ReadOnly]
        public string tag;
        [Tooltip("��Ч����")]
        public AudioClip[] audioClip;    
        [Tooltip("��������Ч��Ӧ�¼�")]
        public InteractionEvent triggerEvent;   
    }

  
    public enum AudioEffectEnablementType
    {
       Trigger,
       Collider,
       Event
    }

    public enum ObjectsMaterials
    {
        Metal,//����
        Wood,//ľ�� 
        Ice,//��
        Water,//ˮ
        Glass,//����
        Flesh,//��
        Plastomer,//����
        Land,//½��
    }
}

