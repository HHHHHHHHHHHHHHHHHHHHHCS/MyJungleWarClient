using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private PlayerType playerType;
    private float moveSpeed = 20f;
    private Rigidbody rigi;

    private void Awake()
    {
        rigi = GetComponent<Rigidbody>();
    }

    public void OnInit(PlayerType _type)
    {
        playerType = _type;
    }

    private void Update()
    {
        rigi.MovePosition(transform.position + transform.forward * moveSpeed * Time.deltaTime);
    }
}
