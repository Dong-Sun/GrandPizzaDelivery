using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ʈ �� �ٸ� ������Ű�� Ŭ����
/// </summary>
public class NoteSpawner : MonoBehaviour
{
    [Range(-1f, 1f)]
    public float Sync;                      // �� ��ũ (���� ���� ���� �ʿ�)
    public static decimal BitSlice;         // 1 ��Ʈ�� 8 ���
    public bool IsAuto;                     // ��Ʈ �ڵ� Ŭ����
    public float BarInterval = 1f;          // �� ����

    public static Queue<Bar> Bars = new Queue<Bar>();       // ���� ������Ʈ Ǯ
    public static Queue<Note> Notes = new Queue<Note>();    // ��Ʈ ������Ʈ Ǯ

    public static Queue<Bar> BarLoad = new Queue<Bar>();    // �����ִ� ����
    public static Queue<Note> NoteLoad = new Queue<Note>(); // �����ִ� ��Ʈ

    private decimal oneBar;                 // 4 ��Ʈ�� 1 ����
    private decimal nextBar;                // ���� ����
    private int barCycle = 0;               // ���� �� ������ ���� �ӽ� ����

    private Note notePrefab;                // ��Ʈ
    private Bar barPrefab;                  // ����
    private Sprite[] pizzaIngredientSprArr;

    private RhythmManager manager;
    void Start()
    {
        manager = RhythmManager.Instance;
        notePrefab = manager.NotePrefab;
        barPrefab = manager.BarPrefab;
        pizzaIngredientSprArr = Resources.LoadAll<Sprite>("UI/Ingredients_120_120");

        Init();
    }

    void Update()
    {
        if (manager.Data != null)
            manager.Data.Sync = Sync;

        // �����ִ� ��Ʈ�� ����
        if (NoteLoad.Count > 0)
        {
            // ��Ʈ Ŭ����� Ű ���ε�
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) ||
                Input.GetKeyDown(KeyCode.Colon) || Input.GetKeyDown(KeyCode.Quote))
            {
                // ��Ʈ Ŭ����
                if (NoteLoad.Peek().SendJudge() != Judge.NONE)
                {
                    JudgeCount();
                    NoteClear();
                }
            }

