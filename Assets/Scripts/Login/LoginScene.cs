﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginScene : MonoBehaviour
{
	private void Awake ()
    {
        GameFacade.Instance.UIManager.ShowPanel(UINames.startPanel);
	}
}
