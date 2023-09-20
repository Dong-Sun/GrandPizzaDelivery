using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeStat : MonoBehaviour
{
    public int Career { get; set; } = -1;// ���. ������ �ϼ����� ������ Ȯ���� �ٿ��ش�.
    public int Handy { get; set; } = 20; // �⺻ ������
    public int Creativity { get; set; } = -1;// â�Ƿ�. ������ �ϼ����� ������ Ȯ���� �����ش�.
    public int Agility { get; set; } = -1; // ���߷�. ������ �ϼ� �ӵ��� �����ش�.
    public int Pay { get; set; } // �ֱ�.

    // ���� �ܰ躰 ��ġ(���, â�Ƿ�)
    public int bad { get; } = -1;
    public int normal { get; } = 1;
    public int good { get; } = 3;
    public int perfect { get; } = 6;

    public string[] CreativityStat { get; } = new string[4] { "����", "����", "����", "õ��" };
    public string[] CareerStat { get; } = new string[4] { "����", "�����", "���׶�", "����" };
    public string[] AgilityStat { get; } = new string[4] { "����", "����", "���� ����", "����" };

    private void Awake()
    {
        Pay = Handy - Agility + Creativity + Career + Random.Range(-10, 11);
    }

    private void Update()
    {
        Debug.Log(Handy);
    }
}