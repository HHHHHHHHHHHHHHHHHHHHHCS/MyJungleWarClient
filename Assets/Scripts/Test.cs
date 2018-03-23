using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Awake()
    {
        GameFacade.Instance.UIManager.ShowMessage("FACK");
    }
}
