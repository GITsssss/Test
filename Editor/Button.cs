using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

namespace HLVR.EventSystems 
{
    public class Button : MonoBehaviour
    {
        [MenuItem("GameObject/UI/Button(HLVR)")]
        public static void CreatButton()
        {
            if (FindObjectOfType<Canvas>() == null)
            {
                GameObject g = new GameObject("Canvas");
                g.AddComponent<Canvas>();
                g.AddComponent<CanvasScaler>();
                g.AddComponent<GraphicRaycaster>();
                GameObject button = new GameObject();
                // button.transform.parent = Selection.transforms[0];
                button.transform.parent = g.transform;
                button.AddComponent<RectTransform>().anchoredPosition = Vector2.zero;
                button.AddComponent<Image>();
                //  button.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                button.AddComponent<InteractionEventElement>();
                button.AddComponent<PCBELink>();
                button.name = "Button - HLVR";
            }
            else 
            {
                GameObject button = new GameObject();
                button.transform.parent = Selection.transforms[0];           
                button.AddComponent<RectTransform>().anchoredPosition = Vector2.zero;
                button.AddComponent<Image>();
                // button.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                button.AddComponent<InteractionEventElement>();
                button.AddComponent<PCBELink>();
                button.name = "Button - HLVR";
            }
           
        }

        [MenuItem("HLVR/UI/Button(HLVR)")]
        public static void CreatButton2()
        {
            if (FindObjectOfType<Canvas>() == null)
            {
                GameObject g = new GameObject("Canvas");
                g.AddComponent<Canvas>();
                g.AddComponent<CanvasScaler>();
                g.AddComponent<GraphicRaycaster>();
                GameObject button = new GameObject();
                button.transform.parent = g.transform;
                button.AddComponent<RectTransform>().anchoredPosition = Vector2.zero;
                button.AddComponent<Image>();
                //  button.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                button.AddComponent<InteractionEventElement>();
                button.AddComponent<PCBELink>();
                button.name = "Button - HLVR";
            }
            else 
            {
                GameObject button = new GameObject();
                button.transform.parent = FindObjectOfType<Canvas>().transform;
                button.AddComponent<RectTransform>().anchoredPosition = Vector2.zero;
                button.AddComponent<Image>();
                //  button.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                button.AddComponent<InteractionEventElement>();
                button.AddComponent<PCBELink>();
                button.name = "Button - HLVR";
            }
        }

        [MenuItem("HLVR/UI/Button-TMPro(HLVR)")]
        public static void CreatButton3()
        {
            if (FindObjectOfType<Canvas>() == null)
            {
                GameObject g = new GameObject("Canvas");
                g.AddComponent<Canvas>();
                g.AddComponent<CanvasScaler>();
                g.AddComponent<GraphicRaycaster>();
                GameObject button = new GameObject();
                button.transform.parent = g.transform;
                button.AddComponent<RectTransform>().anchoredPosition = Vector2.zero;
                button.AddComponent<Image>();
                //  button.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                button.AddComponent<InteractionEventElement>();
                button.AddComponent<PCBELink>();
                GameObject t = new GameObject();
                t.transform.parent = button.transform;
                t.AddComponent<RectTransform>().anchoredPosition = Vector2.zero;
                t.AddComponent<TextMeshProUGUI>();
                t.name = "Content";
                button.name = "Button(TMPro)- HLVR";
            }
            else 
            {
                GameObject button = new GameObject();
                button.transform.parent = FindObjectOfType<Canvas>().transform;
                button.AddComponent<RectTransform>().anchoredPosition = Vector2.zero;
                button.AddComponent<Image>();
                //  button.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                button.AddComponent<InteractionEventElement>();
                button.AddComponent<PCBELink>();
                GameObject t = new GameObject();
                t.transform.parent = button.transform;
                t.AddComponent<RectTransform>().anchoredPosition = Vector2.zero;
                t.AddComponent<TextMeshProUGUI>();
                t.name = "Content";
                button.name = "Button(TMPro)- HLVR";
            }
          
        }

        [MenuItem("GameObject/UI/Button-TMPro(HLVR)")]
        public static void CreatButton4()
        {
            if (FindObjectOfType<Canvas>() == null)
            {
                GameObject g = new GameObject("Canvas");
                g.AddComponent<Canvas>();
                g.AddComponent<CanvasScaler>();
                g.AddComponent<GraphicRaycaster>();
                GameObject button = new GameObject();
                button.transform.parent = g.transform;
                button.AddComponent<RectTransform>().anchoredPosition = Vector2.zero;
                button.AddComponent<Image>();
                //  button.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                button.AddComponent<InteractionEventElement>();
                button.AddComponent<PCBELink>();
                GameObject t = new GameObject();
                t.transform.parent = button.transform;
                t.AddComponent<RectTransform>().anchoredPosition = Vector2.zero;
                t.AddComponent<TextMeshProUGUI>();
                t.name = "Content";
                button.name = "Button(TMPro)- HLVR";
            }
            else 
            {
                GameObject button = new GameObject();
                button.transform.parent = Selection.transforms[0];
                button.AddComponent<RectTransform>().anchoredPosition = Vector2.zero;
                button.AddComponent<Image>();
                //  button.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                button.AddComponent<InteractionEventElement>();
                button.AddComponent<PCBELink>();
                GameObject t = new GameObject();
                t.transform.parent = button.transform;
                t.AddComponent<RectTransform>().anchoredPosition = Vector2.zero;
                t.AddComponent<TextMeshProUGUI>();
                t.name = "Content";
                button.name = "Button(TMPro)- HLVR";
            }
          
        }
    }
}

