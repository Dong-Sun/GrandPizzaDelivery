using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PizzaNS;
using ClerkNS;
using StoreNS;

// �Ѽ�ȣ �ۼ�
public static class Constant
{
	/// <summary>
	/// ������ ��� ��ȣ ����Ʈ. �ߺ��Ǵ� ��ȣ�� �ִ�.
	/// </summary>
	public static List<int> ChoiceIngredientList = new List<int>();
	/// <summary>
	/// ������ �ŷµ�. ������� 100% ��Ȯ�� ������ ���� �ŷµ��̴�. ��Ȯ�� ���̸� �ŷµ��� ����.
	/// </summary>
	public static int PizzaAttractiveness;
	public static int Perfection;
	public static int ProductionCost;
	public static int SellCost;
	public static int TotalDeclineAt;
	public static List<Ingredient> ingreds = new List<Ingredient>();
	/// <summary>
	/// ���� ��ᰪ. [,0]�� ����ȣ, [,1]�� �ŷµ�, [,2]�� �ŷ��϶���, [,3]�� ��ᰪ. [0,]�� ��������.
	/// </summary>
	public static string[,] IngredientsArray = new string[13, 4]
	{
		{"0","-1","-1","-1" },	// ����
		{"1","25","3","150" },	// �丶��
		{"2","30","2","160" },	// ġ��
		{"3","15","2","80" },	// ����
		{"4","20","1","120" },	// ����
		{"5","45","7","500" },	// ������
		{"6","27","3","140" },	// ������
		{"7","40","5","320" },	// �Ҷ��Ǵ�
		{"8","65","12","960" },	// �߰��
		{"9","78","20","1350" },    // �Ұ��
		{"10","32","4","150" }, // ���
		{"11","27","2","200" }, // ���
		{"12","17","1","100" }	// ����
	};
	/// <summary>
	/// ��� ������ ���� ����� ��ȣ��
	/// </summary>
	public static List<int> UsableIngredient = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
	/// <summary>
	/// ������ ���� ����Ʈ
	/// </summary>
	public static List<Pizza> DevelopPizza = new List<Pizza>();
	
