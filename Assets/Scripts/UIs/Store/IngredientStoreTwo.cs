using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConversationNS;

// �Ѽ�ȣ �ۼ� 
public class IngredientStoreTwo : Conversation
{

	public static string text0;
	public static string text10;
	public static string text11;
	public static string text12;
	public static bool IsTalk = false;  // ���� ���ΰ� ����� �ѹ��̶� �ߴ��� ����
	public static bool IsGalicQuest = false;    // ���� ������ ���� ����� �ѹ��̶� ������� ����(���� ��� �ذ��ߴ��� ����)
	public static bool OneChanceGalicClear = false;  // ���� ��� �ذ� �� ù ��ȭ 
	public static int NowDate = 1;
	public static byte Ingredient = 0;
	public static short Discount = -1;
	public static int BounsDiscount = 0;
	public static int Contract = 0;

	public IngredientStoreTwo()
	{
		NpcTextStrArr = new string[38]
		{
			"�δ� �� ! ���� ����ϸ� ### ��(��) $$$���� ���� !",	// 0
			"�������! �ż��� ����, ����, ���� �˴ϴ�!",	// 1
			"�������...",	// 2
			"�� �׷��� ����� ������?",	// 3
			"���� ������ �����ؼ�..���� ����� �⺻�� 17�������� 32�������� �÷Ȱŵ��..",	// 4
			"����Ḧ ����Ϸ� �Խ��ϴ�.",	// 5
			"(����� ����.)",	// 6
			"����� ����� �����Ϸ� �Խ��ϴ�.",	// 7
			"� �� ����Ϸ� �Գ���!",	// 8
			"� �� �����Ϸ� �Գ���?",	// 9
			"(��� �� ���� ��� �� $$$����) ������ ����Ϸ� �Խ��ϴ�.",	// 10
			"(��� �� ���� ��� �� $$$����) ���ĸ� ����Ϸ� �Խ��ϴ�.",	// 11
			"(��� �� ���� ��� �� $$$����) ���߸� ����Ϸ� �Խ��ϴ�.",	// 12
			"�����غ��� ��� ���ص� �� �� �����ϴ�.",	// 13
			"������ �����Ϸ� �Խ��ϴ�.",	// 14
			"���ĸ� �����Ϸ� �Խ��ϴ�.",	// 15
			"���߸� �����Ϸ� �Խ��ϴ�.",	// 16
			"(ù ��ȭ�� ���ư���.)�����غ��� ���� ���ص� �� �� �����ϴ�.",	// 17
			"�����մϴ�!",	// 18
			"(ù ��ȭ�� ���ư���.)",	// 19
			"(����.)",	// 20
			"�ٸ� ��൵ �ѷ����Կ�.",	// 21
			"�ƽ��ԵƳ׿�.",	// 22
			"�ٸ� ��൵ �����غ��Կ�.",	// 23
			"���ڱ� ������ �������� ������ ���ϱ�?",	// 24
			"���� � ���ర�� �Ҹ��� ������ ���� �簣 �ٶ���...",	// 25
			"(ù ��ȭ�� ���ư���.)..����",	// 26
			"��ó ������ ���ù����� ���� �Ҹ��� ��� �ִµ�, \n�׻� �츮���� ������ ���� ���� ������.",	// 27
			"���� ���� �ɱ��?",	// 28
			"�׷�����..! �����ϴ� ���� ��̷� �ϴ� ���ڶ�� �ϴ��󱸿�.",	// 29
			"������! ���� �Ҹ��� ���� ������ �����̴ּٸ鼭��?",	// 30
			"�� �� �ƴ� ���̾����ϴ�.",	// 31
			"������ �ڱ� �ϵ� �ƴѵ� �̷��Ա��� ������ �ذ����ּ���. ���� �����մϴ�!",	// 32
			"��..�� �̷� ���� ������������ ���� �ʽ��ϴ�.",	// 33
			"(����.)",	// 34
			"�翬�� ���縦 ǥ�ؾ���! \n�������� ��ſ��Դ� 2������ ������ ������� ����� �帱�Կ�!",	// 35
			"���� �����Ѱ� �ƴѰ���?",	// 36
			"(ù ��ȭ�� ���ư���.)�����մϴ�"	// 37
		};

		text0 = NpcTextStrArr[0];
		text10 = NpcTextStrArr[10];
		text11 = NpcTextStrArr[11];
		text12 = NpcTextStrArr[12];

		if (Constant.NowDate == 1)
        {
			IsTalk = false;
			IsGalicQuest = false;
			OneChanceGalicClear = false;
			NowDate = 1;
			Ingredient = 0;
			Discount = 0;
			BounsDiscount = 0;
			Contract = 0;
		}

		if (Constant.NowDate != NowDate || Constant.NowDate == 1)
		{
			Ingredient = (byte)Random.Range(0, 3);
			Discount = (byte)Random.Range(0, 3);
			NowDate = Constant.NowDate;

			NpcTextStrArr[0] = text0;

			string s = null;
			string c = null;
			if (Ingredient == 0) { s = "����"; }
			else if (Ingredient == 1) { s = "����"; }
			else if (Ingredient == 2) { s = "����"; }

			if (Discount == 0) { c = "1"; }
			else if (Discount == 1) { c = "2"; }
			else if (Discount == 2) { c = "3"; }

			NpcTextStrArr[0] = NpcTextStrArr[0].Replace("###", s);
			NpcTextStrArr[0] = NpcTextStrArr[0].Replace("$$$", c);
		}

		TextList = new List<TextNodeC>();
		InitTextList();
		InitStartText();
	}
	protected override void InitStartText()
	{

		if (OneChanceGalicClear)
		{
			startText = new int[1] { 30 };
			//OneChanceGalicClear = false;
		}
		else
		{
			if (!LuckyStore.ClearGalicQuest)
			{
				startText = new int[1] { 2 };
			}
			else
			{
				startText = new int[2] { 0, 1 };
			}
		}
	}
	protected override void InitPlayerSelectText()
	{
		int n = 0;
		NpcTextStrArr[10] = text10;
		NpcTextStrArr[11] = text11;
		NpcTextStrArr[12] = text12;

		if (!LuckyStore.ClearGalicQuest)
		{
			n = (320000 - (((Discount + 1) * 10000) + BounsDiscount))/10000;
			NpcTextStrArr[10] = NpcTextStrArr[10].Replace("$$$", n.ToString());
		}
		else
		{
			n = (170000 - (((Discount + 1) * 10000) + BounsDiscount))/10000;
			NpcTextStrArr[10] = NpcTextStrArr[10].Replace("$$$", n.ToString());
		}

		n = (200000 - (((Discount + 1) * 10000) + BounsDiscount))/10000;
		NpcTextStrArr[11] = NpcTextStrArr[11].Replace("$$$", n.ToString());

		n = (230000 - (((Discount + 1) * 10000) + BounsDiscount))/10000;
		NpcTextStrArr[12] = NpcTextStrArr[12].Replace("$$$", n.ToString());
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
			if (OneChanceGalicClear && LuckyStore.ClearGalicQuest)
			{
				index = TextList.FindIndex(a => a.NowTextNum == -1 && System.Linq.Enumerable.SequenceEqual(a.NextTextNum,new int[2] {33,31}));
			}
			else if (!OneChanceGalicClear && LuckyStore.ClearGalicQuest)
			{
				index = Findidx(-1, new int[1] { Random.Range(0, 2) });
			}
			else
			{
				index = Findidx(-1, new int[1] { 2 });
			}
			SettingConversation(index);
			OneChanceGalicClear = false;
			index = -100;
		}
		else if (temInt == 10)
		{
			//NpcTextStrArr[10] = text10;

			if (!LuckyStore.ClearGalicQuest)
			{
				int n = 320000 - (((Discount + 1) * 10000) + BounsDiscount);
				//NpcTextStrArr[10] = NpcTextStrArr[10].Replace("$$$", n.ToString());

				if (GameManager.Instance.Money >= n)
				{
					GameManager.Instance.Money -= n;
					Constant.UsableIngredient.Add(13);
					Contract++;
					index = Findidx(10, new int[1] { 18 });
				}
				else
				{
					index = Findidx(10, new int[1] { 36 });
				}
			}
			else
			{
				int n = 170000 - (((Discount + 1) * 10000) + BounsDiscount);
				//NpcTextStrArr[10] = NpcTextStrArr[10].Replace("$$$", n.ToString());

				if (GameManager.Instance.Money >= n)
				{
					GameManager.Instance.Money -= n;
					Constant.UsableIngredient.Add(13);
					Contract++;
					index = Findidx(10, new int[1] { 18 });
				}
				else
				{
					index = Findidx(10, new int[1] { 36 });
				}
			}
		}
		else if (temInt == 11)
		{
			//NpcTextStrArr[11] = text11;

			int n = 200000 - (((Discount + 1) * 10000) + BounsDiscount);
			//NpcTextStrArr[11] = NpcTextStrArr[11].Replace("$$$", n.ToString());

			if (GameManager.Instance.Money >= n)
			{
				GameManager.Instance.Money -= n;
				Constant.UsableIngredient.Add(14);
				Contract++;
				index = Findidx(11, new int[1] { 18 });
			}
			else
			{
				index = Findidx(11, new int[1] { 36 });
			}
		}
		else if (temInt == 12)
		{
			//NpcTextStrArr[12] = text12;

			int n = 230000 - (((Discount + 1) * 10000) + BounsDiscount);
			//NpcTextStrArr[12] = NpcTextStrArr[12].Replace("$$$", n.ToString());

			if (GameManager.Instance.Money >= n)
			{
				GameManager.Instance.Money -= n;
				Constant.UsableIngredient.Add(15);
				Contract++;
				index = Findidx(12, new int[1] { 18 });
			}
			else
			{
				index = Findidx(12, new int[1] { 36 });
			}
		}
		else if (temInt == 13)
		{
			if (!LuckyStore.ClearGalicQuest)
			{
				index = Findidx(-1, new int[1] { 2 });
			}
			else if (Random.Range(0,2) == 0)
			{
				index = Findidx(-1, new int[1] { 1 });
			}
			else
			{
				index = Findidx(-1, new int[1] { 0 });
			}
		}
		else if (temInt == 14)
		{
			Constant.UsableIngredient.Remove(13);
			Contract--;
			SettingConversation(Findidx(14, new int[1] { 22 }));
			index = -100;
		}
		else if (temInt == 15)
		{
			Constant.UsableIngredient.Remove(14);
			Contract--;
			SettingConversation(Findidx(15, new int[1] { 22 }));
			index = -100;
		}
		else if (temInt == 16)
		{
			Constant.UsableIngredient.Remove(15);
			Contract--;
			SettingConversation(Findidx(16, new int[1] { 22 }));
			index = -100;
		}
		else if (temInt == 17)
		{
			if (!LuckyStore.ClearGalicQuest)
			{
				index = Findidx(-1, new int[1] { 2 });
			}
			else if (Random.Range(0, 2) == 0)
			{
				index = Findidx(-1, new int[1] { 1 });
			}
			else
			{
				index = Findidx(-1, new int[1] { 0 });
			}
		}
		else if (temInt == 19)
		{
			if (!LuckyStore.ClearGalicQuest)
			{
				index = Findidx(-1, new int[1] { 2 });
			}
			else if (Random.Range(0, 2) == 0)
			{
				index = Findidx(-1, new int[1] { 1 });
			}
			else
			{
				index = Findidx(-1, new int[1] { 0 });
			}
		}
		else if (temInt == 26)
		{
			if (!LuckyStore.ClearGalicQuest)
			{
				index = Findidx(-1, new int[1] { 2 });
			}
			else if (Random.Range(0, 2) == 0)
			{
				index = Findidx(-1, new int[1] { 1 });
			}
			else
			{
				index = Findidx(-1, new int[1] { 0 });
			}
		}
		else if (temInt == 24)
		{
			IsGalicQuest = true;
			SettingConversation(Findidx(24, new int[1] { 25 }));
			index = -100;
		}
		else if (temInt == 28)
		{
			IsTalk = true;
			SettingConversation(Findidx(28, new int[1] { 29 }));
			index = -100;
		}
		else if (temInt == 37)
		{
			BounsDiscount = 20000;

			if (!LuckyStore.ClearGalicQuest)
			{
				index = Findidx(-1, new int[1] { 2 });
			}
			else if (Random.Range(0, 2) == 0)
			{
				index = Findidx(-1, new int[1] { 1 });
			}
			else
			{
				index = Findidx(-1, new int[1] { 0 });
			}
			SettingConversation(index);
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
		if (num == 7)
		{
			if (Contract <= 0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		else if (num == 10)
		{
			if (Constant.UsableIngredient.FindIndex(a => a == 13) != -1)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		else if (num == 11)
		{
			if (Constant.UsableIngredient.FindIndex(a => a == 14) != -1)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		else if (num == 12)
		{
			if (Constant.UsableIngredient.FindIndex(a => a == 15) != -1)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		else if (num == 14)
		{
			if (Constant.UsableIngredient.FindIndex(a => a== 13) != -1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if (num == 15)
		{
			if (Constant.UsableIngredient.FindIndex(a => a== 14) != -1)
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
			if (Constant.UsableIngredient.FindIndex(a => a==15) != -1)
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
	/// �ؽ�Ʈ���� �����ؼ� �׷����� ����
	/// </summary>
	/// ���� ���� �̹���  0 : ���� 1 : ���� 2 : �ɱ���� 3 : �ǹ̽���
	protected override void InitTextList()
	{
		nowTextNum = -1; nextTextNum = new int[4] { 5,7,6,34 }; nextTextIsAble = new bool[4] { true,false,true,true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 0 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = -1; nextTextNum = new int[4] { 5, 7, 6, 34 }; nextTextIsAble = new bool[4] { true, false, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = -1; nextTextNum = new int[5] { 5, 7, 6, 34, 3 }; nextTextIsAble = new bool[5] { true, false, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 2 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = -1; nextTextNum = new int[2] { 33,31 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 30 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 3; nextTextNum = new int[1] { 24 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 4 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 5; nextTextNum = new int[4] { 10, 11, 12, 13 }; nextTextIsAble = new bool[4] { false, false, false, true };
		methodSArr = new MethodS[6]
		{
			new MethodS(MethodEnum.INITPLAYERTEXT, new int[0]),
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 8 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 6; nextTextNum = new int[1] { 28 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 27 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 7; nextTextNum = new int[4] { 14, 15, 16, 17 }; nextTextIsAble = new bool[4] { false, false, false, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 9 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 10; nextTextNum = new int[3] { 19, 21, 20 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 18 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 10; nextTextNum = new int[3] { 19, 21, 20 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 36 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 11; nextTextNum = new int[3] { 19, 21, 20 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 18 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 11; nextTextNum = new int[3] { 19, 21, 20 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 36 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 12; nextTextNum = new int[3] { 19, 21, 20 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 18 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 12; nextTextNum = new int[3] { 19, 21, 20 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 36 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 13; nextTextNum = new int[4] { 5, 7, 6, 34 }; nextTextIsAble = new bool[4] { true, false ,true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 })
		};
		AddTextList();
		nowTextNum = 13; nextTextNum = new int[5] { 5, 7, 6, 34, 3 }; nextTextIsAble = new bool[5] { true, false, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 })
		};
		AddTextList();
		nowTextNum = 14; nextTextNum = new int[3] { 19, 23, 20 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 22 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 15; nextTextNum = new int[3] { 19, 23, 20 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 22 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 16; nextTextNum = new int[3] { 19, 23, 20 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 22 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 17; nextTextNum = new int[4] { 5, 7, 6, 34 }; nextTextIsAble = new bool[4] { true, false, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 })
		};
		AddTextList();
		nowTextNum = 17; nextTextNum = new int[5] { 5, 7, 6, 34, 3 }; nextTextIsAble = new bool[5] { true, false, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 })
		};
		AddTextList();
		nowTextNum = 19; nextTextNum = new int[4] { 5, 7, 6, 34 }; nextTextIsAble = new bool[4] { true, false, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 })
		};
		AddTextList();
		nowTextNum = 19; nextTextNum = new int[5] { 5, 7, 6, 34, 3 }; nextTextIsAble = new bool[5] { true, false, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 })
		};
		AddTextList();
		nowTextNum = 20; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[1]
		{
			new MethodS(MethodEnum.ENDPANEL, new int[1] { -1 })
		};
		AddTextList();
		nowTextNum = 21; nextTextNum = new int[4] { 10, 11, 12, 13 }; nextTextIsAble = new bool[4] { false, false, false, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 8 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 23; nextTextNum = new int[4] { 14, 15, 16, 17 }; nextTextIsAble = new bool[4] { false, false, false, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 9 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 24; nextTextNum = new int[1] { 26 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 25 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 26; nextTextNum = new int[4] { 5, 7, 6, 34 }; nextTextIsAble = new bool[4] { true, false, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 })
		};
		AddTextList();
		nowTextNum = 26; nextTextNum = new int[5] { 5, 7, 6, 34, 3 }; nextTextIsAble = new bool[5] { true, false, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 })
		};
		AddTextList();
		nowTextNum = 28; nextTextNum = new int[1] { 19 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 29 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 31; nextTextNum = new int[4] { 5, 7, 6, 34 }; nextTextIsAble = new bool[4] { true, false, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 32 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 })
		};
		AddTextList();
		nowTextNum = 33; nextTextNum = new int[1] { 19 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 35 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 34; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { false };
		methodSArr = new MethodS[1]
		{
			new MethodS(MethodEnum.ENDPANEL, new int[1] { -1 })
		};
		AddTextList();
		nowTextNum = 37; nextTextNum = new int[4] { 5, 7, 6, 34 }; nextTextIsAble = new bool[4] { true, false, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 })
		};
		AddTextList();
	}
}
