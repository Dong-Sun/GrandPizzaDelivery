using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// ���� ������ ��Ʈ�� ���ִ� Ŭ����
/// </summary>
public class Selector : MonoBehaviour
{
    [SerializeField] private string[] Titles;           // ������ ���� �̸�
    [SerializeField] private AudioSource Sound;         // �����
    [SerializeField] private AudioClip[] Highlights;    // �̸���� ���̶���Ʈ Ŭ����
    [SerializeField] private AudioClip[] Clips;         // ���� ������ Ŭ����
    [SerializeField] private Text Title;                // �� ���� �ؽ�Ʈ
    [SerializeField] private Text Bpm;                  // Bpm ��� �ؽ�Ʈ
    [SerializeField] private Text Level;                // ���̵� ��� �ؽ�Ʈ
    [SerializeField] private Text Length;                // �뷡 ���� ��� �ؽ�Ʈ
    [SerializeField] private RectTransform LPDisk;      // ȸ�������� ���� ����
    [SerializeField] private GameObject Menu;
    [SerializeField] private UnityEvent AnimationStart;
    [SerializeField] private UnityEvent AnimationStop;

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
            if (Menu.activeSelf)
                return;

            if (RhythmManager.Instance.IsSelectGuide)
                return;

            // UpArrow = ���� Ŭ��
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Next(0);
            }

            // DownArrow = ���� Ŭ��
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Next(1);
            }

            // Enter = Ŭ�� ����
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                Select();
            }
        }
    }

    public void Next(int i)
    {
        if (i == 0)
        {
            // index + 1, ���� �迭 ���� ����� 0���� ����
            index = (index + 1) >= Titles.Length ? 0 : index + 1;
            // ��ǥ �� 90���� ����
            endAngle = 90f;
        }
        else
        {
            // index - 1, ���� �迭 ���� ����� ������ �ε������� ����
            index = (index - 1) < 0 ? Titles.Length - 1 : index - 1;
            // ��ǥ �� 90���� ����
            endAngle = -90f;
        }

        // Ŭ�� ������
        isChange = true;
        AnimationStop.Invoke();
    }

    public void Select()
    {
        // ���� �Ŵ��� Ÿ��Ʋ, Ŭ�� ����
        RhythmManager.Instance.Title = Titles[index];
        RhythmManager.Instance.AudioClip = Clips[index];

        // �� ��ȯ
        LoadScene.Instance.ActiveTrueFade("RhythmScene");
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
        int minute = (int)(audioData.Length / 60);
        int second = (int)(audioData.Length % 60);
        Length.text = minute.ToString("0") + "m" + second.ToString("00") + "s";

        // �ش� �ε����� Ŭ������ ���� �� ���
        Sound.clip = Highlights[index];
        Sound.Play();
        AnimationStart.Invoke();
    }
}