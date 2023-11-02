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
    private KeyCode[] clearKeys;

    private void Start()
    {
        // ���� �Ŵ��� ĳ��
        manager = RhythmManager.Instance;
        KeyMapping();
    }

    private void Update()
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
                    Clear(i);
                }
                // �� �� ��Ʈ�� Ÿ���� Ȧ�� Ÿ���� ��
                else if (storage.NoteLoad[i].Peek().Type == NoteType.Hold)
                {
                    // �ε忡 �´� Ű �Է� ������ �̸� �ش� ������ ��Ȯ�� ���
                    if (KeyHoldInput(i) && (judge == Judge.PERFECT || storage.NoteLoad[i].Peek().Timing <= 0))
                    {
                        Clear(i);
                    }
                }
            }
            // ���� Ŭ����
            if (IsAuto)
            {
                // �ڵ� �÷��̸� ���� ����
                if (storage.NoteLoad[i].Count > 0 && storage.NoteLoad[i].Peek().Timing <= 0 && (float)storage.NoteLoad[i].Peek().Timing > -0.12501f)
                {
                    Clear(i);
                }
            }
        }
    }

    public void KeyMapping()
    {
        clearKeys = new KeyCode[4];
        if (manager.ClearKeys.Length > 0)
        {
            for (int i = 0; i < manager.ClearKeys.Length; i++)
            {
                clearKeys[i] = manager.ClearKeys[i];
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
        // 1 ���� : [0] [1]
        if (index == 0)
        {
            return Input.GetKeyDown(clearKeys[0]) || Input.GetKeyDown(clearKeys[1]);
        }

        // 2 ���� : [2] [3]
        else if (index == 1)
        {
            return Input.GetKeyDown(clearKeys[2]) || Input.GetKeyDown(clearKeys[3]);
        }
        else
            return false;
    }

    private bool KeyHoldInput(int index)
    {
        // 1 ���� : [0] [1]
        if (index == 0)
        {
            return Input.GetKey(clearKeys[0]) || Input.GetKey(clearKeys[1]);
        }

        // 2 ���� : [2] [3]
        else if (index == 1)
        {
            return Input.GetKey(clearKeys[2]) || Input.GetKey(clearKeys[3]);
        }
        else
            return false;
    }

    private void Clear(int index)
    {
        // ��Ʈ Ŭ����
        JudgeCount(judge);

        // ��Ʈ ����
        storage.NoteClear(index);

        // �����ǿ� ���� ����
        Effects[index].GetJudge(judge);

        // Ű�� ���
        NoteSound.PlayOneShot(NoteSound.clip);
    }
}
