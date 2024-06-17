using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabFixedPointManager : MonoBehaviour
{
    public List<GrabFixedPoint> grabPoints = new List<GrabFixedPoint>();
    private static GrabFixedPointManager pointManager;
    public static GrabFixedPointManager Instance 
    {
        get { return pointManager; }
        set { pointManager = value; }
    }

    private void Awake()
    {
        if (pointManager == null) pointManager = this;
        GrabFixedPoint[] points = FindObjectsOfType<GrabFixedPoint>();
        for (int i=0;i<points.Length;i++)
        {
            if (!grabPoints.Contains(points[i]))
            grabPoints.Add(points[i]);  
        }
    }

    public void CloseOther(GameObject obj)
    {
        for (int i = 0; i < grabPoints.Count; i++)
        {
            if (grabPoints[i].gameObject != obj)
                grabPoints[i].can = false;
            else grabPoints[i].can = true;
        }
    }
}
