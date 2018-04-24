using Common;
using Common.Code;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRequest : MonoBehaviour
{
    protected RequestCode requestCode;
    protected HashSet<RequestActionBase> requestActionSet;

    public virtual void OnInit()
    {
        requestActionSet = new HashSet<RequestActionBase>();
    }

    public RequestActionBase CreateBase(ActionCode actionCode,Action<string> callBack)
    {
        return new RequestActionBase(requestCode, actionCode, callBack);
    }

    protected virtual void OnDestroy()
    {
        //foreach(var item in requestActionSet)
        //{
        //    GameFacade.Instance.RemoveRequest(item.ActionCode);
        //}
    }
}
