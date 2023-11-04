using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gun;

public class PoliceGunShooting : GunShooting
{
    public bool ShootingStance = false;
    private Vector3 dir;
    private Transform MyTransform;
    public Transform PlayerTransfrom;
    int layerMask;
    public PoliceGunShooting(Transform myTransform, string exception)
    {
        MyTransform = myTransform;
        layerMask = ((1 << LayerMask.NameToLayer(exception)) | (1 << LayerMask.NameToLayer("WallObstacle"))
            | (1 << LayerMask.NameToLayer("CrossWalk")));// Everything���� Player, WallObstacle, CrossWalk ���̾ �����ϰ� �浹 üũ��
    }

    public Vector3 GetTargetPos()
    {
        Vector3 point = PlayerTransfrom.position;
        return point;
    }

    public void aiming()
    {
        if (ShootingStance)
        {
            dir = GetTargetPos() - MyTransform.position;
        }
    }
    float time;

    public bool ShootRaycast(float fireRate, short damage)
    {
        time += Time.deltaTime;
        if (fireRate < time)
        {
            if (ShootingStance)
            {
                Debug.Log("�÷��̾����� �ѽ�");
                ani.SetTrigger("NewStart");
                ShootAudio.Play();
                layerMask = ~layerMask;
                RaycastHit2D hit = Physics2D.Raycast(MyTransform.position, dir.normalized, 1000, layerMask);
                if (hit)
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        PlayerStat.HP -= damage;
                    }
                }
                time = 0;
                return true;

            }
        }
        return false;
    }
    public Animator ani;
    public AudioSource ShootAudio;
    public bool Fire(float fireRate, short damage)
    {
        aiming();
        return ShootRaycast(fireRate, damage);
    }
}
