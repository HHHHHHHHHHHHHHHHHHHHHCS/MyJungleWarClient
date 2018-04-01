﻿using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginRequest : BaseRequest
{
    public override void OnInit()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Login;
        base.OnInit();
    }

    public void SendRequest(string username, string password)
    {
        string data = username + "," + password;
        SendRequest(data);
    }

    public override void OnResponse(string data)
    {
        ReturnCode returnCode = (ReturnCode)int.Parse(data);
        GameFacade.Instance.UIManager.GetPanel<LoginPanel>(UINames.loginPanel)
            .OnLoginRespone(returnCode);
    }
}
