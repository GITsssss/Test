using HLVR.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HLVR.InputSystem;

public class GrabFixedPoint : MonoBehaviour
{
    public KeyType key;
    public Vector3 point;
    public Vector3 value;
    public Vector3 handpos;
    public float dd;
    public Vector3 playerposition;
    public  Hand hand;

    bool can;
    private void Update()
    {
        if (hand != null) 
        {
            if (VRInput.GetKey(key, ControllerType.Left)&& can)
            {
                value = point - hand.transform.position;
                handpos = hand.transform.position;
                if (value.magnitude> dd)
                InteractionRay.Instance.transform.position = playerposition + (point - hand.transform.position);
            }
            //else if (VRInput.GetKey(key, ControllerType.Right))
            //{
            //    InteractionRay.Instance.transform.position = playerposition + (point - hand.transform.position);
            //}
        }
        
        //else if (VRInput.GetKeyUp(key, ControllerType.Right))
        //{
        //    hand = null;
        //}
    }




    private void OnTriggerStay(Collider other)
    {
        if (hand == null)
            hand = other.GetComponent<Hand>();

        if (hand != null)
        {
            if (VRInput.GetKeyDown(key, ControllerType.Left) && hand.inputSource == ControllerType.Left)
            {
                playerposition = InteractionRay.Instance.transform.position;
                point = hand.transform.position;
                can = true;
            }
        }
    }

}
