using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HLVR.Interaction;
public class TeleportArea : MonoBehaviour
{
    Material mat;
    Teleport teleport;
    [HideInInspector]
    public  Material current;

    public void Reset()
    {
        mat = new Material(Shader.Find("Shader Graphs/Area"));     
        gameObject.GetComponent<MeshRenderer>().material = mat;
        transform.gameObject.name = "TeleportArea";
        mat.SetVector("_Tiling",new Vector4(transform.localScale.x,transform.localScale.z));
    }

    private void Awake()
    {
        teleport = GameObject.FindObjectOfType<Teleport>();
        current = gameObject.GetComponent<MeshRenderer>().material;
      //  current.SetVector("_Tiling",new Vector2(transform.localScale.x, transform.localScale.y));
    }
 

    private void OnDisable()
    {
        current.SetInt("showColor", 0);
    }
}
