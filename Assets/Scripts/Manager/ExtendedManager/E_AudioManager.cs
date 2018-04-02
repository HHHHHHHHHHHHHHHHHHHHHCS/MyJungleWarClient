using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class E_AudioManager
{
    public static void PlayBgAudio(this GameFacade facade, string audioName)
    {
        facade.AudioManager.PlayBgAudio(audioName);
    }

    public static void PlayNormalAudio(this GameFacade facade, string audioName)
    {
        facade.AudioManager.PlayNormalAudio(audioName);
    }
}
