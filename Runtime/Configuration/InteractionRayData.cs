using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HLVR.InputSystem;
using UnityEditor;
using UnityEngine.InputSystem;
namespace HLVR.Configuration 
{
    [CreateAssetMenu(fileName = "InteractionRayData", menuName = "HLVR/InteractionRayData", order = 1)]
    public class InteractionRayData : ScriptableObject
    {
           public GameObject[] handControllerPrefabs;
           public ControllerType handtype =ControllerType.Right;
           public KeyType button = KeyType.Trigger ;
           public KeyType TeleportButton = KeyType.Rocker;
           public float  m_MaxCastDistance = 10;
           public float  starwidth = 0.01f;
           public float  endwidth = 0.01f;
           public int  BezierPointCount = 20;
           public float m_Distance = 0.6f;
           public Color normalC = new Color(Color.red.r, Color.red.g, Color.red.b,1);
           public Color hitIOC = new Color(0f, 0.9568628f, 0.9568628f, 1f);
           public Material material = null;
           public string shaderColorName = "_Color";
           public bool m_EnebledMove = true;
           public int PointCount = 60;
           public float Interval = 0.4f;
           public float Gravity = -0.02f;
           public float width = 0.02f;
           public LayerMask rayLayerMask;
           public LayerMask CurvelayerMask;
//# if UNITY_EDITOR_WIN
//        public void Save()
//        {
//            EditorUtility.SetDirty(this);
//            AssetDatabase.SaveAssets();
//        }
//# endif
    }
}

