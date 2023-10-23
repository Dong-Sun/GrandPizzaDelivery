using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ѽ�ȣ �ۼ�
public class PoliceSmokeEffect : MonoBehaviour
{
    private SpriteRenderer sprR;
    private Transform trans;
    private float colorA = 1f;
    // Start is called before the first frame update
    void OnEnable()
    {
        // ĳ��
        sprR = this.gameObject.GetComponent<SpriteRenderer>();
        trans = this.gameObject.transform;
        // �ʱ�ȭ
        sprR.color = new Color(1, 1, 1, 1);
        colorA = 1f;
        trans.localScale = Vector3.one;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (colorA > 0)
        {
            // ���İ��� ������ ����
            colorA -= 2 / 255f;
            sprR.color = new Color(1, 1, 1, colorA);
            // ���� ����Ʈ ��ġ�� ũ�⸦ �������� �̵���Ű�� ���̰ų� Ű��
            trans.position += new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100)).normalized * Time.deltaTime * 3f;
            trans.localScale += new Vector3(Random.Range(-10, 100), Random.Range(-10, 100), 0).normalized * 0.1f;
        }
        else
        {
            // ���İ��� 0 �Ʒ��� �������� ���İ��� 0���� �ٲٰ�, ���� ������Ʈ�� ���ش�.
            colorA = 0f;
            this.gameObject.SetActive(false);
        }
    }
}
