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
	private PizzaIngredientSlots[] pizzaIngredientSlotsArr;
	private int nowPage;
	private void Awake()
	{
		pizzaIngredientSprArr = Resources.LoadAll<Sprite>("UI/Ingredients_120_120");
		pizzaIngredientSlotsArr = new PizzaIngredientSlots[slotObjArr.Length];
		for (int i = 0; i < slotObjArr.Length; i++)
		{
			pizzaIngredientSlotsArr[i] = slotObjArr[i].GetComponent<PizzaIngredientSlots>();
			pizzaIngredientSlotsArr[i].SlotNumber = i;
			pizzaIngredientSlotsArr[i].InitCompo();
		}

		nowPage = 0;
		Debug.Log("abc");

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

		for (int i = 0; i < slotObjArr.Length; i++)
		{
			if (i + (nowPage * slotObjArr.Length) + 1 > Constant.IngredientsArray.GetLength(0))
			{
				pizzaIngredientSlotsArr[i].IngredientNumber = 0;
				pizzaIngredientSlotsArr[i].SetIngredientsSpr(pizzaIngredientSprArr[0]);
			}
			else
			{
				pizzaIngredientSlotsArr[i].IngredientNumber = i + (nowPage * slotObjArr.Length) + 1;
				pizzaIngredientSlotsArr[i].SetIngredientsSpr(pizzaIngredientSprArr[i + (nowPage * slotObjArr.Length) + 1]);
			}
		}
	}
}
