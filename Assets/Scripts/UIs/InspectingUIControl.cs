using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �Ѽ�ȣ �ۼ�

public class InspectingUIControl : MonoBehaviour, IInspectingUIText
{
    [SerializeField] private GameObject[] diceObjArr;
    [SerializeField] private GameObject[] playerTextObjArr;
    [SerializeField] private Sprite[] diceSprArr;

    [SerializeField] private GameObject uiControl;
    [SerializeField] private Text diceSuccessText;
    [SerializeField] private Text policeText;

    private IInspectingPanelControl iInspectingPanelControl;

    private RectTransform[] diceRectArr;
    private Image[] diceImgArr;
    private Text[] playerTextArr;
    private PlayerTexts[] playerTextsArr;
    private Coroutine diceCoroutine;
    
    private string[] inspectingPoliceTextStart = new string[23] 
    {
        "��� ������ ����?",   // 0
        "�ű� ���� �ֽǱ�?",   // 1
        "�̰� ���� ������?",   // 2
        "�������ΰ���?",  // 3
        "�����Ѵ�.",    // 4
        "���ڹ�޺� ������...�ű� ���� �������.",  // 5
        "��� �˹��� �ؾ߰ھ�.", // 6
        "���� ������ ���°� ������? �˹��� �ؾ߰ھ�.",    // 7
        "�ѹ� ���캸����. �ҹ� ���İ��� �� �����.", // 8
        "(���� �ֻ��� 7 �̻�) ���� �̻��� �Ŷ� ���������? �̺���, �� ���ÿ� �������� �� �� ���̰�, \n����� �������� �ֹ� ������ ������ �ǻ簡 �־��. �׷��� �ҽÿ� �̷� ���� ���� �����ּ���.",   // 9
        "(20000���� �ش�.)��...�׸� ������ �ǰڽ��ϱ�?",   // 10
        "�̷� ���ξ��� �����ݾ� ! �̷� �ҹ������� �����ϰ� �ִٴ�...�̰� �м���. \n������ ���� ���Կ� �������״� �������� �̷� �� ������ !",  // 11
        "����. ������ ����.",  // 12
        "Ȯ���� �� ���� �±�. ������ ����.", // 13
        "���� �ʹ� ���. �ܸ����� �� ���̳� �������.",   // 14
        "����. �̹��� �������ָ�. �������� �����ϵ���",    // 15
        "��..�� ���� �� ������� ������.",  // 16
        "(20000���� �ش�.) �׷��� ����. ��� �ȵǰڽ��ϱ�?",    // 17
        "������ �༮. ������ �� �� ���� ����.",   // 18
        "��������.", // 19
        "�ð��� �����߱�. ���� ü���ϰھ� !",  // 20
        "(����.)", // 21
        "(�˹��� �޴´�.)" // 22
    };

    private bool isDiceRoll = false;

    private void Awake()
    {
        playerTextArr = new Text[playerTextObjArr.Length];
        playerTextsArr = new PlayerTexts[playerTextObjArr.Length];

        diceImgArr = new Image[diceObjArr.Length];
        diceRectArr = new RectTransform[diceObjArr.Length];

        for (int i = 0; i < playerTextObjArr.Length; i++)
        {
            playerTextArr[i] = playerTextObjArr[i].GetComponent<Text>();
            playerTextsArr[i] = playerTextObjArr[i].GetComponent<PlayerTexts>();
            playerTextsArr[i].SetIInspectingUIText(this);
        }

        for (int i = 0; i < diceObjArr.Length; i++)
		{
            diceImgArr[i] = diceObjArr[i].GetComponent<Image>();
            diceRectArr[i] = diceObjArr[i].GetComponent<RectTransform>();
		}

        iInspectingPanelControl = uiControl.GetComponent<IInspectingPanelControl>();

    }

	private void OnEnable()
    {
        InitPoliceText();
        InitPlayerText();
        InitDice();

        switch (Random.Range(0,3))
		{
            case 0:
                SetPoliceText(0);
                break;
            case 1:
                SetPoliceText(1);
                break;
            case 2:
                SetPoliceText(2); 
                break;
		}

        SetPlayerText(0, 3, 1000);  // �������ΰ���?
        SetPlayerText(1, 4, 1001);  // �����Ѵ�.
    }
    // �ֻ��� �ʱ�ȭ
    private void InitDice()
    {
        for (int i = 0; i < diceObjArr.Length; i++)
        {
            diceImgArr[i].sprite = diceSprArr[0];
        }
        diceSuccessText.text = "";
    }

    // �÷��̾� ������â ������
    private void InitPlayerText()
	{
        for (int i = 0; i < playerTextArr.Length; i++)
		{
            playerTextArr[i].gameObject.SetActive(false);
		}
	}

