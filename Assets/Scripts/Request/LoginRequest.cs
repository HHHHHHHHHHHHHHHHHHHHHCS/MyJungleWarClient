using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginRequest : BaseRequest
{
    protected override void Awake()
    {
        requestCode = Common.RequestCode.User;
        actionCode = Common.ActionCode.Login;
        base.Awake();
    }

    public void SendRequest(string username,string password)
    {
        string data = username + "," + password;
        SendRequest(data);
    }
}
