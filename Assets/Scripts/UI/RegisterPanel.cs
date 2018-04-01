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
    private InputField usernameIF;
    private InputField passwordIF;
    private InputField repasswordIf;
    private RegisterRequest registerRequest; 


    public override void OnInit()
    {
        base.OnInit();
        registerRequest = GameFacade.Instance.GetRequest<RegisterRequest>(ActionCode.Register);
        usernameIF = transform.Find(UINames.register_usernameTextPath).GetComponent<InputField>();
        passwordIF = transform.Find(UINames.register_passwordTextPath).GetComponent<InputField>();
        repasswordIf = transform.Find(UINames.register_repasswordTextPath).GetComponent<InputField>();
        transform.Find(UINames.closeButtonPath).GetComponent<Button>()
            .onClick.AddListener(OnCloseClick);
        transform.Find(UINames.register_registerButtonPath).GetComponent<Button>()
            .onClick.AddListener(OnRegisterClick);
    }


    public override void OnEnter()
    {
        var nowScale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(nowScale, 0.4f);
        base.OnEnter();
    }

    private void OnCloseClick()
    {
        transform.DOScale(0, 0.4f);
        transform.DOLocalMove(new Vector3(1000, 0, 0), 0.4f)
            .OnComplete(() => { GameFacade.Instance.UIManager.BackLastPanel(); });
    }

    private void OnRegisterClick()
    {
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
        if (code == ReturnCode.Success)
        {
            GameFacade.Instance.UIManager.ShowMessageSync("注册成功！");
        }
        else if (code == ReturnCode.Fail)
        {
            GameFacade.Instance.UIManager.ShowMessageSync("注册失败，用户名重复！");
        }
    }
}
