using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Text.RegularExpressions;
using Common;
using System;

public class RegisterPanel : BasePanel
{
    private RegisterRequest registerRequest;
    private InputField usernameIF, passwordIF, repasswordIf;
    private Button closeButton, registerButton;
    private bool getMessage,isSucceed;


    public override void OnInit()
    {
        base.OnInit();
        registerRequest = GameFacade.Instance.GetRequest<RegisterRequest>(ActionCode.Register);
        Transform root = transform;
        usernameIF = root.Find(UINames.register_usernameTextPath).GetComponent<InputField>();
        passwordIF = root.Find(UINames.register_passwordTextPath).GetComponent<InputField>();
        repasswordIf = root.Find(UINames.register_repasswordTextPath).GetComponent<InputField>();
        closeButton = root.Find(UINames.closeButtonPath).GetComponent<Button>();
        closeButton.onClick.AddListener(OnCloseClick);
        registerButton = root.Find(UINames.register_registerButtonPath).GetComponent<Button>();
        registerButton.onClick.AddListener(OnRegisterClick);
    }

    public override void ResumeDefaultState()
    {
        closeButton.interactable = true;
        registerButton.interactable = true;
        usernameIF.text = string.Empty;
        passwordIF.text = string.Empty;
        repasswordIf.text = string.Empty;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (getMessage)
        {
            if (isSucceed)
            {
                GameFacade.Instance.UIManager.BackLastPanel();
                isSucceed = false;
            }
            else
            {
                registerButton.interactable = true;

            }
            getMessage = false;
        }
    }

    public override void OnEnterAnim()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(nowScale, 0.4f);
    }

    public override void OnExitAnim()
    {
        transform.DOScale(0, 0.4f);
        transform.DOLocalMove(new Vector3(1000, 0, 0), 0.4f)
            .OnComplete(() => { GameFacade.Instance.UIManager.BackLastPanel(); });
    }

    private void OnCloseClick()
    {
        closeButton.interactable = false;
        PlayClickSound();
        OnExitAnim();
    }

    private void OnRegisterClick()
    {
        registerButton.interactable = false;
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
        else if (passwordIF.text!=repasswordIf.text)
        {
            msg += "两次密码不一样";
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
            registerRequest.SendRequest(usernameIF.text, passwordIF.text);
        }
    }


    public void OnRegisterRespone(ReturnCode code)
    {
        getMessage = true;
        if (code == ReturnCode.Success)
        {
            isSucceed = true;
            GameFacade.Instance.UIManager.ShowMessageSync("注册成功！");
        }
        else if (code == ReturnCode.Fail)
        {
            GameFacade.Instance.UIManager.ShowMessageSync("注册失败，用户名重复！");
        }
    }
}
