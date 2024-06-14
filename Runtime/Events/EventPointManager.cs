using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HLVR.EventSystems;
using HLVR.AudioSystem;

public class EventPointManager : MonoBehaviour
{
    private void Reset()
    {
        this.gameObject.name = "EventPointGroup";
    }

     [SerializeField]
     public EventElement[] eventPoint;  
    public string CreateLifiEvent()
    {
        GameObject g= CreatGameObject();
        g.AddComponent<LifecycleEvent>();
        return g.name;
    }

    public string CreateAudioEvent()
    {
        GameObject g = CreatGameObject();
        g.AddComponent<AudioEvent>();
       return g.name;
    }

    public string CreateMultimasterEvent()
    {
        GameObject g = CreatGameObject();
        g.AddComponent<MultimasterEvent>();
        return g.name;
    }


    GameObject CreatGameObject()
    {
        GameObject g = new GameObject();
        g.transform.parent = this.transform;
        g.name = "E" + transform.childCount.ToString();
        return g;
    }
}


[System.Serializable]
public struct EventPoint 
{
    public string lable;
    public EventElement[] EventElements;
}


[System.Serializable]
public struct EventElement 
{
    public string lable;
    public EventPointElementType elementType;
    public AudioEvent audioEvent;
    public AudioEventPort audioEventPort;

    public LifecycleEvent lifecycleEvent;
    public LifecycleEventPort lifecycleEventPort;

    public MultimasterEvent multimasterEvent;
    public InteractionEvent m_TrueEvent;

}


public enum EventPointElementType 
{
    AudioEvent=2,
    LifecycleEvent=4,
    MultimasterEvent=8,
}


[System.Serializable]
public struct LifecycleEventPort 
{
   
    public InteractionEvent AwakeEvent;
    public InteractionEvent StartEvent;
    public InteractionEvent OnEnableEvent;
    public InteractionEvent OnDisableEvent;
    public InteractionEvent OnDestroyEvent;
    //public LifecycleEvent lifecycleEvent;
}

[System.Serializable]
public struct AudioEventPort 
{
    public InteractionEvent m_StartEvent;
    public InteractionEvent m_PlayTimeEvent;
    public InteractionEvent m_FinishEvent;
    public InteractionEvent m_FinishDelayEvent;
   // public AudioEvent audioEvent;
}
