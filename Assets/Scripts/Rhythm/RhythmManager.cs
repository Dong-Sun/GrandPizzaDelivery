using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmManager : MonoBehaviour
{
    public static RhythmManager Instance = null;
    [Range(0f, 1f)]
    public decimal Offset;
    public AudioSource sound;
    public Note NotePrefab;        // ��Ʈ
    public Bar BarPrefab;          // ����
    public AudioData Data;         // �� ������

    private decimal StartTime = 0m;
    private decimal currentTime;
    private void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    private void Update()
    {
        
    }

    public void SetStartTime()
    {
        // ���� �ð� ����
        StartTime = (decimal)AudioSettings.dspTime;
    }

    public decimal GetCurrentTime()
    {
        currentTime = (decimal)AudioSettings.dspTime - StartTime;
        return currentTime;
    }

    public void LoadData(string path)
    {
        Data = new AudioData(path);
    }

    public void SaveData(string path)
    {
        JsonManager<AudioData>.Save(Data, path);
    }
}
