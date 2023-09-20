using UnityEngine;

/// <summary>
/// ��� �뷡�� ���õ� Ŭ����
/// </summary>
public class BGSound : MonoBehaviour
{
    private AudioSource source;
    private void Start()
    {
        if (source == null)
            source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // �뷡 ��� �ð� ����ȭ
        RhythmManager.Instance.CurrentTime = (decimal)source.time;
    }
}
