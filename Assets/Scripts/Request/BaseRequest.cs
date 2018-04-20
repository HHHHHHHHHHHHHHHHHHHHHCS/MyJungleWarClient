using Common;
using Common.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRequest : MonoBehaviour
{
    protected RequestCode requestCode = RequestCode.None;
    protected ActionCode actionCode = ActionCode.None;

    public virtual void OnInit()
    {
        GameFacade.Instance.AddRequest(actionCode, this);
    }

    public virtual void SendRequest(string data)
    {
        GameFacade.Instance.Send(requestCode, actionCode, data);
    }

    public virtual void SendRequest()
    {
        GameFacade.Instance.Send(requestCode, actionCode, string.Empty);
    }

    public virtual void OnResponse(string data)
    {

    }


    protected virtual void OnDestroy()
    {
        GameFacade.Instance.RemoveRequest(actionCode);
    }
}
