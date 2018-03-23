using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LoginPanel : BasePanel
{
    public override void OnInit()
    {
        base.OnInit();
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 2f);
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.4f);
    }
}
