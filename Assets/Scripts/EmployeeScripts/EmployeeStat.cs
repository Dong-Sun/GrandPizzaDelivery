using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClerkNS;

//���� ���� ����~
public enum Day
{
    Monday = 0,
    Tuesday = 1,
    Wednesday = 2, 
    Thursday = 3,
    Friday = 4,
    Saturday = 5,
    Sunday = 6
}

public class EmployeeStat : MonoBehaviour
{
    public Tier Career { get; set; } = Tier.ONE;// ���. ������ �ϼ����� ������ Ȯ���� �ٿ��ش�.
    public int Handy { get; set; } = 20; // �⺻ ������
    public Tier Creativity { get; set; } = Tier.ONE;// â�Ƿ�. ������ �ϼ����� ������ Ȯ���� �����ش�.
    public Tier Agility { get; set; } = Tier.ONE; // ���߷�. ������ �ϼ� �ӵ��� �����ش�.
    public int Pay { get; set; } // �ֱ�.
    public int Stress { get; set; } = 0;// ��Ʈ���� ����

    public string[] CreativityStat { get; } = new string[4] { "����", "����", "����", "õ��" };
    public string[] CareerStat { get; } = new string[4] { "����", "�����", "���׶�", "����" };
    public string[] AgilityStat { get; } = new string[4] { "����", "����", "���� ����", "����" };

    public string[] RanName { get; } = new string[40] { "����", "����", "�ø����", "���", "����", "�̻级��", "���Ǿ�", "�̾�", "�轼", "���̵�", "��ī��", "�ƸḮ��", 
        "���ڹ�", "����", "����", "������Ʈ", "�ƺ����", "���ӽ�", "����", "�", "�˷����", "������", "�繫��", "�ٴϿ�", "Į����", "�׷��̽�", "�̼�", "��Į��", 
        "������", "����", "�̻级��", "��", "���̽�", "����", "���̿�", "��", "����", "����", "�Ʒ�", "�ƺ�"};

    private void Awake()
    {
        Pay = (Handy - (int)Agility + (int)Creativity + (int)Career) * 100 + Random.Range(-1000, 1001);
    }
}