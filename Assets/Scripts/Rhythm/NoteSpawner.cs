using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public GameObject Note;         // ��Ʈ
    public GameObject Bar;          // ����
    public double BPM = 84f;        // �� BPM
    public double OneBit = 0f;      // 1��Ʈ�� ���� �ӵ�
    public double OneBar = 0f;      // 1 ���� = 4��Ʈ
    public double CurBit = 0f;      // ���� ��Ʈ
    public double CurBar = 0f;      // ���� ����
    public double StartTime = 0f;   // �� ��� �ð�

    void Start()
    {
        // OneBit ����
        OneBit = 60f / BPM;

        // OneBar ����
        OneBar = OneBit * 4f;
        StartTime = AudioSettings.dspTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(CurBar <= AudioSettings.dspTime - StartTime)
        {
            GameObject b = Instantiate(Bar, transform);
            b.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            CurBar += OneBar;
        }

        //if (CurBit <= AudioSettings.dspTime - StartTime)
        //{
        //    Instantiate(Bar);
        //    CurBit += OneBit;
        //}
    }
}
