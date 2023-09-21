using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PizzaNS;

// �Ѽ�ȣ �ۼ�
public class PizzaStoreUI : MonoBehaviour
{
	[SerializeField] private GameObject[] slotObjArr;

	private Sprite[] pizzaIngredientSprArr;
	private int nowPage;
	private void Awake()
	{
		pizzaIngredientSprArr = Resources.LoadAll<Sprite>("UI/Ingredients_120_120");

		for (int i = 0; i < slotObjArr.Length; i++)
		{
			slotObjArr[i].GetComponent<PizzaIngredientSlots>().SlotNumber = i;
		}

		nowPage = 0;


	}
	

	private void OnEnable()
	{
		// ��ώ���� ������ 0������ ����
		// ��������Ʈ�� ��� ���Կ� ���ߵ�
		InitPage(0);
	}

	private void InitPage(int page)
	{
		nowPage = page;

		for (int i =0; i < slotObjArr.Length; i++)
		{
			IngredientS ing;
			if (i + (nowPage * slotObjArr.Length) + 1 >= Constant.IngredientsArray.GetLength(0))
			{
				ing = new IngredientS
				((Ingredient)int.Parse(Constant.IngredientsArray[0, 0]),
				int.Parse(Constant.IngredientsArray[0, 1]),
				int.Parse(Constant.IngredientsArray[0, 2]),
				int.Parse(Constant.IngredientsArray[0, 3]));

			}
			else
			{
				ing = new IngredientS
					((Ingredient)int.Parse(Constant.IngredientsArray[i + (nowPage * slotObjArr.Length) + 1, 0]),
					int.Parse(Constant.IngredientsArray[i + (nowPage * slotObjArr.Length) + 1, 1]),
					int.Parse(Constant.IngredientsArray[i + (nowPage * slotObjArr.Length) + 1, 2]),
					int.Parse(Constant.IngredientsArray[i + (nowPage * slotObjArr.Length) + 1, 3]));
			}

			slotObjArr[i].GetComponent<PizzaIngredientSlots>().SetIngredients(ing);
		}
	}
}
