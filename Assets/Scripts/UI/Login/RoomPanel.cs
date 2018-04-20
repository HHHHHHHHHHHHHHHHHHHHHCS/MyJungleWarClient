using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Common.Model;
using Common.Code;
using System;

public class RoomPanel : BasePanel
{
    [SerializeField]
    private Color unReadyColor;

    private LeaveRoomRequest leaveRoomRequest;
    private ReadyBattleRequest readyBattleRequest;
    private Text home_UsernameText, home_TotalCountText, home_WinCountText
        , away_UsernameText, away_TotalCountText, away_WinCountText;
    private Transform playerHomeBgTs, playerAwayBgTs, playerAwayNoOneBgTs;
    private GameObject home_ReadyImage, away_ReadyImage;
    private Button readyButton, exitButton;
    private Image readyButtonImage;
    private Color readyColor;
    private float homeBgPos, awayNoOneBgPos, awayBgPos, readyButtonPos, exitButtonPos;

    private bool isReady;
    private UserData homeUserData, awayUserData;

    public override void OnInit()
    {
        leaveRoomRequest = GameFacade.Instance.RequestManager.GetRequest<LeaveRoomRequest>(ActionCode.ClientRoom_Leavel);
        readyBattleRequest = GameFacade.Instance.RequestManager.GetRequest<ReadyBattleRequest>(ActionCode.ClientRoom_Ready);
        Transform root = transform;
        playerHomeBgTs = root.Find(UINames.roomPanel_PlayerHomeBgPath);
        playerAwayBgTs = root.Find(UINames.roomPanel_PlayerAwayBgBgPath);
        playerAwayNoOneBgTs = root.Find(UINames.roomPanel_PlayerNoOneBgPath);

        home_UsernameText = playerHomeBgTs.Find(UINames.roomPanel_UsernameTextPath).GetComponent<Text>();
        home_TotalCountText = playerHomeBgTs.Find(UINames.roomPanel_TotalCountTextPath).GetComponent<Text>();
        home_WinCountText = playerHomeBgTs.Find(UINames.roomPanel_WinCountTextPath).GetComponent<Text>();
        home_ReadyImage = playerHomeBgTs.Find(UINames.roomPanel_ReadyImagePath).gameObject;

        away_UsernameText = playerAwayBgTs.Find(UINames.roomPanel_UsernameTextPath).GetComponent<Text>();
        away_TotalCountText = playerAwayBgTs.Find(UINames.roomPanel_TotalCountTextPath).GetComponent<Text>();
        away_WinCountText = playerAwayBgTs.Find(UINames.roomPanel_WinCountTextPath).GetComponent<Text>();
        away_ReadyImage = playerAwayBgTs.Find(UINames.roomPanel_ReadyImagePath).gameObject;


        readyButton = root.Find(UINames.roomPanel_ReadyButtonPath).GetComponent<Button>();
        readyButtonImage = readyButton.GetComponent<Image>();
        readyColor = readyButtonImage.color;
        readyButton.onClick.AddListener(OnClickReadyButton);

        exitButton = root.Find(UINames.roomPanel_ExitButtonPath).GetComponent<Button>();
        exitButton.onClick.AddListener(OnClickExitButton);

        homeBgPos = playerHomeBgTs.localPosition.x;
        awayBgPos = playerAwayBgTs.localPosition.x;
        awayNoOneBgPos = playerAwayNoOneBgTs.localPosition.x;
        readyButtonPos = readyButton.transform.localPosition.y;
        exitButtonPos = exitButton.transform.localPosition.y;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        readyButton.interactable = true;
        exitButton.interactable = true;
        isReady = false;
        home_ReadyImage.SetActive(false);
        away_ReadyImage.SetActive(false);
    }

    private void OnClickReadyButton()
    {
        PlayClickSound();
        isReady = !isReady;
        readyButton.GetComponent<Image>().color = isReady ? unReadyColor : readyColor;
        readyBattleRequest.SendRequest(isReady);
    }

