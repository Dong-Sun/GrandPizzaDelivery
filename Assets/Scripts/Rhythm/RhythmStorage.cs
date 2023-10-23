using System.Collections.Generic;
using UnityEngine;

public class RhythmStorage : MonoBehaviour
{
    public Note NotePrefab;                         // ��Ʈ
    public Bar BarPrefab;                           // ����

    public Queue<Bar> Bars = new Queue<Bar>();          // ���� ������Ʈ Ǯ
    public Queue<Note> Notes = new Queue<Note>();       // ��Ʈ ������Ʈ Ǯ

    public Queue<Bar>[] BarLoad = new Queue<Bar>[2];       // �����ִ� ����
    public Queue<Note>[] NoteLoad = new Queue<Note>[2];    // �����ִ� ��Ʈ

    private RhythmManager manager;

    private void Awake()
    {
        for (int i = 0; i < 2; i++)
        {
            BarLoad[i] = new Queue<Bar>();
            NoteLoad[i] = new Queue<Note>();
        }
    }

    private void Start()
    {
        manager = RhythmManager.Instance;
    }

    private void Update()
    {
        // �����ִ� ��Ʈ�� �ٸ� �ٽ� ������Ʈ Ǯ�� ����
        ReturnNote();
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
    public void NoteClear(int line)
    {
        Note n = NoteLoad[line].Peek();
        n.gameObject.SetActive(false);
        Notes.Enqueue(NoteLoad[line].Dequeue());
    }

    public void ReturnNote()
    {
        foreach (var load in NoteLoad)
        {
            if (load.Count > 0 && load.Peek().Timing < -0.12501m)
            {
                Notes.Enqueue(load.Dequeue());
                manager.Judges.Miss++;
            }
        }
    }

    /// <summary>
    /// �����ִ� ��� ��Ʈ���� Ǯ�� �������� �Լ�
    /// </summary>
    public void NoteLoadReset()
    {
        foreach (var load in NoteLoad)
        {
            while (load.Count > 0)
            {
                Note note = load.Peek();
                note.gameObject.SetActive(false);
                Notes.Enqueue(load.Dequeue());
            }
        }
    }

    /// <summary>
    /// �����ִ� ��� ������� Ǯ�� �������� �Լ�
    /// </summary>
    public void BarLoadReset()
    {
        foreach (var load in BarLoad)
        {
            while (load.Count > 0)
            {
                Bar bar = load.Peek();
                bar.gameObject.SetActive(false);
                Bars.Enqueue(load.Dequeue());
            }
        }
    }
}
