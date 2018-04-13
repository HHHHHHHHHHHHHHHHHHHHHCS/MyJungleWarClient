using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFacade : MonoBehaviour
{
    public static GameFacade Instance { get; private set; }

    public RequestManager RequestManager { get; private set; }
    public PlayerManager PlayerManager { get; private set; }
    public CameraManager CameraManager { get; private set; }
    public UIManager UIManager { get; private set; }
    public AudioManager AudioManager { get; private set; }
    public ClientManager ClientManager { get; private set; }

    public void OnIntGameFacade()
    {
        RequestManager = new RequestManager();
        RequestManager.OnInit();//RequestManager 不能跟上面合并 不然会出现空指针
        PlayerManager = new PlayerManager().OnInit();
        CameraManager = new CameraManager().OnInit();
        UIManager = new UIManager();
        UIManager.OnInit();//UIManager 不能跟上面合并 不然会出现空指针
        AudioManager = new AudioManager().OnInit();
        ClientManager = new ClientManager().OnInit();
    }

    private void Awake()
    {
        if(Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            OnIntGameFacade();
        }
    }

    private void Update()
    {
        RequestManager.OnUpdate();
        UIManager.OnUpdate();
    }

    private void OnDestroy()
    {
        DestoryManager();
    }

    private void DestoryManager()
    {
        RequestManager.OnDesotry();
        PlayerManager.OnDesotry();
        CameraManager.OnDesotry();
        UIManager.OnDesotry();
        AudioManager.OnDesotry();
        ClientManager.OnDesotry();
    }
}
