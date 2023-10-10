using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// ���� ���� ���õ� �����͸� �����ϴ� �̱��� Ŭ����
/// </summary>
public class RhythmManager : MonoBehaviour
{
    public static RhythmManager Instance = null;    // �̱��� �ν��Ͻ�
    public string Title;                            // ���� �� �� ����
    public decimal CurrentTime;                     // ���� �ð�
    public AudioData Data;                          // �� ������
    public float Speed;                             // �ӵ�
    public bool SceneChange;
    public AudioSource BgSound;
    public RhythmStorage Storage;
    public JudgeStorage Judges;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;
        Judges = new JudgeStorage();
        DontDestroyOnLoad(Instance);
    }

    private void Update()
    {
        Judges.SetAttractive();
        if ((float)CurrentTime >= Data.Length && !SceneChange)
        {
            EndScene();
        }
        if (Input.GetKeyDown(KeyCode.F5) && SceneManager.GetActiveScene().name == "RhythmScene")
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
        if (BgSound == null)
            BgSound = GameObject.Find("BGSound").GetComponent<AudioSource>();
        BgSound.Play();
    }

    private void EndScene()
    {
        LoadScene.Instance.LoadPizzaMenu();
        Constant.PizzaAttractiveness = Judges.Attractive;
        SceneChange = true;
    }
}
