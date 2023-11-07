using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoEffect : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, 3f);
    }
    private void Update()
    {
        transform.Translate(Vector2.up * 50 * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.transform.CompareTag("Police") || collision.transform.CompareTag("ChaserPoliceCar"))
        {
            GameObject blood = Instantiate(BloodEffect, transform.position, transform.rotation);
            Destroy(blood, 0.3f);
        }
        if (collision.transform.CompareTag("House"))//�̻��ϰ� ���̶� �浹üũ �ȵ�
        {
            Debug.Log("House�� �Ѿ� �浹");
            GameObject wallhit = Instantiate(WallHitEffect, transform.position, transform.rotation);
            Destroy(wallhit, 0.3f);
        }*/
        Destroy(gameObject);
    }
}
