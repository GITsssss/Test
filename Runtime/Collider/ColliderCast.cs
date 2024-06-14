using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ColliderCast : MonoBehaviour
{
   
    public bool cast;
    Rigidbody rig;
    [HideInInspector]
    public Transform target;
    private void Awake()
    {
        rig= GetComponent<Rigidbody>(); 
        rig.isKinematic = true;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void Update()
    {
        if (!cast) 
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer== LayerMask.NameToLayer("PhyCast"))
        cast = true;
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PhyCast"))
            cast = false;
    }
}
