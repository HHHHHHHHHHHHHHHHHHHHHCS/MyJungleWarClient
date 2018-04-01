﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : BaseManager<AudioManager>
{
    private AudioSource audioSource_bg;
    private AudioSource audioSource_normal;

    public override AudioManager OnInit()
    {
        audioSource_bg = GameObject.Find(ObjectNames.audioSource_Bg).GetComponent<AudioSource>();
        audioSource_normal = GameObject.Find(ObjectNames.audioSource_Normal).GetComponent<AudioSource>();
        PlaySound(audioSource_bg, AudioNames.bg_moderate,-1, true, true);
        return base.OnInit();
    }

    public void PlaySound(AudioSource source, string _name
        ,float volume = -1, bool needSetClip = false, bool isLoop = false)
    {
        var clip = LoadSound(_name);
        source.loop = isLoop;
        if(volume>=0)
        {
            source.volume = volume;
        }
        if (needSetClip)
        {
            source.clip = clip;
            source.Play();
        }
        else
        {
            source.PlayOneShot(clip);
        }
    }

    private AudioClip LoadSound(string _name)
    {
        return Resources.Load<AudioClip>(AudioNames.audioDir + _name);
    }
}
