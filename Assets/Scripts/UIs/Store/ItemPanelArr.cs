using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using StoreNS;
// �Ѽ�ȣ �ۼ�
public class ItemPanelArr : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] private UnityEngine.UI.Text itemExplain;
	[SerializeField] private UnityEngine.UI.Text itemName;
	
	public ItemS? Item { get; set; }
	public int ItemCost { get; set; }

	public void SetItemName(string name)
	{
		itemName.text = name;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		int n = 0;
		if (Item != null)
		{
			n = Constant.PlayerItemDIc.ContainsKey(Item.Value) ? Constant.PlayerItemDIc[Item.Value] : 0;
			itemExplain.text =
				Item.Value.Explain +
				"\n������ " + ItemCost.ToString() + "���̴�.\n\n" +
				$"���� {n}�� �������̴�.";
		}

	}

	public void OnPointerExit(PointerEventData eventData)
	{
		itemExplain.text = "";
	}
}
