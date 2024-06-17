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
        [MenuItem("HLVR/UI/UIManager")]
        public static void UIManager()
        {
            if (FindObjectOfType<UIManager>() == null)
            {
                GameObject g = new GameObject("CanvasManager");
                g.AddComponent<Canvas>();
                g.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
                g.AddComponent<CanvasScaler>();
                g.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                g.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920, 1080);
                g.AddComponent<GraphicRaycaster>();
                g.AddComponent<UIManager>();
                GameObject bg = new GameObject("BackGround");
                bg.transform.parent = g.transform;
                bg.AddComponent<RectTransform>();
                bg.GetComponent<RectTransform>().sizeDelta = new Vector2(1920, 1080);
                bg.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
                bg.AddComponent<CanvasRenderer>();
                bg.AddComponent<Image>();
                bg.GetComponent<Image>().color = Color.gray;

                GameObject dl = new GameObject("DefaultLayer");
                dl.transform.parent = bg.transform;
                dl.AddComponent<RectTransform>();
                dl.GetComponent<RectTransform>().sizeDelta = new Vector2(800, 500);
                dl.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
                dl.AddComponent<CanvasRenderer>();
                dl.AddComponent<Image>();
                dl.GetComponent<Image>().color = Color.gray+new Color(0.2f,0.2f,0.2f);

                g.GetComponent<UIManager>().DefaultLayer = dl;
            }
            else 
            {
                Debug.LogError("当前场景中已经存在UIMnager");
            }
           
        }
        [MenuItem("GameObject/UI/UIModule")]
        public static void UIModule()
        {
            if (FindObjectOfType<UIManager>() != null) 
            {
                if (FindObjectOfType<UIManager>().transform.childCount > 0)
                {
                    GameObject dl = new GameObject("UIModule");
                    dl.AddComponent<UIModule>();
                    dl.transform.parent = FindObjectOfType<UIManager>().transform.GetChild(0);
                    dl.name= dl.name+ (FindObjectOfType<UIManager>().transform.GetChild(0).childCount-1).ToString();
                    dl.AddComponent<RectTransform>();
                    dl.GetComponent<RectTransform>().sizeDelta = new Vector2(1920, 1080);
                    dl.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
                    dl.AddComponent<CanvasRenderer>();

                    FindObjectOfType<UIManager>().m_UIModules.Add(dl.GetComponent<UIModule>());
                }
                else 
                {
                    GameObject bg = new GameObject("BackGround");
                    bg.transform.parent = FindObjectOfType<UIManager>().transform;
                    bg.AddComponent<RectTransform>();
                    bg.GetComponent<RectTransform>().sizeDelta = new Vector2(1920, 1080);
                    bg.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
                    bg.AddComponent<CanvasRenderer>();
                    bg.AddComponent<Image>();
                    bg.GetComponent<Image>().color = Color.gray;
                  
                    GameObject dl = new GameObject("UIModule"+bg.transform.childCount);
                    dl.transform.parent = bg.transform;
                    dl.AddComponent<UIModule>();
                    dl.AddComponent<RectTransform>();
                    dl.GetComponent<RectTransform>().sizeDelta = new Vector2(1920, 1080);
                    dl.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
                    dl.AddComponent<CanvasRenderer>();
                }
            }
           
        }


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

