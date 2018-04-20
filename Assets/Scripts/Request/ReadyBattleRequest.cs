using Common.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyBattleRequest : BaseRequest
{
    public override void OnInit()
    {
        requestCode = RequestCode.ClientRoom;
        actionCode = ActionCode.ClientRoom_Ready;
        base.OnInit();
    }

    public void SendRequest(bool isReady)
    {
        base.SendRequest(((int)(isReady ? ReturnCode.True : ReturnCode.Fail)).ToString());
    }

    public override void OnResponse(string data)
    {
        string[] readyUsernameArray = data.Split(',');
        var roomPanel = GameFacade.Instance.UIManager.GetPanel<RoomPanel>(UINames.roomPanel);
        roomPanel.UserReady(readyUsernameArray);
    }
}

