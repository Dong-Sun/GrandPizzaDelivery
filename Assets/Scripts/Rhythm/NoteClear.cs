using UnityEngine;

public class NoteClear : MonoBehaviour
{
    public bool IsAuto;                     // ��Ʈ �ڵ� Ŭ����
    public AudioSource NoteSound;           // Ű�� �ҷ��� ����� �ҽ�
    public RhythmStorage storage;           // ��Ʈ�� Ŭ�����ϸ� �������� ����� ĳ��
    public ClearEffect[] Effects;           // ��Ʈ Ŭ���� �� ����Ʈ ����� ������
    public AudioSource BgSound;             // ����� �ҷ��� ����� �ҽ�
    private RhythmManager manager;          // ���� �Ŵ��� ĳ��
    private Judge judge;                    // ���� ����

    void Start()
    {
        // ���� �Ŵ��� ĳ��
        manager = RhythmManager.Instance;
    }

    void Update()
    {
        // �÷��� ���� �ƴϸ� ���� x
        if (!BgSound.isPlaying)
            return;

        // ��� �ε� Ž��
        for (int i = 0; i < storage.NoteLoad.Length; i++)
        {
            // �ش� �ε忡 ���� ��Ʈ�� ������ ��
            if (storage.NoteLoad[i].Count > 0)
            {
                // �ش� �ε��� ���� ����
                judge = storage.NoteLoad[i].Peek().SendJudge();

                // �ε忡 �´� �Է°� ������ ��ȿ�� ���
                if (KeyDownInput(i) && judge != Judge.NONE)
                {
                    // ��Ʈ Ŭ����
                    JudgeCount(judge);

                    // ��Ʈ ����
                    storage.NoteClear(i);

                    // �����ǿ� ���� ����
                    Effects[i].GetJudge(judge);

                    // Ű�� ���
                    NoteSound.PlayOneShot(NoteSound.clip);
                }

                // �� �� ��Ʈ�� Ÿ���� Ȧ�� Ÿ���� ��
                else if (storage.NoteLoad[i].Peek().Type == NoteType.Hold)
                {
                    // �ε忡 �´� Ű �Է� ������ �̸� �ش� ������ ��Ȯ�� ���
                    if (KeyHoldInput(i) && (judge == Judge.PERFECT || storage.NoteLoad[i].Peek().Timing <= 0))
                    {
                        // ��Ʈ Ŭ����
                        JudgeCount(judge);

                        // ��Ʈ ����
                        storage.NoteClear(i);

                        // �����ǿ� ���� ����
                        Effects[i].GetJudge(judge);

                        // Ű�� ���
                        NoteSound.PlayOneShot(NoteSound.clip);
                    }
                }
            }
            // ���� Ŭ����
            if (IsAuto)
            {
                // �ڵ� �÷��̸� ���� ����
                if (storage.NoteLoad[i].Count > 0 && storage.NoteLoad[i].Peek().Timing <= 0 && (float)storage.NoteLoad[i].Peek().Timing > -0.12501f)
                {
                    // ��Ʈ Ŭ����
                    JudgeCount(judge);

                    // ��Ʈ ����
                    storage.NoteClear(i);

                    // �����ǿ� ���� ����
                    Effects[i].GetJudge(judge);

                    // Ű�� ���
                    NoteSound.PlayOneShot(NoteSound.clip);
                }
            }
        }
    }

    /// <summary>
    /// ���� ������ ī��Ʈ ���ִ� �Լ�
    /// </summary>
    private void JudgeCount(Judge judge)
    {
        switch (judge)
        {
            case Judge.PERFECT:
                manager.Judges.Perfect++;
                break;
            case Judge.GREAT:
                manager.Judges.Great++;
                break;
            case Judge.GOOD:
                manager.Judges.Good++;
                break;
            case Judge.MISS:
                manager.Judges.Miss++;
                break;
            default:
                Debug.LogError("�߸��� ���� (Judge)");
                return;
        }
    }

    private bool KeyDownInput(int index)
    {
        // 1 ���� : A, S
        if (index == 0)
        {
            return Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S);
        }

        // 2 ���� : ;, '
        else if (index == 1)
        {
            return Input.GetKeyDown(KeyCode.Semicolon) || Input.GetKeyDown(KeyCode.Quote);
        }
        else
            return false;
    }

    private bool KeyHoldInput(int index)
    {
        // 1 ���� : A, S
        if (index == 0)
        {
            return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S);
        }

        // 2 ���� : ;, '
        else if (index == 1)
        {
            return Input.GetKey(KeyCode.Semicolon) || Input.GetKey(KeyCode.Quote);
        }
        else
            return false;
    }
}
