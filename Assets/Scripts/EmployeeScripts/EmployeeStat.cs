using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClerkNS;

// ���� �ܰ躰 ��ġ(���, â�Ƿ�)
enum StatRate
{
    Bad = -1,
    Normal = 1,
    Good = 3,
    Perfect = 6
}

public class EmployeeStat : MonoBehaviour
{
    public int Career { get; set; } = -1;// ���. ������ �ϼ����� ������ Ȯ���� �ٿ��ش�.
    public int Handy { get; set; } = 20; // �⺻ ������
    public int Creativity { get; set; } = -1;// â�Ƿ�. ������ �ϼ����� ������ Ȯ���� �����ش�.
    public int Agility { get; set; } = -1; // ���߷�. ������ �ϼ� �ӵ��� �����ش�.
    public int Pay { get; set; } // �ֱ�.
    public int Stress { get; set; } = 0;// ��Ʈ���� ����

    public string[] CreativityStat { get; } = new string[4] { "����", "����", "����", "õ��" };
    public string[] CareerStat { get; } = new string[4] { "����", "�����", "���׶�", "����" };
    public string[] AgilityStat { get; } = new string[4] { "����", "����", "���� ����", "����" };

    private void Awake()
    {
        Pay = Handy - Agility + Creativity + Career + Random.Range(-10, 11);
    }
}