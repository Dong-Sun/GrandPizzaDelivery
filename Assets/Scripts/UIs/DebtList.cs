using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebtList : MonoBehaviour
{
	[SerializeField] private Text text;
	[SerializeField] private RectTransform contentsRect;

	public void InitDebtListText()
	{
		text.text = "";
		int height = 0;
		// key�� ��¥
		foreach (var key in Constant.PayMoneyDate.Keys)
		{
			text.text += $"<size=60>{key}�� : </size>\n";
			height += 150;
			// key2�� �����ü �ڵ�
			foreach (var key2 in Constant.PayMoneyDate[key].Keys)
			{
				text.text += $"<size=45>- '{Constant.MoneyStoreName[key2]}' �����ü�� {Constant.PayMoneyDate[key][key2]}�� ���ƾߵ�. </size>\n";
				height += 100;
			}
		}

		contentsRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
	}

	public void InitDebtListTextForName()
	{
		text.text = "";
		int height = 0;
		for (int i = 0; i < Constant.MoneyStoreName.Length; i++)
		{
			text.text += $"<size=60>'{Constant.MoneyStoreName[i]}' �����ü </size> \n";
			height += 150;
			foreach (var key in Constant.PayMoneyDate.Keys)
			{
				if (Constant.PayMoneyDate[key].ContainsKey(i))
				{
					text.text += $"<size=45>'{key}��---{Constant.PayMoneyDate[key][i]}��.' </size> \n";
					height += 100;
				}
			}
		}

		contentsRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

	}
}
