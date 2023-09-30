using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �Ѽ�ȣ �ۼ�
public class MakingPizzaPanel : MonoBehaviour
{
	[SerializeField] private RectTransform mainPanelRect;
	[SerializeField] private Text backText;
	[SerializeField] private Text mainText;

	private Pizza temPizza = new Pizza();	// �ӽ÷� ������ ����

	/// <summary>
	/// ���ڸ� �ӽ÷� �����մϴ�.
	/// </summary>
	/// <param name="pizza"></param>

	public void SetPizza(Pizza pizza)
	{
		temPizza = pizza;
		backText.text = temPizza.Name;
		mainText.text = temPizza.Name;
	}
	/// <summary>
	/// ���ڸ� ���մϴ�. ���� ���ٸ� true, Ʋ���ٸ� false�� ��ȯ�մϴ�.
	/// </summary>
	/// <param name="pizza"></param>
	/// <returns></returns>
	public bool ComparePizza(Pizza pizza)
	{
		// ����ü �ȿ� ����Ʈ ����־ equals�� �ȸ����� ������ ���ؾߵ� �ƿ�.
		if (temPizza.Name.Equals(pizza.Name) && temPizza.Perfection == pizza.Perfection && temPizza.Charisma == pizza.Charisma
			&& temPizza.ProductionCost == pizza.ProductionCost && temPizza.SellCost == pizza.SellCost &&
			temPizza.TotalDeclineAt == pizza.TotalDeclineAt && temPizza.Ingreds.Count == pizza.Ingreds.Count)
		{
			for (int i = 0; i < temPizza.Ingreds.Count; i++)
			{
				if (temPizza.Ingreds[i] != pizza.Ingreds[i])
				{
					return false;
				}
			}

			return true;
		}
		else
		{
			return false;
		}
	}
	/// <summary>
	/// ������ ���� ������� �����մϴ�.
	/// </summary>
	/// <param name="percentage"></param>
	public void SetMainPanelRect(float percentage)
	{
		mainPanelRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,600f * ((100 - percentage) / 100f));
	}
	
}
