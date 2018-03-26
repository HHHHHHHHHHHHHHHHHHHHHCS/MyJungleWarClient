using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    public override void OnInit()
    {
        base.OnInit();
        transform.Find(UINames.showLoginButtonPath).GetComponent<Button>()
            .onClick.AddListener(ClickLoginButton);
    }

    public void ClickLoginButton()
    {
        OnExit();
        GameFacade.Instance.UIManager.ShowPanel(UINames.loginPanel);
    }

    public override void OnExit()
    {
        gameObject.SetActive(false);
        base.OnExit();
    }
}
