using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteSpawner : MonoBehaviour
{
    public Note NotePrefab;        // ��Ʈ
    public Bar BarPrefab;          // ����
    public double BPM = 300d;        // �� BPM
    public double OneBit = 0d;      // 1��Ʈ�� ���� �ӵ�
    public double NextBit = 0d;      // ���� ��Ʈ
    public double OneBar = 0d;      // 1 ���� = 4��Ʈ
    public double NextBar = 0d;      // ���� ����
    [Range(0f, 1f)]
    public double Offset;
    public bool IsAuto;
    public AudioSource sound;

    Queue<Bar> Bars = new Queue<Bar>();
    Queue<Bar> BarLoad = new Queue<Bar>();

    Queue<Note> Notes = new Queue<Note>();
    Queue<Note> NoteLoad = new Queue<Note>();

    List<double> times = new List<double>();
    int index = 0;
    void Start()
    {
        // OneBit ����
        OneBit = BPM / 60d;
        OneBit = 1 / OneBit;

        // OneBar ����
        OneBar = OneBit * 4d;

        NextBar = Offset;
        NextBit = Offset;

        if (sound == null)
            sound = GameObject.Find("Metronome").GetComponent<AudioSource>();

        Debug.Log(OneBit);
        // Bar ����
        for (int i = 0; i < 30; i++)
        {
            Bar b = Instantiate(BarPrefab, transform);
            b.gameObject.SetActive(false);
            Bars.Enqueue(b);

            Note n = Instantiate(NotePrefab, transform);
            n.gameObject.SetActive(false);
            Notes.Enqueue(n);

            times.Add(Random.Range(0f, 20f));
        }
        times.Sort();
    }

    // Update is called once per frame
    void Update()
    {
        // ���� �� ����
        if (NextBar <= RhythmManager.Instance.CurrentTime())
        {
            Bar b;
            if (Bars.Count > 0)
                b = Bars.Dequeue();
            else
                b = Instantiate(BarPrefab, transform);
            b.Init();
            b.gameObject.SetActive(true);
            b.GetComponent<Image>().color = Color.cyan;
            BarLoad.Enqueue(b);
            NextBar += OneBar;
        }

        // ���� �� ��� ��Ʈ ����
        if (index < times.Count && times[index] <= RhythmManager.Instance.CurrentTime())
        {
            Note n;
            if (Notes.Count > 0)
                n = Notes.Dequeue();
            else
                n = Instantiate(NotePrefab, transform);
            n.Init();
            n.gameObject.SetActive(true);
            n.GetComponent<Image>().color = Color.red;
            NoteLoad.Enqueue(n);
            index++;
        }

        if (NoteLoad.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // ��Ʈ Ŭ����
                if (NoteLoad.Peek().SendJudge() != Judge.None)
                    NoteClear();
            }
            // ���� Ŭ����
            if (IsAuto)
            {
                if (NoteLoad.Peek().SendJudge() == Judge.Perfect)
                    NoteClear();
            }
        }

        // �����ִ� ��Ʈ�� �ٸ� �ٽ� ������Ʈ Ǯ�� ����
        if (BarLoad.Count > 0 && BarLoad.Peek().CurTime < -0.12501f)
            QueueSwaping(BarLoad, Bars);

        if (NoteLoad.Count > 0 && NoteLoad.Peek().CurTime < -0.12501f)
            QueueSwaping(NoteLoad, Notes);
    }

    private void NoteClear()
    {
        Note n = NoteLoad.Peek();
        n.gameObject.SetActive(false);
        QueueSwaping(NoteLoad, Notes);
        sound.PlayOneShot(sound.clip);
    }

    private void QueueSwaping<T>(Queue<T> start, Queue<T> end)
    {
        end.Enqueue(start.Dequeue());
    }
}
