using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using Common;

public class LoginPanel : BasePanel
{
    private InputField usernameIF;
    private InputField passwordIF;
    private LoginRequest loginRequest;


    public override void OnInit()
    {
        base.OnInit();
        loginRequest = GameFacade.Instance.GetRequest<LoginRequest>(ActionCode.Login);
        Transform root = transform;
        usernameIF = root.Find(UINames.login_usernameTextPath).GetComponent<InputField>();
        passwordIF = root.Find(UINames.login_passwordTextPath).GetComponent<InputField>();
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
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOScale(nowScale, 0.6f);
        transform.DOLocalMove(nowPos, 0.4f);
    }

    private void OnCloseClick()
    {
        PlayClickSound();
        transform.DOScale(0, 0.4f);
        transform.DOLocalMove(new Vector3(1000, 0, 0), 0.4f)
            .OnComplete(GameFacade.Instance.UIManager.BackLastPanel);
    }

    private void OnLoginClick()
    {
        PlayClickSound();
        string msg = string.Empty;
        if (string.IsNullOrEmpty(usernameIF.text))
        {
            msg += "用户名不能为空";
        }
        else if (string.IsNullOrEmpty(passwordIF.text))
        {
            msg += "密码不能为空";
        }
        else if (Regex.IsMatch(usernameIF.text, @"[\,]")
            || Regex.IsMatch(passwordIF.text, @"[\,]"))
        {
            msg += "帐号密码有非法字符";
        }

        if (!string.IsNullOrEmpty(msg))
        {
            GameFacade.Instance.UIManager.ShowMessage(msg);
        }
        else
        {
            loginRequest.SendRequest(usernameIF.text, passwordIF.text);
        }
    }

    public void OnLoginRespone(ReturnCode code)
    {
        if (code == ReturnCode.Success)
        {
            GameFacade.Instance.UIManager.ShowMessageSync("登录成功！");
        }
        else if (code == ReturnCode.Fail)
        {
            GameFacade.Instance.UIManager.ShowMessageSync("用户名或密码错误！");
        }
    }

    private void OnRegisterClick()
    {
        GameFacade.Instance.UIManager.ShowPanel(UINames.registerPanel);
    }
}
