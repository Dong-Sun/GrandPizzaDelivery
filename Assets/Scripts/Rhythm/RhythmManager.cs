using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
class AudioData
{
    public string Name;         // �� �̸�
    public decimal BPM;         // �� BPM
    public decimal Length;      // �� ����
    public bool[] IsNote;     // ��Ʈ ���� �ð�

    public AudioData(string name)
    {

    }
}
public class RhythmManager : MonoBehaviour
{
    public static RhythmManager Instance = null;
    [Range(0f, 1f)]
    public decimal Offset;
    public AudioSource sound;
    public Note NotePrefab;        // ��Ʈ
    public Bar BarPrefab;          // ����

    public decimal BPM = 120m;        // �� BPM
    public decimal OneBar = 0m;      // 1 ���� = 4��Ʈ
    public decimal NextBar = 0m;      // ���� ����

    private decimal StartTime = 0m;
    private decimal currentTime;

    private Queue<Bar> Bars = new Queue<Bar>();
    private Queue<Bar> BarLoad = new Queue<Bar>();

    private Queue<Note> Notes = new Queue<Note>();
    private Queue<Note> NoteLoad = new Queue<Note>();

    private AudioData data;
    private Note n;


    private decimal bitSlice;
    private void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;
        // Object Initialize
        for (int i = 0; i < 30; i++)
        {
            Bar b = Instantiate(BarPrefab, transform);
            b.gameObject.SetActive(false);
            Bars.Enqueue(b);

            n = Instantiate(NotePrefab, transform);
            n.gameObject.SetActive(false);
            Notes.Enqueue(n);
        }
        //Init();
        DontDestroyOnLoad(Instance);
    }

    public  void Init()
    {
        // OneBar ����
        OneBar = 60m / BPM * 4m;

        NextBar = Offset;
        bitSlice = (60m / data.BPM) / 4m;    // 1/16
        // ��Ʈ ����
        for (int i = 0; i < data.IsNote.Length; i++)
        {
            if (data.IsNote[i])
            {
                if (Notes.Count > 0)
                    n = Notes.Dequeue();
                else
                    n = Instantiate(NotePrefab, transform);
                n.Init(bitSlice * i);
                n.gameObject.SetActive(true);
                n.GetComponent<Image>().color = Color.red;
                NoteLoad.Enqueue(n);
            }
        }

        // ���� �ð� ����
        StartTime = (decimal)AudioSettings.dspTime;
    }

    public decimal CurrentTime()
    {
        currentTime = (decimal)AudioSettings.dspTime - StartTime;
        return currentTime;
    }
    private void NoteClear()
    {
        Note n = NoteLoad.Peek();
        n.gameObject.SetActive(false);
        QueueSwaping(NoteLoad, Notes);
        sound.PlayOneShot(sound.clip);
    }

    public void CreateBar()
    {
        if (NextBar <= RhythmManager.Instance.CurrentTime())
        {
            Bar b;
            if (Bars.Count > 0)
                b = Bars.Dequeue();
            else
                b = Instantiate(BarPrefab, transform);
            b.Init(NextBar + 6m);
            b.gameObject.SetActive(true);
            b.GetComponent<Image>().color = Color.cyan;
            BarLoad.Enqueue(b);
            NextBar += OneBar;
        }
    }
    public void QueueSwaping<T>(Queue<T> start, Queue<T> end)
    {
        end.Enqueue(start.Dequeue());
    }
}
