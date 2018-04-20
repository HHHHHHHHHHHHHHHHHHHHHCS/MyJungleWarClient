using Common.Code;
using Common.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    private Text usernameText, totalCountText, winCountText;
    private Button joinButton;
    private string homeRoomUsername;

    private void Awake()
    {
        Transform root = transform;
        usernameText = root.Find(UINames.roomItem_UsernameTextPath).GetComponent<Text>();
        totalCountText = root.Find(UINames.roomItem_TotalCountTextPath).GetComponent<Text>();
        winCountText = root.Find(UINames.roomItem_WinCountTextPath).GetComponent<Text>();

        joinButton = root.Find(UINames.roomItem_JoinButtonPath).GetComponent<Button>();
        joinButton.onClick.AddListener(OnClickJoinButton);
    }


    public void UpdateRoomItemInfo(UserData userData)
    {
        homeRoomUsername = userData.Username;
        usernameText.text = userData.Username;
        totalCountText.text = userData.TotalCount.ToString();
        winCountText.text = userData.WinCount.ToString();
        joinButton.interactable = true;
        gameObject.SetActive(true);
    }

    private void OnClickJoinButton()
    {
        joinButton.interactable = false;
        GameFacade.Instance.RequestManager.GetRequest<JoinRoomRequest>(ActionCode.ClientRoom_Join)
            .SendRequest(homeRoomUsername);
    }
}
