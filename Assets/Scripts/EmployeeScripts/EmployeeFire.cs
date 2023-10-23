using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeFire : MonoBehaviour
{
    [SerializeField] Transform FireWinParent;
    [SerializeField] Transform FireWinBG;
    [SerializeField] Transform EmployeeParent;

    private void Start()
    {
        WinOff();
    }

    void WinOff()
    {
        FireWinBG.gameObject.SetActive(false);

        for (int i = 0; i < FireWinParent.childCount; i++)
        {
            FireWinParent.GetChild(i).gameObject.SetActive(false);
        }
    }

    [SerializeField] bool isApear = false;

    public void ApearCheck()
    {
        if (FireWinBG.gameObject.activeInHierarchy)
        {
            isApear = false;
        }
        else
        {
            isApear = true;

            FireWinBG.gameObject.SetActive(true);

            for (int i = 0; i < EmployeeParent.childCount; i++)
            {
                FireWinParent.GetChild(i * 2).gameObject.SetActive(true);
                FireWinParent.GetChild(i * 2).GetComponent<Button>().interactable = true;
            }
        }

        ShowFireWin();
    }

    void ShowFireWin()
    {
        string EmployeeStat = null;

        EmployeeStat Employee = null;

        if (isApear == true)
        {
            for (int i = 0; i < EmployeeParent.childCount; i++)
            {
                Employee = EmployeeParent.GetChild(i).GetComponent<EmployeeStat>();

                EmployeeStat = "���� : " + Employee.Handy.ToString() + "\n" + "�޿� : " + Employee.Pay.ToString();

                FireWinParent.GetChild(i * 2).GetChild(0).
                  GetComponent<Text>().text = EmployeeStat;
            }
        }
        else
        {
            FireWinBG.gameObject.SetActive(false);

            for (int i = 0; i < FireWinParent.childCount / 2; i++)
            {
                FireWinParent.GetChild(i).gameObject.SetActive(false);
                FireWinParent.GetChild(i + 1).gameObject.SetActive(false);

                FireWinParent.GetChild(i + 1).GetChild(1).
                   GetComponent<Button>().interactable = true;

                FireWinParent.GetChild(i + 1).GetChild(1).GetChild(0).
                    GetComponent<Text>().text = "�ذ��ϱ�";
            }
        }
    }

    public void ShowDetail(int value)
    {
        FireWinParent.GetChild(value + 1).gameObject.SetActive(true);

        FireWinParent.GetChild(value).GetComponent<Button>().interactable = false; 

        FindEmployeeData(1);

        pay[value] = 0;
    }

    void FindEmployeeData(int value)
    {
        string EmployeeStat = null;

        for (int i = 0; i < EmployeeParent.childCount; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                EmployeeStat += Stat(i, j) + "\n";
            }

            FireWinParent.GetChild(i * 2 + value).GetChild(0).
                   GetComponent<Text>().text = EmployeeStat;
        }
    }

    string Stat(int Evalue, int Svalue)
    {
        string result = null;

        switch (Svalue)
        {
            case 0:
                result = "������ : " + EmployeeParent.GetChild(Evalue).
                    GetComponent<EmployeeStat>().Handy.ToString();
                break;
            case 1:
                switch (EmployeeParent.GetChild(Evalue).GetComponent<EmployeeStat>().Agility)
                {
                    case ClerkNS.Tier.ONE:
                        result = "���߷� : " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().AgilityStat[0].ToString();
                        break;
                    case ClerkNS.Tier.TWO:
                        result = "���߷� : " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().AgilityStat[1].ToString();
                        break;
                    case ClerkNS.Tier.THREE:
                        result = "���߷� : " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().AgilityStat[2].ToString();
                        break;
                    case ClerkNS.Tier.FOUR:
                        result = "���߷� : " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().AgilityStat[3].ToString();
                        break;
                }
                break;
            case 2:
                switch (EmployeeParent.GetChild(Evalue).GetComponent<EmployeeStat>().Career)
                {
                    case ClerkNS.Tier.ONE:
                        result = "��� : " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().CareerStat[0].ToString();
                        break;
                    case ClerkNS.Tier.TWO:
                        result = "��� : " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().CareerStat[1].ToString();
                        break;
                    case ClerkNS.Tier.THREE:
                        result = "��� : " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().CareerStat[2].ToString();
                        break;
                    case ClerkNS.Tier.FOUR:
                        result = "��� : " + EmployeeParent.GetChild(Evalue).
                 GetComponent<EmployeeStat>().CareerStat[3].ToString();
                        break;
                }
                break;
            case 3:
                switch (EmployeeParent.GetChild(Evalue).GetComponent<EmployeeStat>().Creativity)
                {
                    case ClerkNS.Tier.ONE:
                        result = "â�Ƿ� : " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().CreativityStat[0].ToString();
                        break;
                    case ClerkNS.Tier.TWO:
                        result = "â�Ƿ� : " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().CreativityStat[1].ToString();
                        break;
                    case ClerkNS.Tier.THREE:
                        result = "â�Ƿ� : " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().CreativityStat[2].ToString();
                        break;
                    case ClerkNS.Tier.FOUR:
                        result = "â�Ƿ� : " + EmployeeParent.GetChild(Evalue).
                 GetComponent<EmployeeStat>().CreativityStat[3].ToString();
                        break;
                }
                break;
            case 4:
                result = "��Ʈ���� : " + EmployeeParent.GetChild(Evalue).
                   GetComponent<EmployeeStat>().Stress.ToString();
                break;
            case 5:
                result = "�ֱ� :     " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().Pay.ToString();
                break;
        }

        return result;
    }

    public void FireButtonOn(int value)
    {
        if (EmployeeParent.childCount > 1)
        {
            EmployeeParent.GetComponent<PizzaQuality>().Employees.Remove
                (EmployeeParent.GetChild(value).gameObject);

            Destroy(EmployeeParent.GetChild(value).gameObject);

            FireWinParent.GetChild(value).GetChild(5).
                GetComponent<Button>().interactable = false;

            FireWinParent.GetChild(value).GetChild(5).GetChild(0).
                GetComponent<Text>().text = "�ذ�Ϸ�";
        }
        else
        {
            NoticeMessage("������ �ּ� �Ѹ� �̻��� �ʿ��մϴ�.");
        }
    }

    int[] pay = new int[5];

    public void PayRateButton(int value)// â�� ���� pay�� ���� �� Ȯ�� ��ư ������ ���� ������ �ʱ�ȭ
    {
        string EmployeeStat = null;

        if (value > 0)
        {
            pay[value - 1]++;

            for (int j = 0; j < 5; j++)
            {
                EmployeeStat += Stat(value - 1, j) + "\n";
            }

            EmployeeStat += "�ֱ� :     " + (EmployeeParent.GetChild(value - 1).GetComponent<EmployeeStat>().Pay + pay[value - 1]).ToString() + "\n";

            FireWinParent.GetChild((value - 1) * 2 + 1).GetChild(0).
                   GetComponent<Text>().text = EmployeeStat;
        }
        else if(value < 0)
        {
            pay[(value + 1) * -1]--;

            for (int j = 0; j < 5; j++)
            {
                EmployeeStat += Stat((value + 1) * -1, j) + "\n";
            }

            EmployeeStat += "�ֱ� :     " + (EmployeeParent.GetChild((value + 1) * -1).GetComponent<EmployeeStat>().Pay + pay[(value + 1) * -1]).ToString() + "\n";

            FireWinParent.GetChild(((value + 1) * -1) * 2 + 1).GetChild(0).
                   GetComponent<Text>().text = EmployeeStat;
        }
    }

    public void FireWinHeightCon(bool value)
    {
        RectTransform rect = FireWinParent.GetComponent<RectTransform>();

        if (value)
        {
            rect.sizeDelta = new Vector3(rect.sizeDelta.x, rect.sizeDelta.y + 150);
        }
        else
        {
            rect.sizeDelta = new Vector3(rect.sizeDelta.x, rect.sizeDelta.y - 150);
        }
    }

    public void SavePayRate(int value)
    {
        EmployeeParent.GetChild(value).GetComponent<EmployeeStat>().Pay += pay[value];

        pay[value] = 0;

        ShowFireWin();
    }

    [SerializeField] GameObject NoticeWin;

    void NoticeMessage(string Message)
    {
        NoticeWin.SetActive(true);

        NoticeWin.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = Message;
    }
}