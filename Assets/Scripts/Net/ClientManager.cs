using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using Common;
using Common.Code;

public class ClientManager : BaseManager<ClientManager>
{
    private const string ip = "127.0.0.1";
    private const int port = 2333;

    private Socket clientSocket;
    private Message msg;


    public void FirstConnect()
    {
        if (clientSocket == null)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork
                , SocketType.Stream, ProtocolType.Tcp);
            msg = new Message();
            try
            {
                clientSocket.Connect(ip, port);
                StartReceive();
            }
            catch (Exception e)
            {
                Debug.Log("连接socket失败：" + e);
            }
        }
    }

    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
    {
        FirstConnect();
        byte[] bytes = Message.PackData(requestCode, actionCode, data);
        clientSocket.Send(bytes);
    }

    public void StartReceive()
    {
        clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainIndex, SocketFlags.None, ReceiveCallBack, null);
    }

    public void ReceiveCallBack(IAsyncResult ar)
    {
        try
        {
            if (clientSocket == null || !clientSocket.Connected)
            {
                return;
            }
            int count = clientSocket.EndReceive(ar);
            msg.GetOneContent(count, OnProcessDataCallBack);
            StartReceive();
        }
        catch (Exception e)
        {
            Debug.Log("接受数据出现异常：" + e);
        }
    }

    private void OnProcessDataCallBack(ActionCode actionCode, string data)
    {
        GameFacade.Instance.HandleRespone(actionCode, data);
    }

    public override void OnDesotry()
    {
        base.OnDesotry();
        try
        {
            if (clientSocket != null && clientSocket.Connected)
            {
                clientSocket.Close();
            }
        }
        catch (Exception e)
        {
            Debug.Log("关闭socket失败：" + e);
        }
    }
}
