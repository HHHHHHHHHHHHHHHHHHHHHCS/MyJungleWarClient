using Common.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRoomRequest : BaseRequest
{
    public override void OnInit()
    {
        requestCode = RequestCode.ClientRoom;
        actionCode = ActionCode.ClientRoom_Close;
        base.OnInit();
    }

    public override void OnResponse(string data)
    {
        ReturnCode returnCode = (ReturnCode)int.Parse(data);
        if (returnCode == ReturnCode.Success)
        {
            var roomPanel = GameFacade.Instance.UIManager.GetPanel<RoomPanel>(UINames.roomPanel);
            roomPanel.SetUserDataInfo(null,null);
            GameFacade.Instance.UIManager.ShowMessage("房间关闭！");
        }
    }
}
