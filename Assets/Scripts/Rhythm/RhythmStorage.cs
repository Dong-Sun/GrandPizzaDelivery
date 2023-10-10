using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmStorage : MonoBehaviour
{
    public Note NotePrefab;                         // ��Ʈ
    public Bar BarPrefab;                           // ����
    public AudioSource NoteSound;                   // ��Ʈ �Ҹ�

    public Queue<Bar> Bars;         // ���� ������Ʈ Ǯ
    public Queue<Note> Notes;       // ��Ʈ ������Ʈ Ǯ
    public Queue<Bar> BarLoad;      // �����ִ� ����
    public Queue<Note> NoteLoad;    // �����ִ� ��Ʈ

    void Start()
    {
        Bars = new Queue<Bar>();
        Notes = new Queue<Note>();
        BarLoad = new Queue<Bar>();
        NoteLoad = new Queue<Note>();
    }

    public Note DequeueNote()
    {
        Note note;

        // ������Ʈ Ǯ�� ��Ʈ�� ���� (����)
        if (Notes.Count > 0)
            note = Notes.Dequeue();

        // ������Ʈ Ǯ�� ��Ʈ�� �������� ���� (���� ����)
        else
            note = Instantiate(NotePrefab, transform);

        return note;
    }

    public Bar DequeueBar()
    {
        Bar bar;

        // ������Ʈ Ǯ�� ���� ���� (����)
        if (Bars.Count > 0)
            bar = Bars.Dequeue();

        // ������Ʈ Ǯ�� ���� �������� ���� (���� ����)
        else
            bar = Instantiate(BarPrefab, transform);

        return bar;
    }

    /// <summary>
    /// ��Ʈ Ŭ���� �Լ�
    /// </summary>
    public void NoteClear()
    {
        Note n = NoteLoad.Peek();
        n.ActiveEffect();
        Notes.Enqueue(NoteLoad.Dequeue());
        NoteSound.PlayOneShot(NoteSound.clip);
    }
    public void ReturnNote()
    {
        if (NoteLoad.Count > 0 && NoteLoad.Peek().Timing < -0.12501m)
        {
            Notes.Enqueue(NoteLoad.Dequeue());
            RhythmManager.Instance.Judges.Miss++;
        }
    }
    /// <summary>
    /// �����ִ� ��� ��Ʈ���� Ǯ�� �������� �Լ�
    /// </summary>
    public void NoteLoadReset()
    {
        while (NoteLoad.Count > 0)
        {
            Note note = NoteLoad.Peek();
            note.gameObject.SetActive(false);
            Notes.Enqueue(NoteLoad.Dequeue());
        }
    }

    /// <summary>
    /// �����ִ� ��� ������� Ǯ�� �������� �Լ�
    /// </summary>
    public void BarLoadReset()
    {
        while (BarLoad.Count > 0)
        {
            Bar bar = BarLoad.Peek();
            bar.gameObject.SetActive(false);
            Bars.Enqueue(BarLoad.Dequeue());
        }
    }
}
