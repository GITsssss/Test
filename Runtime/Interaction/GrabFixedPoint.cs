using HLVR.Interaction;
using UnityEngine;
using HLVR.InputSystem;

public class GrabFixedPoint : MonoBehaviour
{
    public KeyType key;
    public  Hand  hand;
    Vector3 markPostion;
    Vector3 currentPostion;
    public Vector3 oripos;
    [HideInInspector]
    public bool can=true;
    private void Update()
    {
        if (hand != null) 
        {
            if (VRInput.GetKeyDown(key, hand.inputSource))
            {
                if(hand.inputSource==ControllerType.Left)
                markPostion = InteractionRay.Instance.lefthandInfoData.position;
                else if(hand.inputSource==ControllerType.Right)
                 markPostion = InteractionRay.Instance.righthandInfoData.position;
                oripos =InteractionRay.Instance.transform.position;
                GrabFixedPointManager.Instance.CloseOther(this.gameObject);
            }
            if (VRInput.GetKey(key, hand.inputSource) && can)
            {
                Vector3 value = currentPostion - markPostion;
                InteractionRay.Instance.transform.position = oripos +new Vector3(value.x, value.y*-1, value.z);
                if (hand.inputSource == ControllerType.Left)
                    currentPostion = InteractionRay.Instance.lefthandInfoData.position;
                else if (hand.inputSource == ControllerType.Right)
                    currentPostion = InteractionRay.Instance.righthandInfoData.position;
            }
            else
            if (VRInput.GetKeyUp(key, hand.inputSource))
            {
                hand = null;
            }
        }   

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<Hand>()!=null)
        if(hand==null|| other.GetComponent<Hand>().inputSource!=hand.inputSource)
        hand = other.GetComponent<Hand>();
    }
}
