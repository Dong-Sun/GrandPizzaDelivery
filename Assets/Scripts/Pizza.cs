using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BuildingAddressNS;
using PizzaNS;

public struct Pizza
{
    public string Name;//�����̸�
    public int Perfection;//�ϼ���
    public int ProductionCost;//������
    public int SellCost;//�Ǹź��
    public int Charisma;//�ŷµ�
    public List<Ingredient> Ingreds;
    public int TotalDeclineAt;

    public Pizza(string name, int perfection, int productionCost, int sellCost, int charisma, List<Ingredient> Ingreds, int TotalDeclineAt)
    {
        Name = name;
        Perfection = perfection;
        ProductionCost = productionCost;
        SellCost = sellCost;
        Charisma = charisma;
        this.Ingreds = new List<Ingredient>();
        for (int i = 0; i < Ingreds.Count; i++)
        {
            this.Ingreds.Add(Ingreds[i]);
        }
        this.TotalDeclineAt = TotalDeclineAt;
    }
    public string GetName()
    {
        return Name;
    }
    public int GetPerfection()
    {
        return Perfection;
    }
    public int GetProductionCost()
    {
        return ProductionCost;
    }
    public int GetSellCost()
    {
        return SellCost;
    }
    public int GetCharisma()
    {
        return Charisma;
    }
    public List<Ingredient> GetIngreds()
    {
        return Ingreds;
    }
    public int GetTotalDeclineAt()
    {
        return TotalDeclineAt;
    }
}

public class Request//�����ֹ�
{
    public Pizza Pizza;
    public bool Accept;
    public AddressS AddressS;
    public Request(Pizza pizza, bool accept)
    {
        Pizza = pizza;
        Accept = accept;
    }
}
namespace Inventory
{

    //�κ��丮 ����
    public class Slot
    {
        public GameObject InventorySlot;
        public Pizza? Pizza = null;
        bool ButtonActivity = false;

        public Slot(GameObject inventorySlot)
        {
            InventorySlot = inventorySlot;
        }

        /*public void TextUpdate()
        {
            if(Pizza != null)
                InventorySlot.transform.GetChild(0).GetComponent<Text>().text = Pizza?.GetName();
        }*/
    }
}
