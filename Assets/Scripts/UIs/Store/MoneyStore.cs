using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConversationNS;

// �Ѽ�ȣ �ۼ�

public class MoneyStore : Conversation
{
	public static bool IsTalk = false;	// �����ü�� ��ӴϿ� ���� �̾߱⸦ �ߴ��� ����
	public MoneyStore()
	{
		NpcTextStrArr = new string[57];
	}
}
