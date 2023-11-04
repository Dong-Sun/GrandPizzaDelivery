using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConversationNS;

// �Ѽ�ȣ �ۼ�
public class Hospital : Conversation
{
	private int hpToMoney = -1;
	public Hospital()
	{
		NpcTextStrArr = new string[17]
		{
			"(������ ����)",	// 0
			"���� ������ ������ �����ΰ���?",	// 1
			"ġ�Ḧ ������ �Խ��ϴ�.",	// 2
			"�����Դϴ�.",	// 3
			"��..�׷�����",	//	4
			"�� $$$���Դϴ�.",	// 5
			"(���� �����Ѵ�.)(1�ð� �Ҹ�)(�ֻ��� �� 3 �̻�) ġ�Ḧ �޴´�.",	// 6
			"ġ�� �������ϴ�.",	// 7
			"ġ�� �����߽��ϴ�.",	// 8
			"(����.)",	// 9
			"(1�ð� �Ҹ�)(�ֻ��� �� 5 �̻�)������? ��ȭ..�ƴ� �ٽ� �����սô�.",	// 10
			"�����ϼ̽��ϴ�. ���ϵ帳�ϴ�.",	// 11
			"�����߽��ϴ�. �˼��մϴ�.",	// 12
			"(�ֻ��� �� 7 �̻�)�̰� ������. ġ���� �������.",	// 13
			"�װ�..����Ұ� �����ϴ�. ����.",	// 14
			"(ġ��� ���� ��������)�˰ڽ��ϴ�. �帮�ڽ��ϴ�.",	// 15
			"(����.)���� ���׿�. �̷�.."	// 16
		};

		TextList = new List<TextNodeC>();
		InitTextList();
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

		if (temInt == 1)
		{
			SettingConversation(Findidx(1, new int[1] { 3 }));
			index = -100;
		}
		else if (temInt == 2)
		{
			hpToMoney = 100 - (int)(((float)PlayerMove.HP / (float)PlayerMove.MaxHP) * 100);

			SettingConversation(Findidx(2, new int[1] { 5 }), hpToMoney * 1000);

			index = -100;
		}
		else if (temInt == 6)
		{
			GameManager.Instance.Money -= hpToMoney * 1000;
			GameManager.Instance.time += 3600;
			DiceRoll(3);
			index = -100;
		}
		else if (temInt == 9)
		{
			EndPanel();
			index = -100;
		}
		else if (temInt == 10)
		{
			GameManager.Instance.time += 3600;
			DiceRoll(5);
			index = -100;
		}
		else if (temInt == 13)
		{
			DiceRoll(7);
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
		if (num == 6)
		{
			if (GameManager.Instance.Money >= hpToMoney * 1000)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if (num == 16)
		{
			if (GameManager.Instance.Money < hpToMoney * 1000)
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
	/// �ֻ��� ����� ���� ��ȭ �б���
	/// </summary>
	/// <param name="bo"></param>
	public override void DiceResult(bool bo)
	{
		int index = -1;

		if (temInt == 6)
		{
			if (bo)
			{
				PlayerStat.HP = PlayerStat.MaxHP;
				index = Findidx(6, new int[1] { 7 });
			}
			else
			{
				index = Findidx(6, new int[1] { 8 });
			}
		}
		else if (temInt == 10)
		{
			if (bo)
			{
				PlayerStat.HP = PlayerStat.MaxHP;
				index = Findidx(10, new int[1] { 11 });
			}
			else
			{
				index = Findidx(10, new int[1] { 12 });
			}
		}
		else if (temInt == 13)
		{
			if (bo)
			{
				GameManager.Instance.Money += hpToMoney * 1000;
				index = Findidx(13, new int[1] { 15 });
			}
			else
			{
				index = Findidx(13, new int[1] { 14 });
			}
		}

		SettingConversation(index);
	}
	/// <summary>
	/// �ؽ�Ʈ���� �����ؼ� �׷����� ����
	/// </summary>
	/// ���� �ǻ� �̹���  0 : ���� 1 : �߰� 2 : ŭ 3 : �ſ�ŭ
	protected override void InitTextList()
	{
		startText = new int[1] { 0 };

		nowTextNum = -1; nextTextNum = new int[3] { 1,2, 9 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])

		};
		AddTextList();
		nowTextNum = 1; nextTextNum = new int[1] { 4 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 3 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 2; nextTextNum = new int[2] { 6, 16 }; nextTextIsAble = new bool[2] { false, false };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 5 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
		};
		AddTextList();
		nowTextNum = 4; nextTextNum = new int[3] { 1,2,9 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 6; nextTextNum = new int[1] { 9 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 7 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 6; nextTextNum = new int[2] { 10,9 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 8 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 9; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { false };
		methodSArr = new MethodS[1]
		{
			new MethodS(MethodEnum.ENDPANEL, new int[1] {-1})
		};
		AddTextList();
		nowTextNum = 10; nextTextNum = new int[1] { 9 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 11 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 10; nextTextNum = new int[2] { 13, 9 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 12 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 13; nextTextNum = new int[1] { 9 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 15 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 13; nextTextNum = new int[1] { 9 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 14 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
	}
}
