using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeStat : MonoBehaviour
{
    public int career { get; set; }// ���. ������ �ϼ����� ������ Ȯ���� �ٿ��ش�.
    public int handy { get; set; } // �⺻ ������
    public int creativity { get; set; } // â�Ƿ�. ������ �ϼ����� ������ Ȯ���� �����ش�.
    public int agility { get; set; } // ���߷�. ������ �ϼ� �ӵ��� �����ش�.
    public int pay { get; set; } // �ֱ�.

    // ���� ��ġ(���, â�Ƿ�)
    public int bad { get; } = -1;
    public int normal { get; } = 1;
    public int good { get; } = 3;
    public int perfect { get; } = 6;

    public string[] creativityStat { get; } = new string[4] { "����", "����", "����", "õ��" };
    public string[] careerStat { get; } = new string[4] { "����", "�����", "���׶�", "����" };

    private void Start()
    {
        handy = 20;
        agility = -1;
        creativity = -1;
        career = -1;
        pay = handy + agility + creativity + career + Random.Range(-10, 11);

        Debug.Log(handy);
        Debug.Log(agility);
        Debug.Log(creativity);
        Debug.Log(career);
        Debug.Log(pay);
    }
}