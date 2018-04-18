using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using Common;
using Common.Code;

public class LoginPanel : BasePanel
{
    private LoginRequest loginRequest;
    private InputField usernameIF, passwordIF;
    private Button clsoeButton, loginButton, registerButton;
    private bool getMessage = false;

    public override void OnInit()
    {
        base.OnInit();
        loginRequest = GameFacade.Instance.GetRequest<LoginRequest>(ActionCode.Login);
        Transform root = transform;
        usernameIF = root.Find(UINames.login_usernameTextPath).GetComponent<InputField>();
        passwordIF = root.Find(UINames.login_passwordTextPath).GetComponent<InputField>();
        clsoeButton = root.Find(UINames.closeButtonPath).GetComponent<Button>();
        clsoeButton.onClick.AddListener(OnCloseClick);
        loginButton = root.Find(UINames.loginButton).GetComponent<Button>();
        loginButton.onClick.AddListener(OnLoginClick);
        registerButton = root.Find(UINames.showRegisterButton).GetComponent<Button>();
        registerButton.onClick.AddListener(OnRegisterClick);
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void ResumeDefaultState()
    {
        clsoeButton.interactable = true;
        loginButton.interactable = true;
        registerButton.interactable = true;
        usernameIF.text = string.Empty;
        passwordIF.text = string.Empty;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (getMessage)
        {
            clsoeButton.interactable = true;
            loginButton.interactable = true;
            getMessage = false;
        }
    }

    public override void OnEnterAnim()
    {
        transform.localScale = Vector3.zero;
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOScale(nowScale, 0.6f);
        transform.DOLocalMove(nowPos, 0.4f);
    }

    public override void OnExitAnim()
    {
        transform.DOScale(0, 0.4f);
        transform.DOLocalMove(new Vector3(1000, 0, 0), 0.4f)
            .OnComplete(GameFacade.Instance.UIManager.BackLastPanel);
    }

    private void OnCloseClick()
    {
        clsoeButton.interactable = false;
        PlayClickSound();
        OnExitAnim();
    }

    private void OnLoginClick()
    {
        clsoeButton.interactable = false;
        loginButton.interactable = false;
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
            getMessage = true;
            GameFacade.Instance.UIManager.ShowMessage(msg);
        }
        else
        {
            loginRequest.SendRequest(usernameIF.text, passwordIF.text);
        }
    }

    public void OnLoginRespone(ReturnCode code)
    {
        getMessage = true;
        if (code == ReturnCode.Success)
        {
            GameFacade.Instance.UIManager.ShowMessage("登录成功！");

        }
        else if (code == ReturnCode.Fail)
        {
            GameFacade.Instance.UIManager.ShowMessage("用户名或密码错误！");
        }
    }

    private void OnRegisterClick()
    {
        registerButton.interactable = false;
        GameFacade.Instance.UIManager.ShowPanel(UINames.registerPanel);
    }
}
