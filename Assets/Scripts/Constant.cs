using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PizzaNS;
using ClerkNS;

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
	public static string[,] IngredientsArray = new string[10, 4]
	{
		{"0","-1","-1","-1" },
		{"1","25","3","150" },
		{"2","30","2","160" },
		{"3","15","2","80" },
		{"4","20","1","120" },
		{"5","45","7","500" },
		{"6","27","3","140" },
		{"7","40","5","320" },
		{"8","65","12","960" },
		{"9","78","20","1350" }
	};
	/// <summary>
	/// ������ ���� ����Ʈ
	/// </summary>
	public static List<Pizza> DevelopPizza = new List<Pizza>();

	public static bool IsMakePizza = false;
	/// <summary>
	/// 0.1�ʰ��� ��
	/// </summary>
	public static WaitForSeconds OneTime = new WaitForSeconds(0.1f);
	/// <summary>
	/// ����� ���� ����Ʈ
	/// </summary>
	public static List<ClerkC> ClerkList = new List<ClerkC>();
	
}
