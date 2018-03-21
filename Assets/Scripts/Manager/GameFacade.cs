using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFacade : MonoBehaviour
{
    public static GameFacade Instance { get; private set; }

    public PlayerManager PlayerManager { get; private set; }
    public CameraManager CameraManager { get; private set; }
    public UIManager UIManager { get; private set; }
    public AudioManager AudioManager { get; private set; }
    public RequestManager RequestManager { get; private set; }
    public ClientManager ClientManager { get; private set; }

    private void InitManager()
    {
        PlayerManager = new PlayerManager(this).OnInit();
        CameraManager = new CameraManager(this).OnInit();
        UIManager = new UIManager(this).OnInit();
        AudioManager = new AudioManager(this).OnInit();
        RequestManager = new RequestManager(this).OnInit();
        ClientManager = new ClientManager(this).OnInit();
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        DestoryManager();
    }

    private void DestoryManager()
    {
        PlayerManager.OnDesotry();
        CameraManager.OnDesotry();
        UIManager.OnDesotry();
        AudioManager.OnDesotry();
        RequestManager.OnDesotry();
        ClientManager.OnDesotry();
    }
}
