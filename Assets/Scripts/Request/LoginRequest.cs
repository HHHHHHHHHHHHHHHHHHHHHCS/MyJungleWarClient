using Common;
using Common.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginRequest : BaseRequest
{
    public override void OnInit()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Login;
        base.OnInit();
    }

    public void SendRequest(string username, string password)
    {
        string data = username + "," + password;
        SendRequest(data);
    }

    public override void OnResponse(string data)
    {
        string[] result = data.Split(',');
        ReturnCode returnCode = (ReturnCode)int.Parse(result[0]);
        GameFacade.Instance.UIManager.GetPanel<LoginPanel>(UINames.loginPanel)
            .OnLoginRespone(returnCode);
        if (returnCode == ReturnCode.Success)
        {
            var roomListPanel = GameFacade.Instance.UIManager.GetPanel<RoomListPanel>(UINames.roomListPanel);
            roomListPanel.UpdateBattleInfo(result[1], result[2], result[3]);
            GameFacade.Instance.UIManager.ShowPanel(roomListPanel);
        }
    }
}
