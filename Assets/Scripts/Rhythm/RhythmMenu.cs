using UnityEngine;

/// <summary>
/// ������� �÷��� ���� �޴��� Ȱ��ȭ Ŭ����
/// </summary>
public class RhythmMenu : MonoBehaviour
{
    public GameObject Menu;         // Ȱ��ȭ �� �޴�
    public AudioSource BgSound;     // �����
    void Update()
    {
        // ��� �������� �޴�x
        if (BgSound.time <= 0f)
            return;

        // Esc Ű�� Ȱ��ȭ/��Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ���� �޴����� Ȱ��ȭ ���ο� ���� ����Ī
            Menu.SetActive(!Menu.activeSelf);

            // �޴��� Ȱ��ȭ �� ���� �Ͻ�����/ ��Ȱ��ȭ �� ���� ���
            if (Menu.activeSelf)
                BgSound.Pause();
            else
                BgSound.Play();
        }
    }
}
