using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PizzaNS;

// �Ѽ�ȣ �ۼ�

public class PizzaMenuUI : MonoBehaviour, IAddPizza
{
	[SerializeField] private GameObject[] addPizzaSlot;

	[SerializeField] private GameObject pizzaMenuListObj;
	[SerializeField] private GameObject menuObj;
	[SerializeField] private GameObject questionPanel;
	[SerializeField] private GameObject addPizzaPanel;
	[SerializeField] private GameObject changePizzaNamePanel;
	[SerializeField] private Text addPizzaExplainText;
	
	private List<int> openExplainList = new List<int>();
	private AddPizzaSlot[] addPizzaSlots;
	private GameObject[] pizzaMenuSlot;
	private Text[] pizzaMenuSlotText;
	private Text[] explainMenuSlotText;
	private Text[] addPizzaSlotText;

	private GridLayoutGroup gridLayoutGroup;
	private RectTransform pizzaMenuListRect;
	private int temIndex = -1;
	private int temSlotNumber = -1;
	private int nowPage = 0;
	private void Awake()
	{
		//for (int i = 0; i < 7; i++)
		//{
		//	List<Ingredient> ing = new List<Ingredient>();
		//	ing.Add(Ingredient.BACON);
		//	ing.Add(Ingredient.TOMATO);
		//	ing.Add(Ingredient.CHEESE);
		//	Constant.DevelopPizza.Add(new Pizza("SuperPizza", 60, 5000, 10000, Random.Range(0, 1500) + 500, ing, Random.Range(0, 1500) + 200));
		//}

		pizzaMenuSlot = new GameObject[pizzaMenuListObj.transform.childCount];
		pizzaMenuSlotText = new Text[pizzaMenuSlot.Length];
		explainMenuSlotText = new Text[pizzaMenuSlot.Length];

		gridLayoutGroup = pizzaMenuListObj.GetComponent<GridLayoutGroup>();
		pizzaMenuListRect = pizzaMenuListObj.GetComponent<RectTransform>();

		for (int i = 0; i < pizzaMenuSlot.Length; i++)
		{
			pizzaMenuSlot[i] = pizzaMenuListObj.transform.GetChild(i).gameObject;
			pizzaMenuSlotText[i] = pizzaMenuSlot[i].transform.GetChild(0).GetComponent<Text>();
			explainMenuSlotText[i] = pizzaMenuSlot[i].transform.GetChild(1).GetChild(0).GetComponent<Text>();
		}
		RefreshAllSlot();

		addPizzaSlotText = new Text[addPizzaSlot.Length];
		addPizzaSlots = new AddPizzaSlot[addPizzaSlot.Length];
		for (int i = 0; i < addPizzaSlotText.Length; i++)
		{
			addPizzaSlotText[i] = addPizzaSlot[i].transform.GetChild(0).GetComponent<Text>();
			addPizzaSlots[i] = addPizzaSlot[i].GetComponent<AddPizzaSlot>();
			addPizzaSlots[i].InitAddPizzaSlot(this);
		}
	}
	private void OnEnable()
	{
		RefreshAllSlot();
	}
	private void RefreshAllSlot()
	{
		InitSlot();
		for (int i = 0; i < GameManager.Instance.PizzaMenu.Count; i++)
		{
			if (i < pizzaMenuSlotText.Length)
			{
				SetSlot(GameManager.Instance.PizzaMenu[i], i);
			}
		}
		ReSize();

		if (openExplainList.Count == 0)
		{
			gridLayoutGroup.enabled = true;
		}
		if (menuObj.GetComponent<RectTransform>().localPosition.y >= 0)
		{
			pizzaMenuListRect.localPosition = new Vector3(0, pizzaMenuListRect.rect.height - 540);
		}
	}
	private void ReSize()
	{
		int n = GameManager.Instance.PizzaMenu.Count * 160 + openExplainList.Count * 400;
		if (n > 1080)
		{
			pizzaMenuListRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, n);
		}
		else
		{
			pizzaMenuListRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1080);
		}
	}
	// �޴� �ʱ�ȭ
	private void InitSlot()
	{
		for (int i = 0; i < pizzaMenuSlot.Length; i++)
		{
			pizzaMenuSlotText[i].text = "";
			explainMenuSlotText[i].text = "";
			pizzaMenuSlot[i].SetActive(false);
		}
	}
	// �޴��� �־��ش�.
	private void SetSlot(Pizza pizza, int index)
	{
		pizzaMenuSlot[index].SetActive(true);
		pizzaMenuSlotText[index].text = $"<size=50>{pizza.Name}</size>\n<size=40>{pizza.SellCost} ��</size>";

		string str = "";
		for (int i = 0; i < pizza.Ingreds.Count; i++)
		{
			str += pizza.Ingreds[i].ToString() + " ,";
		}
		explainMenuSlotText[index].text
			= $"<size=35>�ŷµ� : {pizza.Charisma}\n�ŷ��϶��� : {pizza.TotalDeclineAt}\n������ : {pizza.ProductionCost}\n</size>"
			+ $"<size=30>�� ��� : {str}</size>";

	}
	public void RemoveQuestion(int index)
	{
		temIndex = index;
		questionPanel.SetActive(true);
	}
	public void CancelQuestion()
	{
		temIndex = -1;
		questionPanel.SetActive(false);
	}
	public void ReMovePizza()
	{
		explainMenuSlotText[temIndex].gameObject.transform.parent.gameObject.SetActive(false);
		openExplainList.Remove(temIndex);

		GameManager.Instance.PizzaMenu.RemoveAt(temIndex);
		RefreshAllSlot();
		temIndex = -1;
		questionPanel.SetActive(false);
	}
	public void OnOffExplainPizza(int index)
	{
		if (!explainMenuSlotText[index].gameObject.transform.parent.gameObject.activeSelf)
		{
			gridLayoutGroup.enabled = false;
			explainMenuSlotText[index].gameObject.transform.parent.gameObject.SetActive(true);
			for (int i = index + 1; i < pizzaMenuSlot.Length; i++)
			{
				pizzaMenuSlot[i].GetComponent<RectTransform>().localPosition += new Vector3(0, -400);
			}
			openExplainList.Add(index);
		}
		else
		{
			explainMenuSlotText[index].gameObject.transform.parent.gameObject.SetActive(false);
			for (int i = index + 1; i < pizzaMenuSlot.Length; i++)
			{
				pizzaMenuSlot[i].GetComponent<RectTransform>().localPosition += new Vector3(0, 400);
			}
			openExplainList.Remove(index);
		}

		ReSize();

		if (openExplainList.Count == 0)
		{
			gridLayoutGroup.enabled = true;
		}

		if (menuObj.GetComponent<RectTransform>().localPosition.y >= 0)
		{
			pizzaMenuListRect.localPosition = new Vector3(0, pizzaMenuListRect.rect.height - 540);
		}
	}

	public void AddMenu()
	{
		if (GameManager.Instance.PizzaMenu.Count >= 30) { return; }

		addPizzaPanel.SetActive(true);

		InitAddPizzaPage(0);
		SetTemSlotNumber(-1);
	}
	public void NextAddMenuList()
	{
		if (nowPage >= Constant.DevelopPizza.Count / addPizzaSlot.Length)
		{
			return;
		}
		else
		{
			nowPage++;
			InitAddPizzaPage(nowPage);
		}
	}
	public void BackAddMenuList()
	{
		if (nowPage <= 0)
		{
			return;
		}
		else
		{
			nowPage--;
			InitAddPizzaPage(nowPage);
		}
	}
	private void InitAddPizzaPage(int page)
	{
		nowPage = page;

		for (int i = 0 + page * addPizzaSlot.Length; i < addPizzaSlot.Length + page * addPizzaSlot.Length; i++)
		{
			if (i < Constant.DevelopPizza.Count)
			{
				addPizzaSlotText[i % addPizzaSlot.Length].text = Constant.DevelopPizza[i].Name;
				addPizzaSlots[i % addPizzaSlot.Length].SlotNumber = i;
			}
			else
			{
				addPizzaSlotText[i % addPizzaSlot.Length].text = "����";
				addPizzaSlots[i % addPizzaSlot.Length].SlotNumber = -1;
			}
		}
	}
	public void CloseMenu()
	{
		for (int i = 0; i < addPizzaSlots.Length; i++)
		{
			addPizzaSlots[i].InitColor();
		}

		SetTemSlotNumber(-1);
		addPizzaPanel.SetActive(false);

	}
	public void SetAddPizzaExplain(int num)
	{
		if (num == - 1) { addPizzaExplainText.text = "";  return; }

		string str = "";
		for (int i = 0; i < Constant.DevelopPizza[num].Ingreds.Count; i++)
		{
			str += Constant.DevelopPizza[num].Ingreds[i].ToString() + ", ";
		}
		addPizzaExplainText.text = "�����̸� : " + Constant.DevelopPizza[num].Name 
								+ "\n���ڸŷµ� : " + Constant.DevelopPizza[num].Charisma
								+ "\n���ڰ��� : " + Constant.DevelopPizza[num].SellCost
								+ "\n���ڻ����� : " + Constant.DevelopPizza[num].ProductionCost
								+ "\n���� ��� : " + str;
	}

	public void SetTemSlotNumber(int num)
	{

		if (temSlotNumber != -1)
		{
			addPizzaSlots[temSlotNumber].InitColor();
		}
		temSlotNumber = num;
	}
	/// <summary>
	/// ������ ������ �̸��� �ٲ���
	/// </summary>
	/// <param name="str"></param>
	public void ChangeDevelopPizzaName(string str)
	{
		if (temSlotNumber == -1) { return; }
		// ������ ���ڵ� �̸��� �޴� ��� ���ϰ� ����
		for (int i = 0; i < Constant.DevelopPizza.Count; i++)
        {
			if (Constant.DevelopPizza[i].Name.Equals(str))
            {
				// ���â ���ߵ�
				return;
            }
        }

		Constant.DevelopPizza[temSlotNumber] = new Pizza
			(str,
			Constant.DevelopPizza[temSlotNumber].Perfection,
			Constant.DevelopPizza[temSlotNumber].ProductionCost,
			Constant.DevelopPizza[temSlotNumber].SellCost,
			Constant.DevelopPizza[temSlotNumber].Charisma,
			Constant.DevelopPizza[temSlotNumber].Ingreds,
			Constant.DevelopPizza[temSlotNumber].TotalDeclineAt);

		InitAddPizzaPage(nowPage);
		changePizzaNamePanel.SetActive(false);
	}
	/// <summary>
	/// ������ ���ڸ� �޴��� �߰��Ѵ�.
	/// </summary>
	public void ChoiceDevelopPizza()
	{
		if (temSlotNumber == -1) { return; }
		// �ߺ�üũ�ؼ� �ߺ����̸��̳� ����� ���ڴ� ������ ���� ��
		for (int i = 0; i < GameManager.Instance.PizzaMenu.Count; i++)
        {
			if (GameManager.Instance.PizzaMenu[i].Name.Equals(Constant.DevelopPizza[temSlotNumber].Name) ||
				GameManager.Instance.PizzaMenu[i].Ingreds.CompareIngredientList(Constant.DevelopPizza[temSlotNumber].Ingreds))
            {
				// ���â ���ߵ�
				return;
            }
        }
		// �ش� ���ڿ� ���� �̸��̰ų�, ���� ����� ���, �޴��ǿ� �ּ� �������� ������ �߰��� �� �ֵ��� ��.
		foreach ( var key in Constant.menuDateDic.Keys)
        {
			// �ش� ���ڿ� ���� �̸��̰ų� ���� ��������� �޴����� ���� �����
			if (key.Name == Constant.DevelopPizza[temSlotNumber].Name ||
				key.Ingreds.CompareIngredientList(Constant.DevelopPizza[temSlotNumber].Ingreds))
            {
				// �޴��ǿ� �ش� �޴��� ���� �������� �ּ� �������� �־�� �߰��� ������.
				if (Constant.menuDateDic[key] <= 7)
                {
					// ���â ���ߵ�
					return;
                }
            }
        }


		Constant.menuDateDic.Add(Constant.DevelopPizza[temSlotNumber], 0);
		GameManager.Instance.PizzaMenu.Add(Constant.DevelopPizza[temSlotNumber]);

		RefreshAllSlot();
	}
	public void OpenChangePizzaNamePanel()
	{
		if (temSlotNumber == -1) { return; }

		changePizzaNamePanel.SetActive(true);
	}
	/// <summary>
	/// �Ϸ簡 ������ �ߵ��ؾ� �ϴ� �Լ���
	/// </summary>
	private void OneDayMenuInit()
    {
		List<Pizza> p = new List<Pizza>();
		foreach (var key in Constant.menuDateDic.Keys)
        {
			Constant.menuDateDic[key]++;
			if (GameManager.Instance.PizzaMenu.FindIndex(a => a.Name == key.Name) == -1 &&
				GameManager.Instance.PizzaMenu.FindIndex(a => a.Ingreds.CompareIngredientList(key.Ingreds)) == -1)
            {
				p.Add(key);
            }
        }
		for (int i = 0; i < p.Count; i++)
        {
			Constant.menuDateDic.Remove(p[i]);
        }
    }
}
