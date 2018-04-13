using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListPanel : BasePanel
{
    private RectTransform battleInfo, roomList;
    private Button closeButton;
    private Text usernameText, totalCountText, winCountText;


    public override void OnInit()
    {
        base.OnInit();
        Transform root = transform;
        battleInfo = root.Find(UINames.roomList_BattleInfoPath).GetComponent<RectTransform>();
        roomList = root.Find(UINames.roomList_RoomListPath).GetComponent<RectTransform>();
        usernameText= battleInfo.Find(UINames.roomList_UsernameTextPath).GetComponent<Text>();
        totalCountText = battleInfo.Find(UINames.roomList_TotalCountTextPath).GetComponent<Text>();
        winCountText = battleInfo.Find(UINames.roomList_WinCountTextPath).GetComponent<Text>();

        closeButton = roomList.Find(UINames.closeButtonPath).GetComponent<Button>();
        closeButton.onClick.AddListener(OnClickClose);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnEnter()
    {
        closeButton.interactable = true;
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
            .OnComplete(() =>
            {
                GameFacade.Instance.UIManager.BackLastPanel();
            });
    }

    private void OnClickClose()
    {
        closeButton.interactable = false;
        PlayClickSound();
        OnExitAnim();
    }

    public void UpdateBattleInfo(string _username,string _totalCount,string _winCount)
    {
        usernameText.text = _username;
        totalCountText.text = _totalCount;
        winCountText.text = _winCount;
    }
}
