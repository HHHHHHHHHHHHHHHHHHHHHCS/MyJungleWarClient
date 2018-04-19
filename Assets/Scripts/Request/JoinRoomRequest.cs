using Common.Code;
using Common.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinRoomRequest : BaseRequest
{
    public override void OnInit()
    {
        requestCode = RequestCode.ClientRoom;
        actionCode = ActionCode.ClientRoom_Join;
        base.OnInit();
    }

    public new void SendRequest(string username)
    {
        base.SendRequest(username);
    }

    public override void OnResponse(string data)
    {
        string[] result = data.Split(',');
        ReturnCode returnCode = (ReturnCode)int.Parse(result[0]);
        if (returnCode == ReturnCode.Success)
        {
            UserData homeUserData = new UserData(result[1], int.Parse(result[2]), int.Parse(result[3]));
            UserData awayUserData = new UserData(result[4], int.Parse(result[5]), int.Parse(result[6]));
            GameFacade.Instance.UIManager.GetPanel<RoomListPanel>(UINames.roomListPanel)
                .EnterRoomPanel(homeUserData, awayUserData);
        }
        else if (returnCode == ReturnCode.Fail)
        {
            GameFacade.Instance.RequestManager.GetRequest<ShowRoomListRequest>(ActionCode.ClientRoom_Show)
                .SendRequest();
            GameFacade.Instance.UIManager.ShowMessage("加入失败！");
        }
    }
}
