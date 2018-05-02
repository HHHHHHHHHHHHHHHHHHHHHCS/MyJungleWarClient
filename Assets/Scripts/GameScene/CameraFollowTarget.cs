using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField]
    private Vector3 offest = new Vector3(0, 20, -10);
    [SerializeField]
    private float smoothing = 2;

    private float sqrSmoothing = 4;
    private Transform target;


    private CameraFollowTarget()
    {
        sqrSmoothing = smoothing * smoothing;
    }

    public void OnInit(Transform _target)
    {
        target = _target;
        //offest = transform.position - _target.position;
    }

    public void Update()
    {
        if (target)
        {
            Vector3 endPos = target.position + offest;
            if ((transform.position - endPos).sqrMagnitude <= sqrSmoothing * Time.deltaTime)
            {
                transform.position = endPos;
            }
            else
            {
                transform.position += (endPos - transform.position).normalized * smoothing * Time.deltaTime;
            }
        }
    }
}
