using ClerkNS;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CreateEmployee : MonoBehaviour
{
    [SerializeField] Transform EmployeeMother;
    //[SerializeField] GameObject EmployeePrefab;
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
        EmployeeRecruit employeeStat = EmployeeRecruitMother.GetComponent<EmployeeRecruit>();

        ClerkC clerk = 
            new ClerkC(employeeStat.Handy[SValue], (Tier)employeeStat.Agility[SValue], (Tier)employeeStat.Career[SValue], (Tier)employeeStat.Creativity[SValue], 
             0, employeeStat.Pay[SValue], employeeStat.Name[SValue]);

        Constant.ClerkList.Add(clerk);
    }
}