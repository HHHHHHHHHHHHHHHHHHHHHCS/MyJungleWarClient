﻿using Common;
using Common.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterRequest : BaseRequest
{
    public override void OnInit()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Register;
        base.OnInit();
    }

    public void SendRequest(string username, string password)
    {
        string data = username + "," + password;
        SendRequest(data);
    }

    public override void OnResponse(string data)
    {
        string[] result = data.Split(',');
        ReturnCode returnCode = (ReturnCode)int.Parse(result[0]);
        GameFacade.Instance.UIManager.GetPanel<RegisterPanel>(UINames.registerPanel)
            .OnRegisterRespone(returnCode);
    }
}
