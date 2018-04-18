using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : BasePanel
{
    private const float delayHideTime = 2f;
    private const float hideTime = 2.5f;

    private Text text;
    private Image image;

    private CanvasGroup canvasGroup;
    TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> tweener;
    private WaitForSeconds waitTime = new WaitForSeconds(delayHideTime);
    private Coroutine coroutine;

    public override void OnInit()
    {
        base.OnInit();
        image = GetComponent<Image>();
        text = GetComponentInChildren<Text>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ShowMessage(string msg)
    {
        gameObject.SetActive(true);
        if (tweener != null)
        {
            tweener.Kill();
            tweener = null;
        }
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        canvasGroup.alpha = 1;
        text.text = msg;
        OnEnter();
        coroutine = StartCoroutine(WaitHide());
    }

    private IEnumerator WaitHide()
    {
        yield return waitTime;
        tweener = DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 0, hideTime);
        tweener.OnComplete(() => { gameObject.SetActive(false); });
    }
}
