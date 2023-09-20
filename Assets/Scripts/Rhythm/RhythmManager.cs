using UnityEngine;
using System.Collections.Generic;

public class RhythmManager : MonoBehaviour
{
    public static RhythmManager Instance = null;
    public decimal CurrentTime;
    public string Title;
    public float Offset;
    public AudioSource Metronome;
    public AudioData Data;         // �� ������
    public Note NotePrefab;        // ��Ʈ
    public Bar BarPrefab;          // ����

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    public void SaveData()
    {
        JsonManager<AudioData>.Save(Data, Title);
    }

    public void LoadData()
    {
        Data = new AudioData(Title);
    }
}