	public static bool IsMakePizza = false;
	/// <summary>
	/// ������ ���ξ��� ����
	/// </summary>
	public static int PineappleCount = 0;
	/// <summary>
	/// ���ξ��� ����
	/// </summary>
	public static Pizza PineapplePizza = new Pizza("PineapplePizza", 100, 0, 2000000, 99999, new List<Ingredient>() { Ingredient.TOMATO, Ingredient.CHEESE, Ingredient.PINEAPPLE }, 0);
	/// <summary>
	/// 0.02�ʰ��� ��
	/// </summary>
	public static WaitForSeconds OneTime = new WaitForSeconds(0.02f);
	/// <summary>
	/// ����� ���� ����Ʈ
	/// </summary>
	public static List<ClerkC> ClerkList = new List<ClerkC>();
	/// <summary>
	/// �÷��̾ ������ ������. ItemS�� ItemType���� �����۵��� �з��� �� �ִ�. int�� ���� ����
	/// </summary>
	public static Dictionary<ItemS, int> PlayerItemDIc = new Dictionary<ItemS, int>();
	/// <summary>
	/// ���� Ÿ���� �������� ���� ��ųʸ����� ã�Ƽ� Dictionary<ItemS, int> ������ �����Ѵ�.
	/// </summary>
	/// <param name="dic"></param>
	/// <param name="type"></param>
	/// <returns></returns>
	public static Dictionary<ItemS, int> FindAllItemS(this Dictionary<ItemS, int> dic, ItemType type)
	{
		Dictionary<ItemS, int> dictionary = new Dictionary<ItemS, int>();
		foreach (var key in dic.Keys)
		{
			if (key.Type == type)
			{
				dictionary.Add(key, dic[key]);
			}
		}

		return dictionary;
	}
	/// <summary>
	/// �ε����� ���� ���θ� Ȯ���ϴ� Ȯ��޼���
	/// </summary>
	/// <param name="dic"></param>
	/// <param name="index"></param>
	/// <returns></returns>
	public static bool CheckIndexDic(this Dictionary<ItemS, int> dic, int index)
	{
		if (dic.Count > index) { return true; }
		else { return false; }
	}
	/// <summary>
	/// �ε����� �´� Ű�� ã�� Ȯ�� �޼���
	/// </summary>
	/// <param name="dic"></param>
	/// <param name="index"></param>
	/// <returns></returns>
	public static ItemS? FindKeyForIndex(this Dictionary<ItemS, int> dic, int index)
	{
		if (dic.Count <= index) { return null; }
		int n = 0;
		foreach (var key in dic.Keys)
		{
			if (index == n) { return key; }
			n++;
		}
		return null;
	}
	public static ItemS[] DiceItem = new ItemS[10]
	{
		new ItemS(ItemType.DICE, 1, "�� �ֻ���", "���� ���� �ֻ�����. \n �ֻ��� �� ���� 0,1,2,3,4,5 �� ��¡�Ѵ�.", 0),
		new ItemS(ItemType.DICE, 1, "�ݼ� �ֻ���", "�ݼ����� ���� �ֻ�����. \n �ֻ��� �� ���� 3,4,5,6,7,8 �� ��¡�Ѵ�.", 1),
		new ItemS(ItemType.DICE, 1, "8�� �ֻ���", "8������ �� �ֻ�����. \n �ֻ��� �� ���� 2,2,3,3,4,4,5,6 �� ��¡�Ѵ�.", 2),
		new ItemS(ItemType.DICE, 1, "12�� �ֻ���", "12������ �� �ֻ�����. \n �ֻ��� �� ���� \n1,2,3,3,4,4,5,5,6,7,8,9 �� ��¡�Ѵ�.", 3),
		new ItemS(ItemType.DICE, 1, "¦�� �ֻ���", "¦���� �����ϴ� �ֻ�����. \n �ֻ��� �� ���� 2,2,4,4,6,6 �� ��¡�Ѵ�.", 4),
		new ItemS(ItemType.DICE, 1, "Ȧ�� �ֻ���", "Ȧ���� �����ϴ� �ֻ�����. \n �ֻ��� �� ���� 1,1,3,3,5,5 �� ��¡�Ѵ�.", 5),
		new ItemS(ItemType.DICE, 1, "�Ҽ� �ֻ���", "�Ҽ��� �����ϴ� �ֻ�����. \n �ֻ��� �� ���� 2,3,5,7,11,13 �� ��¡�Ѵ�.", 6),
		new ItemS(ItemType.DICE, 1, "��� �ֻ���", "���ڰ� �� ���� �ֻ�����. \n �ֻ��� �� ���� 1,1,1,1,1,15 �� ��¡�Ѵ�.", 7),
		new ItemS(ItemType.DICE, 1, "���� �ֻ���", "������ ���� �ֻ�����. \n �ֻ��� �� ���� 2,3,4,5,6,7 �� ��¡�Ѵ�.", 8),
		new ItemS(ItemType.DICE, 1, "�ö�ƽ �ֻ���", "�ö�ƽ���� ���� �ֻ�����. \n �ֻ��� �� ���� 1,2,3,4,5,6 �� ��¡�Ѵ�.", 9)
	};

