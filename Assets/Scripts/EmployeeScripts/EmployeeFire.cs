using System.Collections;
using System.Collections.Generic;
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

    public void ShowFireWin()
    {
        bool value = true;

        if (FireWinBG.gameObject.activeInHierarchy)
        {
            value = false;
        }
        else
        {
            value = true;
        }

        if (value == true)
        {
            FireWinBG.gameObject.SetActive(true);

            FindEmployeeData();
        }
        else
        {
            FireWinBG.gameObject.SetActive(false);

            for (int i = 0; i < FireWinParent.childCount; i++)
            {
                FireWinParent.GetChild(i).gameObject.SetActive(false);

                FireWinParent.GetChild(i).GetChild(1).
                   GetComponent<Button>().interactable = true;

                FireWinParent.GetChild(i).GetChild(1).GetChild(0).
                    GetComponent<Text>().text = "�ذ��ϱ�";
            }
        }
    }

    void FindEmployeeData()
    {
        string EmployeeStat = null;

        for (int i = 0; i < EmployeeParent.childCount; i++)
        {
            FireWinParent.GetChild(i).gameObject.SetActive(true);

            for (int j = 0; j < 6; j++)
            {
                EmployeeStat += Stat(i, j) + "\n";
            }

            FireWinParent.GetChild(i).GetChild(0).
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
                    case -1:
                        result = "���߷� : " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().AgilityStat[0].ToString();
                        break;
                    case 1:
                        result = "���߷� : " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().AgilityStat[1].ToString();
                        break;
                    case 3:
                        result = "���߷� : " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().AgilityStat[2].ToString();
                        break;
                    case 6:
                        result = "���߷� : " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().AgilityStat[3].ToString();
                        break;
                }
                break;
            case 2:
                switch (EmployeeParent.GetChild(Evalue).GetComponent<EmployeeStat>().Career)
                {
                    case -1:
                        result = "��� : " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().CareerStat[0].ToString();
                        break;
                    case 1:
                        result = "��� : " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().CareerStat[1].ToString();
                        break;
                    case 3:
                        result = "��� : " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().CareerStat[2].ToString();
                        break;
                    case 6:
                        result = "��� : " + EmployeeParent.GetChild(Evalue).
                 GetComponent<EmployeeStat>().CareerStat[3].ToString();
                        break;
                }
                break;
            case 3:
                switch (EmployeeParent.GetChild(Evalue).GetComponent<EmployeeStat>().Creativity)
                {
                    case -1:
                        result = "â�Ƿ� : " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().CreativityStat[0].ToString();
                        break;
                    case 1:
                        result = "â�Ƿ� : " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().CreativityStat[1].ToString();
                        break;
                    case 3:
                        result = "â�Ƿ� : " + EmployeeParent.GetChild(Evalue).
                  GetComponent<EmployeeStat>().CreativityStat[2].ToString();
                        break;
                    case 6:
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
            Debug.Log("����� ������");
        }
    }

    public void PayRateButton(bool value)
    {
        int pay = 

        if (value)
        {

        }
        else
        {

        }
    }
}