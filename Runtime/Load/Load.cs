using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Load : MonoBehaviour
{

    public bool m_OnLoadDontDestroy;
    [HideInInspector]
    public string sceneName;
    private void OnLevelWasLoaded(int level)
    {
        //if (GameObject.FindObjectsOfType<Load>().Length > 1)
        //    Destroy(this.gameObject);
    }

    private void Awake()
    {
        if(m_OnLoadDontDestroy)
        DontDestroyOnLoad(this.transform.root);
    }

    public void FastLoad(string scene)
    {
        SceneManager.LoadScene(scene);      
    }

    public void LoadSceneAsync(string scene)
    {
      
        sceneName = scene;
        SceneManager.LoadScene("Load");
       
    }

    public void LoadSceneAsyns()
    {
        SceneManager.LoadScene("Load");
    }

    public void ResetCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitScene() 
    {
        Application.Quit();
    }
}
