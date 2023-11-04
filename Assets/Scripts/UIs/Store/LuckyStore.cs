using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConversationNS;

// �Ѽ�ȣ �ۼ�
public class LuckyStore : Conversation
{
	public static bool IsAngry = false;
	public static bool IsLuckyTest = false;
	public static bool ClearGalicQuest = false;
	public static bool ClearSonQuest = false;
	public static bool BigDicePlus = false;
	public static bool BigDiceMinus = false;
	public static bool SmallDicePlus = false;
	public static bool SmallDiceMinus = false;
	public static int AngryDate = 0;
	public static int NowDate = 1;
	public LuckyStore()
	{
		NpcTextStrArr = new string[39]
		{
			"���δ�..������ ���� ���� ����..", // 0
			"���� �� �ϴ� ���ΰ���?",	// 1
			"���� ����� ��Ʋ���ִ� ������..",	// 2
			"(ù ��ȭ�� ���ư���.)",	// 3
			"(������ �ѷ�����)������ ���׿�?",	// 4
			"�̰��� ���� �ູ..�ູ�� �ѷ����� �̰��� �����̳� �ٸ�����!",	// 5
			"�׷� ���������� ������ ������ �� �ǰ���?",	// 6
			"������ �׾��ۿ� �� ���濡 �⿩�ϸ�, ����, ���忡 ������ �ְ� ������, \n��ȭ�� �����ϸ� �Ǻ� ��ȭ�� �����ϰ� �� ��� Ȱ��ȭ�� ���� �� ��ȭ..",	// 7
			"(ù ��ȭ�� ���ư���.)(�׸� ��´�.)",	// 8
			"�ҸӴϰ� ������ ���� �����ϴ� �ٶ��� ���� ���԰� �ֽ��ϴ�.",	// 9
			"�װ� ��? ��·�ٴ� �ų�.",	// 10
			"(�ֻ��� �� 11�̻�)�ƹ��� �׷��� ������ ������ ��� �� �ƴ���. \n�������� �̷��� �����ּ���.",	// 11
			"�� ���� � ���� ���浵 ���ϴ� �༮�� �� ������ ���� ���� �� ����? \n (��� ��������)",	// 12
			"��ȣ. ����ִ� �༮�̷α���. ����. ����ϸ�. \n�׷�, ��ģ�迡 ���õ� ���� ��������.",	// 13
			"���� �ּ���. ��������.",	// 14
			"(����.)",	// 15
			"���� ���� �Խ��ϴ�.",	// 16
			"��ȣȣ. ���� ������ ���� 20������ ��������.",	// 17
			"(���� �����Ѵ�.)",	// 18
			"(ù ��ȭ�� ���ư���.)������ ���ڽ��ϴ�.",	// 19
			"����..���� �̷�, ����� ����..��, �ʵ� �̸��ͼ� �� �������� ����.",	// 20
			"(�ֻ��� �� 8 �̻��� ¦��)(�鿩�ٺ���.)",	// 21
			"(�ֻ��� �� 5 ������ Ȧ��)(�鿩�ٺ��� �ʴ´�.)",	// 22
			"� �ް��� �̷��� �ƴϴ�? (�Ϸ簣 �ֻ��� ���ʽ� 4 �߰�)",	// 23
			"�ƽ����� ���� �̷��� �߰ſ� �ұ����� ���� ���ġ�±���! \n(�Ϸ簣 �ֻ��� ���ʽ� 4 ����)",	// 24
			"�Ǹ���. ���� ������ �̷��� �����ַ��� �� ���߱���..!\n(�Ϸ簣 �ֻ��� ���ʽ� 2 �߰�)",	// 25
			"�� ���� �̷��� �����ϴٴ�, ��¼�� �װ� ���� ����̰���..!\n(�Ϸ簣 �ֻ��� ���ʽ� 2 ����)",	// 26
			"(����� �Ѵ�.)",	// 27
			"�鸮�� �ҹ��� ���ϸ� ���ڶ�� ������ϴ�.",	// 28
			"��? �׷��� ��������. ���� �� ���� ����...",	// 29
			"�ҸӴ� �Ƶ鿡�� ���� ������ϴ�. \n ���� �˷����Ⱑ �ִ� �Ƶ��� ���� ���� ���ϵ��� ��� �Ŷ�鼭��?",	// 30
			"��! �׳��� �׷��� ����! �׷� �´�! �׷� ���̳� �ϴ� �༮�� ���� �̻ڳ�?",	// 31
			"(�ֻ��� �� 14�̻�)�׷��� �׺��� ȿ���� ������ ����Դϴ�. ���ö�����\n ���� �������� ������ ���� �����ְ� ������ �ҸӴ� �Ⱥα��� ��� ���Ͻ��ݾƿ�.",	// 32
			"��������! ��������!(��� ��������)",	// 33
			"Ȯ���� �ϸ��� �ֱ�..���� �ʹ� ���������� �𸣰ڴ�.",	// 34
			"�׷� ������ ġ��ô� �ǰ���?",	// 35
			"�ȵ�! ����� ������ �������� ��Դ� �Ŷ� ���̴�...\n��� ���� ���� �Ƶ��� ���� ���� �ڱ���.",	// 36
			"��..���� 15�����̴�.",	// 37
			"���� �̾߱Ⱑ �Ұ� �ִ°ų�?"	// 38
		};

		if (IsAngry)
		{
			AngryDate += (Constant.NowDate - NowDate);
			if (AngryDate >= 3) { IsAngry = false; AngryDate = 0; }
		}

		if (Constant.NowDate != NowDate)
		{
			IsLuckyTest = false;
			NowDate = Constant.NowDate;

			if (BigDicePlus) { Constant.DiceBonus -= 4; BigDicePlus = false; }
			if (BigDiceMinus) { Constant.DiceBonus += 4; BigDiceMinus = false; }
			if (SmallDicePlus) { Constant.DiceBonus -= 2; SmallDicePlus = false; }
			if (SmallDiceMinus) { Constant.DiceBonus += 2; SmallDiceMinus = false; }
		}

		TextList = new List<TextNodeC>();
		InitTextList();
		InitStartText();
	}
	protected override void InitStartText()
	{
		if (!IsAngry)
		{
			startText = new int[1] { 0 };
		}
		else
		{
			startText = new int[1] { 14 };
		}
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

		if (temInt == -1)
		{
			if (!IsAngry)
			{
				index = Findidx(-1, new int[1] { 0 });
			}
			else
			{
				index = Findidx(-1, new int[1] { 14 });
			}
		}
		else if (temInt == 11)
		{
			DiceRoll(11);
			index = -100;
		}
		else if (temInt == 16)
		{
			if (ClearSonQuest)
			{
				SettingConversation(Findidx(16, new int[1] { 37 }));
			}
			else
			{
				SettingConversation(Findidx(16, new int[1] { 17 }));
			}
			IsLuckyTest = true;
			isCondition = true;
			index = -100;
		}
		else if (temInt == 18)
		{
			if (ClearSonQuest)
			{
				GameManager.Instance.Money -= 150000;
			}
			else
			{
				GameManager.Instance.Money -= 200000;
			}
			SettingConversation(Findidx(18, new int[1] { 20 }));
			index = -100;
		}
		else if (temInt == 19)
		{
			IsLuckyTest = false;
			SettingConversation(Findidx(19, new int[1] { -1 }));
			index = -100;
		}
		else if (temInt == 21)
		{
			DiceRoll(20008);
			index = -100;
		}
		else if (temInt == 22)
		{
			DiceRoll(-10005);
			index = -100;
		}
		else if (temInt == 32)
		{
			DiceRoll(14);
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
		if (num == 9)
		{
			if (IngredientStoreTwo.IsGalicQuest && !ClearGalicQuest)
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
			if (!IsLuckyTest)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if (num == 18)
		{
			if (ClearSonQuest)
			{
				if (GameManager.Instance.Money >= 150000)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				if (GameManager.Instance.Money >= 200000)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}
		else if (num == 27)
		{
			// ��� ����2���� ����� �ؾ��Ѵ�.
			if (IngredientStoreTwo.IsTalk)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if (num == 30)
		{
			if (MoneyStore.IsTalk && !ClearSonQuest)
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

		if (temInt == 11)
		{
			if (bo)
			{
				ClearGalicQuest = true;
				IngredientStoreTwo.OneChanceGalicClear = true;
				index = Findidx(11, new int[1] { 13 });
			}
			else
			{
				IsAngry = true;
				index = Findidx(11, new int[1] { 12 });
			}
		}
		else if (temInt == 21)
		{
			if (bo)
			{
				BigDicePlus = true;
				Constant.DiceBonus += 4;
				index = Findidx(21, new int[1] { 23 });
			}
			else
			{
				BigDiceMinus = true;
				Constant.DiceBonus -= 4;
				index = Findidx(21, new int[1] { 24 });
			}
		}
		else if (temInt == 22)
		{
			if (bo)
			{
				SmallDicePlus = true;
				Constant.DiceBonus += 2;
				index = Findidx(22, new int[1] { 25 });
			}
			else
			{
				SmallDiceMinus = true;
				Constant.DiceBonus -= 2;
				index = Findidx(22, new int[1] { 26 });
			}
		}
		else if (temInt == 32)
		{
			if (bo)
			{
				ClearSonQuest = true;
				index = Findidx(32, new int[1] { 34 });
			}
			else
			{
				IsAngry = true;
				index = Findidx(32, new int[1] { 33 });
			}
		}

		SettingConversation(index);
	}
	/// <summary>
	/// �ؽ�Ʈ���� �����ؼ� �׷����� ����
	/// </summary>
	/// ���� ���� �̹���  0 : ���� 1 : ���� 2 : �ɱ���� 3 : ȭ��
	protected override void InitTextList()
	{
		startText = new int[1] { 0 };

		nowTextNum = -1; nextTextNum = new int[1] { 15 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 14 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = -1; nextTextNum = new int[5] { 1,4,16,27,15 }; nextTextIsAble = new bool[5] { true,true,false,false,true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 0 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 1; nextTextNum = new int[1] { 3 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 2 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 3; nextTextNum = new int[5] { 1,4,16,27,15 }; nextTextIsAble = new bool[5] { true, true, false, false, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 4; nextTextNum = new int[1] { 6 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 5 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 6; nextTextNum = new int[2] { 8, 9 }; nextTextIsAble = new bool[2] { true, false };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 7 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 8; nextTextNum = new int[5] { 1,4,16,27,15 }; nextTextIsAble = new bool[5] { true, true, false, false, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 9; nextTextNum = new int[2] { 3, 11 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 10 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 11; nextTextNum = new int[1] { 3 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 13 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 11; nextTextNum = new int[1] { 15 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 12 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 15; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { false };
		methodSArr = new MethodS[1]
		{
			new MethodS(MethodEnum.ENDPANEL, new int[1] {-1})
		};
		AddTextList();
		nowTextNum = 16; nextTextNum = new int[2] { 18, 19 }; nextTextIsAble = new bool[2] { false, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 17 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 16; nextTextNum = new int[2] { 18,19 }; nextTextIsAble = new bool[2] { false,true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 37 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 18; nextTextNum = new int[2] { 21,22 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 20 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 19; nextTextNum = new int[5] { 1, 4, 16, 27, 15 }; nextTextIsAble = new bool[5] { true, true, false, false, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 21; nextTextNum = new int[1] { 3 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 23 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 })
		};
		AddTextList();
		nowTextNum = 21; nextTextNum = new int[1] { 3 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 24 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 22; nextTextNum = new int[1] { 3 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 25 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 })
		};
		AddTextList();
		nowTextNum = 22; nextTextNum = new int[1] { 3 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 26 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 27; nextTextNum = new int[1] { 28 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 38 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 28; nextTextNum = new int[2] { 30,3 }; nextTextIsAble = new bool[2] { false, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 29 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 30; nextTextNum = new int[1] { 32 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 31 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 32; nextTextNum = new int[1] { 15 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 33 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 32; nextTextNum = new int[1] { 35 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 34 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 35; nextTextNum = new int[1] { 3 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 36 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
	}
}
