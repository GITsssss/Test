using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;


public class ReflectClass : MonoBehaviour
{
    [HideInInspector]
    public MethodInfo[] methodInfos;
    public List<string> menames = new List<string>();
    public List<string> className = new List<string>();
    [HideInInspector]
    public GameObject obj;
    public ReflectGroup[] reflectGroup;
    public void GetObjectClassName(GameObject obj)
    {
        Component[] components = obj.GetComponents<Component>();
        foreach (Component c in components)
        {
            if (!className.Contains(c.GetType().Name))
            className.Add(c.GetType().Name);
        }
       // reflectGroup=new ReflectGroup[className.Count];
    }

    public Type GetClassType(string typename)
    {
        Component[] components = obj.GetComponents<Component>();
        Type type= null;
        for (int i=0;i< components.Length;i++)
        {
            if (typename == components[i].GetType().Name)
            {
                type= components[i].GetType();
            }
      
        }
        return type;
    }


    //public void Get<T>() where T:Behaviour
    //{
    //    Type type = typeof(T);
    //    methodInfos = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance);

    //    foreach (MethodInfo methodInfo in methodInfos)
    //    {
    //      print(methodInfo.Name);
    //        menames.Add(methodInfo.Name); 
    //    }
    //}

    public void Get(string classname)
    {
        menames.Clear();
        Type type = GetClassType(classname);
        methodInfos = type.GetMethods(BindingFlags.Public | BindingFlags.Instance|BindingFlags.DeclaredOnly|BindingFlags.OptionalParamBinding);
        foreach (MethodInfo methodInfo in methodInfos)
        {
            print(methodInfo.Name);
            string cs ="";
            //for (int i=0;i< methodInfo.GetParameters().Length;i++)
            //{
            //    cs += methodInfo.GetParameters().GetValue(i).ToString();
            //}   
            menames.Add(methodInfo.Name + cs);
        }

      

    }
}

[System.Serializable]
public struct ReflectGroup 
{
    public Transform Transform;
    public int intvalue;
    public float floatvalue;
    public bool boolvalue;
    public string stringvalue;
    public GameObject gameobject;
}
