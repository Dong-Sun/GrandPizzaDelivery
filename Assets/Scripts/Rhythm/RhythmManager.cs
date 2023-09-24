using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ���� ���� ���õ� �����͸� �����ϴ� �̱��� Ŭ����
/// </summary>
public class RhythmManager : MonoBehaviour
{
    public static RhythmManager Instance = null;    // �̱��� �ν��Ͻ�
    public string Title;                            // ���� �� �� ����
    public decimal CurrentTime;                     // ���� �ð�
    public AudioSource NoteSound;                   // ��Ʈ �Ҹ�
    public AudioData Data;                          // �� ������
    public Note NotePrefab;                         // ��Ʈ
    public Bar BarPrefab;                           // ����
    public float Speed;                             // �ӵ�
    public float Accuracy;                          // ��Ȯ��
    public int Attractive;                          // �ŷµ�
    public int Perfect;
    public int Great;
    public int Good;
    public int Miss;
    public bool SceneChange;
    public AudioSource BgSound;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    private void Update()
    {
        if (Perfect + Great + Good + Miss > 0)
            Accuracy = (float)(Perfect + Great * 0.7f + Good * 0.5f) / (Perfect + Great + Good + Miss) * 100f;
        else
            Accuracy = 100;
        Attractive = (int)(Constant.PizzaAttractiveness * (Accuracy / 100));
        if ((float)CurrentTime >= Data.Length && !SceneChange)
        {
            LoadScene.Instance.LoadPizzaMenu();
            Constant.PizzaAttractiveness = Attractive;
            SceneChange = true;
        }
        if (Input.GetKeyDown(KeyCode.F5) && SceneManager.GetActiveScene().name == "RhythmScene")
        {
            LoadScene.Instance.LoadPizzaMenu();
            Constant.PizzaAttractiveness = Attractive;
            SceneChange = true;
        }
    }

    /// <summary>
    /// �� �����͸� Json ���Ϸ� ����
    /// </summary>
    public void SaveData()
    {
        JsonManager<AudioData>.Save(Data, Title);
    }

    /// <summary>
    /// Json ������ �� ������ �ҷ�����
    /// </summary>
    public void LoadData()
    {
        Data = new AudioData(Title);
    }

    public void Init()
    {
        LoadData();
        CurrentTime = 0;
        Accuracy = 0;
        Attractive = 0;
        Perfect = 0;
        Great = 0;
        Good = 0;
        Miss = 0;
        SceneChange = false;
        if (BgSound == null)
            BgSound = GameObject.Find("BGSound").GetComponent<AudioSource>();
        BgSound.Play();
    }
}
