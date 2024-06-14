using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCollider : MonoBehaviour
{
    public Collider[] colliders;
    public bool m_OnEnable;
    public  Color color;
    public Mesh cube;
  
    public Mesh Capa;
    [Range(0, 2)]
    public float size;
    public  Collider[] hand;

    public  Collider[] foot;

    public  Collider[] leg;

    public  Collider    head;

    public  Collider heart;
 
    public  Collider[] body;
 
    public  Collider[] arm;

    private void OnDrawGizmos()
    {
        if (m_OnEnable) 
        {
            Gizmos.color = color;
            for (int i = 0; i < hand.Length; i++)
            {
                if (hand[i].GetComponent<BoxCollider>() != null)
                    Gizmos.DrawMesh(cube, hand[i].transform.position, hand[i].transform.rotation, hand[i].bounds.size * size);
                else if (hand[i].GetComponent<CapsuleCollider>() != null)
                    Gizmos.DrawMesh(Capa, hand[i].transform.position, hand[i].transform.rotation, hand[i].bounds.size * size);
                // Gizmos.color = color;
            }

            for (int i = 0; i < foot.Length; i++)
            {
                if (foot[i].GetComponent<MeshFilter>() != null)
                    Gizmos.DrawMesh(foot[i].GetComponent<MeshFilter>().mesh, foot[i].transform.position, foot[i].transform.rotation, foot[i].transform.localScale * size);
                //Gizmos.color = color;
            }

            for (int i = 0; i < leg.Length; i++)
            {
                if (leg[i].GetComponent<MeshFilter>() != null)
                    Gizmos.DrawMesh(leg[i].GetComponent<MeshFilter>().mesh, leg[i].transform.position, leg[i].transform.rotation, leg[i].transform.localScale * size);
                //Gizmos.color = color;
            }

            if (head.GetComponent<MeshFilter>() != null)
            {

                Gizmos.DrawMesh(head.GetComponent<MeshFilter>().mesh, head.transform.position, head.transform.rotation, head.transform.localScale * size);
                //  Gizmos.color = color;
            }



            if (heart.GetComponent<MeshFilter>() != null)
            {
                Gizmos.DrawMesh(heart.GetComponent<MeshFilter>().mesh, heart.transform.position, heart.transform.rotation, heart.transform.localScale * size);
                //  Gizmos.color = color;
            }



            for (int i = 0; i < body.Length; i++)
            {
                if (body[i].GetComponent<MeshFilter>() != null)
                    Gizmos.DrawMesh(body[i].GetComponent<MeshFilter>().mesh, body[i].transform.position, body[i].transform.rotation, body[i].transform.localScale * size);
                // Gizmos.color = color;
            }

            for (int i = 0; i < arm.Length; i++)
            {
                if (arm[i].GetComponent<MeshFilter>() != null)
                    Gizmos.DrawMesh(arm[i].GetComponent<MeshFilter>().mesh, arm[i].transform.position, arm[i].transform.rotation, arm[i].transform.localScale * size);
                //  Gizmos.color = color;
            }

        }

    }

    private void OnEnable()
    {
        
    }
}
