using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRequest : MonoBehaviour
{
    private RequestCode requestCode = RequestCode.None;

    public void Awake()
    {
        GameFacade.Instance.AddRequest(requestCode, this);
    }

    public virtual void SendRequest()
    {

    }

    public virtual void OnResponse(string data)
    {

    }


    protected virtual void OnDestroy()
    {
        GameFacade.Instance.RemoveRequest(requestCode);
    }
}
