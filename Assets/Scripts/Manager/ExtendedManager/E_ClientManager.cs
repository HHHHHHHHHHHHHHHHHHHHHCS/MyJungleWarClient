using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class E_ClientManager
{
    public static void Send(this GameFacade facade, RequestCode requestCode
        , ActionCode actionCode, string data)
    {
        facade.ClientManager.SendRequest(requestCode,actionCode, data);
    }
}
