using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ÿ�̸� UI ǥ�� Ŭ����
/// </summary>
public class RhythmTimer : MonoBehaviour
{
    Text text;      // Ÿ�̸� �ؽ�Ʈ
    int minute;     // ��
    int second;     // ��
    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        second = (int)RhythmManager.Instance.CurrentTime % 60;
        minute = (int)RhythmManager.Instance.CurrentTime / 60;
        text.text = minute.ToString("00") + ":" + second.ToString("00");
    }
}