            // ���� Ŭ����
            if (IsAuto)
            {
                if (NoteLoad.Count > 0 && NoteLoad.Peek().SendJudge() == Judge.PERFECT)
                {
                    JudgeCount();
                    NoteClear();
                }
            }
        }

        // �����ִ� ��Ʈ�� �ٸ� �ٽ� ������Ʈ Ǯ�� ����
        ReturnNote();
    }
    public void Init()
    {
        // ���� �ʱ�ȭ
        manager.Init();

        // ������ �� ����
        DataCalculator();

        // ��Ʈ ����
        CreateNote();

        // �� ����
        CreateBar();
    }

    /// <summary>
    /// ��Ʈ Ŭ���� �Լ� (���Ŀ� �ٸ� Ŭ������ �̵�)
    /// </summary>
    public static void NoteClear()
    {
        Note n = NoteLoad.Peek();
        n.gameObject.SetActive(false);
        QueueSwaping(NoteLoad, Notes);
        RhythmManager.Instance.NoteSound.PlayOneShot(RhythmManager.Instance.NoteSound.clip);
    }

    /// <summary>
    /// ��Ʈ�� �����ϴ� �Լ�
    /// </summary>
    public void CreateNote()
    {
        // ����
        NoteLoadReset();
        // ����
        int ratio = Constant.ChoiceIngredientList.Count;
        int curList = 0;
        float nextList = manager.Data.Length / ratio;
        foreach (var v in manager.Data.IsNote)
        {
            // ��Ʈ�� ������
            Note n;

            // ������Ʈ Ǯ�� ��Ʈ�� ���� (����)
            if (Notes.Count > 0)
                n = Notes.Dequeue();

            // ������Ʈ Ǯ�� ��Ʈ�� �������� ���� (���� ����)
            else
                n = Instantiate(notePrefab, transform);

            // ��Ʈ �ʱ�ȭ
            n.Init(BitSlice * v.Key + (decimal)Sync);
            if ((curList + 1) * nextList < (float)(BitSlice * v.Key + (decimal)Sync))
                curList++;

            if (Constant.ChoiceIngredientList.Count > 0)
                n.GetComponent<SpriteRenderer>().sprite =
                pizzaIngredientSprArr[Constant.ChoiceIngredientList[curList]];

            n.gameObject.SetActive(true);

            // ��Ʈ�� NoteLoad(�����ִ� ��Ʈ ����)�� �߰�
            NoteLoad.Enqueue(n);
        }
    }

    /// <summary>
    /// ���� �����ϴ� �Լ�
    /// </summary>
    private void CreateBar()
    {
        // ����
        BarLoadReset();

        // ����
        barCycle = 0;
        // 5000���� ���� ����(���Ŀ� �� ���̿� ���� ����� ����)
        for (int i = 0; i < 5000; i++)
        {
            Bar bar;

            // ������Ʈ Ǯ�� ���� ���� (����)
            if (Bars.Count > 0)
                bar = Bars.Dequeue();

            // ������Ʈ Ǯ�� ���� �������� ���� (���� ����)
            else
                bar = Instantiate(barPrefab, transform);

            // ���� �ʱ�ȭ
            bar.Init(nextBar);
            bar.gameObject.SetActive(true);

            // ���� BarLoad(�����ִ� ���� ����)�� �߰�
            BarLoad.Enqueue(bar);


            nextBar += (oneBar / (decimal)BarInterval);
            barCycle++;
        }
    }

    /// <summary>
    /// �����ִ� ��� ��Ʈ���� Ǯ�� �������� �Լ�
    /// </summary>
    private void NoteLoadReset()
    {
        while (NoteLoad.Count > 0)
        {
            Note note = NoteLoad.Peek();
            note.gameObject.SetActive(false);
            QueueSwaping(NoteLoad, Notes);
        }
    }

    /// <summary>
    /// �����ִ� ��� ������� Ǯ�� �������� �Լ�
    /// </summary>
    private void BarLoadReset()
    {
        while (BarLoad.Count > 0)
        {
            Bar bar = BarLoad.Peek();
            bar.gameObject.SetActive(false);
            QueueSwaping(BarLoad, Bars);
        }
    }

    /// <summary>
    /// ������ ��Ʈ�� �������� �Լ�
    /// </summary>
    private void ReturnNote()
    {
        if (NoteLoad.Count > 0 && NoteLoad.Peek().Timing < -0.12501m)
        {
            QueueSwaping(NoteLoad, Notes);
            RhythmManager.Instance.Miss++;
        }
    }

    /// <summary>
    /// ť ���� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <typeparam name="T">���׸� Ÿ��</typeparam>
    /// <param name="start">�̾Ƴ� ť</param>
    /// <param name="end">�־��� ť</param>
    private static void QueueSwaping<T>(Queue<T> start, Queue<T> end)
    {
        end.Enqueue(start.Dequeue());
    }

    /// <summary>
    /// �����͸� ������� ���� �� �����ϴ� �Լ�
    /// </summary>
    private void DataCalculator()
    {
        // 60m / (decimal)data.BPM = 1 ��Ʈ
        // 1 ���� = 4 ��Ʈ
        oneBar = 60m / (decimal)manager.Data.BPM * 4m;
        Sync = manager.Data.Sync;
        nextBar = 0;

        // BitSlice = ��Ʈ / 8 = ���� / 32
        BitSlice = oneBar / 32m;
    }

    /// <summary>
    /// ���� ������ ī��Ʈ ���ִ� �Լ�
    /// </summary>
    private void JudgeCount()
    {
        switch (NoteLoad.Peek().SendJudge())
        {
            case Judge.PERFECT:
                manager.Perfect++;
                break;
            case Judge.GREAT:
                manager.Great++;
                break;
            case Judge.GOOD:
                manager.Good++;
                break;
            case Judge.MISS:
                manager.Miss++;
                break;
            default:
                Debug.LogError("�߸��� ���� (Judge)");
                return;
        }
    }
}
