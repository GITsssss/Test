using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HLVR.InputSystem;

public class GrabPoint : MonoBehaviour
{
    [Tooltip("按下哪个按键可以抓取")]
    public KeyType grabKey;
    public Vector3 offsetPostion;
    public Vector3 offsetRotation;
}
