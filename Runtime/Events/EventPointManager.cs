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
     public List<EventElement> eventPoint=new List<EventElement>();  
    public string CreateLifiEvent(ref LifecycleEvent lifecycleEvent )
    {
        GameObject g= CreatGameObject();
        g.AddComponent<LifecycleEvent>();
        lifecycleEvent = g.GetComponent<LifecycleEvent>();

        return g.name;
    }

    public string CreateAudioEvent(ref AudioEvent audioEvent)
    {
        GameObject g = CreatGameObject();
        g.AddComponent<AudioEvent>();
        audioEvent= g.GetComponent<AudioEvent>();
        return g.name;
    }

    public string CreateMultimasterEvent(ref MultimasterEvent multimasterEvent)
    {
        GameObject g = CreatGameObject();
        g.AddComponent<MultimasterEvent>();
        multimasterEvent= g.GetComponent<MultimasterEvent>();
        print(multimasterEvent);
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
    public InteractionEvent m_TrueEvents;

    public AudioEventPort AudioEventPort 
    {
        get 
        {
            return audioEventPort;
        }

        set 
        {
            audioEventPort = value;
        }
    }


    public MultimasterEvent Multimaster 
    {
        get { return multimasterEvent;}
        set { multimasterEvent=value; }
    }

    public LifecycleEvent LifecycleEvent 
    {
        get { return lifecycleEvent; }
        set
        {
            lifecycleEvent = value;
        }
    }

    public EventElement(EventPointElementType eet)
    {
        elementType = eet;
        lable = "";
        audioEvent= null;
        audioEventPort = new AudioEventPort();
        lifecycleEvent= null;
        lifecycleEventPort= new LifecycleEventPort();
        multimasterEvent= null;
        m_TrueEvents= null;
    }
    public void SetAudioEvent()
    {
        if (audioEvent != null)
        {
            audioEvent.m_StartEvent = audioEventPort.m_StartEvent;
            audioEvent.m_PlayTimeEvent = audioEventPort.m_PlayTimeEvent;
            audioEvent.m_FinishEvent = audioEventPort.m_FinishEvent;
            audioEvent.m_FinishDelayEvent = audioEventPort.m_FinishDelayEvent;
        }
        else 
        {
            Debug.LogError("事件中AudioEvent为空");
        }
         
    }

    public void ClearAudioEvent() 
    {
        if (audioEvent != null) 
        {
            audioEvent.m_StartEvent = null;
            audioEvent.m_PlayTimeEvent = null;
            audioEvent.m_FinishEvent = null;
            audioEvent.m_FinishDelayEvent = null;
        }
    
    }

    public void SetLifecyleEvent()
    {
        if (lifecycleEvent != null)
        {
            lifecycleEvent.AwakeEvent = lifecycleEventPort.AwakeEvent;
            lifecycleEvent.StartEvent = lifecycleEventPort.StartEvent;
            lifecycleEvent.OnEnableEvent = lifecycleEventPort.OnEnableEvent;
            lifecycleEvent.OnDisableEvent = lifecycleEventPort.OnDisableEvent;
            lifecycleEvent.OnDestroyEvent = lifecycleEventPort.OnDestroyEvent;
        }
        else
        {
            Debug.LogError("事件中LifecycleEvent为空");
        }
      
    }

    public void ClearLifecyleEvent() 
    {
        if (lifecycleEvent != null) 
        {
            lifecycleEvent.AwakeEvent = null;
            lifecycleEvent.StartEvent = null;
            lifecycleEvent.OnEnableEvent = null;
            lifecycleEvent.OnDisableEvent = null;
            lifecycleEvent.OnDestroyEvent = null;
        }
     
    }

    public void SetMultimasterEvent()
    {
        if (multimasterEvent != null)
        {
            multimasterEvent.m_TrueEvent = m_TrueEvents;
        }
        else
        {
            Debug.LogError("事件中MultimasterEvent为空");
        }
    }

    public void ClearMultimasterEvent() 
    {
        if(multimasterEvent!=null)
        multimasterEvent.m_TrueEvent = null;
    }
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
