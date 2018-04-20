using Common.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveRoomRequest : BaseRequest
{
    public override void OnInit()
    {
        requestCode = RequestCode.ClientRoom;
        actionCode = ActionCode.ClientRoom_Leavel;
        base.OnInit();
    }

    public override void OnResponse(string data)
    {
        ReturnCode returnCode = (ReturnCode)int.Parse(data);
        if (returnCode == ReturnCode.Success)
        {
            var roomPanel = GameFacade.Instance.UIManager.GetPanel<RoomPanel>(UINames.roomPanel);
            roomPanel.SetUserDataInfo(null, null);
            GameFacade.Instance.UIManager.ShowMessage("离开房间！");
        }
    }
}
