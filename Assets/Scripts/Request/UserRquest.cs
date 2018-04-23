using Common.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserRquest : BaseRequest
{
    public override void OnInit()
    {
        base.OnInit();
        requestCode = RequestCode.User;
        requestActionSet.Add(CreateBase(ActionCode.Login, OnResponse_Login));
        requestActionSet.Add(CreateBase(ActionCode.Register, OnResponse_Register));
    }

    public void OnResponse_Login(string data)
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


    public void OnResponse_Register(string data)
    {
        string[] result = data.Split(',');
        ReturnCode returnCode = (ReturnCode)int.Parse(result[0]);
        GameFacade.Instance.UIManager.GetPanel<RegisterPanel>(UINames.registerPanel)
            .OnRegisterRespone(returnCode);
    }
}