    private void OnClickExitButton()
    {
        readyButton.interactable = false;
        exitButton.interactable = false;
        PlayClickSound();
        leaveRoomRequest.SendRequest();
    }

    public override void OnEnterAnim()
    {
        DoEnterAnim(playerHomeBgTs, -1000, homeBgPos, true);
        DoEnterAnim(playerAwayBgTs, 1000, awayBgPos, true);
        DoEnterAnim(playerAwayNoOneBgTs, 1000, awayNoOneBgPos, true);
        DoEnterAnim(readyButton.transform, -1000, readyButtonPos, false);
        DoEnterAnim(exitButton.transform, -1000, exitButtonPos, false);
    }

    public override void OnExitAnim()
    {
        readyButton.interactable = false;
        exitButton.interactable = false;
        DoExitAnim(playerHomeBgTs, -1000, true);
        DoExitAnim(playerAwayBgTs, 1000, true);
        DoExitAnim(playerAwayNoOneBgTs, 1000, true);
        DoExitAnim(readyButton.transform, -1000, false);
        DoExitAnim(exitButton.transform, -1000, false)
            .OnComplete(GameFacade.Instance.UIManager.BackLastPanel);
    }

    private Tweener DoEnterAnim(Transform mainBody, float startPos, float endPos, bool isX)
    {
        var vec3 = mainBody.localPosition;
        if (isX)
        {
            vec3.x = startPos;
        }
        else
        {
            vec3.y = startPos;
        }
        mainBody.localPosition = vec3;
        if (isX)
        {
            return mainBody.DOLocalMoveX(endPos, 0.4f);
        }
        else
        {
            return mainBody.DOLocalMoveY(endPos, 0.4f);
        }
    }

    private Tweener DoExitAnim(Transform mainBody, float endPos, bool isX)
    {
        var vec3 = mainBody.localPosition;
        mainBody.localPosition = vec3;
        if (isX)
        {
            return mainBody.DOLocalMoveX(endPos, 0.25f);
        }
        else
        {
            return mainBody.DOLocalMoveY(endPos, 0.25f);
        }
    }

    public void SetUserDataInfo(UserData homeUserData, UserData awayUserData)
    {
        SetHomeInfo(homeUserData);
        SetAwayInfo(awayUserData);
        if (homeUserData == null && awayUserData == null)
        {
            LeaveRoom();
        }
    }

    public void SetHomeInfo(UserData userData)
    {
        homeUserData = userData;
        home_ReadyImage.SetActive(false);
        if (userData != null)
        {
            home_UsernameText.text = userData.Username;
            home_TotalCountText.text = userData.TotalCount.ToString();
            home_WinCountText.text = userData.WinCount.ToString();
        }
    }

    public void SetAwayInfo(UserData userData)
    {
        awayUserData = userData;
        away_ReadyImage.SetActive(false);
        if (userData != null)
        {
            playerAwayNoOneBgTs.gameObject.SetActive(false);
            playerAwayBgTs.gameObject.SetActive(true);
            away_UsernameText.text = userData.Username;
            away_TotalCountText.text = userData.TotalCount.ToString();
            away_WinCountText.text = userData.WinCount.ToString();
        }
        else
        {
            playerAwayNoOneBgTs.gameObject.SetActive(true);
            playerAwayBgTs.gameObject.SetActive(false);
        }
    }

    public void LeaveRoom()
    {
        OnExitAnim();
    }


    public void UserReady(string[] readyUsernameArray)
    {
        //先全部隐藏最后显示
        home_ReadyImage.SetActive(false);
        away_ReadyImage.SetActive(false);

        foreach (var item in readyUsernameArray)
        {
            if (homeUserData != null && homeUserData.Username == item)
            {
                home_ReadyImage.SetActive(true);
            }
            else if (awayUserData != null && awayUserData.Username == item)
            {
                away_ReadyImage.SetActive(true);
            }
        }
    }

}
