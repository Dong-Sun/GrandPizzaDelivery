using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteSpawner : MonoBehaviour
{
    public decimal BPM = 120m;          // �� BPM
    public decimal OneBar = 0m;         // 1 ���� = 4��Ʈ
    public decimal NextBar = 0m;        // ���� ����
    
    [Range(0f, 1f)]
    public double Offset;
    public bool IsAuto;
    public AudioSource sound;

    private Note notePrefab;        // ��Ʈ
    private Bar barPrefab;          // ����

    private static Queue<Bar> Bars = new Queue<Bar>();
    private static Queue<Note> Notes = new Queue<Note>();

    private Queue<Bar> BarLoad = new Queue<Bar>();
    private Queue<Note> NoteLoad = new Queue<Note>();

    private AudioData data;
    private decimal bitSlice;
    void Start()
    {
        RhythmManager.Instance.Init();
        data = new AudioData();
        if (sound == null)
            sound = GameObject.Find("Metronome").GetComponent<AudioSource>();

        notePrefab = RhythmManager.Instance.NotePrefab;
        barPrefab = RhythmManager.Instance.BarPrefab;

        // Object Initialize
        if (Bars.Count == 0 && Notes.Count == 0)
        {
            for (int i = 0; i < 30; i++)
            {
                Bar b = Instantiate(barPrefab, transform);
                b.gameObject.SetActive(false);
                Bars.Enqueue(b);

                Note n = Instantiate(notePrefab, transform);
                n.gameObject.SetActive(false);
                Notes.Enqueue(n);
            }
        }

        Init();
        //for(int i = 0; i < times.Count; i++)
        //{
        //    int divide = (int)(times[i] / bitSlice);
        //    if (times[i] % bitSlice < bitSlice / 2m)
        //        times[i] = bitSlice * divide;
        //    else
        //        times[i] = bitSlice * (divide + 1);
        //}
    }

    void Update()
    {
        // ���� �� ����
        CreateBar();

        if (NoteLoad.Count > 0)
        {
            if (Input.anyKeyDown)
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
        if (BarLoad.Count > 0 && BarLoad.Peek().Timing < -0.12501m)
            QueueSwaping(BarLoad, Bars);

        if (NoteLoad.Count > 0 && NoteLoad.Peek().Timing < -0.12501m)
            QueueSwaping(NoteLoad, Notes);
    }
    private void Init()
        {
            // OneBar ����
            OneBar = 60m / BPM * 4m;

            NextBar = (decimal)Offset;
            bitSlice = OneBar / 16m;    // 1/16
            // ��Ʈ ����
            for (int i = 0; i < data.IsNote.Length; i++)
            {
                if (data.IsNote[i])
                {
                    Note n;
                    if (Notes.Count > 0)
                        n = Notes.Dequeue();
                    else
                        n = Instantiate(notePrefab, transform);
                    n.Init(OneBar / 2m * i + (decimal)Offset);
                    n.gameObject.SetActive(true);
                    n.GetComponent<Image>().color = Color.red;
                    NoteLoad.Enqueue(n);
                    //Debug.Log(bitSlice * i);
                }
            }
        }
    private void CreateBar()
    {
        if (NextBar <= RhythmManager.Instance.CurrentTime())
        {
            Bar b;
            if (Bars.Count > 0)
                b = Bars.Dequeue();
            else
                b = Instantiate(barPrefab, transform);
            b.Init(NextBar + 6m);
            b.gameObject.SetActive(true);
            b.GetComponent<Image>().color = Color.cyan;
            BarLoad.Enqueue(b);
            NextBar += OneBar;
        }
    }

    private void NoteClear()
    {
        Note n = NoteLoad.Peek();
        n.gameObject.SetActive(false);
        Debug.Log(n.SendJudge());
        QueueSwaping(NoteLoad, Notes);
        sound.PlayOneShot(sound.clip);
    }

    private void QueueSwaping<T>(Queue<T> start, Queue<T> end)
    {
        end.Enqueue(start.Dequeue());
    }
}
