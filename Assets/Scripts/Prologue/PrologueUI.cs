using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �Ѽ�ȣ �ۼ�
public class PrologueUI : MonoBehaviour
{
    [SerializeField] private Sprite[] prologueSpr;
    [SerializeField] private Image img;
    [SerializeField] private Text text;
    [SerializeField] private GameObject nextButton;

    private string[] textList = new string[13]
    {
        "������! ����! ��...",
        "(Ƽ�� ����.)",
        "��° �� ����� ����..",
        "��~!(�� �տ� ���� �︰��.)",
        "(������ ���� �� �ִ�.)",
        "�������׼� �� ������?",
        "'�� ��ī����'",
        "'��ī��. �������� �����ϴ±���.'",
        "''���۽������� ������ ��Ź�� ����� �� �ִ�?",
        "'������ ���� ���� ����鿡�� �ѱ�� �ִܴ�. ���� �� ������ �־�.'",
        "'�����Ǵ� �� ���� ���±��� ��ߴ� ���ڰ��Էα���.'",
        "'�����ϴٸ� ����� �� �������� �þ��־����� �ϴ±���.'",
        "'����� ����...'"
    };
    private int index = 0;

    void Awake()
    {
        img.sprite = prologueSpr[0];
        text.text = textList[0];
        index = 0;
        Invoke("ActiveNextButton", 1.5f);
    }

    private void ActiveNextButton()
    {
        nextButton.SetActive(true);
    }

    public void ClickNextButton()
    {
        if (prologueSpr.Length - 1 > index)
        {
            index++;
            img.sprite = prologueSpr[index];
            text.text = textList[index];
            nextButton.SetActive(false);
            Invoke("ActiveNextButton", 1.5f);
        }
        else
        {
            LoadScene.Instance.LoadPrologueToInGameScene();
        }
    }
}
