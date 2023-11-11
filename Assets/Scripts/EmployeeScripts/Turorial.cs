using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turorial : MonoBehaviour
{
    string[] TutorialText;
    [SerializeField] GameObject Tuto;

    bool isSkip = true;
    public static bool IsTuto = false;

    int tutoProgressCount = 0;

    private void Awake()
    {
        tutoProgressCount = 0;

        TutorialText = new string[19]
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
            "������ ä�� ����̿���. ����... ����� ���̰ŵ��. �� Ȯ���ϰ� �����ּ���",//13
            "���� �����Ŵٸ�... ���� ���� �����ǰſ���! �׷��� �������ּ���.",
            "�ھ� ���� ���� �Ѿ�� ���ʿ����� ���ڸ� ���°��� Ȯ�� �ϽǼ� ������, ���� ��Ŭ������ �޾Ƽ� ����Ͻ� ���� �ִ�ϴ�.", // 15
            "�������� �� ���� ���� �������ٴ� ��ȣ�Դϴ�.",
        "���� �˷��帱 ���� �����������.", // 17
        "�׷� ������ �����, �����~"
       };

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
            Tuto.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = TutorialText[tutoProgressCount];

            tutoProgressCount++;
        }
        else if(tutoProgressCount == TutorialText.Length)
        {
            tutoProgressCount = 0;

            IsTuto = true;
        }
        else if(tutoProgressCount >= 15 && tutoProgressCount != TutorialText.Length)
        {
            Tuto.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = TutorialText[tutoProgressCount];

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

            Tuto.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = TutorialText[tutoProgressCount];

            TextBox();

            IsTuto = false;
        }
        else
        {
            tutoProgressCount = 3;

            Tuto.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
            Tuto.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);

            Tuto.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = TutorialText[tutoProgressCount];

            TextBox();

            IsTuto = true;
        }

        tutoProgressCount = 4;
    }
}