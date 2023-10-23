using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���� ������ ��Ʈ�� ���ִ� Ŭ����
/// </summary>
public class Selector : MonoBehaviour
{
    public string[] Titles;         // ������ ���� �̸�
    public AudioSource Sound;       // �����
    public AudioClip[] Clips;       // ���� ������ Ŭ����
    public Text Title;              // �� ���� �ؽ�Ʈ
    public Text Bpm;                // Bpm ��� �ؽ�Ʈ
    public Text Level;              // ���̵� ��� �ؽ�Ʈ
    public RectTransform LPDisk;    // ȸ�������� ���� ����

    private float startAngle = 0f;  // ȸ�� ���� ��
    private float endAngle = 0f;    // ȸ�� �� ��
    private float angle;            // ���� ��
    private bool isChange = false;  // ���� ���� �� ���� Ȯ��
    private static int index = 0;   // ���� ����� �ε���
    private AudioData audioData;    // �� ������ �ҷ��� ������
    private float timer = 0f;       // ���� ������ ���� Ÿ�̸� ����
    void Start()
    {
        // index ���� ���� ���� ����
        ChangeSong(index);
    }

    void Update()
    {
        // Ŭ�� ������
        if (isChange)
        {
            // ���� �� ���ư�
            if (timer < 1f)
            {
                // Ÿ�̸ӿ� �ð� ����
                timer += Time.deltaTime * 2f;

                // Ÿ�̸ӿ� ���� ���� ��ȯ
                angle = Mathf.LerpAngle(startAngle, endAngle, timer);

                // ȸ��
                LPDisk.eulerAngles = new Vector3(0, 0, angle);
            }
            else
            {
                // Ÿ�̸� 0���� �ǵ���
                timer = 0f;

                // ���� �Ϸ�
                isChange = false;

                // Rotate �ʱ�ȭ
                LPDisk.eulerAngles = new Vector3(0, 0, 0);

                // �� ����
                ChangeSong(index);
            }
        }
        // Ŭ�� �������� �ƴ�
        else
        {
            // UpArrow = ���� Ŭ��
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                // index + 1, ���� �迭 ���� ����� 0���� ����
                index = (index + 1) >= Titles.Length ? 0 : index + 1;

                // ��ǥ �� 90���� ����
                endAngle = 90f;

                // Ŭ�� ������
                isChange = true;
            }

            // DownArrow = ���� Ŭ��
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                // index - 1, ���� �迭 ���� ����� ������ �ε������� ����
                index = (index - 1) < 0 ? Titles.Length - 1 : index - 1;

                // ��ǥ �� 90���� ����
                endAngle = -90f;

                // Ŭ�� ������
                isChange = true;
            }

            // Enter = Ŭ�� ����
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // ���� �Ŵ��� Ÿ��Ʋ, Ŭ�� ����
                RhythmManager.Instance.Title = Titles[index];
                RhythmManager.Instance.AudioClip = Clips[index];

                // �� ��ȯ
                LoadScene.Instance.ActiveTrueFade("RhythmScene");
            }
        }
    }

    /// <summary>
    /// Ŭ�� �����ϴ� �Լ�
    /// </summary>
    /// <param name="index">������ �ε���</param>
    private void ChangeSong(int index)
    {
        // Ÿ��Ʋ �������� ������ �ε�
        audioData = new AudioData(Titles[index]);

        // �ε��� ������ ǥ��
        Title.text = audioData.Name;
        Bpm.text = audioData.BPM.ToString() + "bpm";
        Level.text = audioData.Level;

        // �ش� �ε����� Ŭ������ ���� �� ���
        Sound.clip = Clips[index];
        Sound.Play();
    }
}
