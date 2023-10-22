using UnityEngine;

/// <summary>
/// ��� �뷡�� ���õ� Ŭ����
/// </summary>
public class BGSound : MonoBehaviour
{
    private AudioSource source;
    private RhythmManager manager;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    private void Start()
    {
        manager = RhythmManager.Instance;
        source.clip = manager.AudioClip;
        source.Play();
    }

    private void Update()
    {
        source.volume = manager.MusicSound;
        // �뷡 ��� �ð� ����ȭ
        manager.CurrentTime = (decimal)source.time;
    }
}
