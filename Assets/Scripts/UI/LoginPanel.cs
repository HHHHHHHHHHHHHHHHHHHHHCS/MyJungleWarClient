using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    private InputField usernameIF;
    private InputField passwordIF;

    public override void OnInit()
    {
        base.OnInit();
        Transform root = transform;
        usernameIF = root.Find(UINames.usernameTextPath).GetComponent<InputField>();
        passwordIF = root.Find(UINames.passwordTextPath).GetComponent<InputField>();
        root.Find(UINames.closeButtonPath).GetComponent<Button>()
            .onClick.AddListener(OnCloseClick);
        root.Find(UINames.loginButton).GetComponent<Button>()
            .onClick.AddListener(OnLoginClick);
        root.Find(UINames.showRegisterButton).GetComponent<Button>()
            .onClick.AddListener(OnRegisterClick);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.6f);
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.4f);
    }

    private void OnCloseClick()
    {
        transform.DOScale(0, 0.4f);
        transform.DOLocalMove(new Vector3(1000, 0, 0), 0.4f)
            .OnComplete(GameFacade.Instance.UIManager.BackLastPanel);
    }

    private void OnLoginClick()
    {
        string msg = string.Empty;
        if(string.IsNullOrEmpty(usernameIF.text))
        {
            msg += "用户名不能为空";
        }
        else if (string.IsNullOrEmpty(passwordIF.text))
        {
            msg += "密码不能为空";
        }

        if (!string.IsNullOrEmpty(msg))
        {
            GameFacade.Instance.UIManager.ShowMessage(msg);
        }
        else
        {

        }
    }

    private void OnRegisterClick()
    {
        GameFacade.Instance.UIManager.ShowPanel(UINames.registerPanel);
    }
}
