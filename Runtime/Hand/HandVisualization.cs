using HLVR.InputSystem;
using HLVR.Interaction;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[RequireComponent(typeof(GrabObjects))]
public class HandVisualization : MonoBehaviour
{
    GrabObjects grab;
    public HandData handData;
    public GameObject[] visualization;//0left 1right
    public Transform[] bonesL,bonesR;
    [ReadOnly]
    public GameObject handleft, handright;
    [ReadOnly]
    public Transform[] bonesEdior = new Transform[25];
    private void Awake()
    {
        grab = GetComponent<GrabObjects>();
        bonesL = new Transform[25];
        bonesR = new Transform[25];
       
        switch (grab.controllerType) 
        {
            case ControllerType.Left:
                handleft = Instantiate(visualization[0]);
                handleft.transform.parent = this.transform;
              bonesL = handleft.transform.GetChild(0).GetChild(5).GetComponentsInChildren<Transform>();
                break;
            case ControllerType.Right:
                handright = Instantiate(visualization[1]);
                handright.transform.parent = this.transform;
                bonesR = handright.transform.GetChild(0).GetChild(5).GetComponentsInChildren<Transform>();
                break;
            case ControllerType.All:
                handleft = Instantiate(visualization[0]);
                handright = Instantiate(visualization[1]);
                handleft.transform.parent = this.transform;
                handright.transform.parent = this.transform;
                bonesL = handleft.transform.GetChild(0).GetChild(5).GetComponentsInChildren<Transform>();
                bonesR = handright.transform.GetChild(0).GetChild(5).GetComponentsInChildren<Transform>();
                break;
        }
      
       
        Gesture();
        Hide();
    }

    private void OnEnable()
    {
        grab.grabEvent.grabEnter.AddListener(Show);
        grab.grabEvent.Fall.AddListener(Hide);
    }

    private void OnDisable()
    {
       grab.grabEvent.grabEnter.RemoveListener(Show);
        grab.grabEvent.Fall.RemoveListener(Hide);
    }


    public void Show()
    {
        Debug.LogWarning("123456987"+ grab.currentHand.name);
        if (grab.currentHand != null) 
        {
            switch (grab.currentHand.inputSource)
            {
                case HLVR.InputSystem.ControllerType.Left:
                    handleft.SetActive(true);
                    if (grab.grabResponse.ToString().Contains("GrabPointAsCenter"))
                    {
                        handleft.transform.localPosition = grab.currentHand.CollideredgePoint;
                        handleft.transform.localRotation = handData.leftLocalRotation;
                    }
                    else 
                    {
                        handleft.transform.localPosition = handData.leftLocalPosition;
                        handleft.transform.localRotation = handData.leftLocalRotation;
                    }
                  
                    break;
                case HLVR.InputSystem.ControllerType.Right:
                    handright.SetActive(true);
                    if (grab.grabResponse.ToString().Contains("GrabPointAsCenter"))
                    {
                        handright.transform.localPosition = grab.currentHand.CollideredgePoint;
                        handright.transform.localRotation = handData.leftLocalRotation;
                    }
                    else 
                    {
                        handright.transform.localPosition = handData.rightLocalPosition;
                        handright.transform.localRotation = handData.rightLocalRotation;
                    }
                 
                    break;
            }
        }
    }

    public void Hide()
    {
        grab.HideHand = true;
        handright?.SetActive(false);
        if(handleft!=null)
        handleft?.SetActive(false);
    }


    public void Gesture()
    {

        switch (grab.controllerType)
        {
            case ControllerType.Left:
                for (int i = 0; i < bonesL.Length; i++)
                {
                    bonesL[i].localRotation = handData.lefthand[i];
                }
                break;

            case ControllerType.Right:
                for (int i = 0; i < bonesR.Length; i++)
                {
                    bonesR[i].localRotation = handData.righthand[i];
                }
                break;
            case ControllerType.All:
                for (int i = 0; i < bonesL.Length; i++)
                {
                    bonesL[i].localRotation = handData.lefthand[i];
                };
                for (int i = 0; i < bonesR.Length; i++)
                {
                    bonesR[i].localRotation = handData.righthand[i];
                }
                break;
        }
    }
}
