using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingAddressNS;

public struct Pizza
{
    public string Name;//�����̸�
    public int Perfection;//�ϼ���
    public int ProductionCost;//������
    public int SellCost;//�Ǹź��
    public int Charisma;//�ŷµ�

    public Pizza(string name, int perfection, int productionCost, int sellCost, int charisma)
    {
        this.Name = name;
        this.Perfection = perfection;
        this.ProductionCost = productionCost;
        this.SellCost = sellCost;
        this.Charisma = charisma;
    }
}

public class Request
{
    public Pizza Pizza;
    public bool Accept;
    public AddressS AddressS;
    public Request(Pizza pizza, bool accept)
    {
        this.Pizza = pizza;
        this.Accept = accept;
    }
}
