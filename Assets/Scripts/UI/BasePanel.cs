using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    protected Vector3 nowPos;
    protected Vector3 nowScale;

    public virtual void OnInit()
    {
        nowPos = transform.localPosition;
        nowScale = transform.localScale;
        GameFacade.Instance.UIManager.UpdateEvent += OnUpdate;
    }

    public virtual void OnUpdate()
    {
    }


    /// <summary>
    /// 界面被显示出来
    /// </summary>
    public virtual void OnEnter()
    {
        ResumeDefaultState();
        gameObject.SetActive(true);
        OnEnterAnim();
    }

    /// <summary>
    /// 界面暂停
    /// </summary>
    public virtual void OnPause()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 界面继续
    /// </summary>
    public virtual void OnResume()
    {
        ResumeDefaultState();
        gameObject.SetActive(true);
    }


    /// <summary>
    /// 界面不显示,退出这个界面，界面被关闭
    /// </summary>
    public virtual void OnExit()
    {
        gameObject.SetActive(false);
    }


    public virtual void OnEnterAnim()
    {

    }

    public virtual void OnExitAnim()
    {

    }

    public virtual void ResumeDefaultState()
    {

    }


    protected virtual void PlayClickSound()
    {
        GameFacade.Instance.PlayNormalAudio(AudioNames.buttonClick);
    }

}
