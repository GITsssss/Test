using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HLVR.EventSystems;
public class UIManager : MonoBehaviour
{
    [Tooltip("Ĭ�ϲ㼶UI")]
    public GameObject DefaultLayer;//ԭʼ����
    [Tooltip("ȫ�ַ�����ҳ��ť����Ϊ��")]
    public InteractionEventElement back;
    [Tooltip("UIһ��ģ��")]
    public List<UIModule> m_UIModules;//uiģ��  ÿ��ֻ����һ��ģ�鴦�ڼ���״̬
    [Tooltip("UI�¼�Ԫ��ģ��")]
    public UIElementGroup[] m_UIElementGroup;
    [ReadOnly]
    public HLVRStack stack;

    static UIManager manager;
    public static UIManager Instance 
    {
        get 
        {
            return manager;
        }
    }


    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
        }
        else 
        {
            Destroy(this);
        }
        stack = new HLVRStack(0);
        for (int i = 0; i < m_UIElementGroup.Length; i++)
        {
            for (int n = 0; n < m_UIElementGroup[i].m_IEES.Length; n++)
            {
                if(m_UIElementGroup[i].IES[n]!=null)
                m_UIElementGroup[i].m_IEES[n].buttonEvent.m_Button.m_Events = m_UIElementGroup[i].IES[n];
            }
        }

        DefaultLayer.SetActive(true);
        back?.gameObject.SetActive(false);

    }

    private void OnEnable()
    {
       back?.buttonEvent.m_Button.m_Events.AddListener(Back);
    }

    private void OnDisable()
    {
        back?.buttonEvent.m_Button.m_Events.RemoveListener(Back);
    }

    public void Back()//�ر����ϲ㼶��UI
    {
        
        stack.Pop()?.SetActive(false);
        stack.Peek()?.SetActive(true);
        if (stack.Peek()?.GetComponent<UIModule>() != null)
        {
            if (!GetChildActiveState(stack.Peek().transform))
            {
                Back();
            }
        }

        if (stack.Count == 0) 
        {
           DefaultLayer.SetActive(true);
            back?.gameObject.SetActive(false);
        }
        else 
        {
            if (DefaultLayer.activeSelf)
            DefaultLayer.SetActive(false);
            if (back!=null&&!back.gameObject.activeSelf)
                back?.gameObject.SetActive(true);
        }
    }

    public void OpenModule(int index)
    {
        m_UIModules[index].gameObject.SetActive(true);
    }
    public void AddElementStack(GameObject ui)
    {
        GameObject g= stack.Peek();
        if(g.GetComponent<UIModule>()==null&&g.transform.gameObject!=ui.transform.parent.gameObject)
        g?.SetActive(false);
        stack.Push(ui);
        if (DefaultLayer.activeSelf)
            DefaultLayer.SetActive(false);
        if (back != null && !back.gameObject.activeSelf)
            back.gameObject.SetActive(true);
    }

    public void AddStack(GameObject ui)
    {
        stack.Push(ui);
        if (DefaultLayer.activeSelf)
            DefaultLayer.SetActive(false);
        if (back != null && !back.gameObject.activeSelf)
            back.gameObject.SetActive(true);
    }

    public void ReadChildInteractionEventElement()
    {
        List<InteractionEventElement> interactionEvents = new List<InteractionEventElement>();
      //  interactionEvents = transform.GetComponentsInChildren<InteractionEventElement>();
    }

    public void ActionUIModuld(GameObject current)
    { 
       for(int i=0;i< m_UIModules.Count;i++)
       {
            if (current == m_UIModules[i].gameObject)
            {
                m_UIModules[i].gameObject.SetActive(true);
            }
            else 
            {
                m_UIModules[i].gameObject.SetActive(false);
            }
       }
    }

    /// <summary>
    /// �ж��������Ƿ�
    /// </summary>
    /// <param name="self">����</param>
    /// <returns></returns>
    bool GetChildActiveState(Transform self)
    {
        bool state = false;
        for (int i = 0; i < self.childCount; i++)
        {
            if (self.GetChild(i).gameObject.activeSelf)
                state = true;
            else 
            {
                state = false;
                break;
            } 
        }

        return state;
    }
}

[System.Serializable]
public struct UIElementGroup 
{
    public string label;
    public InteractionEventElement[] m_IEES;
    public InteractionEvent[] IES;
}

[System.Serializable]
public struct HLVRStack 
{
    public List<GameObject> stack;
    public int Count;
    public HLVRStack(int index)
    {
        Count = 0;
        stack = new List<GameObject>();
    }
    /// <summary>
    /// �������Ԫ��
    /// </summary>
    public void Clear()
    {
        stack.Clear();
    }

    /// <summary>
    /// �ж�Ԫ���Ƿ����б���
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public bool Contains(GameObject obj) 
    {
       return  stack.Contains(obj);
    }
    /// <summary>
    /// �Ƴ��������б���˵�Ԫ��
    /// </summary>
    /// <returns></returns>
    public GameObject Pop()
    {
        if (stack.Count > 0)
        {
            GameObject ob = stack[stack.Count - 1];
            stack.RemoveAt(stack.Count - 1);
            Count = stack.Count;
            return ob;
        }
        else 
        {
            return null;
        }   
    }
    /// <summary>
    /// �Ƴ�ָ����Ԫ��
    /// </summary>
    /// <returns></returns>
    public void RemoveIndex(GameObject obj)
    {
        if (Contains(obj)) 
        {
            stack.Remove(obj);
            Count = stack.Count;
        }
  
        else Debug.LogWarning("��Ԫ�ز����б���");
    }

    /// <summary>
    /// ���б������һ��Ԫ��
    /// </summary>
    public void Push(GameObject obj) 
    {
        stack.Add(obj);
        Count = stack.Count;
    }
    /// <summary>
    /// �����б������һ��Ԫ��,�����Ƴ���
    /// </summary>
    public GameObject Peek() 
    {
        Count = stack.Count;
        if (stack.Count > 0)
            return stack[stack.Count - 1];
        else
            return null;
    }
}
