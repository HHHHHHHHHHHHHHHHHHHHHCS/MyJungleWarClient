using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : BasePanel
{
    private float delayHideTime = 2f;
    private float hideTime = 2.5f;



    private Text text;
    private Image image;
    private CanvasRenderer textCR;
    private CanvasRenderer imageCR;

    public override void OnInit()
    {
        image = GetComponent<Image>();
        text = GetComponentInChildren<Text>(true);
        textCR = text.GetComponent<CanvasRenderer>();
        imageCR = image.GetComponent<CanvasRenderer>();
        text.gameObject.SetActive(false);
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }


    public void ShowMessage(string msg)
    {
        textCR.SetAlpha(1);
        imageCR.SetAlpha(1);

        text.gameObject.SetActive(true);
        text.text = msg;
        Invoke("Hide", delayHideTime);
        OnEnter();
    }

    private void Hide()
    {
        text.CrossFadeAlpha(0f, hideTime, false);
        image.CrossFadeAlpha(0f, hideTime, false);
    }
}
