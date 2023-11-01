using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConversationNS;

// �Ѽ�ȣ �ۼ�
public class GunStore : Conversation, ICloseStore
{
	private int itemSumCost = -1;

	public GunStore()
	{
		store = new GunStoreItem(this);

		NpcTextStrArr = new string[14]
		{
			"...",	// 0
			"�� ������ �Ȱ� �ֽ��ϱ�?",	// 1
			"�� !",	// 2
			"(���ư���.)",	// 3
			"(��ǰ�� ����.)",	// 4
			"(����.)",	// 5
			"(�θ� �� ��)...",	// 6
			"$$$ �� !",	// 7
			"���� �����ϳ׿�. �˼��մϴ�..",	// 8
			"�ٽ� ��󺸰ڽ��ϴ�.",	// 9
			"(�ݾ��� �����Ѵ�.)",	// 10
			"�� �޾Ҵ�.",	// 11
			"�� ��󺸰ڽ��ϴ�.",	// 12
			"(ù ���� ���ư���.)"	// 13
		};

		TextList = new List<TextNodeC>();
		InitTextList();
	}
	public override void JumpConversation(int num)
	{
		int index = -1;

		if (temInt == -5)
		{
			itemSumCost = num;
			SettingConversation(Findidx(-5, new int[1] { 7 }), itemSumCost);
			return;
		}
	}
	public override void JumpConversation()
	{
		int index = -1;

		if (temInt == -5)
		{
			itemSumCost = -1;
			index = Findidx(-5, new int[1] { -1 });
		}
		SettingConversation(index);
	}
	/// <summary>
	/// ���ǿ� ���� ��ȭ �б���
	/// </summary>
	/// <param name="tem"></param>
	/// <returns></returns>
	protected override int Bifurcation(List<TextNodeC> tem)
	{
		int index = -1;
		temInt = tem[0].NowTextNum;

		if (temInt == 8)
		{
			SettingConversation(Findidx(8, new int[1] { -1 }));
			index = -100;
		}
		else if (temInt == 9)
		{
			SettingConversation(Findidx(9, new int[1] { 6 }));
			index = -100;
		}
		else if (temInt == 10)
		{
			// �� ���ǵ� �κ��� ��
			AddPlayerItemDic();
			// �ݾ�����
			GameManager.Instance.Money -= itemSumCost;
			itemSumCost = -1;

			SettingConversation(Findidx(10, new int[1] { 11 }));
			index = -100;
		}

		return index;
	}
	/// <summary>
	/// �÷��̾��� ���¿� ���� ��ȭ ���� ����
	/// </summary>
	/// <param name="num"></param>
	/// <returns></returns>
	protected override bool Condition(int num)
	{
		if (num == 8)
		{
			if (GameManager.Instance.Money < itemSumCost)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if (num == 10)
		{
			if (GameManager.Instance.Money >= itemSumCost)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		return false;
	}
	/// <summary>
	/// �ؽ�Ʈ�� �����ؼ� �׷����� ����
	/// </summary>
	/// �������� �̹��� 0 : ���� , 1 : �ɱ���� , 2 : ��г��� , 3 : ����
	protected override void InitTextList()
	{
		startText = new int[1] { 0 };
		nowTextNum = -5; nextTextNum = new int[3] { 1, 4, 5 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = -5; nextTextNum = new int[3] { 8, 10, 9 }; nextTextIsAble = new bool[3] { false, false, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 7 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = -1; nextTextNum = new int[3] { 1, 4, 5 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[3]
		{
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 } )
		};
		AddTextList();
		nowTextNum = 1; nextTextNum = new int[1] { 3 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 2 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 3; nextTextNum = new int[3] { 1, 4, 5 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 4; nextTextNum = new int[0]; nextTextIsAble = new bool[0];
		methodSArr = new MethodS[6]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 6 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 0 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 }),
			new MethodS(MethodEnum.SAVETEXTINDEX, new int[1] { -5 }),
			new MethodS(MethodEnum.OPENSTORE, new int[0])
		};
		AddTextList();
		nowTextNum = 5; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { false };
		methodSArr = new MethodS[1]
		{
			new MethodS(MethodEnum.ENDPANEL, new int[1] { -1 })
		};
		AddTextList();
		nowTextNum = 8; nextTextNum = new int[3] { 1, 4, 5 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 9; nextTextNum = new int[0]; nextTextIsAble = new bool[0];
		methodSArr = new MethodS[6]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 6 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 0 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 }),
			new MethodS(MethodEnum.SAVETEXTINDEX, new int[1] { -5 }),
			new MethodS(MethodEnum.OPENSTORE, new int[0])
		};
		AddTextList();
		nowTextNum = 10; nextTextNum = new int[3] { 13, 12, 5 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 11 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 12; nextTextNum = new int[0]; nextTextIsAble = new bool[0];
		methodSArr = new MethodS[6]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 6 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 0 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SAVETEXTINDEX, new int[1] { -5 }),
			new MethodS(MethodEnum.OPENSTORE, new int[0])
		};
		AddTextList();
		nowTextNum = 13; nextTextNum = new int[3] { 1, 4, 5 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
	}
}
