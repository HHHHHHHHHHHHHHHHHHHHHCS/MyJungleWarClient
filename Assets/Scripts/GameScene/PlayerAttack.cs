using Common.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Arrow arrowPrefab;
    private RoleType roleType;
    private Color col;

    private Animator anim;
    private Transform leftHandTs;
    private Camera mainCamera;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        mainCamera = Camera.main;
        leftHandTs = transform.Find(@"Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 L Clavicle/Bip001 L UpperArm/Bip001 L Forearm/Bip001 L Hand");
    }

    public void OnInit(Arrow _arrow,RoleType _type,Color _col)
    {
        arrowPrefab = _arrow;
        roleType = _type;
        col = _col;
        Shoot(Vector3.zero);
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
                StartCoroutine(PlayShootAnim(point));
            }
        }
    }

    private IEnumerator PlayShootAnim(Vector3 targetPos,float waitTime = 0.5f)
    {
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(waitTime);
        Shoot(targetPos);
    }

    private void Shoot(Vector3 targetPos)
    {
        Vector3 vec3 = transform.position;
        vec3.y = leftHandTs.position.y;
        Instantiate(arrowPrefab, leftHandTs.position, transform.rotation)
            .OnInit(roleType, col) ;
    }

}
