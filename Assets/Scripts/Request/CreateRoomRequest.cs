using Common;
using Common.Code;
using Common.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoomRequest : BaseRequest
{
    public override void OnInit()
    {
        requestCode = RequestCode.ClientRoom;
        actionCode = ActionCode.ClientRoom_Create;
        base.OnInit();
    }

    public override void SendRequest()
    {
        base.SendRequest("");
    }

    public override void OnResponse(string data)
    {
        string[] result = data.Split(',');
        ReturnCode returnCode = (ReturnCode)int.Parse(result[0]);
        if (returnCode == ReturnCode.Success)
        {
            var roomPanel = GameFacade.Instance.UIManager.GetPanel<RoomPanel>(UINames.roomPanel);
            UserData homeUserData = new UserData(result[1], int.Parse(result[2]), int.Parse(result[3]));
            roomPanel.SetHomeInfo(homeUserData);
            GameFacade.Instance.UIManager.ShowPanel(roomPanel);
            GameFacade.Instance.UIManager.ShowMessage("创建成功！");
        }
    }
}