    private void SetPlayerText(int n1, int n2, int n3)
	{
        playerTextArr[n1].gameObject.SetActive(true);
        playerTextArr[n1].text = inspectingPoliceTextStart[n2];
        playerTextsArr[n1].TextNum = n3;
	}

    private void SetPoliceText(int n1)
	{
        policeText.text = inspectingPoliceTextStart[n1];
	}
    // ���� ��ȭâ ������
    private void InitPoliceText()
	{
        policeText.text = "";
	}

    public void ChoiceText(int num)
	{
        if (isDiceRoll) { return; }
        InitPlayerText();
        InitDice();

        switch (num)
		{
            case 1000:  // ���� ���ΰ���? ~
                int r = Random.Range(0, 3);
                if (r == 0) { SetPoliceText(5); }
                else if (r == 1) { SetPoliceText(6); }
                else if (r == 2) { SetPoliceText(7); }

                SetPlayerText(0, 8, 1002);  // �� �� ���캸���� ~
                SetPlayerText(1, 9, 1003);  // ���� �̻��� �Ŷ� ~
                SetPlayerText(2, 10, 1004);  // ��.. �׸� ������ ~
                SetPlayerText(3, 4, 1001);  // �����Ѵ�.
                break;
            case 1001:  // �����Ѵ�.
                SetPoliceText(20);

                SetPlayerText(0, 19, 1005);
                break;
            case 1002:  // �� �� ���캸���� ~, (�˹��� �޴´�.)
                // �ϴ� ���ξ��� ���ڰ� ���ٰ� ����
                SetPoliceText(12);

                SetPlayerText(0, 21, 1006);
                break;
            case 1003:  // ���� �̻��� �Ŷ� ~        
                // ���� ���ο� ���� ������ ���� �޶���
                isDiceRoll = true;
                diceCoroutine = StartCoroutine(DIceRoll(num));
                break;
            case 1004:  // ��.. �׸� ������ ~
                // ���� �����ϳĿ� ���ϳĿ� ���� ������ ���� �޶���.
                int r1 = Random.Range(0, 100);
                if (r1 < 5) 
                {
                    SetPoliceText(16);

                    SetPlayerText(0, 17, 1004);
                    SetPlayerText(1, 18, 1005);
                }
                else
				{
                    SetPoliceText(15);

                    SetPlayerText(0, 21, 1006);
				}
                break;
            case 1005:  // ��������.
                InitPoliceText();
                iInspectingPanelControl.ControlInspectUI(false, null);
                // ���� ����

                break;
            case 1006:  // (����.)
                InitPoliceText();
                iInspectingPanelControl.ControlInspectUI(false, null); 
                break;
		}
	}

    IEnumerator DIceRoll(int num)
    {
        var time = new WaitForSeconds(0.05f);
        int rand = 0;
        int dice1 = 0;
        int dice2 = 0;
        isDiceRoll = true;


        while (true)
        {
            if (num == 1003)
            {
                Vector3[] originVec = new Vector3[diceRectArr.Length];

                for (int j = 0; j < diceRectArr.Length; j++)
                {
                    originVec[j] = diceRectArr[j].anchoredPosition;
                }

                for (int i = 0; i < 20; i++)
                {
                    // �ֻ��� ����� �����ش�.
                    dice1 = Random.Range(1, 7);
                    dice2 = Random.Range(1, 7);

                    diceImgArr[0].sprite = diceSprArr[dice1 - 1];
                    diceImgArr[1].sprite = diceSprArr[dice2 - 1];

                    for (int j = 0; j < diceRectArr.Length; j++)
					{
                        diceRectArr[j].anchoredPosition = originVec[j] + Vector3.Normalize(new Vector3(Random.Range(0, 100), 0, Random.Range(0, 100))) * 10f;
					}
                    
                    rand = dice1 + dice2;

                    yield return time;
                }

                for (int j = 0; j < diceRectArr.Length; j++)
				{
                    diceRectArr[j].anchoredPosition = originVec[j];
				}

                // ���� ���� ����(�ҽɰ˹� ����)
                if (rand >= 7)
                {
                    diceSuccessText.text = "���� ���� !";
                    SetPoliceText(13);

                    SetPlayerText(0, 21, 1006);
                }
                else
                {
                    diceSuccessText.text = "���� ����..";
                    SetPoliceText(14);

                    SetPlayerText(0, 22, 1002);
                }

                isDiceRoll = false;

                break;
            }
        }

        if (diceCoroutine != null)
        {
            StopCoroutine(diceCoroutine);
        }
    }
}
