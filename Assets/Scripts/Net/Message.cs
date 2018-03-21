﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Common;

public class Message
{

    private byte[] data = new byte[1024];
    private int startIndex = 0;//我们存取了多少个字节的数据在数组里面

    public byte[] Data
    {
        get
        {
            return data;
        }
    }

    public int StartIndex
    {
        get
        {
            return startIndex;
        }
    }

    public int RemainIndex
    {
        get
        {
            return data.Length - startIndex;
        }
    }

    public byte[] Length
    {
        get; set;
    }


    private void AddIndex(int count)
    {
        startIndex += count;
    }


    public string GetOneContent(int newDataAmount
        , Action<RequestCode, string> processDataCallBack)
    {
        AddIndex(newDataAmount);
        if (startIndex <= 4)
        {
            return null;
        }
        int count = BitConverter.ToInt32(data, 0);
        if ((startIndex - 4) >= count)
        {
            RequestCode requestCode = (RequestCode)BitConverter.ToInt32(data, 4);
            ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 8);
            string str = Encoding.UTF8.GetString(data, 8, count - 4);

            Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);
            startIndex -= count + 4;
            processDataCallBack(requestCode, str);
            return str;
        }
        return null;
    }


    public List<string> GetAllContent(int newDataAmount
        , Action<RequestCode, string> processDataCallBack)
    {
        List<string> strList = new List<string>();
        while (true)
        {
            var str = GetOneContent(newDataAmount, processDataCallBack);
            if (str != null)
            {
                strList.Add(str);
            }
            else
            {
                break;
            }
        }
        return strList;
    }

    public static byte[] PackData(RequestCode requestCode, string data)
    {
        byte[] requestCodeBytes = BitConverter.GetBytes((int)requestCode);
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        int dataAmount = requestCodeBytes.Length + dataBytes.Length;
        byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
        return dataAmountBytes.Concat(requestCodeBytes).Concat(dataBytes).ToArray();
    }

    public static byte[] PackData(RequestCode requestCode, ActionCode actionCode, string data)
    {
        byte[] requestCodeBytes = BitConverter.GetBytes((int)requestCode);
        byte[] actionCodeBytes = BitConverter.GetBytes((int)actionCode);
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        int dataAmount = requestCodeBytes.Length + actionCodeBytes.Length + dataBytes.Length;
        byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
        return dataAmountBytes.Concat(requestCodeBytes).Concat(actionCodeBytes).Concat(dataBytes).ToArray();
    }
}