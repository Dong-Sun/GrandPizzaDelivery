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
    public GameObject[] Lines;

    private RhythmManager manager;
    private RhythmStorage storage;

    private Sprite[] pizzaIngredientSprArr;
    private Sprite[] pizzaIngredientSprArrGolden;

    private Vector2 end = new Vector2(-8f, 0f);

    void Start()
    {
        manager = RhythmManager.Instance;
        storage = manager.Storage;
        pizzaIngredientSprArr = Resources.LoadAll<Sprite>("UI/Ingredients_120_120");
        pizzaIngredientSprArrGolden = Resources.LoadAll<Sprite>("UI/Golden_Ingredients_120_120");
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
    public void CreateNote()
    {
        // ����
        storage.NoteLoadReset();
        // ����
        int ratio = Constant.ChoiceIngredientList.Count;
        int menuList = 0;
        float nextList = manager.Data.Length / ratio;
        for (int i = 0; i < Lines.Length; i++)
        {
            foreach(var v in manager.Data.NoteLines[i])
            {
                // ��Ʈ�� ������
                Note note;

                note = storage.DequeueNote();

                note.Type = v.Value;

                if ((menuList + 1) * nextList < (float)(BitSlice * v.Key))
                    menuList++;

                if (Constant.ChoiceIngredientList.Count > 0)
                {
                    if (note.Type == NoteType.Normal)
                        note.GetComponent<SpriteRenderer>().sprite =
                        pizzaIngredientSprArr[Constant.ChoiceIngredientList[menuList]];

                    if (note.Type == NoteType.Hold)
                        note.GetComponent<SpriteRenderer>().sprite =
                        pizzaIngredientSprArrGolden[Constant.ChoiceIngredientList[menuList]];
                }
                end.y = Lines[i].transform.position.y;

                // ��Ʈ �ʱ�ȭ
                note.Init(BitSlice * v.Key, end);
                note.gameObject.SetActive(true);
                // ��Ʈ�� NoteLoad(�����ִ� ��Ʈ ����)�� �߰�
                storage.NoteLoad[i].Enqueue(note);
            }
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
        for(int i = 0; i < 2000; i++)
        {
            for (int j = 0; j < storage.BarLoad.Length; j++)
            {
                Bar bar;

                bar = storage.DequeueBar();

                end.y = (j == 0) ? 0 : 2;
                // ���� �ʱ�ȭ
                bar.Init(nextBar, end);
                bar.gameObject.SetActive(true);

                // ���� BarLoad(�����ִ� ���� ����)�� �߰�
                storage.BarLoad[j].Enqueue(bar);
            }
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
