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
    public decimal Timing { set { timing = value; } get { return timing; } }    // ���� �ð� ��ȯ
    public NoteType Type;
    private float speed;                                    // �̵� �ӵ�
    private decimal arrive;                                 // ���� �ð�
    private decimal timing;                                 // ���� �ð�
    private Vector2 end;                                    // ���� ��ġ
    private Transform trans;
    private bool Effect;
    private float fade = 1f;

    private void Update()
    {
        if (Effect)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, fade);
            fade -= Time.deltaTime * 5f;
            if (fade <= 0f)
                gameObject.SetActive(false);
        }
        else
        {
            timing = arrive - RhythmManager.Instance.CurrentTime;
            NoteMove();
            NoteDrop();
        }
    }

    /// <summary>
    /// ���� �ʱ�ȭ �Լ�
    /// </summary>
    /// <param name="arriveTime">���� �ð�</param>
    public void Init(decimal arriveTime, Vector2 _end)
    {
        if (trans == null)
            trans = GetComponent<Transform>();
        GetComponent<SpriteRenderer>().color = Color.white;
        Effect = false;
        arrive = arriveTime;
        fade = 1f;
        timing = 300m;
        end = _end;
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

    public void ActiveEffect()
    {
        Effect = true;
    }

    /// <summary>
    /// ��Ʈ �̵�
    /// </summary>
    private void NoteMove()
    {
        speed = RhythmManager.Instance.Speed;
        trans.localPosition = end + Vector2.right * (float)timing * speed * 5f;
    }

    /// <summary>
    /// ������ ��Ʈ ���
    /// </summary>
    private void NoteDrop()
    {
        if (timing < -0.12501m)
        {
            gameObject.SetActive(false);
        }
    }
}
