using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class LookAtObject : MonoBehaviour
{
    LookAtConstraint lookatConstraint;
    ConstraintSource source;
    private void Awake()
    {
        lookatConstraint = GetComponent<LookAtConstraint>();
        if (lookatConstraint == null) 
        {
           this.gameObject.AddComponent<LookAtConstraint>();
           lookatConstraint = GetComponent<LookAtConstraint>();
        }
        source.sourceTransform = Camera.main.transform;
        source.weight = 1;
        lookatConstraint.SetSource(0, source);
    }
}
