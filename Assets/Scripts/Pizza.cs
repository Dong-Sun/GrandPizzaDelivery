using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Pizza
{
    public string Name;//�����̸�
    public int Perfection;//�ϼ���
    public int Production_Cost;//������
    public int Sell_Cost;//�Ǹź��

    public Pizza(string name, int perfection, int production_Cost, int Sell_Cost)
    {
        this.Name = name;
        this.Perfection = perfection;
        this.Production_Cost = production_Cost;
        this.Sell_Cost = Sell_Cost;
    }
}
