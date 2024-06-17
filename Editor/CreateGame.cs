using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class CreateGame :Editor
{
    [MenuItem("HLVR/EventSystem/EventPointManager")]
    public static void EventManager()
    {
        if (GameObject.FindObjectOfType<EventPointManager>() == null) 
        {
            GameObject g=new GameObject("EventPointManager");
            g.AddComponent<EventPointManager>();
            EditorGUIUtility.PingObject(g);
            Selection.activeGameObject = g;
        }
    }
}
