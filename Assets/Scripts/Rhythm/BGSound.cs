using UnityEngine;
using System.Collections;

/// <summary>
/// ��� �뷡�� ���õ� Ŭ����
/// </summary>
public class BGSound : MonoBehaviour
{
    public float Timer = 3f;
    private float timer;
    private AudioSource source;
    private RhythmManager manager;
    
    private void Awake()
    {
        source = GetComponent<AudioSource>();
        timer = Timer;
    }
    private void Start()
    {
        manager = RhythmManager.Instance;
        source.clip = manager.AudioClip;
        StartCoroutine(WaitPlaying());
    }

    private void Update()
    {
        source.volume = manager.MusicSound;
        if(timer > 0f)
        {

            timer -= Time.deltaTime;
            manager.CurrentTime = -(decimal)timer;
        }
        else
            // �뷡 ��� �ð� ����ȭ
            manager.CurrentTime = (decimal)source.time;
    }

    private IEnumerator WaitPlaying()
    {
        yield return new WaitForSeconds(Timer);
        source.Play();
    }
}
