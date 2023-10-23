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
    [SerializeField] GameObject EmployeePrefab;
    [SerializeField] Transform EmployeeRecruitMother;

    [SerializeField] GameObject NoticeWin;

    public void SpawnEmployee(int value)
    {
        int employeeCount = 0;

        employeeCount = EmployeeMother.transform.childCount;

        if(employeeCount < 5 )
        {
            GameObject employee;

            employee = Instantiate(EmployeePrefab);
            employee.transform.SetParent(EmployeeMother.transform);

            EmployeeMother.GetComponent<PizzaQuality>().
                Employees.Add(employee);

            GetStat(value, employeeCount);
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

    public void GetStat(int SValue, int Gvalue)
    {
        EmployeeMother.GetChild(Gvalue).GetComponent<EmployeeStat>().Handy 
            = EmployeeRecruitMother.GetComponent<EmployeeRecruit>().Handy[SValue];
        EmployeeMother.GetChild(Gvalue).GetComponent<EmployeeStat>().Agility
            = (Tier)EmployeeRecruitMother.GetComponent<EmployeeRecruit>().Agility[SValue];
        EmployeeMother.GetChild(Gvalue).GetComponent<EmployeeStat>().Career
            = (Tier)EmployeeRecruitMother.GetComponent<EmployeeRecruit>().Career[SValue];
        EmployeeMother.GetChild(Gvalue).GetComponent<EmployeeStat>().Creativity
            = (Tier)EmployeeRecruitMother.GetComponent<EmployeeRecruit>().Creativity[SValue];
        EmployeeMother.GetChild(Gvalue).GetComponent<EmployeeStat>().Pay
            = EmployeeRecruitMother.GetComponent<EmployeeRecruit>().Pay[SValue];
    }

    private void Awake()
    {
        Constant.ClerkList.Clear();

        SaveEmployeeData(0, false);
    }

    public void SaveEmployeeData(int Employeeindex, bool IsFire)
    {
        EmployeeStat employeeStat = EmployeeMother.GetChild(Employeeindex).GetComponent<EmployeeStat>();

        ClerkC clerk = new ClerkC(employeeStat.Handy, employeeStat.Agility, employeeStat.Creativity, employeeStat.Career, employeeStat.Stress, employeeStat.Pay);

        Constant.ClerkList.Add(clerk);
    }
}