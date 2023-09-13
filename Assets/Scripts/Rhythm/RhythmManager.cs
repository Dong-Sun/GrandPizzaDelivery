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

    private decimal StartTime = 0m;
    private decimal currentTime;

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);

        Instance = this;

        //Init();
        DontDestroyOnLoad(Instance);
    }

    public  void Init()
    {
        // ���� �ð� ����
        StartTime = (decimal)AudioSettings.dspTime;
    }

    public decimal CurrentTime()
    {
        currentTime = (decimal)AudioSettings.dspTime - StartTime;
        return currentTime;
    }
}
