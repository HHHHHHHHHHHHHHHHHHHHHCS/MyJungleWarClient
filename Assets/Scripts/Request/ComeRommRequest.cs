using Common.Code;
using Common.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComeRommRequest : BaseRequest
{
    public override void OnInit()
    {
        requestCode = RequestCode.ClientRoom;
        actionCode = ActionCode.ClientRoom_Come;
        base.OnInit();
    }

    public override void OnResponse(string data)
    {
        Debug.Log(data);
        string[] result = data.Split(',');
        UserData awayUserData = new UserData(result[0], int.Parse(result[1]), int.Parse(result[2]));
        GameFacade.Instance.UIManager.GetPanel<RoomPanel>(UINames.roomPanel)
            .SetAwayInfo(awayUserData);
    }
}
