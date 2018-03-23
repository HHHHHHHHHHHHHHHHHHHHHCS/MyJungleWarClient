using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : BaseManager<UIManager>
{
    private Transform root;

    private Dictionary<string, BasePanel> panelDic;

    public override UIManager OnInit()
    {
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

    public void ShowPanel(string panelName)
    {
        BasePanel panel = GetPanel<BasePanel>(panelName);
        if (panel)
        {
            panel.OnEnter();
        }
    }

    public void HidePanel(string panelName)
    {
        BasePanel panel = GetPanel<BasePanel>(panelName);
        if (panel)
        {
            panel.OnExit();
        }
    }

    public void ShowMessage(string data)
    {
        var panel = GetPanel<MessagePanel>(UINames.messagePanel);
        if (panel)
        {
            panel.ShowMessage(data);
        }
    }
}
