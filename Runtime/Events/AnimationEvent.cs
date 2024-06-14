using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HLVR.EventSystems;

namespace HLVR.Animations 
{
    public class AnimationEvent : MonoBehaviour
    {
        Animator anim;
        public AnimatorEventID[] animatorEventIDs;
        public AnimatorEventID[] animatorEventAutoAddIDs;
        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        public void AnimationEventIDIndex(int Index)
        {
            animatorEventIDs[Index].countPlay++;
            if (animatorEventIDs[Index].countPlay >= animatorEventIDs[Index].m_CountRun)
                animatorEventIDs[Index].Event?.Invoke();
            Debug.LogWarning(transform.gameObject.name);
        }


        public void AnimatorSpeed(float speed)
        {
            anim.speed = speed;
        }


        public void AnimatorSpeedZero()
        {
            anim.speed = 0;
        }

        public void AnimatorSpeedOne()
        {
            anim.speed = 1;
        }

        int addindex=0;
        public void AnimationEventIDAutoAddIndex()
        {
            animatorEventAutoAddIDs[addindex].countPlay++;
            if (animatorEventAutoAddIDs[addindex].countPlay >= animatorEventAutoAddIDs[addindex].m_CountRun) 
            {
                animatorEventAutoAddIDs[addindex].Event?.Invoke();
                addindex = Mathf.Clamp(addindex+1,0, animatorEventAutoAddIDs.Length-1);
            }
               
        }

        public void ResetAddIndex()
        {
            addindex = 0;
        }

    }
}



