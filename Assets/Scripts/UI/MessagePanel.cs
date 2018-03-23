using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : BasePanel
{
    [SerializeField]
    private float delayShowTime = 2f;
    [SerializeField]
    private float hideTime = 2.5f;
    private Text text;


    public override void OnInit()
    {
        text = GetComponentInChildren<Text>(true);
        text.gameObject.SetActive(false);
    }

    public override void OnEnter()
    {
        gameObject.SetActive(true);
        base.OnEnter();
    }


    public void ShowMessage(string msg)
    {
        text.gameObject.SetActive(true);
        text.text = msg;
        Invoke("Hide", delayShowTime);
        OnEnter();
    }

    private void Hide()
    {
        text.CrossFadeAlpha(0f, hideTime, false);
    }
}
