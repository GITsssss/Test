using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabable 
{   
    /// <summary>
    ///  抓取到,指抓住物体的那一刻，调用一次
    /// </summary>
    public void OnGrab()
    { 
      
    }
    /// <summary>
    /// 抓取中，指物体抓在手中，就会一直调用
    /// </summary>
    public void InGrab()
    {

    }
    /// <summary>
    /// 松开，当手中抓主物体的时候，松开物体会调用一次
    /// </summary>
    public void Release()
    {

    }
}
