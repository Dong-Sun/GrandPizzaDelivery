using ClerkNS;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CreateEmployee : MonoBehaviour
{
    [SerializeField] Transform EmployeeMother;
    [SerializeField] Transform EmployeeRecruitMother;

    [SerializeField] GameObject NoticeWin;

    // ������Ʈ �����ϴ� ��� ���
    public void SpawnEmployee(int value)
    {
        int employeeCount = 0;

        employeeCount = Constant.ClerkList.Count;

        if(employeeCount < 29 )
        {
            GetStat(value);
        }
        else // �ο� �ʰ� �� ���â ����
        {
            NoticeMessage("��� ������ �ο��� �ʰ��߽��ϴ�.");
        }
    }

    void NoticeMessage(string Message)
    {
        NoticeWin.SetActive(true);

        NoticeWin.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = Message;
    }

    public void GetStat(int SValue)
    {
        Constant.ClerkList.Add(EmployeeRecruit.RecruitClerk[SValue]);

        EmployeeRecruit.IsRecruited[SValue] = false;
    }
}