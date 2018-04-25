using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private Arrow arrowPrefab;

    private Animator anim;
    private Transform leftHandTs;
    private Camera mainCamera;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        mainCamera = Camera.main;
        leftHandTs = transform.Find(@"Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 L Clavicle/Bip001 L UpperArm/Bip001 L Forearm/Bip001 L Hand");
    }

    private void Update()
    {
        if (Input.GetMouseButton(0)
            && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool isHit = Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask(TagLayerNames.L_floor));
            if (isHit)
            {
                Vector3 point = hit.point;
                anim.SetTrigger("Attack");
                Shoot(point);
            }
        }
    }

    private void Shoot(Vector3 targetPos)
    {
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
        Instantiate(arrowPrefab, leftHandTs.position, Quaternion.LookRotation(targetPos));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.forward * 30 + transform.position);
    }
}
