using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HLVR.EventSystems;

namespace HLVR.Interaction 
{
    public class GrabTriggerEvent : MonoBehaviour
    {
        public int ID;
        [Tooltip("SynchronizationPosition:ͬ��λ�ã�SynchronizationRotationͬ���Ƕȣ�EnableRigKinematic���ø��徲̬��DisEnableCollider�ر���ײ���� SynchronizationParentͬ��������,ReleaseTrigger�ֲ��ͷ������Ŵ����¼�,DisableInteraction�ر�GrabObjects�Ľ���")]
        public GrabResponseEventGroup grabResponseEventGroup;
        [Header("ָ������ײ��������ô��¼�")]
        public InteractionEvent m_ResponseEvent;
        [Header("����ָ������ײ��������ô��¼�")]
        public InteractionEvent m_InadequacyEvent;
        public bool m_NoClose;
        [Tooltip("���ѵ�ǰ��Collider�ı�Ϊ������")]
        public bool m_NoTrigger;
        public Material PromptEffect;
        public GTEA gTEA;
        public MultipleIDEvent[] multipleIDEvents;

        private void Awake()
        {
            if (gameObject.GetComponent<Collider>() != null && !m_NoTrigger)
            {
                gameObject.GetComponent<Collider>().isTrigger = true;
            }
 
            MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
            if (mr != null) 
            {
                if (PromptEffect != null)
                    mr.material = PromptEffect;
                else
                    mr.material = new Material(Shader.Find("Unlit/Color"));
            }
        }

        public void Close()
        {
            if (!m_NoClose)
                transform.gameObject.SetActive(false);
        }

        public void Event(int Id)
        {
            if (Id == ID)
            {
                m_ResponseEvent?.Invoke();
                if (!m_NoClose)
                    transform.gameObject.SetActive(false);
                gTEA.Play(transform.position);
            }
        }

        public void MultipleEvent(int Id)
        {
            for (int i = 0; i < multipleIDEvents.Length; i++)
            {
                if (multipleIDEvents[i].ID == Id) 
                {
                    multipleIDEvents[i].m_ResponseEvent?.Invoke();
                  //  gTEA.Play(transform.position);
                }
              
            }
        }

        //public bool MultipleState(int Id, int index)
        //{
        //    if (multipleIDEvents[index].ID == Id)
        //        return true;
        //    else
        //        return false;
        //}


        public void NoInadequacyEvent()
        {
            m_InadequacyEvent?.Invoke();
            if (!m_NoClose)
                transform.gameObject.SetActive(false);
         
        }

        //����ID�Ƿ���ͬ ����ͬ����True ,���򷵻�False
       
    }
    //// [Tooltip("����-λ��")]
    //public bool m_UsePosition;
    //[Tooltip("����-��ת")]
    //public bool m_UseRotation;
    //[Tooltip("�򿪸��徲̬")]
    //public bool m_OpenRigStatic;
    [System.Flags]
    public enum GrabResponseEventGroup
    {    
        SynchronizationPosition=2,
        SynchronizationRotation=4,
        EnableRigKinematic=8,
        DisEnableCollider=32,
        SynchronizationParent=64,
        ReleaseTrigger=128,
        DisableInteraction=256,
        CloseTriggerGameObject=512,
    }

    [System.Serializable]
    public struct GTEA 
    {
        public bool use;
        public AudioClip clip;
        public void Play(Vector3 pos)
        {
            if(use)
            AudioSource.PlayClipAtPoint(clip,pos);
            //GameObject g = new GameObject("AudioEffect");
            //g.AddComponent<AudioSource>();
            //AudioSource au=  g.GetComponent<AudioSource>();

        }
    }
}

