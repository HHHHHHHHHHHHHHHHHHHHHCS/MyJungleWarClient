using Common.Code;
using Common.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRoomListRequest : BaseRequest
{
    public override void OnInit()
    {
        requestCode = RequestCode.ClientRoom;
        actionCode = ActionCode.ClientRoom_Show;
        base.OnInit();
    }

    public override void OnResponse(string data)
    {
        UserData[] roomItemArray = null;
        if (data.Length > 0)
        {
            string[] roomArray = data.Split('|');
            string[] userDataStr;
            roomItemArray = new UserData[roomArray.Length];
            
            for (int i = 0; i < roomArray.Length; i++)
            {
                userDataStr = roomArray[i].Split(',');
                roomItemArray[i] = new UserData(userDataStr[0], int.Parse(userDataStr[1]), int.Parse(userDataStr[2])); ;
            }
        }
        GameFacade.Instance.UIManager.GetPanel<RoomListPanel>(UINames.roomListPanel)
            .UpdateRoomItemList(roomItemArray);
    }
}
