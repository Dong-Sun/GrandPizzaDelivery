using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �Ѽ�ȣ �ۼ�
public class EndingSetting : MonoBehaviour
{
    [SerializeField] private Sprite[] spriteImg;
    [SerializeField] private Text endingTypeText;
    [SerializeField] private Text endingExplainText;
    [SerializeField] private Image endingImg;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (GameManager.Instance.Money - Constant.Dept >= 10000000)
		{
            endingImg.sprite = spriteImg[2];
            endingTypeText.text = "(�� ����) - 1�� �̻� ����";
            endingExplainText.text = "���� ���Է� ū ������ �ø��� ���ڰ� �Ǿ��� !";
		}
        else if (GameManager.Instance.Money >= Constant.Dept)
		{
            endingImg.sprite = spriteImg[0];
            endingTypeText.text = "(��� ����) - �ƹ�ư ����";
            endingExplainText.text = "����� ���� û���ϰ� ������ �Ǿ��� !";
		}
        else
		{
            endingImg.sprite = spriteImg[1];
            endingTypeText.text = "(��� ����) - ����";
            endingExplainText.text = "�ᱹ ���� ���� ���ϰ� ���� ���а� ��������...";
		}

    }

    public void EndingNext()
	{
        GameManager.Instance.time = 32400;
        LoadScene.Instance.ActiveTrueFade("InGameScene");
	}
}
