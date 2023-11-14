using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turorial : MonoBehaviour
{
    string[] tutorialText = new string[19]
           { "�ȳ��ϼ���! ����Բ� ���� ����� �� ���� �����ߴµ� �̷��� ��� ���ּż� �����̿���!",
            "���� ���Ը� ��� ���� ���� �����Ű���?",
            "�׷��ñ���... �׷��� ���� �����! ���� ������ ���͵帱 �� �־��!",
        "�����������׿�! �׷� ���ú��� ��а� �� ��Ź�����~",
        "���� �������� ���ø� ���� ���� ������� ���Ǹ� ����Ͽ� ���� �������� ���� ����� �����߾��.", // 4
        "�̰� ������� ���͵帱 �˹� ���� ���� �� �� �ִ� ���̿���.", // 5
        "�˹ٴ� �ٰ��� ���ڸ� ����µ�, �˹��� �ɷ��� �������� ������ ����Ƽ�� ����� �ӵ��� �޶����ſ���~",
        "�����ִ� ������ ����Ƽ�� �������� ������ �ְ�, â�Ƿ��� ������ ����Ƽ�� ������ Ȯ���� �����ָ�, ����� ������ ����Ƽ�� ������ Ȯ���� ������ݴϴ�.",
        "�翬�� �ϱް� �ٹ������� ����� �����̿���! ���� �ϱ��̳� �ٹ� ������ ��� ��������, ���� ����� �˹ٵ��� ��Ʈ������ ���� �� �ִ�ϴ�.",
        "������ �޴��� ������ �� �ִ� ���̿���.", //9
        "��Ḧ ����, ���ڿ� ���� Ű�� ������ �����Ǵ� �ݹ��̶��ϴ�!",
            "������ �����ص� ���� �޴��� ����ϴ� ���̿���.", //11
            "�������� �̸��� ���� ���ð�, ����ϰ� ���� �޴��� �߰��Ͻ� �� ����. ���� ������ �����ϴ�ϴ�.",
            "������ ä�� ����̿���. ����... ����� ���̰ŵ��. �� Ȯ���ϰ� �����ּ���.",//13
            "���� �����Ŵٸ�... ���� ���� �����ǰſ���! �׷��� �������ּ���.",
            "�ھ� ���� ���� �Ѿ�� ���ʿ����� ���ڸ� ���°��� Ȯ�� �ϽǼ� ������, ���� ��Ŭ������ �޾Ƽ� ����Ͻ� ���� �ִ�ϴ�.", // 15
            "�������� �� ���� ���� �������ٴ� ��ȣ�Դϴ�.",
        "���� �˷��帱 ���� �����������.", // 17
        "�׷� ������ �����, �����~"
           };

    string[] employeeTutoText = new string[7]
    {
    "��� ������ �ο��� �Ϸ翡 3������ �����Ǿ�������, ��ħ���� ���ο� �ο��� ���ɴϴ�.",//0
    "����� �ο��� ����â�� ������ �� ������ �� �� �ֽ��ϴ�.",//1
    "�ϱ��� ���� ���� ȭ��ǥ�� ���� ���̰ų�, �ø� �� �ֽ��ϴ�. ���ڰ� ���� ���̸� ���� �޿����� ���ٴ� ���̰�, �ʷ� ���̸� ���� �޿����� ���ٴ� ���Դϴ�.",
    "���� �޿����� ������ ��Ʈ������ �پ��� ���� �޿����� ������ ��Ʈ������ �����մϴ�.",
    "�ٹ�ǥ�� ���� �ش� �˹��� �ٹ� ������ ������ �� �ֽ��ϴ�.",//4
    "������ ���� ��ư�� �������̸� ��ư�� ������ �� ���� �ٹ� �ο� ��Ͽ��� �����Ǹ�, �Ͼ���� �� �߰��˴ϴ�.",
    "���� ��ȣ���ϰ� �ٸ� ���� �ٹ��� ���� �ȴٸ� �˹��� ��Ʈ������ ���Դϴ�.",
    };

    [SerializeField] GameObject Tuto;
    [SerializeField] GameObject EmployeeTutoPanel;
    [SerializeField] GameObject EmloyeePanel;

    bool isSkip = true;
    public static bool IsTuto = false;
    public static bool IsEmployeeTuto = false;

    int tutoProgressCount = 0;

    [SerializeField] Vector2[] TutoPos;

    private void Awake()
    {
        tutoProgressCount = 0;

        EmloyeePanel.SetActive(false);

        if (IsTuto == false)
        {
            Tuto.SetActive(true);

            TextBox();

            Tuto.transform.GetChild(1).gameObject.SetActive(true);

            Tuto.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);

            Tuto.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            Tuto.SetActive(false);
        }
    }

    public void TextBox()
    {
        // 4 5 9 11 13 15 17
        switch (tutoProgressCount)
        {
            case 1:
                Tuto.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);

                Tuto.transform.GetChild(1).GetChild(2).gameObject.SetActive(true);

                Tuto.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
                break;
            case 4:
                Tuto.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);  

                Tuto.transform.GetChild(0).gameObject.SetActive(true);
                break;
            case 5:
                Tuto.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);

                Tuto.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);

                Tuto.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                Tuto.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                Tuto.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
                Tuto.transform.GetChild(0).GetChild(4).gameObject.SetActive(false);

                Tuto.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                break;
            case 9:
                Tuto.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
                Tuto.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);

                Tuto.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                break;
            case 11:
                Tuto.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
                Tuto.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);

                Tuto.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                break;
            case 13:
                Tuto.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);

                Tuto.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                Tuto.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
                break;
            case 15:
                Tuto.transform.GetChild(1).gameObject.SetActive(false);
                Tuto.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
                Tuto.transform.GetChild(0).GetChild(4).gameObject.SetActive(true);

                Tuto.transform.GetChild(2).gameObject.SetActive(true);
                break;
            case 17:
                Tuto.transform.GetChild(0).gameObject.SetActive(false);

                for (int i = 0; i < Tuto.transform.GetChild(0).childCount; i++)
                {
                    Tuto.transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
                }
                break;
        }

        if(tutoProgressCount < 15)
        {
            Tuto.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = tutorialText[tutoProgressCount];

            tutoProgressCount++;
        }
        else if(tutoProgressCount == tutorialText.Length)
        {
            tutoProgressCount = 0;

            IsTuto = true;
        }
        else if(tutoProgressCount >= 15 && tutoProgressCount != tutorialText.Length)
        {
            Tuto.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = tutorialText[tutoProgressCount];

            tutoProgressCount++;
        }

        if (IsTuto == true)
        {
            Tuto.SetActive(false);
        }
    }

    public void YesOrNO(bool value)
    {
        isSkip = value;

        SkipText();
    }

    void SkipText()
    {
        if (isSkip)
        {
            tutoProgressCount = 2;

            Tuto.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
            Tuto.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);

            Tuto.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = tutorialText[tutoProgressCount];

            TextBox();

            IsTuto = false;
        }
        else
        {
            tutoProgressCount = 3;

            Tuto.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
            Tuto.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);

            Tuto.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = tutorialText[tutoProgressCount];

            TextBox();

            IsTuto = true;
        }

        tutoProgressCount = 4;
    }

    int EmployeeTutoCount = 0;
    int TextPosCount = 0;

    public void EmployeeTuto()
    {
        if (IsEmployeeTuto == false && IsTuto == true)
        {
            switch (EmployeeTutoCount)
            {
                case 0:
                    EmployeeTutoPanel.SetActive(true);

                    EmployeeTutoPanel.transform.GetChild(1).gameObject.SetActive(true);
                    EmployeeTutoPanel.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
                    EmployeeTutoPanel.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
                    EmployeeTutoPanel.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
                    break;
                case 1:
                    EmployeeTutoPanel.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
                    EmployeeTutoPanel.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
                    EmployeeTutoPanel.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
                    break;
                case 5:
                    EmployeeTutoPanel.transform.GetChild(1).GetChild(2).gameObject.SetActive(true);
                    break;
                case 6:
                    IsEmployeeTuto = true;
                    break;
            }

            if (EmployeeTutoCount == 0 || EmployeeTutoCount == 1 || EmployeeTutoCount == 4)
            {
                EmployeeTutoPanel.transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = TutoPos[TextPosCount];

                TextPosCount++;
            }

            EmployeeTutoPanel.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = employeeTutoText[EmployeeTutoCount];

            if (EmployeeTutoCount < employeeTutoText.Length - 1)
            {
                EmployeeTutoCount++;
            }
        }
        else if(IsEmployeeTuto == true)
        {
            EmployeeTutoPanel.SetActive(false);

            TextPosCount = 0;
            EmployeeTutoCount = 0;
        }
    }
}