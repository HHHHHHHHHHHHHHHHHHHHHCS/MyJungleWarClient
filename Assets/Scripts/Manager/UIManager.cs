using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : BaseManager<UIManager>
{
    private Transform root;

    private Stack<BasePanel> panelStack;
    private Dictionary<string, BasePanel> panelDic;

    private MessagePanel messagePanel;

    public override UIManager OnInit()
    {
        panelStack = new Stack<BasePanel>();
        panelDic = new Dictionary<string, BasePanel>();
        root = GameObject.Find(UINames.uiRootPath).transform;
        foreach (var item in root.GetComponentsInChildren<BasePanel>(true))
        {
            item.OnInit();
            panelDic.Add(item.name, item);
        }
        return base.OnInit();
    }

    public T GetPanel<T>(string panelName) where T : BasePanel
    {
        BasePanel panel;
        panelDic.TryGetValue(panelName, out panel);
        if (panel)
        {
            return (T)panel;
        }
        return null;
    }

    public override void OnUpdate()
    {
        if (messagePanel)
        {
            messagePanel.UpdateByMessage();
        }
    }

    public void ShowPanel(string panelName, bool needLastHide = true)
    {
        BasePanel panel = GetPanel<BasePanel>(panelName);
        if (panel)
        {
            if (needLastHide && panelStack.Count > 0)
            {
                PausePanel(panelStack.Peek());
            }
            panelStack.Push(panel);
            ShowPanel(panel);
        }
    }

    public void ShowPanel(BasePanel panel)
    {
        panel.OnEnter();
    }

    public void HidePanel(string panelName)
    {
        BasePanel panel = GetPanel<BasePanel>(panelName);
        if (panel)
        {
            HidePanel(panel);
        }
    }

    public void HidePanel(BasePanel panel)
    {
        panel.OnExit();
    }

    public void PausePanel(BasePanel panel)
    {
        panel.OnPause();
    }

    public void ResumePanel(BasePanel panel)
    {
        panel.OnResume();
    }

    public void BackLastPanel()
    {
        if (panelStack.Count > 0)
        {
            HidePanel(panelStack.Pop());
            if (panelStack.Count > 0)
            {
                ResumePanel(panelStack.Peek());
            }
        }
    }

    public void ShowMessageSync(string data)
    {
        if (!messagePanel)
        {
            messagePanel = GetPanel<MessagePanel>(UINames.messagePanel);
        }
        if (messagePanel)
        {
            messagePanel.ShowMessageSync(data);
        }
    }

    public void ShowMessage(string data)
    {
        if (!messagePanel)
        {
            messagePanel = GetPanel<MessagePanel>(UINames.messagePanel);
        }
        if (messagePanel)
        {
            messagePanel.ShowMessage(data);
        }
    }


}
