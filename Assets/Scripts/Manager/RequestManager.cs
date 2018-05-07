using Common;
using Common.Code;
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

    private Dictionary<ActionCode, RequestActionBase> requestDic;
    private Queue<ResponeMessage> requestMessageQueue;

    public override RequestManager OnInit()
    {
        GameFacade.Instance.OnUpdateEvent += OnUpdate;
        requestDic = new Dictionary<ActionCode, RequestActionBase>();
        requestMessageQueue = new Queue<ResponeMessage>();
        var requestArray = GameFacade.Instance.GetComponentsInChildren<BaseRequest>();
        foreach (var item in requestArray)
        {
            item.OnInit();
        }
        return this;
    }

    public override void OnUpdate()
    {
        HandleResponeMessageList();
    }

    public void AddRequest(ActionCode actionCode, RequestActionBase baseRequest)
    {
        if (!requestDic.ContainsKey(actionCode))
        {
            requestDic.Add(actionCode, baseRequest);
        }
    }

    public RequestActionBase GetRequest(ActionCode actionCode)
    {
        return GetRequest<RequestActionBase>(actionCode);
    }

    public T GetRequest<T>(ActionCode actionCode) where T : RequestActionBase
    {
        RequestActionBase baseRequest = null;
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

    public void SendRequest(ActionCode actionCode, string data = "")
    {
        var code = GetRequest(actionCode);
        if (code != null)
        {
            code.SendRequest(data);
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

                RequestActionBase baseRequest;
                requestDic.TryGetValue(actionCode, out baseRequest);
                if (baseRequest != null && baseRequest.OnResponse != null)
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

    public override void OnDesotry()
    {
        GameFacade.Instance.OnUpdateEvent -= OnUpdate;
    }
}
