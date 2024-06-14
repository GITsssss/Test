using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HLVR.EventSystems;
namespace HLVR.AudioSystems
{
    public enum SETriggerState//音效触发状态
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
        [Tooltip("触发标签")]
        [ReadOnly]
        public string tag;
        [Tooltip("音效剪辑")]
        public AudioClip[] audioClip;    
        [Tooltip("触发的音效响应事件")]
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
        Metal,//金属
        Wood,//木材 
        Ice,//冰
        Water,//水
        Glass,//玻璃
        Flesh,//肉
        Plastomer,//塑料
        Land,//陆地
    }
}

