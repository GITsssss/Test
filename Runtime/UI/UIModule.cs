using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIModule : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.Log(UIManager.Instance);
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
}
