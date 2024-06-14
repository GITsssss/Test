using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using HLVR.Interaction;
using HLVR.InputSystem;



namespace HLVR.UI 
{
    public class Menu : MonoBehaviour
    {
        public ControllerType inputSource;
        public KeyType button;
        InputActionAsset m_Actions;
        ///public GameObject ui;
        public Canvas[] canvas;
        public GameObject[] objects;
        public int index;

        private void Update()
        {
            if (VRInput.GetKeyDown(button, inputSource))
            {

                if (canvas.Length > 0 && canvas[index].gameObject.activeSelf)
                {
                    canvas[index].gameObject.SetActive(false);

                }
                else
                {
                    for (int i = 0; i < canvas.Length; i++)
                    {
                        if (i != index)
                        {
                            canvas[i].transform.gameObject.SetActive(false);
                        }
                        else
                        {
                            canvas[i].transform.gameObject.SetActive(true);
                        }
                    }

                }

                if (objects.Length > 0 && objects[index].gameObject.activeSelf)
                {

                    objects[index].SetActive(false);
                }
                else
                {

                    for (int i = 0; i < objects.Length; i++)
                    {
                        if (i != index)
                        {
                            objects[i].transform.gameObject.SetActive(false);
                        }
                        else
                        {
                            objects[i].transform.gameObject.SetActive(true);
                        }
                    }
                }

            }
        }
    }

}
