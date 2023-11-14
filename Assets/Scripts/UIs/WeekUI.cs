using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ѽ�ȣ �ۼ�
public class WeekUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text text;
    public void Awake()
    {
        switch(Constant.NowDay)
        {
            case DayNS.DayEnum.MONDAY:
                text.text = "������";
                break;
            case DayNS.DayEnum.TUESDAY:
                text.text = "ȭ����";
                break;
            case DayNS.DayEnum.WENDESDAY:
                text.text = "������";
                break;
            case DayNS.DayEnum.THURSDAY:
                text.text = "�����";
                break;
            case DayNS.DayEnum.FRIDAY:
                text.text = "�ݿ���";
                break;
            case DayNS.DayEnum.SATURDAY:
                text.text = "�����";
                break;
            case DayNS.DayEnum.SUNDAY:
                text.text = "�Ͽ���";
                break;
        }
    }
}
