using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum NowScenes
{
    LoadScene,
    LoginScene,
    GameScene,
}


public class SceneChanger
{
    public static NowScenes NowScene { get; private set; }

    public static void ChangeScene(NowScenes _scene)
    {
        SceneManager.LoadSceneAsync(_scene.ToString());
    }

    public static void SetNowScene()
    {
        GC.Collect();
        string sceneName = SceneManager.GetActiveScene().name;
        NowScene = (NowScenes)Enum.Parse(typeof(NowScenes), sceneName);
    }

}
