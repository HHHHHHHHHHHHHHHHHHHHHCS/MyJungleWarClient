using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFacade : MonoBehaviour
{
    public PlayerManager PlayerManager { get; private set; }
    public CameraManager CameraManager { get; private set; }
    public UIManager UIManager { get; private set; }
    public AudioManager AudioManager { get; private set; }
    public RequestManager RequestManager { get; private set; }
    public ClientManager ClientManager { get; private set; }

    private void InitManager()
    {
        PlayerManager = new PlayerManager().OnInit();
        CameraManager = new CameraManager().OnInit();
        UIManager = new UIManager().OnInit();
        AudioManager = new AudioManager().OnInit();
        RequestManager = new RequestManager().OnInit();
        ClientManager = new ClientManager().OnInit();
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
