using UnityEngine;

/// <summary>
/// ����
/// </summary>
public enum Judge { NONE = 0, PERFECT, GREAT, GOOD, MISS }

/// <summary>
/// ��Ʈ
/// </summary>
public class Note : MonoBehaviour
{
    public decimal Timing { get { return timing; } }    // ���� �ð� ��ȯ

    private float speed;            // �̵� �ӵ�
    private decimal arrive;         // ���� �ð�
    private decimal timing;         // ���� �ð�
    private Vector2 start;          // ���� ��ġ
    private Vector2 end;            // ���� ��ġ
    private NoteSpawner spawner;
    private Transform trans;
    void Update()
    {
        timing = arrive - RhythmManager.Instance.CurrentTime;
        NoteMove();
        NoteDrop();
    }

    /// <summary>
    /// ���� �ʱ�ȭ �Լ�
    /// </summary>
    /// <param name="arriveTime">���� �ð�</param>
    public void Init(decimal arriveTime)
    {
        FindCompnent();
        arrive = arriveTime;
        start = new Vector2(10f, 0);
    }

    /// <summary>
    /// ���� ���� �Լ�
    /// </summary>
    /// <returns>����</returns>
    public Judge SendJudge()
    {
        if (Mathf.Abs((float)timing) > 0.12501f)
            return Judge.NONE;
        else if (Mathf.Abs((float)timing) <= 0.04167f)
            return Judge.PERFECT;
        else if (Mathf.Abs((float)timing) <= 0.08334f)
            return Judge.GREAT;
        else
            return Judge.GOOD;
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
    /// ��Ʈ �̵�
    /// </summary>
    private void NoteMove()
    {
        speed = RhythmManager.Instance.Speed;
        if (timing > 0m)
            trans.localPosition = Vector2.Lerp(end, start * speed, (float)timing / 10f * speed);
        else
            trans.localPosition = Vector2.Lerp(end, (end - start) * speed, (float)-timing / 10f * speed);
    }

    /// <summary>
    /// ������ ��Ʈ ���
    /// </summary>
    private void NoteDrop()
    {
        if (timing < -0.12501m)
        {
            //Debug.Log("Delete Note");
            gameObject.SetActive(false);
        }
    }
}
