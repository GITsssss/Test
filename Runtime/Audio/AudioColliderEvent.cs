using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HLVR.AudioSystems 
{
    public class AudioColliderEvent : MonoBehaviour
    {
        [Tooltip("触发音效的类型：Trigger 触发器，Collider 碰撞器 ，Event 事件点")]
        public AudioEffectEnablementType enablementType;
        public AudioClip catapult;
        public ACE[] m_AudioEvent;
        int random;
        int colliderCount;
        public void Reset()//初始化检测
        {
            if(GetComponent<Collider>()==null)
            DebugInfo.DebugWarning(OutColor.Red,"当前物体上一个触发器也没有！音效触发将会无法运行！");
            m_AudioEvent = new ACE[8];
            for (int i = 0; i < m_AudioEvent.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        m_AudioEvent[i].tag = "Metal";
                        break;
                    case 1:
                        m_AudioEvent[i].tag = "Wood";
                        break;
                    case 2:
                        m_AudioEvent[i].tag = "Ice";
                        break;
                    case 3:
                        m_AudioEvent[i].tag = "Water";
                        break;
                    case 4:
                        m_AudioEvent[i].tag = "Glass";
                        break;
                    case 5:
                        m_AudioEvent[i].tag = "Flesh";
                        break;
                    case 6:
                        m_AudioEvent[i].tag = "Plastomer";
                        break;
                    case 7:
                        m_AudioEvent[i].tag = "Land";
                        break;

                }
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (enablementType == AudioEffectEnablementType.Collider) 
            {
                colliderCount++;
                if(colliderCount>=1&& catapult!=null)
                    AudioSource.PlayClipAtPoint(catapult,transform.position);
                if(collision.collider.GetComponent<ObjectMaterial>()!=null)
                switch (collision.collider.GetComponent<ObjectMaterial>().objectsMaterials)
                {
                    case ObjectsMaterials.Metal:
                        random = Random.Range(0, m_AudioEvent[(int)ObjectsMaterials.Metal].audioClip.Length);
                        if(m_AudioEvent[(int)ObjectsMaterials.Metal].audioClip[random]!=null)
                        AudioSource.PlayClipAtPoint(m_AudioEvent[(int)ObjectsMaterials.Metal].audioClip[random], collision.contacts[0].point);
                        break;
                    case ObjectsMaterials.Wood:
                        random = Random.Range(0, m_AudioEvent[(int)ObjectsMaterials.Wood].audioClip.Length);
                        if (m_AudioEvent[(int)ObjectsMaterials.Wood].audioClip[random] != null)
                            AudioSource.PlayClipAtPoint(m_AudioEvent[(int)ObjectsMaterials.Wood].audioClip[random], collision.contacts[0].point);
                        break;
                    case ObjectsMaterials.Ice:
                        random = Random.Range(0, m_AudioEvent[(int)ObjectsMaterials.Ice].audioClip.Length);
                        if (m_AudioEvent[(int)ObjectsMaterials.Ice].audioClip[random] != null)
                            AudioSource.PlayClipAtPoint(m_AudioEvent[(int)ObjectsMaterials.Ice].audioClip[random], collision.contacts[0].point);
                        break;
                    case ObjectsMaterials.Water:
                        random = Random.Range(0, m_AudioEvent[(int)ObjectsMaterials.Water].audioClip.Length);
                        if (m_AudioEvent[(int)ObjectsMaterials.Water].audioClip[random] != null)
                            AudioSource.PlayClipAtPoint(m_AudioEvent[(int)ObjectsMaterials.Water].audioClip[random], collision.contacts[0].point);
                        break;
                    case ObjectsMaterials.Glass:
                        random = Random.Range(0, m_AudioEvent[(int)ObjectsMaterials.Glass].audioClip.Length);
                        if (m_AudioEvent[(int)ObjectsMaterials.Glass].audioClip[random] != null)
                            AudioSource.PlayClipAtPoint(m_AudioEvent[(int)ObjectsMaterials.Glass].audioClip[random], collision.contacts[0].point);
                        break;
                    case ObjectsMaterials.Flesh:
                        random = Random.Range(0, m_AudioEvent[(int)ObjectsMaterials.Flesh].audioClip.Length);
                        if (m_AudioEvent[(int)ObjectsMaterials.Flesh].audioClip[random] != null)
                            AudioSource.PlayClipAtPoint(m_AudioEvent[(int)ObjectsMaterials.Flesh].audioClip[random], collision.contacts[0].point);
                        break;
                    case ObjectsMaterials.Plastomer:
                        random = Random.Range(0, m_AudioEvent[(int)ObjectsMaterials.Plastomer].audioClip.Length);
                        if (m_AudioEvent[(int)ObjectsMaterials.Plastomer].audioClip[random] != null)
                            AudioSource.PlayClipAtPoint(m_AudioEvent[(int)ObjectsMaterials.Plastomer].audioClip[random], collision.contacts[0].point);
                        break;
                    case ObjectsMaterials.Land:
                        random = Random.Range(0, m_AudioEvent[(int)ObjectsMaterials.Land].audioClip.Length);
                        if (m_AudioEvent[(int)ObjectsMaterials.Land].audioClip[random] != null)
                            AudioSource.PlayClipAtPoint(m_AudioEvent[(int)ObjectsMaterials.Land].audioClip[random], collision.contacts[0].point);
                        break;
                }
            }
        }

        //private void OnCollisionExit(Collision collision)
        //{
        //    if (enablementType == AudioEffectEnablementType.Collider)
        //    {

        //    }
        //}


        //private void OnTriggerEnter(Collider other)
        //{
        //    if (enablementType == AudioEffectEnablementType.Trigger)//other.ClosestPoint(transform.position)
        //    {
        //        switch (other.GetComponent<ObjectMaterial>().objectsMaterials)
        //        {
        //            case ObjectsMaterials.Metal:
        //                random = Random.Range(0, m_AudioEvent[(int)ObjectsMaterials.Metal].audioClip.Length);
        //                AudioSource.PlayClipAtPoint(m_AudioEvent[(int)ObjectsMaterials.Metal].audioClip[random], other.ClosestPoint(transform.position));
        //                break;
        //            case ObjectsMaterials.Wood:
        //                random = Random.Range(0, m_AudioEvent[(int)ObjectsMaterials.Wood].audioClip.Length);
        //                AudioSource.PlayClipAtPoint(m_AudioEvent[(int)ObjectsMaterials.Wood].audioClip[random], other.ClosestPoint(transform.position));
        //                break;
        //            case ObjectsMaterials.Ice:
        //                random = Random.Range(0, m_AudioEvent[(int)ObjectsMaterials.Ice].audioClip.Length);
        //                AudioSource.PlayClipAtPoint(m_AudioEvent[(int)ObjectsMaterials.Ice].audioClip[random], other.ClosestPoint(transform.position));
        //                break;
        //            case ObjectsMaterials.Water:
        //                random = Random.Range(0, m_AudioEvent[(int)ObjectsMaterials.Water].audioClip.Length);
        //                AudioSource.PlayClipAtPoint(m_AudioEvent[(int)ObjectsMaterials.Water].audioClip[random], other.ClosestPoint(transform.position));
        //                break;
        //            case ObjectsMaterials.Glass:
        //                random = Random.Range(0, m_AudioEvent[(int)ObjectsMaterials.Glass].audioClip.Length);
        //                AudioSource.PlayClipAtPoint(m_AudioEvent[(int)ObjectsMaterials.Glass].audioClip[random], other.ClosestPoint(transform.position));
        //                break;
        //            case ObjectsMaterials.Flesh:
        //                random = Random.Range(0, m_AudioEvent[(int)ObjectsMaterials.Flesh].audioClip.Length);
        //                AudioSource.PlayClipAtPoint(m_AudioEvent[(int)ObjectsMaterials.Flesh].audioClip[random], other.ClosestPoint(transform.position));
        //                break;
        //            case ObjectsMaterials.Plastomer:
        //                random = Random.Range(0, m_AudioEvent[(int)ObjectsMaterials.Plastomer].audioClip.Length);
        //                AudioSource.PlayClipAtPoint(m_AudioEvent[(int)ObjectsMaterials.Plastomer].audioClip[random], other.ClosestPoint(transform.position));
        //                break;
        //            case ObjectsMaterials.Land:
        //                random = Random.Range(0, m_AudioEvent[(int)ObjectsMaterials.Land].audioClip.Length);
        //                AudioSource.PlayClipAtPoint(m_AudioEvent[(int)ObjectsMaterials.Land].audioClip[random], other.ClosestPoint(transform.position));
        //                break;
        //        }
        //    }
        //}

        //private void OnTriggerExit(Collider other)
        //{
        //    if (enablementType == AudioEffectEnablementType.Trigger)
        //    {

        //    }
        //}
    }
}
