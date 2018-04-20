using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Common.Code;
using Common.Model;

public class RoomListPanel : BasePanel
{
    [SerializeField]
    private RoomItem roomItemPrefab;

    private CreateRoomRequest createRoomRequest;
    private ShowRoomListRequest showRoomListRequest;
    private RectTransform roomListContent;
    private Button closeButton, createRoomButton, refreshRoomList;
    private Text usernameText, totalCountText, winCountText;

    private List<RoomItem> roomItemList;

    public override void OnInit()
    {
        base.OnInit();
        createRoomRequest = GameFacade.Instance.GetRequest<CreateRoomRequest>(ActionCode.ClientRoom_Create);
        showRoomListRequest = GameFacade.Instance.GetRequest<ShowRoomListRequest>(ActionCode.ClientRoom_Show);
        roomItemList = new List<RoomItem>();
        Transform root = transform;
        RectTransform battleInfo = root.Find(UINames.roomList_BattleInfoPath).transform as RectTransform;
        RectTransform roomList = root.Find(UINames.roomList_RoomListPath).transform as RectTransform;
        usernameText = battleInfo.Find(UINames.roomList_UsernameTextPath).GetComponent<Text>();
        totalCountText = battleInfo.Find(UINames.roomList_TotalCountTextPath).GetComponent<Text>();
        winCountText = battleInfo.Find(UINames.roomList_WinCountTextPath).GetComponent<Text>();
        roomListContent = roomList.Find(UINames.roomList_RooomContentPath).transform as RectTransform;

        closeButton = roomList.Find(UINames.closeButtonPath).GetComponent<Button>();
        closeButton.onClick.AddListener(OnClickClose);

        createRoomButton = roomList.Find(UINames.roomList_CreateRoomButtonPath).GetComponent<Button>();
        createRoomButton.onClick.AddListener(OnClickCreateRoom);
        refreshRoomList = roomList.Find(UINames.roomList_RefreshRoomListPath).GetComponent<Button>();
        refreshRoomList.onClick.AddListener(OnClickUpdateRoomList);
    }



    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnEnter()
    {
        ResumeDefaultState();
        var pos = roomListContent.localPosition;
        pos.y = 0;
        roomListContent.localPosition = pos;
        OnClickUpdateRoomList();
        base.OnEnter();
    }

    public override void OnEnterAnim()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(nowScale, 0.6f);
    }

    public override void OnExitAnim()
    {
        transform.DOScale(0, 0.4f)
            .OnComplete(GameFacade.Instance.UIManager.BackLastPanel);
    }

    public override void OnPause()
    {
        transform.DOScale(0, 0.25f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public override void OnResume()
    {
        ResumeDefaultState();
        OnClickUpdateRoomList();
        gameObject.SetActive(true);
        transform.DOScale(nowScale, 0.25f);
    }

    public override void ResumeDefaultState()
    {
        closeButton.interactable = true;
        createRoomButton.interactable = true;
        refreshRoomList.interactable = true;
    }

    private void OnClickClose()
    {
        closeButton.interactable = false;
        PlayClickSound();
        OnExitAnim();
    }

    private void OnClickUpdateRoomList()
    {
        refreshRoomList.interactable = false;
        showRoomListRequest.SendRequest();
    }

    private void OnClickCreateRoom()
    {
        closeButton.interactable = false;
        createRoomButton.interactable = false;
        refreshRoomList.interactable = false;
        createRoomRequest.SendRequest();
    }

    public void UpdateBattleInfo(string _username, string _totalCount, string _winCount)
    {
        usernameText.text = _username;
        totalCountText.text = _totalCount;
        winCountText.text = _winCount;
    }

    public void UpdateRoomItemList(UserData[] roomItemArray)
    {
        refreshRoomList.interactable = true;
        GameFacade.Instance.UIManager.ShowMessage("刷新成功！");
        if (roomItemArray != null)
        {
            for (int i = 0; i < roomItemArray.Length; i++)
            {
                if (i < roomItemList.Count)
                {
                    roomItemList[i].UpdateRoomItemInfo(roomItemArray[i]);
                }
                else
                {
                    RoomItem newItem = Instantiate(roomItemPrefab, roomListContent);
                    newItem.UpdateRoomItemInfo(roomItemArray[i]);
                    roomItemList.Add(newItem);
                }
            }
        }

        for (int i = roomItemArray == null ? 0 : roomItemArray.Length; i < roomItemList.Count; i++)
        {
            if (roomItemList.Count <= 20)
            {
                roomItemList[i].gameObject.SetActive(false);
            }
            else
            {
                Destroy(roomItemList[i].gameObject);
            }
        }
    }

    public void EnterRoomPanel(UserData homeUserData, UserData awayUserData)
    {
        var UIManager = GameFacade.Instance.UIManager;
        var roomPanel = UIManager.GetPanel<RoomPanel>(UINames.roomPanel);
        roomPanel.SetUserDataInfo(homeUserData, awayUserData);
        UIManager.ShowPanel(roomPanel);
    }
}
