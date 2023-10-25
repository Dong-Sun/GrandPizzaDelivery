using UnityEngine;

public class ClearEffect : MonoBehaviour
{
    public float Speed = 3f;            // ����� ������ ���ƿ��� ���� �ɸ��� �ð� = 1 / Speed;
    public GameObject Ring;             // �� ����Ǹ鼭 ����� ����Ʈ ��
    private SpriteRenderer render;      // SpriteRenderer ĳ��
    private Color[] color;              // ������ ���� ������ ���� ��� �迭 
    private Color startColor;           // ó�� �����ִ� ��
    private Color endColor;             // ������ ��
    private float timer = 0f;           // ���� ������ Ÿ�̸� ����
    void Start()
    {
        // ������ ĳ��
        render = GetComponent<SpriteRenderer>();

        // ���� ����
        color = new Color[] { Color.cyan, Color.green, Color.yellow };

        // ���� ���� ����
        startColor = render.color;
    }

    void Update()
    {
        // Ÿ�̸Ӱ� ������ ����x
        if (timer <= 0)
            return;

        // ���� ����� ���� ������ ���� ������ ������ ���� �ʱ�ȭ
        render.color = Color.Lerp(startColor, endColor, timer);

        // �ð��� ������ ���� Ÿ�̸� ����
        timer -= Time.deltaTime * Speed;
    }

    /// <summary>
    /// �޾ƿ� ������ ���� ������ ���� �ϴ� �Լ�
    /// </summary>
    /// <param name="judge"></param>
    public void GetJudge(Judge judge)
    {
        switch (judge)
        {
            case Judge.PERFECT:
                Init(0);
                break;
            case Judge.GREAT:
                Init(1);
                break;
            case Judge.GOOD:
                Init(2);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// ����Ʈ �ʱ�ȭ �Լ�
    /// </summary>
    /// <param name="index">������ ���� ����</param>
    private void Init(int index)
    {
        //��ǥ ���� �ʱ�ȭ
        endColor = color[index];

        // ����Ʈ �� �ν��Ͻ�
        GameObject ring = Instantiate(Ring, transform);

        // �� ����, ��ǥ �ʱ�ȭ
        ring.GetComponent<SpriteRenderer>().color = color[index];
        ring.transform.localPosition = Vector2.zero;
        timer = 1f;
    }
}
