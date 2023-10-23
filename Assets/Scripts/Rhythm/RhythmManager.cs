using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ���� ���� ���õ� �����͸� �����ϴ� �̱��� Ŭ����
/// </summary>
public class RhythmManager : MonoBehaviour
{
    public static RhythmManager Instance             // �̱��� �ν��Ͻ�
    {
        get { return instance; }
    }
    public string Title;                            // ���� �� �� ����
    public AudioClip AudioClip;                     // ����� �� Ŭ��
    public decimal CurrentTime;                     // ���� �ð�
    public AudioData Data;                          // �� ������
    public float Speed;                             // �ӵ�
    public float MusicSound;
    public float KeySound;
    public bool SceneChange;
    public JudgeStorage Judges;

    private static RhythmManager instance = null;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        Judges = new JudgeStorage();
        Data = new AudioData();
        MusicSound = 0.5f;
        KeySound = 0.5f;
    }

    private void Update()
    {
        Judges.SetAttractive();

        if ((float)CurrentTime >= Data.Length && !SceneChange)
        {
            EndScene();
        }
        if (Input.GetKeyDown(KeyCode.F5) && SceneManager.GetActiveScene().name == "RhythmScene" && !SceneChange)
        {
            EndScene();
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
        Judges.Init();
        SceneChange = false;
    }

    private void EndScene()
    {
        SceneChange = true;
        Constant.PizzaAttractiveness = Judges.Attractive;
        LoadScene.Instance.LoadPizzaMenu();
    }
}
