using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PizzaNS;

// �Ѽ�ȣ �ۼ�
public class PizzaStoreUI : MonoBehaviour, IIngredientSlot
{
	[SerializeField] private GameObject[] slotObjArr;	// ��� ����
	[SerializeField] private GameObject[] choiceSlotArr;	// ������ ��ᰡ ��� ����
	[SerializeField] private Text ingredientExplainText;
	[SerializeField] private Text choiceIngredientExplainText;

	//private List<int> choiceIngredientList = new List<int>();
	/// <summary>
	/// ��� ��������Ʈ
	/// </summary>
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
	/// <summary>
	/// ���� ����� �ʱ�ȭ. ���� ����� ȭ���� ���� �� ȣ��
	/// </summary>
	public void BackMakePizza()
	{
		InitPage(0);
		SetChoiceText(false);
		InitChoiceIngredient();
		Constant.PizzaAttractiveness = 0;
	}
	/// <summary>
	/// ������� ������ �ɷ�ġ �ʱ�ȭ
	/// </summary>
	private void InitValue()
	{
		attractiveness = 0;
		declineAt = 0;
		ingredientPrice = 0;
	}

	/// <summary>
	/// ��� ������ �ѱ�
	/// </summary>
	/// <param name="next"></param>
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
	/// <summary>
	/// �������� �ش��ϴ� ����� �̹���, ���� ����
	/// </summary>
	/// <param name="page"></param>
	private void InitPage(int page)
	{
		nowPage = page;

		for (int i = 0; i < slotObjArr.Length; i++)
		{
			// ���� �迭 ������ �ʰ��ϴ� ������ ���� �������� ���¸� ����
			if (i + (nowPage * slotObjArr.Length) + 1 >= Constant.IngredientsArray.GetLength(0))
			{
				pizzaIngredientSlotsArr[i].IngredientNumber = 0;
				pizzaIngredientSlotsArr[i].SetIngredientsSpr(pizzaIngredientSprArr[0]);
			}
			else
			{
				// �رݵ� ���ϵ鸸 ������ �־������

				// + 1 �� ���ִ� ������ �̹����� 0 ���� '����'�� �ǹ��ϱ� ����.
				pizzaIngredientSlotsArr[i].IngredientNumber = i + (nowPage * slotObjArr.Length) + 1;
				pizzaIngredientSlotsArr[i].SetIngredientsSpr(pizzaIngredientSprArr[i + (nowPage * slotObjArr.Length) + 1]);
			}
		}
	}
	/// <summary>
	/// ���� ��ῡ ���� ����
	/// </summary>
	/// <param name="ingNum"></param>
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
		Constant.ChoiceIngredientList.Clear();
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
			if (Constant.ChoiceIngredientList.Count >= 10) { return; }

			Constant.ChoiceIngredientList.Add(ingNum);
		}
		// ��Ḧ ����
		else
		{
			if (Constant.ChoiceIngredientList.Count - 1 < index) { return; }

			Constant.ChoiceIngredientList.RemoveAt(index);
		}

		InitValue();

		for (int i = 0; i < choiceSlotArr.Length; i++)
		{
			if (Constant.ChoiceIngredientList.Count - 1 < i)
			{
				choiceIngredientSlotArr[i].SetIngredientsSpr(pizzaIngredientSprArr[0]);
				choiceIngredientSlotArr[i].IngredientNumber = 0;
			}
			else
			{
				choiceIngredientSlotArr[i].SetIngredientsSpr(pizzaIngredientSprArr[Constant.ChoiceIngredientList[i]]);
				choiceIngredientSlotArr[i].IngredientNumber = Constant.ChoiceIngredientList[i];

				attractiveness += int.Parse(Constant.IngredientsArray[Constant.ChoiceIngredientList[i], 1]);
				declineAt += int.Parse(Constant.IngredientsArray[Constant.ChoiceIngredientList[i], 2]);
				ingredientPrice += int.Parse(Constant.IngredientsArray[Constant.ChoiceIngredientList[i], 3]);
			}
		}

		Constant.PizzaAttractiveness = attractiveness;
		Constant.TotalDeclineAt = declineAt;
		Constant.ProductionCost = ingredientPrice;
		Constant.ingreds.Clear();
		for (int i = 0; i< Constant.ChoiceIngredientList.Count; i++)
		{
			Constant.ingreds.Add((Ingredient)Constant.ChoiceIngredientList[i]);
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
