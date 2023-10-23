using UnityEngine;
using System.Collections;

/// <summary>
/// ��������� ��� �뷡 ��°� ���õ� Ŭ����
/// </summary>
public class BGSound : MonoBehaviour
{
    public float Timer = 2f;        // ���� ������
    private float timer;            // Ÿ�̸� ������ ���� ����
    private AudioSource source;     // ���� ����� ���� ����� �ҽ� ĳ��
    private RhythmManager manager;  // �Ŵ��� ĳ��
    
    private void Awake()
    {
        // ����� �ҽ� ĳ��
        source = GetComponent<AudioSource>();

        // Ÿ�̸� �ʱ�ȭ
        timer = Timer;
    }
    private void Start()
    {
        // �Ŵ��� ĳ��
        manager = RhythmManager.Instance;

        // �뷡�� �Ŵ����� �ִ� Ŭ������ ����
        source.clip = manager.AudioClip;

        // �ڷ�ƾ�� ���� �÷��� ����
        StartCoroutine(WaitPlaying());
    }

    private void Update()
    {
        // ������ �Ŵ��� ������ ����ȭ
        source.volume = manager.MusicSound;

        // ���� �����̰� ���� ������(���� x)
        if(timer > 0f)
        {
            // Ÿ�̸Ӹ� �ð���ŭ ���ֱ�
            timer -= Time.deltaTime;

            // �Ŵ����� ����ð��� Ÿ�̸� ������ �����
            manager.CurrentTime = -(decimal)timer;
        }
        else
            // �뷡 ��� �ð� ����ȭ
            manager.CurrentTime = (decimal)source.time;
    }

    /// <summary>
    /// �÷��� ���� �ڷ�ƾ
    /// </summary>
    /// <returns>�����ð�</returns>
    private IEnumerator WaitPlaying()
    {
        // Ÿ�̸� �ð���ŭ ��ٷȴٰ� ����� ����
        yield return new WaitForSeconds(Timer);
        source.Play();
    }
}
