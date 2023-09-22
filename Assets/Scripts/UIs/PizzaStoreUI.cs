using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PizzaNS;

// �Ѽ�ȣ �ۼ�
public class PizzaStoreUI : MonoBehaviour, IIngredientSlot
{
	[SerializeField] private GameObject[] slotObjArr;
	[SerializeField] private GameObject[] choiceSlotArr;
	[SerializeField] private Text ingredientExplainText;
	[SerializeField] private Text choiceIngredientExplainText;

	private List<int> choiceIngredientList = new List<int>();

	private Sprite[] pizzaIngredientSprArr;
	private PizzaIngredientSlots[] pizzaIngredientSlotsArr;
	private ChoiceIngredientSlot[] choiceIngredientSlotArr;

	private int attractiveness;
	private int declineAt;
	private int ingredientPrice;
	private int nowPage;
	private void Awake()
	{
		pizzaIngredientSprArr = Resources.LoadAll<Sprite>("UI/Ingredients_120_120");
		pizzaIngredientSlotsArr = new PizzaIngredientSlots[slotObjArr.Length];
		choiceIngredientSlotArr = new ChoiceIngredientSlot[choiceSlotArr.Length];
		
		for (int i = 0; i < slotObjArr.Length; i++)
		{
			pizzaIngredientSlotsArr[i] = slotObjArr[i].GetComponent<PizzaIngredientSlots>();
			pizzaIngredientSlotsArr[i].SlotNumber = i;
			pizzaIngredientSlotsArr[i].InitCompo();
			pizzaIngredientSlotsArr[i].InitInterface(this);
		}

		for (int i = 0; i < choiceSlotArr.Length; i++)
		{
			choiceIngredientSlotArr[i] = choiceSlotArr[i].GetComponent<ChoiceIngredientSlot>();
			choiceIngredientSlotArr[i].SlotNumber = i;
			choiceIngredientSlotArr[i].InitCompo();
			choiceIngredientSlotArr[i].InitInterface(this);
			choiceIngredientSlotArr[i].SetIngredientsSpr(pizzaIngredientSprArr[0]);
		}

		nowPage = 0;
		InitValue();
	}
	private void OnEnable()
	{
		// ��ώ���� ������ 0������ ����
		// ��������Ʈ�� ��� ���Կ� ���ߵ�
		BackMakePizza();
	}
	public void BackMakePizza()
	{
		InitPage(0);
		SetChoiceText(false);
		InitChoiceIngredient();
	}
	private void InitValue()
	{
		attractiveness = 0;
		declineAt = 0;
		ingredientPrice = 0;
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
	public void InitChoiceIngredient()
	{
		choiceIngredientList.Clear();
		for (int i = 0; i < choiceSlotArr.Length; i++)
		{
			choiceIngredientSlotArr[i].SetIngredientsSpr(pizzaIngredientSprArr[0]);
			choiceIngredientSlotArr[i].IngredientNumber = 0;
		}
	}
	public void ChoiceIngredient(int ingNum, int index)
	{
		// ��Ḧ �߰�
		if (index == -1)
		{
			if (choiceIngredientList.Count >= 10) { return; }

			choiceIngredientList.Add(ingNum);
		}
		// ��Ḧ ����
		else
		{
			if (choiceIngredientList.Count - 1 < index) { return; }

			choiceIngredientList.RemoveAt(index);
		}

		InitValue();

		for (int i = 0; i < choiceSlotArr.Length; i++)
		{
			if (choiceIngredientList.Count - 1 < i)
			{
				choiceIngredientSlotArr[i].SetIngredientsSpr(pizzaIngredientSprArr[0]);
				choiceIngredientSlotArr[i].IngredientNumber = 0;
			}
			else
			{
				choiceIngredientSlotArr[i].SetIngredientsSpr(pizzaIngredientSprArr[choiceIngredientList[i]]);
				choiceIngredientSlotArr[i].IngredientNumber = choiceIngredientList[i];

				attractiveness += int.Parse(Constant.IngredientsArray[choiceIngredientList[i], 1]);
				declineAt += int.Parse(Constant.IngredientsArray[choiceIngredientList[i], 2]);
				ingredientPrice += int.Parse(Constant.IngredientsArray[choiceIngredientList[i], 3]);
			}
		}

		SetChoiceText(true);
	}

	private void SetChoiceText(bool bo)
	{
		if (bo)
		{
			choiceIngredientExplainText.text
			  = "�� �ŷµ� : " + attractiveness + "\n"
			  + "�� �ŷ��϶��� : " + declineAt + "\n"
			  + "�� ��ᰪ : " + ingredientPrice;
		}
		else
		{
			choiceIngredientExplainText.text = "";
		}
	}
}
