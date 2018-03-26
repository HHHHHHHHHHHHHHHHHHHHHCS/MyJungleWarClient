using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestManager : BaseManager<RequestManager>
{
    private Dictionary<ActionCode, BaseRequest> requestDic
        = new Dictionary<ActionCode, BaseRequest>();


    public void AddRequest(ActionCode actionCode, BaseRequest baseRequest)
    {
        if (!requestDic.ContainsKey(actionCode))
        {
            requestDic.Add(actionCode, baseRequest);
        }
    }

    public void RemoveRequest(ActionCode actionCode)
    {
        if (requestDic.ContainsKey(actionCode))
        {
            requestDic.Remove(actionCode);
        }
    }

    public virtual void HandleRespone(ActionCode actionCode, string data)
    {
        BaseRequest baseRequest;
        requestDic.TryGetValue(actionCode, out baseRequest);
        if(baseRequest)
        {
            baseRequest.OnResponse(data);
        }
        else
        {
            Debug.Log("无法得到ActionCode["+ actionCode + "]对应的BaseRequest");
        }
    }
}
