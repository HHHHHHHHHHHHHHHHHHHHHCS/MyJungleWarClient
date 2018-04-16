using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    private Text usernameText,totalCountText,winCountText;

    private void Awake()
    {
        Transform root = transform;
        usernameText = root.Find(UINames.roomItem_UsernamePath).GetComponent<Text>();
        totalCountText = root.Find(UINames.roomItem_UsernamePath).GetComponent<Text>();
        winCountText = root.Find(UINames.roomItem_UsernamePath).GetComponent<Text>();

        root.Find(UINames.roomItem_JoinButtonPath).GetComponent<Button>()
            .onClick.AddListener(OnClickJoinButton);
    }


    public void UpdateRoomItemInfo(string _usernameText,string _totalCountText,string _winCountText)
    {
        usernameText.text = _usernameText;
        totalCountText.text = _totalCountText;
        winCountText.text = _winCountText;
    }

    private void OnClickJoinButton()
    {
        Debug.Log("老子点击了");
    }
}
