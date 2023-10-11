using UnityEngine;

/// <summary>
/// ��Ʈ �� �ٸ� ������Ű�� Ŭ����
/// </summary>
public class NoteSpawner : MonoBehaviour
{
    [Range(-1f, 1f)]
    public float Sync;                      // �� ��ũ (���� ���� ���� �ʿ�)
    public static decimal BitSlice;         // 1 ��Ʈ�� 8 ���
    public float BarInterval = 1f;          // �� ����

    private decimal oneBar;                 // 4 ��Ʈ�� 1 ����
    private decimal nextBar;                // ���� ����
    private int barCycle = 0;               // ���� �� ������ ���� �ӽ� ����

    private Sprite[] pizzaIngredientSprArr;

    private RhythmManager manager;
    private RhythmStorage storage;
    void Start()
    {
        manager = RhythmManager.Instance;
        storage = manager.Storage;
        pizzaIngredientSprArr = Resources.LoadAll<Sprite>("UI/Ingredients_120_120");

        Init();
    }

    void Update()
    {
        if (manager.Data != null)
            manager.Data.Sync = Sync;

        // �����ִ� ��Ʈ�� �ٸ� �ٽ� ������Ʈ Ǯ�� ����
        storage.ReturnNote();
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
    /// ��Ʈ�� �����ϴ� �Լ�
    /// </summary>
    private void CreateNote()
    {
        // ����
        storage.NoteLoadReset();
        // ����
        int ratio = Constant.ChoiceIngredientList.Count;
        int curList = 0;
        float nextList = manager.Data.Length / ratio;
        foreach (var v in manager.Data.Notes)
        {
            // ��Ʈ�� ������
            Note note;

            note = storage.DequeueNote();

            note.Type = v.Value;
            // ��Ʈ �ʱ�ȭ

            if ((curList + 1) * nextList < (float)(BitSlice * v.Key))
                curList++;

            if (Constant.ChoiceIngredientList.Count > 0)
                note.GetComponent<SpriteRenderer>().sprite =
                pizzaIngredientSprArr[Constant.ChoiceIngredientList[curList]];

            note.Init(BitSlice * v.Key);
            Debug.Log(BitSlice * v.Key);
            note.gameObject.SetActive(true);
            // ��Ʈ�� NoteLoad(�����ִ� ��Ʈ ����)�� �߰�
            storage.NoteLoad.Enqueue(note);
        }
    }

    /// <summary>
    /// ���� �����ϴ� �Լ�
    /// </summary>
    private void CreateBar()
    {
        // ����
        storage.BarLoadReset();

        // ����
        barCycle = 0;
        // 5000���� ���� ����(���Ŀ� �� ���̿� ���� ����� ����)
        for (int i = 0; i < 5000; i++)
        {
            Bar bar;

            bar = storage.DequeueBar();

            // ���� �ʱ�ȭ
            bar.Init(nextBar);
            bar.gameObject.SetActive(true);

            // ���� BarLoad(�����ִ� ���� ����)�� �߰�
            storage.BarLoad.Enqueue(bar);

            nextBar += (oneBar / (decimal)BarInterval);
            barCycle++;
        }
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
}
