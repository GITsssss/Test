
using UnityEngine;
using UnityEngine.Animations.Rigging;
//[RequireComponent(typeof())]
public class SetGesture : MonoBehaviour
{
    public HandData handData;
    public GameObject lefthand;
    public Transform[] leftbones;
    Transform leftBone;//骨骼父物体
    Quaternion[] leftori;
    
    int leftbonecount;
    public GameObject righthand;
    public Transform[] rightbones;
    Transform rightBone;//骨骼父物体
    Quaternion[] rightori;
    int rightbonecount;
    public bool m_SaveLeft;
    public bool m_SaveRight;

    public bool m_ResetLeft;
    public bool m_ResetRight;
    private void Reset()
    {
        GameObject ins_LEft = Resources.Load("LeftHand") as GameObject;      
        lefthand = Instantiate(ins_LEft);
        // lefthand.transform.parent = transform.parent;
#if UNITY_EDITOR_WIN
        lefthand.AddComponent<BoneRenderer>();
#endif
        leftBone = lefthand.transform.GetChild(0).GetChild(5);
      
        GameObject ins_Right = Resources.Load("RightHand") as GameObject;
        righthand = Instantiate(ins_Right);
        // righthand.transform.parent = transform.parent;
#if UNITY_EDITOR_WIN
        righthand.AddComponent<BoneRenderer>();
#endif
        rightBone = righthand.transform.GetChild(0).GetChild(5);
       
        GetHandBones();

        leftori = new Quaternion[leftbones.Length];
        for (int i = 0; i < leftbones.Length; i++)
        {
           leftori[i] = leftbones[i].localRotation;
        }

        rightori = new Quaternion[rightbones.Length];
        for (int i = 0; i < rightbones.Length; i++)
        {
            rightori[i] = rightbones[i].localRotation;
        }
    }


    public void GetHandBones()
    {
        leftbones = new Transform[24];
        for (int i=0;i< leftBone.childCount;i++)
        {
            leftbones = leftBone.GetComponentsInChildren<Transform>() ;
        }

#if UNITY_EDITOR_WIN
        lefthand.GetComponent<BoneRenderer>().transforms = leftbones;
#endif
        rightbones = new Transform[24];
        for (int i = 0; i < leftBone.childCount; i++)
        {
           rightbones = rightBone.GetComponentsInChildren<Transform>();
        }

# if UNITY_EDITOR_WIN
        righthand.GetComponent<BoneRenderer>().transforms = rightbones;
# endif
    }

    public void Save()
    {
        if (m_SaveLeft) 
        {
            handData.lefthand= new Quaternion[leftbones.Length];
            for (int i = 0; i < leftbones.Length; i++)
            {
                handData.lefthand[i] = leftbones[i].localRotation;
            }
          //  handData.Save();
        }

        if (m_SaveRight) 
        {
            handData.righthand = new Quaternion[rightbones.Length];
            for (int i = 0; i < rightbones.Length; i++)
            {
                handData.righthand[i] = rightbones[i].localRotation;
            }
           // handData.Save();
        }
    }


    public void ResetGesture()
    {
        if(m_ResetLeft)
        for (int i = 0; i < leftbones.Length; i++)
        {
            leftbones[i].localRotation = leftori[i];
        }

        if(m_ResetRight)
        for (int i = 0; i < rightbones.Length; i++)
        {
            rightbones[i].localRotation= rightori[i];
        }
    }

    public void CopyRightToLeft()
    {
        for (int i = 0; i < leftbones.Length; i++)
        {
            handData.lefthand[i] = handData.righthand[i];
            //   handData.righthand[i] = rightbones[i].localRotation;      
        }
    }
}
