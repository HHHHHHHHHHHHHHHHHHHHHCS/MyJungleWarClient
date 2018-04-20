using Common.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitRoomRequest : BaseRequest
{
    public override void OnInit()
    {
        requestCode = RequestCode.ClientRoom;
        actionCode = ActionCode.ClientRoom_Quit;
        base.OnInit();
    }

    public override void OnResponse(string data)
    {
        ReturnCode returnCode = (ReturnCode)int.Parse(data);
        if (returnCode == ReturnCode.Success)
        {
            var roomPanel = GameFacade.Instance.UIManager.GetPanel<RoomPanel>(UINames.roomPanel);
            roomPanel.SetAwayInfo(null);
            GameFacade.Instance.UIManager.ShowMessage("玩家离开！");
        }
    }
}
