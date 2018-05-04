using Common.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRequest : BaseRequest
{
    public override void OnInit()
    {
        base.OnInit();
        requestCode = RequestCode.Game;
        requestActionSet.Add(CreateBase(ActionCode.Game_CanDo, OnResponse_CanDo));
    }

    private void OnResponse_CanDo(string data)
    {

    }
}
