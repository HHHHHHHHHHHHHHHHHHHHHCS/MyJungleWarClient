using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestManager : BaseManager<RequestManager>
{
    private struct ResponeMessage
    {
        public ActionCode actionCode;
        public string data;

        public ResponeMessage(ActionCode _actionCode, string _data)
        {
            actionCode = _actionCode;
            data = _data;
        }
    }


    private Dictionary<ActionCode, BaseRequest> requestDic
        = new Dictionary<ActionCode, BaseRequest>();
    private Queue<ResponeMessage> requestMessageQueue
         = new Queue<ResponeMessage>();

    public override RequestManager OnInit()
    {
        var requestArray = GameFacade.Instance.GetComponentsInChildren<BaseRequest>();
        foreach (var item in requestArray)
        {
            item.OnInit();
        }
        return base.OnInit();
    }

    public override void OnUpdate()
    {
        HandleResponeMessageList();
    }

    public void AddRequest(ActionCode actionCode, BaseRequest baseRequest)
    {
        if (!requestDic.ContainsKey(actionCode))
        {
            requestDic.Add(actionCode, baseRequest);
        }
    }

    public T GetRequest<T>(ActionCode actionCode) where T : BaseRequest
    {
        BaseRequest baseRequest = null;
        requestDic.TryGetValue(actionCode, out baseRequest);
        return (T)baseRequest;
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
        lock (requestMessageQueue)
        {
            requestMessageQueue.Enqueue(new ResponeMessage(actionCode, data));
        }
    }

    public virtual void HandleResponeMessageList()
    {
        lock (requestMessageQueue)
        {
            ActionCode actionCode;
            string data;
            while (requestMessageQueue.Count > 0)
            {
                var message = requestMessageQueue.Dequeue();
                actionCode = message.actionCode;
                data = message.data;

                BaseRequest baseRequest;
                requestDic.TryGetValue(actionCode, out baseRequest);
                if (baseRequest)
                {
                    baseRequest.OnResponse(data);
                }
                else
                {
                    Debug.Log("无法得到ActionCode[" + actionCode + "]对应的BaseRequest");
                }
            }
        }
    }
}
