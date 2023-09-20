using UnityEngine;

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

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;
        DontDestroyOnLoad(Instance);
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
}
