using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ѽ�ȣ �ۼ�
public class CrossWalk : MonoBehaviour, ICheckIsGreen
{
    [SerializeField] private Sprite redSpr;
    [SerializeField] private Sprite greenSpr;
    [SerializeField] private Sprite yellowSpr;

    private SpriteRenderer sprRenderer;
    private Coroutine lightCoroutine;
    private List<Police> policeList = new List<Police>();
    private bool isGreen = false;
    
    private void Awake()
    {
        sprRenderer = this.GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        lightCoroutine = StartCoroutine(LightChange());
    }
    /// <summary>
    /// �ð��� ���� ��ȣ��(Ⱦ�ܺ���) �� ��ȭ�� �ִ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    private IEnumerator LightChange()
    {
        // Ⱦ�ܺ������� ���� ���� �� �ٸ�
        int r = Random.Range(0, 9) * 90; 
        for (int i = 0; i < r; i++)
        {
            yield return Constant.OneTime;
        }

        while(true)
        {
            for(int i = 0; i < 1500; i++)
            {
                yield return Constant.OneTime; 
            }
            isGreen = true;
            sprRenderer.sprite = greenSpr;
            for (int i = 0; i < 450; i++)
            {
                yield return Constant.OneTime;
            }
            sprRenderer.sprite = yellowSpr; ;
            isGreen = false;
            for (int i = 0; i < 150; i++)
            {
                yield return Constant.OneTime;
            }
            sprRenderer.sprite = redSpr;
            // Ⱦ�ܺ����� �������� �Ǹ� �������� �ٽ� �����̰� ��
            for (int i = 0; i < policeList.Count; i++)
            {
                policeList[i].PausePoliceCar(false);
            }
            policeList.Clear();

        }
    }
    /// <summary>
    /// �ʷϺ������� �Ǻ�����
    /// </summary>
    /// <returns></returns>
    public bool CheckIsGreen()
    {
        return isGreen;
    }
    /// <summary>
    /// ��ȣ� ���� �̵��� ��ȭ�� �ִ� �ݶ��̴��� ã�Ƽ� �������� ������
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �ʷϺ��̰� �������� �͵鿡�� �ش�
        if (collision.GetComponent<Police>() != null && isGreen)
        {
            // �������� �Ͻ����� ��Ŵ
            collision.GetComponent<Police>().PausePoliceCar(true);
            // �Ͻ������� �������� ���߿� �����ϱ� ���� ����Ʈ�� �ӽ÷� ����
            policeList.Add(collision.GetComponent<Police>());
        }
    }
}
