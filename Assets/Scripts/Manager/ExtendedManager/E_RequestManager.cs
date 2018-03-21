using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class E_RequestManager
{
    public static void AddRequest(this GameFacade facade
        ,RequestCode requestCode,BaseRequest baseRequest)
    {
        facade.RequestManager.AddRequest(requestCode, baseRequest);
    }

    public static void RemoveRequest(this GameFacade facade
        , RequestCode requestCode)
    {
        facade.RequestManager.RemoveRequest(requestCode);
    }

    public static void HandleRespone(this GameFacade facade
        , RequestCode requestCode,string data)
    {
        facade.RequestManager.HandleRespone(requestCode, data);
    }

}
