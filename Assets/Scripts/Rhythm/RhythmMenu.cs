using UnityEngine;

/// <summary>
/// ������� �÷��� ���� �޴��� Ȱ��ȭ Ŭ����
/// </summary>
public class RhythmMenu : MonoBehaviour
{
    public GameObject Menu;         // Ȱ��ȭ �� �޴�
    public AudioSource Sound;       // �����
    public float Delay;
    private BGSound bgSound;

    private void Start()
    {
        bgSound = Sound.GetComponent<BGSound>();
    }

    void Update()
    {
        // ��� �������� �޴�x
        if (Sound.time <= 0f)
            return;

        // Esc Ű�� Ȱ��ȭ/��Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Escape) && !bgSound.IsReWind)
        {
            // ���� �޴����� Ȱ��ȭ ���ο� ���� ����Ī
            Menu.SetActive(!Menu.activeSelf);

            // �޴��� Ȱ��ȭ �� ���� �Ͻ�����/ ��Ȱ��ȭ �� ���� ���
            if (Menu.activeSelf)
                Sound.Pause();
            else
                bgSound.RePlay(Delay);
        }
    }
}
