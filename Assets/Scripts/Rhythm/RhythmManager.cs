using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    public static RhythmManager Instance = null;
    [Range(0f, 1f)]
    public decimal Offset;
    public AudioSource sound;
    public Note NotePrefab;        // ��Ʈ
    public Bar BarPrefab;          // ����
    public AudioData Data;         // �� ������
    public decimal CurrentTime;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    public void SaveData(string fileName)
    {
        JsonManager<AudioData>.Save(Data, fileName);
    }

    public void LoadData(string fileName)
    {
        Data = new AudioData(fileName);
    }
}