	public static DiceS[] DiceInfo = new DiceS[10]
	{
		new DiceS(6, new int[6] { 0, 1, 2, 3, 4, 5} , "UI/MiniDice_80_80"),
		new DiceS(6, new int[6] { 3, 4, 5, 6, 7, 8} , "UI/MiniDice_80_80"),
		new DiceS(8, new int[8] { 2, 2, 3, 3, 4, 4, 5, 6} , "UI/MiniDice_80_80"),
		new DiceS(12, new int[12] { 1, 2, 3, 3, 4, 4, 5, 5, 6, 7, 8, 9} , "UI/MiniDice_80_80"),
		new DiceS(6, new int[6] { 2, 2, 4, 4, 6, 6} , "UI/MiniDice_80_80"),
		new DiceS(6, new int[6] { 1, 1, 3, 3, 5, 5} , "UI/MiniDice_80_80"),
		new DiceS(6, new int[6] { 2, 3, 5, 7, 11, 13} , "UI/MiniDice_80_80"),
		new DiceS(6, new int[6] { 1, 1, 1, 1, 1, 15} , "UI/MiniDice_80_80"),
		new DiceS(6, new int[6] { 2, 3, 4, 5, 6, 7} , "UI/MiniDice_80_80"),
		new DiceS(6, new int[6] { 1, 2, 3, 4, 5, 6} , "UI/MiniDice_80_80")
	};

	public static int[] nowDice = new int[2] { 9, 9 };

	public static ItemS[] GunItem = new ItemS[8]
	{
		new ItemS(ItemType.GUN, 1, "Glick 19","������� : ���ڵ� \n����ӵ� : �߰� \n����� : ���� \n���߷� : ", 0),
		new ItemS(ItemType.GUN, 1, "S&U m500","������� : ���� \n����ӵ� : ���� \n����� : ���� \n���߷� : ", 1),
		new ItemS(ItemType.GUN, 1, "Mi1911","������� : ���ڵ� \n����ӵ� : ���ݴ��� \n����� : �ſ���� \n���߷� : ", 2),
		new ItemS(ItemType.GUN, 1, "MiP9","������� : �ڵ� \n����ӵ� : �ſ���� \n����� : ���� \n���߷� : ", 3),
		new ItemS(ItemType.GUN, 1, "MiPX","������� : �ڵ� \n����ӵ� : ���� \n����� : �߰� \n���߷� : ", 4),
		new ItemS(ItemType.GUN, 1, "Pi90","������� : �ڵ� \n����ӵ� : ���� \n����� : �߰� \n���߷� : ", 5),
		new ItemS(ItemType.GUN, 1, "Kress Victor","������� : �ڵ� \n����ӵ� : �ſ�ſ���� \n����� : ���� \n���߷� : ", 6),
		new ItemS(ItemType.GUN, 1, "Thimpson SMG","������� : �ڵ� \n����ӵ� : ���� \n����� : �ſ���� \n���߷� : ", 7)
	};
	/// <summary>
	/// ����ӵ� - 10~19 : �ſ���� - 20~29 : ���� - 30~39 : ���� ���� - 40~55 : �߰� - 56~ 64 : ���� ���� - 65~79 : ���� - 80~89 : �ſ� ���� - 90~ : �ſ�ſ����
	/// ����� - 10~19 : �ſ� ���� - 20~29 : ���� - 30~39 : ���� ���� -40~55 : �߰� - 56~74 : ���� ���� - 75~85 : ���� - 86~ : �ſ� ����
	/// </summary>
	public static GunS[] GunInfo = new GunS[8]
	{
		new GunS(LoadEnum.SEMIAUTO, 50, 20, 100, "UI/Glick19_240_120"),
		new GunS(LoadEnum.MANUAL, 20, 80, 100, "UI/S&Um500_240_120"),
		new GunS(LoadEnum.SEMIAUTO, 30, 10, 100, "UI/Mi1911_240_120"),
		new GunS(LoadEnum.AUTO, 80, 20, 100, "UI/MiP9_240_120"),
		new GunS(LoadEnum.AUTO, 65, 50, 100, "UI/MiPX_240_120"),
		new GunS(LoadEnum.AUTO, 65, 50, 100, "UI/Pi90_240_120"),
		new GunS(LoadEnum.AUTO, 90, 20, 100, "UI/KressVictor_240_120"),
		new GunS(LoadEnum.AUTO, 65, 10, 100, "UI/ThimpsonSMG_240_120")
	};
	/// <summary>
	/// ���� ������ ��. -1�� ������ ���¸� �ǹ���.
	/// </summary>
	public static int[] nowGun = new int[1] { -1 };
}
