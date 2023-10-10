using UnityEngine;

/// <summary>
/// ���� üũ �� �� Ŭ����
/// </summary>
public class Bar : MonoBehaviour
{
    public decimal Timing { get { return timing; } }    // ���� �ð� ��ȯ

    private float speed;            // �̵� �ӵ�
    private decimal arrive;         // ���� �ð�
    private decimal timing;         // ���� �ð�
    private Vector2 end;            // ���� ��ġ
    private NoteSpawner spawner;
    private Transform trans;

    void Update()
    {
        timing = arrive - RhythmManager.Instance.CurrentTime;
        BarMove();
    }

    /// <summary>
    /// ���� �ʱ�ȭ �Լ�
    /// </summary>
    /// <param name="arriveTime">���� �ð�</param>
    public void Init(decimal arriveTime)
    {
        FindCompnent();
        arrive = arriveTime;
    }

    /// <summary>
    /// �Ҵ���� ���� ������Ʈ ã�Ƽ� �Ҵ�
    /// </summary>
    private void FindCompnent()
    {
        if (end == Vector2.zero)
            end = GameObject.Find("Judgement").GetComponent<Transform>().localPosition;
        if (trans == null)
            trans = GetComponent<Transform>();
        if (spawner == null)
            spawner = transform.parent.GetComponent<NoteSpawner>();
    }

    /// <summary>
    /// �� �̵�
    /// </summary>
    private void BarMove()
    {
        speed = RhythmManager.Instance.Speed;
        trans.localPosition = end + Vector2.right * (float)timing * speed * 5f;
    }
}
