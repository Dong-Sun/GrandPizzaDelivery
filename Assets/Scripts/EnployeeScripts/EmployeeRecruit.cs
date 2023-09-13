using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeRecruit : MonoBehaviour
{
    [SerializeField] GameObject RecruitWin;

    int limitCount = 3;

    [SerializeField] string[] Stat = new string[5];

    private int[] handy = new int[3];
    private int[] career = new int[3];
    private int[] creativity = new int[3];
    private int[] agility = new int[3];
    private int[] pay = new int[3];

    public bool IsMondayMorning = false;

    private void Start()
    {
        RecruitWin.SetActive(false);
    }

    private void Update()
    {
        ShowApplicant();

        IsMondayMorning = false;
    }

    public void ShowRecruitWin()
    {
        bool value = true;

        if(RecruitWin.activeInHierarchy)
        {
            value = false;
        }
        else
        {
            value = true;
        }

        if (value == true)
        {
            RecruitWin.SetActive(true);
        }
        else
        {
            RecruitWin.SetActive(false);
        }
    }

    void ShowApplicant()
    {
        if (IsMondayMorning == true)
        {
            for (int i = 0; i < limitCount; i++)
            {
                for (int j = 0; j < Stat.Length; j++)
                {
                    RecruitWin.transform.GetChild(i).GetChild(j).GetComponent<Text>().text
                        = Stat[j] + State(i, j, RecruitWin.transform.GetChild(i).gameObject);
                }

                RecruitWin.transform.GetChild(i).GetChild(Stat.Length).GetComponent<Button>().interactable
                    = true;
            }

            IsMondayMorning = false;
        }
    }

    string State(int index, int statValue, GameObject employee)
    {
        string result = "";

        switch (statValue)
        {
            case 0:
                handy[index] = Random.Range(20, 81);

                result = handy[index].ToString();
                break;
            case 1:
                agility[index] = RandomStat(employee);

                result = agility[index].ToString();
                break;
            case 2:
                career[index] = RandomStat(employee);

                switch (career[index])
                {
                    case -1:
                        result = employee.GetComponent<EmployeeStat>().careerStat[0].ToString();
                        break;
                    case 1:
                        result = employee.GetComponent<EmployeeStat>().careerStat[1].ToString();
                        break;
                    case 3:
                        result = employee.GetComponent<EmployeeStat>().careerStat[2].ToString();
                        break;
                    case 6:
                        result = employee.GetComponent<EmployeeStat>().careerStat[3].ToString();
                        break;
                }
                break;
            case 3:
                creativity[index] = RandomStat(employee);

                switch (creativity[index])
                {
                    case -1:
                        result = employee.GetComponent<EmployeeStat>().creativityStat[0].ToString();
                        break;
                    case 1:
                        result = employee.GetComponent<EmployeeStat>().creativityStat[1].ToString();
                        break;
                    case 3:
                        result = employee.GetComponent<EmployeeStat>().creativityStat[2].ToString();
                        break;
                    case 6:
                        result = employee.GetComponent<EmployeeStat>().creativityStat[3].ToString();
                        break;
                }
                break;
            case 4:
                pay[index] = handy[index] + agility[index] + creativity[index] + career[index] + Random.Range(-10, 11);

                result = pay[index].ToString();
                break;
        }

        return result;
    }

    int RandomStat(GameObject employee)
    {
        int RanCount = Random.Range(0, 4);

        switch (RanCount)
        {
            case 0:
                RanCount = employee.GetComponent<EmployeeStat>().bad;
                break;
            case 1:
                RanCount = employee.GetComponent<EmployeeStat>().normal;
                break;
            case 2:
                RanCount = employee.GetComponent<EmployeeStat>().good;
                break;
            case 3:
                RanCount = employee.GetComponent<EmployeeStat>().perfect;
                break;
        }

        return RanCount;
    }

    public void test()
    {
        IsMondayMorning = true;
    }// ���߿� �ð� �����Ǹ� ��¥ �ٲ𶧸��� �����ǰ� �ٲٱ�~
}