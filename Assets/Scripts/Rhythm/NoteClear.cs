using System.Collections;
using System.Collections.Generic;
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
        // �����ִ� ��Ʈ�� ����
        if (storage.NoteLoad.Count > 0)
        {
            if (storage.NoteLoad.Peek().Type == NoteType.Normal)
            {
                // ��Ʈ Ŭ����� Ű ���ε� [A S ; ']
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) ||
                    Input.GetKeyDown(KeyCode.Semicolon) || Input.GetKeyDown(KeyCode.Quote))
                {
                    // ��Ʈ Ŭ����
                    if (storage.NoteLoad.Peek().SendJudge() != Judge.NONE)
                    {
                        JudgeCount();
                        storage.NoteClear();
                    }
                }
            }
            if (storage.NoteLoad.Peek().Type == NoteType.Hold)
            {
                // ��Ʈ Ŭ����� Ű ���ε� [A S ; ']
                if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
                {
                    // ��Ʈ Ŭ����
                    if (storage.NoteLoad.Peek().SendJudge() == Judge.PERFECT || storage.NoteLoad.Peek().Timing < 0)
                    {
                        JudgeCount();
                        storage.NoteClear();
                    }
                }
            }
            // ���� Ŭ����
            if (IsAuto)
            {
                if (storage.NoteLoad.Count > 0 && storage.NoteLoad.Peek().SendJudge() == Judge.PERFECT)
                {
                    JudgeCount();
                    storage.NoteClear();
                }
            }
        }
    }

    /// <summary>
    /// ���� ������ ī��Ʈ ���ִ� �Լ�
    /// </summary>
    private void JudgeCount()
    {
        switch (storage.NoteLoad.Peek().SendJudge())
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
}
