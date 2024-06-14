using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCollider : MonoBehaviour
{
    public Mesh[] mesh;
    [Range(0,10)]
    public float size=0.1f;
    private void OnDrawGizmos()
    {
     
        //Gizmos.color = Color.red;
        //Gizmos.DrawMesh(mesh[0], transform.position,Quaternion.Euler(Quaternion.ToEulerAngles(transform.rotation) + new Vector3(90, 0, 0)) , new Vector3(size, size, size));

        Gizmos.color = Color.blue;
        Gizmos.DrawMesh(mesh[0], transform.position,transform.localRotation, new Vector3(size, size, size));

        //Gizmos.color = Color.green;
        //Gizmos.DrawMesh(mesh[2], transform.position, Quaternion.Euler(Quaternion.ToEulerAngles(transform.rotation) + new Vector3(0, 90, 0)), new Vector3(size, size, size));
    }
}
