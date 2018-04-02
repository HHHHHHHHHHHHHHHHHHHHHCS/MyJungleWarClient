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
    }

    /// <summary>
    /// 界面被显示出来
    /// </summary>
    public virtual void OnEnter()
    {
        gameObject.SetActive(true);
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
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 界面不显示,退出这个界面，界面被关闭
    /// </summary>
    public virtual void OnExit()
    {
        gameObject.SetActive(false);
    }

    protected virtual void PlayClickSound()
    {
        GameFacade.Instance.PlayNormalAudio(AudioNames.buttonClick);
    }
}
