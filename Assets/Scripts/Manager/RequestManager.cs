using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestManager : BaseManager<RequestManager>
{
    private Dictionary<RequestCode, BaseRequest> requestDic
        = new Dictionary<RequestCode, BaseRequest>();

    public RequestManager(GameFacade facade) : base(facade)
    {

    }


    public void AddRequest(RequestCode requestCode, BaseRequest baseRequest)
    {
        if (!requestDic.ContainsKey(requestCode))
        {
            requestDic.Add(requestCode, baseRequest);
        }
    }

    public void RemoveRequest(RequestCode requestCode)
    {
        if (requestDic.ContainsKey(requestCode))
        {
            requestDic.Remove(requestCode);
        }
    }


    public virtual void HandleRespone(RequestCode requestCode, string data)
    {
        BaseRequest baseRequest;
        requestDic.TryGetValue(requestCode, out baseRequest);
        if(baseRequest)
        {
            baseRequest.OnResponse(data);
        }
        else
        {
            Debug.Log("无法得到RequestCode["+requestCode + "]对应的BaseRequest");
        }
    }
}
