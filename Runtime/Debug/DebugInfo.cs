using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugInfo
{
    public static void DebugWarning(OutColor outColor,string info)
    {
        switch (outColor)
        {
            case OutColor.Blue:
                Debug.LogWarning("<color=blue>"+ info +"</color>");
                break;
            case OutColor.Green:
                Debug.LogWarning("<color=green>" + info + "</color>");
                break;
            case OutColor.Red:
                Debug.LogWarning("<color=red>" + info + "</color>");
                break;
            case OutColor.Yellow:
                Debug.LogWarning("<color=yellow>" + info + "</color>");
                break;
        }  
    }

    public static void DebugLog(OutColor outColor, string info)
    {
        switch (outColor)
        {
            case OutColor.Blue:
                Debug.Log("<color=blue>" + info + "</color>");
                break;
            case OutColor.Green:
                Debug.Log("<color=green>" + info + "</color>");
                break;
            case OutColor.Red:
                Debug.Log("<color=red>" + info + "</color>");
                break;
            case OutColor.Yellow:
                Debug.Log("<color=yellow>" + info + "</color>");
                break;
            case OutColor.White:
                Debug.Log("<color=white>" + info + "</color>");
                break;
            case OutColor.Black:
                Debug.Log("<color=black>" + info + "</color>");
                break;
        }
    }

    public static void DebugLog(OutColor outColor, bool info)
    {
        switch (outColor)
        {
            case OutColor.Blue:
                Debug.Log("<color=blue>" + info + "</color>");
                break;
            case OutColor.Green:
                Debug.Log("<color=green>" + info + "</color>");
                break;
            case OutColor.Red:
                Debug.Log("<color=red>" + info + "</color>");
                break;
            case OutColor.Yellow:
                Debug.Log("<color=yellow>" + info + "</color>");
                break;
            case OutColor.White:
                Debug.Log("<color=white>" + info + "</color>");
                break;
            case OutColor.Black:
                Debug.Log("<color=black>" + info + "</color>");
                break;
        }
    }

    public static void DebugLog(string info)
    {
        Debug.Log("<color=green>" + info + "</color>");
    }

    public static void DebugLog(bool info)
    {
        Debug.Log("<color=green>" + info + "</color>");
    }


    public static void DebugLog(long info)
    {
        Debug.Log("<color=green>" + info + "</color>");
    }
}

public enum OutColor
{
    Yellow,
    Green,
    Blue,
    Red,
    White,
    Black
}
