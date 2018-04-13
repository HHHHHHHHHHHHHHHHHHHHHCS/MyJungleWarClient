using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    protected enum PanelState
    {
        None,
        Enter,
        Exit,
        Pause,
        Resume
    }

    protected Vector3 nowPos;
    protected Vector3 nowScale;
    protected PanelState wantPanelState = PanelState.None;
    protected PanelState nowPanelState = PanelState.None;

    public virtual void OnInit()
    {
        nowPos = transform.localPosition;
        nowScale = transform.localScale;
        GameFacade.Instance.UIManager.UpdateEvent += OnUpdate;
    }

    public virtual void OnUpdate()
    {
        if (wantPanelState != PanelState.None
            && wantPanelState != nowPanelState)
        {
            switch (wantPanelState)
            {
                case PanelState.Enter:
                    OnEnter();
                    break;
                case PanelState.Exit:
                    OnExit();
                    break;
                case PanelState.Pause:
                    OnPause();
                    break;
                case PanelState.Resume:
                    OnResume();
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 界面将要被显示出来
    /// </summary>
    public virtual void OnAsyncEnter()
    {
        wantPanelState = PanelState.Enter;
    }

    /// <summary>
    /// 界面被显示出来
    /// </summary>
    public virtual void OnEnter()
    {
        nowPanelState = PanelState.Enter;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 界面将要被暂停
    /// </summary>
    public virtual void OnAsyncPause()
    {
        wantPanelState = PanelState.Pause;
    }

    /// <summary>
    /// 界面暂停
    /// </summary>
    public virtual void OnPause()
    {
        nowPanelState = PanelState.Pause;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 界面将要被继续
    /// </summary>
    public virtual void OnAsyncResume()
    {
        wantPanelState = PanelState.Resume;
    }

    /// <summary>
    /// 界面继续
    /// </summary>
    public virtual void OnResume()
    {
        nowPanelState = PanelState.Resume;
        gameObject.SetActive(true);
    }


    /// <summary>
    /// 界面将要被退出
    /// </summary>
    public virtual void OnAsyncExit()
    {
        wantPanelState = PanelState.Exit;
    }

    /// <summary>
    /// 界面不显示,退出这个界面，界面被关闭
    /// </summary>
    public virtual void OnExit()
    {
        nowPanelState = PanelState.Exit;
        gameObject.SetActive(false);
    }

    protected virtual void PlayClickSound()
    {
        GameFacade.Instance.PlayNormalAudio(AudioNames.buttonClick);
    }

}
