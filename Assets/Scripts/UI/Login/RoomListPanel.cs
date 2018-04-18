using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Common.Code;

public class RoomListPanel : BasePanel
{
    [SerializeField]
    private GameObject roomItemPrefab;

    private CreateRoomRequest createRoomRequest;
    private RectTransform roomListContent;
    private Button closeButton, createRoomButton, refreshRoomList;
    private Text usernameText, totalCountText, winCountText;

    public override void OnInit()
    {
        base.OnInit();
        createRoomRequest = GameFacade.Instance.GetRequest<CreateRoomRequest>(ActionCode.ClientRoom_Create);
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
        //CreateRoomItems();
        ResumeDefaultState();
        var pos = roomListContent.localPosition;
        pos.y = 0;
        roomListContent.localPosition = pos;
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



    public void CreateRoomItems()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject go = Instantiate(roomItemPrefab, roomListContent);
        }
    }

    public void EnterRoomPanel()
    {
        GameFacade.Instance.UIManager.ShowPanel(UINames.roomPanel);
    }
}
