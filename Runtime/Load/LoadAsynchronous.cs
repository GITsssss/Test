using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class LoadAsynchronous : MonoBehaviour
{
    public string sceneName;
    public float loadProgress;
    AsyncOperation async;
    public TextMeshProUGUI proUGUI;
    public TextMeshPro TextMesh;
    Load load;
    private void Awake()
    {
        load = FindObjectOfType<Load>();
        if (load != null) 
        {
            sceneName = load.sceneName;
         
        }       
    }

    private void Start()
    {
        if(sceneName!="")
        StartCoroutine(LoadScene());
    }


    IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            loadProgress=async.progress*100;
            if (proUGUI != null)
                proUGUI.text = loadProgress + "%";
            if (TextMesh != null)
                TextMesh.text = loadProgress + "%";
            if (async.progress >= 0.9f) 
            {
                if(load!=null)
                Destroy(load.gameObject);
                loadProgress = 100;
                  if (proUGUI != null)
                proUGUI.text = loadProgress + "%";
                if (TextMesh != null)
                TextMesh.text = loadProgress + "%";
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
