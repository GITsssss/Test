using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModule : MonoBehaviour
{
    private void OnEnable()
    {
        if (!UIManager.Instance.stack.Contains(this.gameObject)) 
        {
            UIManager.Instance.AddStack(this.gameObject);
            UIManager.Instance.ActionUIModuld(this.gameObject);
        }
        if (transform.childCount > 0) 
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
       // UIManager.Instance.stack.RemoveIndex(this.gameObject);
    }
}
