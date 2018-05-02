using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float speed = 3;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            if (Mathf.Abs(h) > 0.001f || Mathf.Abs(v) > 0.001f)
            {

                Vector3 stepPos = new Vector3(h, 0, v) * speed * Time.deltaTime;
                //transform.Translate(stepPos, Space.World);//不习惯用这个 还是用下面的吧
                transform.position += stepPos;
                if (stepPos != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(stepPos);
                }

            }
            float res = Mathf.Max(Mathf.Abs(h), Mathf.Abs(v));
            anim.SetFloat("Forward", res);
        }
    }
}
