using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{

    public override void OnInit()
    {
        base.OnInit();
        transform.Find(UINames.closeButtonPath).GetComponent<Button>()
            .onClick.AddListener(OnCloseClick);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.6f);
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.4f);
    }

    private void OnCloseClick()
    {
        transform.DOScale(0, 0.4f);
        transform.DOLocalMove(new Vector3(1000, 0, 0), 0.4f)
            .OnComplete(GameFacade.Instance.UIManager.BackLastPanel);
    }
}
