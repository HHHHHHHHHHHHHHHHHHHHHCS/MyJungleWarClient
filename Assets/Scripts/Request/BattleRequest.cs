using Common.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRequest : BaseRequest
{
    public override void OnInit()
    {
        base.OnInit();
        requestCode = RequestCode.Battle;
        requestActionSet.Add(CreateBase(ActionCode.Battle_CanPlay, OnResponse_CanPlay));
        //requestActionSet.Add(CreateBase(ActionCode.Battle_Move, null));
    }

    private void OnResponse_CanPlay(string data)
    {
        ((Game_PlayerManager)GameFacade.Instance.PlayerManager).SetCurrentRoleData();
    }
}
