using Common.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFacade : MonoBehaviour
{
    public static GameFacade Instance { get; private set; }

    public event Action OnUpdateEvent;

    public RequestManager RequestManager { get; private set; }
    public PlayerManager PlayerManager { get; private set; }
    public CameraManager CameraManager { get; private set; }
    public UIManager UIManager { get; private set; }
    public AudioManager AudioManager { get; private set; }
    public ClientManager ClientManager { get; private set; }

    private void OnIntGameFacade(NowScenes nowScene)
    {
        if (ClientManager == null)
        {
            ClientManager = new ClientManager().OnInit();
        }
        switch (nowScene)
        {
            case NowScenes.LoadScene:
                break;
            case NowScenes.LoginScene:
                OnInitLoginSceneGameFacade();
                break;
            case NowScenes.GameScene:
                OnInitGameSceneGameFacade();
                break;
            default:
                break;
        }
    }

    private void OnInitLoginSceneGameFacade()
    {
        RequestManager = new Login_RequestManager();
        RequestManager.OnInit();//RequestManager 不能跟上面合并 不然会出现空指针
        UIManager = new Login_UIManager();
        UIManager.OnInit();//UIManager 不能跟上面合并 不然会出现空指针
        AudioManager = new Login_AudioManager().OnInit();
    }

    private void OnInitGameSceneGameFacade()
    {
        RequestManager = new Game_RequestManager();
        RequestManager.OnInit();//RequestManager 不能跟上面合并 不然会出现空指针
        PlayerManager = new Game_PlayerManager();
        CameraManager = new Game_CameraManager();
        UIManager = new Game_UIManager();
        UIManager.OnInit();//UIManager 不能跟上面合并 不然会出现空指针
        AudioManager = new Game_AudioManager().OnInit();
    }

    private void Awake()
    {
        Instance = this;
        SceneChanger.SetNowScene();
        OnIntGameFacade(SceneChanger.NowScene);
        InvokeRepeating("OnUpdate", 0, 0.001f);
    }

    private void OnUpdate()
    {
        if (OnUpdateEvent != null)
        {
            OnUpdateEvent();
        }
    }

    private void OnDestroy()
    {
        DestoryManager();
    }

    private void DestoryManager()
    {
        if (RequestManager != null)
        {
            RequestManager.OnDesotry();
            RequestManager = null;
        }

        if (PlayerManager != null)
        {
            PlayerManager.OnDesotry();
            PlayerManager = null;
        }

        if (CameraManager != null)
        {
            CameraManager.OnDesotry();
            CameraManager = null;
        }

        if (UIManager != null)
        {
            UIManager.OnDesotry();
            UIManager = null;
        }

        if (AudioManager != null)
        {
            AudioManager.OnDesotry();
            AudioManager = null;
        }

        //if (ClientManager != null)
        //{
        //    ClientManager.OnDesotry();
        //    ClientManager = null;
        //}

        OnUpdateEvent = null;
    }
}
