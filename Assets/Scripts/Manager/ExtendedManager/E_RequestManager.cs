﻿using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class E_RequestManager
{
    public static void AddRequest(this GameFacade facade
        , ActionCode actionCode, BaseRequest baseRequest)
    {
        facade.RequestManager.AddRequest(actionCode, baseRequest);
    }

    public static void RemoveRequest(this GameFacade facade
        , ActionCode actionCode)
    {
        facade.RequestManager.RemoveRequest(actionCode);
    }

    public static void HandleRespone(this GameFacade facade
        , ActionCode actionCode, string data)
    {
        facade.RequestManager.HandleRespone(actionCode, data);
    }



}
