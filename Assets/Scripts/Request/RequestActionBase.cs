using Common.Code;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestActionBase
{
    public RequestCode RequestCode { get; private set; }
    public ActionCode ActionCode { get; private set; }
    public Action<string> OnResponse;

    public RequestActionBase(RequestCode requestCode, ActionCode action, Action<string> onResponse)
    {
        RequestCode = requestCode;
        ActionCode = action;
        OnResponse += onResponse;
        GameFacade.Instance.AddRequest(ActionCode, this);
    }

    public void SendRequest(string data)
    {
        GameFacade.Instance.SendRequest(RequestCode, ActionCode, data);
    }
}
