using UnityEngine;

public class NoteClear : MonoBehaviour
{
    public bool IsAuto;                     // ��Ʈ �ڵ� Ŭ����

    private RhythmManager manager;
    private RhythmStorage storage;


    void Start()
    {
        manager = RhythmManager.Instance;
        storage = manager.Storage;
    }

    void Update()
    {
        for(int i = 0; i < storage.NoteLoad.Length; i++)
        {
            if (storage.NoteLoad[i].Count > 0)
            {
                if (KeyDownInput(i) && storage.NoteLoad[i].Peek().SendJudge() != Judge.NONE)
                {
                    // ��Ʈ Ŭ����   
                    JudgeCount(i);
                    storage.NoteClear(i);
                }

                else if (storage.NoteLoad[i].Peek().Type == NoteType.Hold)
                {
                    if (KeyHoldInput(i) && (storage.NoteLoad[i].Peek().SendJudge() == Judge.PERFECT || storage.NoteLoad[i].Peek().Timing <= 0))
                    {
                        // ��Ʈ Ŭ����
                        JudgeCount(i);
                        storage.NoteClear(i);
                    }
                }
            }
            // ���� Ŭ����
            if (IsAuto)
            {
                if (storage.NoteLoad[i].Peek().Timing <= 0)
                {
                    JudgeCount(i);
                    storage.NoteClear(i);
                }
                if (storage.NoteLoad[i].Peek().Timing <= 0)
                {
                    JudgeCount(i);
                    storage.NoteClear(i);
                }
            }
        }

    }

    /// <summary>
    /// ���� ������ ī��Ʈ ���ִ� �Լ�
    /// </summary>
    private void JudgeCount(int index)
    {
        switch (storage.NoteLoad[index].Peek().SendJudge())
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
