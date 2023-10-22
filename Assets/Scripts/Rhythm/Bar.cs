using UnityEngine;

/// <summary>
/// ���� üũ �� �� Ŭ����
/// </summary>
public class Bar : MonoBehaviour
{
    public decimal Timing { get { return timing; } }    // ���� �ð� ��ȯ

    private float speed;                                // �̵� �ӵ�
    private decimal arrive;                             // ���� �ð�
    private decimal timing;                             // ���� �ð�
    private Vector2 end = new Vector2(-8f, 0f);         // ���� ��ġ
    private Transform trans;
    private RhythmManager manager;
    void Update()
    {
        timing = arrive - manager.CurrentTime;
        BarMove();
    }

    /// <summary>
    /// ���� �ʱ�ȭ �Լ�
    /// </summary>
    /// <param name="arriveTime">���� �ð�</param>
    public void Init(decimal arriveTime, Vector2 _end)
    {
        manager = RhythmManager.Instance;
        if (trans == null)
            trans = GetComponent<Transform>();
        arrive = arriveTime;
        timing = 300m;
        end = _end;
    }

    /// <summary>
    /// �� �̵�
    /// </summary>
    private void BarMove()
    {
        speed = manager.Speed;
        trans.localPosition = end + Vector2.right * (float)timing * speed * 5f;
    }
}
