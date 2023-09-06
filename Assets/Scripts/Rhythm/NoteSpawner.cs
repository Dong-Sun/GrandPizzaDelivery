using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class NoteSpawner : MonoBehaviour
{
    public Note NotePrefab;        // ��Ʈ
    public Bar BarPrefab;          // ����
    public double BPM = 60d;        // �� BPM
    public double OneBit = 0d;      // 1��Ʈ�� ���� �ӵ�
    public double CurBit = 0d;      // ���� ��Ʈ
    public double OneBar = 0d;      // 1 ���� = 4��Ʈ
    public double CurBar = 0d;      // ���� ����
    [Range(0f, 1f)]
    public double Offset;

    Queue<Bar> Bars = new Queue<Bar>();
    Queue<Note> Notes = new Queue<Note>();

    void Start()
    {
        // OneBit ����
        OneBit = BPM / 60d;
        OneBit = 1 / OneBit;

        // OneBar ����
        OneBar = OneBit * 4d;

        CurBar = Offset;
        CurBit = Offset;
        Debug.Log(OneBit);
        // Bar ����
        for(int i = 0; i < 30; i++)
        {
            Bar b = Instantiate(BarPrefab, transform);
            b.gameObject.SetActive(false);
            Bars.Enqueue(b);

            Note n = Instantiate(NotePrefab, transform);
            n.gameObject.SetActive(false);
            Notes.Enqueue(n);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ���� �� ����
        if (CurBar <= RhythmManager.Instance.CurrentTime())
        {
            Bar b;
            if (Bars.Count > 0)
                b = Bars.Dequeue();
            else
                b = Instantiate(BarPrefab, transform);
            b.Init();
            b.gameObject.SetActive(true);
            b.GetComponent<Image>().color = Color.cyan;
            CurBar += OneBar;
        }

        // ��Ʈ ���� BPM ���
        if (CurBit <= RhythmManager.Instance.CurrentTime())
        {
            Note n;
            if (Notes.Count > 0)
                n = Notes.Dequeue();
            else
                n = Instantiate(NotePrefab, transform);
            n.Init();
            n.gameObject.SetActive(true);
            n.GetComponent<Image>().color = Color.red;
            CurBit += OneBit;
        }

        // ���� �� ��� ����
    }

    public void BarComeBack(Bar bar)
    {
        Bars.Enqueue(bar);
    }
    public void NoteComeBack(Note note)
    {
        Notes.Enqueue(note);
    }
}
