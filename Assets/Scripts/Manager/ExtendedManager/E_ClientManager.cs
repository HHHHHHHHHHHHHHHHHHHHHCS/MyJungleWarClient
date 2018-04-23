using Common;
using Common.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class E_ClientManager
{
    public static void SendRequest(this GameFacade facade
        , ActionCode actionCode, string data = "")
    {
        facade.RequestManager.SendRequest(actionCode, data);
    }

    public static void SendRequest(this GameFacade facade, RequestCode requestCode
        , ActionCode actionCode, string data = "")
    {
        facade.ClientManager.SendRequest(requestCode, actionCode, data);
    }
}
