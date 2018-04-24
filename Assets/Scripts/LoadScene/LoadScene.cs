using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    private void Awake()
    {
        SceneChanger.ChangeScene(NowScenes.LoginScene);
    }
}
