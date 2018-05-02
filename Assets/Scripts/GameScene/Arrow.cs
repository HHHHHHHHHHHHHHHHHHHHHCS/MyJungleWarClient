using Common.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private RoleType playerType;
    private float moveSpeed = 20f;

    public void OnInit(RoleType _type,Color col)
    {
        playerType = _type;
        foreach(Transform item in transform)
        {
            item.GetComponent<MeshRenderer>().material.color = col;
        }
    }

    private void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
