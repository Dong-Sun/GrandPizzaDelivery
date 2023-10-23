using UnityEngine;

public class NoteClear : MonoBehaviour
{
    public bool IsAuto;                     // ��Ʈ �ڵ� Ŭ����
    public AudioSource NoteSound;
    public RhythmStorage storage;
    public ClearEffect[] Effects;
    public AudioSource BgSound;
    private RhythmManager manager;
    private Judge judge;

    void Start()
    {
        manager = RhythmManager.Instance;
    }

    void Update()
    {
        if (!BgSound.isPlaying)
            return;

        for (int i = 0; i < storage.NoteLoad.Length; i++)
        {
            if (storage.NoteLoad[i].Count > 0)
            {
                judge = storage.NoteLoad[i].Peek().SendJudge();
                if (KeyDownInput(i) && judge != Judge.NONE)
                {
                    // ��Ʈ Ŭ����
                    JudgeCount(judge);
                    storage.NoteClear(i);
                    Effects[i].GetJudge(judge);
                    NoteSound.PlayOneShot(NoteSound.clip);
                }
                else if (storage.NoteLoad[i].Peek().Type == NoteType.Hold)
                {
                    if (KeyHoldInput(i) && (judge == Judge.PERFECT || storage.NoteLoad[i].Peek().Timing <= 0))
                    {
                        // ��Ʈ Ŭ����
                        JudgeCount(judge);
                        storage.NoteClear(i);
                        Effects[i].GetJudge(judge);
                        NoteSound.PlayOneShot(NoteSound.clip);
                    }
                }
            }
            // ���� Ŭ����
            if (IsAuto)
            {
                if (storage.NoteLoad[i].Count > 0 && storage.NoteLoad[i].Peek().Timing <= 0 && (float)storage.NoteLoad[i].Peek().Timing > -0.12501f)
                {
                    JudgeCount(judge);
                    storage.NoteClear(i);
                    Effects[i].GetJudge(judge);
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
        if (index == 0)
        {
            return Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S);
        }
        else if (index == 1)
        {
            return Input.GetKeyDown(KeyCode.Semicolon) || Input.GetKeyDown(KeyCode.Quote);
        }
        else
            return false;
    }

    private bool KeyHoldInput(int index)
    {
        if (index == 0)
        {
            return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S);
        }
        else if (index == 1)
        {
            return Input.GetKey(KeyCode.Semicolon) || Input.GetKey(KeyCode.Quote);
        }
        else
            return false;
    }
}
