using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Code;
using Common.Model;

public class RoomRequest : BaseRequest
{
    public override void OnInit()
    {
        base.OnInit();
        requestCode = RequestCode.ClientRoom;
        requestActionSet.Add(CreateBase(ActionCode.ClientRoom_Show, OnResponse_ShowRoomList));
        requestActionSet.Add(CreateBase(ActionCode.ClientRoom_Create, OnResponse_CreateRoom));
        requestActionSet.Add(CreateBase(ActionCode.ClientRoom_Join, OnResponse_Join));
        requestActionSet.Add(CreateBase(ActionCode.ClientRoom_Come, OnResponse_Come));
        requestActionSet.Add(CreateBase(ActionCode.ClientRoom_Ready, OnResponse_Ready));
        //requestActionSet.Add(CreateBase(ActionCode.ClientRoom_AllReady, Register_OnResponse));
        //requestActionSet.Add(CreateBase(ActionCode.ClientRoom_CancelReady, Login_OnResponse));
        //requestActionSet.Add(CreateBase(ActionCode.ClientRoom_StartGame, Register_OnResponse));
        requestActionSet.Add(CreateBase(ActionCode.ClientRoom_Leavel, OnResponse_Leave));
        requestActionSet.Add(CreateBase(ActionCode.ClientRoom_Close, OnResponse_Close));
        requestActionSet.Add(CreateBase(ActionCode.ClientRoom_Quit, OnResponse_Quit));
    }


    public void OnResponse_ShowRoomList(string data)
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

    public void OnResponse_CreateRoom(string data)
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

    public void OnResponse_Join(string data)
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
            GameFacade.Instance.SendRequest(ActionCode.ClientRoom_Show);
            GameFacade.Instance.UIManager.ShowMessage("加入失败！");
        }
    }


    public void OnResponse_Come(string data)
    {
        string[] result = data.Split(',');
        UserData awayUserData = new UserData(result[0], int.Parse(result[1]), int.Parse(result[2]));
        GameFacade.Instance.UIManager.GetPanel<RoomPanel>(UINames.roomPanel)
            .SetAwayInfo(awayUserData);
    }
    public void OnResponse_Ready(string data)
    {
        string[] readyUsernameArray = data.Split(',');
        var roomPanel = GameFacade.Instance.UIManager.GetPanel<RoomPanel>(UINames.roomPanel);
        roomPanel.UserReady(readyUsernameArray);
    }


    public void OnResponse_Leave(string data)
    {
        ReturnCode returnCode = (ReturnCode)int.Parse(data);
        if (returnCode == ReturnCode.Success)
        {
            var roomPanel = GameFacade.Instance.UIManager.GetPanel<RoomPanel>(UINames.roomPanel);
            roomPanel.SetUserDataInfo(null, null);
            GameFacade.Instance.UIManager.ShowMessage("离开房间！");
        }
    }

    public void OnResponse_Close(string data)
    {
        ReturnCode returnCode = (ReturnCode)int.Parse(data);
        if (returnCode == ReturnCode.Success)
        {
            var roomPanel = GameFacade.Instance.UIManager.GetPanel<RoomPanel>(UINames.roomPanel);
            roomPanel.SetUserDataInfo(null, null);
            GameFacade.Instance.UIManager.ShowMessage("房间关闭！");
        }
    }

    public void OnResponse_Quit(string data)
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
