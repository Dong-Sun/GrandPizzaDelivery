using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PizzaNS;

// �Ѽ�ȣ �ۼ�
public class PizzaStoreUI : MonoBehaviour, IPizzaStore
{
	[SerializeField] private GameObject[] slotObjArr;
	[SerializeField] private Text ingredientExplainText;

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
			pizzaIngredientSlotsArr[i].InitInterface(this);
		}

		nowPage = 0;

	}
	

	private void OnEnable()
	{
		// ��ώ���� ������ 0������ ����
		// ��������Ʈ�� ��� ���Կ� ���ߵ�
		InitPage(0);
	}
	// ��� ������ �ѱ�
	public void Page(bool next)
	{
		if (next)
		{
			if (Constant.IngredientsArray.GetLength(0) / slotObjArr.Length <= nowPage)
			{
				return;
			}
			else
			{
				InitPage(++nowPage);
			}
		}
		else
		{
			if (nowPage <= 0)
			{
				return;
			}
			else
			{
				InitPage(0);
			}
		}
	}

	private void InitPage(int page)
	{
		nowPage = page;

		for (int i = 0; i < slotObjArr.Length; i++)
		{
			if (i + (nowPage * slotObjArr.Length) + 1 >= Constant.IngredientsArray.GetLength(0))
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

	public void IngredientExplain(int ingNum)
	{
		if (ingNum == 0)
		{
			ingredientExplainText.text 
				= "�ŷµ� : " + Constant.IngredientsArray[0, 1] + "\n"
				+ "�ŷ��϶��� : " + Constant.IngredientsArray[0,2] + "\n"
				+ "��ᰪ : " + Constant.IngredientsArray[0,3];
		}
		else if (ingNum == -1)
		{
			ingredientExplainText.text = "";
		}
		else
		{
			ingredientExplainText.text
			= "�ŷµ� : " + Constant.IngredientsArray[ingNum, 1] + "\n"
			+ "�ŷ��϶��� : " + Constant.IngredientsArray[ingNum, 2] + "\n"
			+ "��ᰪ : " + Constant.IngredientsArray[ingNum, 3];

		}
	}
}
