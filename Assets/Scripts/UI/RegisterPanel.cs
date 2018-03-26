using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : BasePanel
{
    public override void OnInit()
    {
        base.OnInit();
        transform.Find(UINames.closeButtonPath).GetComponent<Button>()
            .onClick.AddListener(OnCloseClick);
    }

    private void OnCloseClick()
    {

    }
}
