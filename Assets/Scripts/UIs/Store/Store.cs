using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StoreNS;
// �Ѽ�ȣ �ۼ�
public class Store
{
	public struct StoreItemS
	{
		public ItemS Item;
		public int Cost;

		public StoreItemS(ItemS itemS, int cost)
		{
			Item = itemS;
			Cost = cost;
		}
	}
	public List<StoreItemS> StoreItemList { get; protected set; } // �Ǹ� ��ǰ ���. int�� ����
	public ICloseStore CloseStore { get; protected set; }

	protected ItemType itemType;
	
}
