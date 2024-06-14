using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HLVR.EventSystems;
using HLVR.AudioSystems;
using HLVR.AudioSystem;


namespace HLVR.AudioSystem 
{
    public class AudioEventManager : MonoBehaviour
    {
        private static AudioEventManager aem;
        [ReadOnly]
        public AudioEvent current;
        public static AudioEventManager Instance
        {
            get
            {
                if (aem == null) 
                {
                    GameObject asg = new GameObject("AudioEventManager");
                    asg.AddComponent<AudioEventManager>();
                    aem=asg.GetComponent<AudioEventManager>();  
                }
                return aem;
            }
        }

        private void Awake()
        {
            if (aem == null) aem = this;
            else Destroy(aem);
        }
        public void CloseOther(AudioEvent audioEvent)
        {
            if (current != null) 
            {
                current.Stop();
                current = null;
                current = audioEvent;
            }
        }
    }

}
