using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using HLVR.EventSystems;
using UnityEditor.SceneManagement;

namespace HLVR.Text 
{
    public class AudioTyper : MonoBehaviour
    {
        [TextArea(1, 10)]
        public string content;
        public TMP_Text text;
        [TextArea(1, 10)]    
        string typerContent;
        public AudioSource source;
        float last = -1;
        public float thresholdvalue;
        bool finish = true;
        public InteractionEvent m_TyperFinish;
        public float delaytime;
        public bool closeFnish;
        private void Awake()
        {
            if (source == null)
            {
                source = GetComponent<AudioSource>();
                if (source==null)
                {
                    source=this.gameObject.AddComponent<AudioSource>(); 
                }
            }
        }
        private void Update()
        {
            Typer();
        }

        public void Fnish()
        {
            m_TyperFinish?.Invoke();
            if (closeFnish) 
            {
                this.gameObject.SetActive(false);
            }
        }

        public void SetContent(string con)
        {
             finish = false;
             content = "";
             last = -1;
             typerContent = "";
             source.Stop();
             content = con;
             finish = true;
        }

        public void SetAudioClip(AudioClip clip)
        {
            source.clip = clip;
            if (!source.isPlaying)
                source.Play();
        }


        void Typer()
        {
            if (finish)
            {
                float pro = source.time * 1f / source.clip.length * 1f;
                if (last < (float)Mathf.CeilToInt(pro * content.Length) + thresholdvalue)
                {
                    if (Mathf.CeilToInt(pro * content.Length) < content.Length)
                    {
                        typerContent += content[Mathf.CeilToInt(pro * content.Length)];
                        last = Mathf.CeilToInt(pro * content.Length);
                    }
                }
                if (text != null)
                    text.text = typerContent;

                Debug.Log(typerContent.Trim().Replace(" ", "").Length + "/" + content.Trim().Replace(" ", "").Length);
                if (typerContent.Trim().Replace(" ", "").Length == content.Trim().Replace(" ", "").Length)
                {
                    finish = false;
                    Invoke("Fnish", delaytime);
                   
                }
            }
        }

      
    }
}
