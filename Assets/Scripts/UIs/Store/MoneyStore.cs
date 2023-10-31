using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConversationNS;

// �Ѽ�ȣ �ۼ�

public class MoneyStore : Conversation
{
	public static bool IsTalk = false;  // �����ü�� ��ӴϿ� ���� �̾߱⸦ �ߴ��� ����
	public static bool StartSonQuest = false;   // ���� ���� ����Ʈ�� �����ߴ��� ����
	public static bool OneChanceClearSon = false;   // Ŭ���� ���� ù ���
	public static bool IsTalkOneChanceDiscount = false;	// �ѹ��� ���ؼ� ���ڸ� ���� �� ����. true�� �Ǹ� ���� �õ� �̹� �� ��.
	public static Dictionary<int, int> BorrowMoneyDate = new Dictionary<int, int>();    // ������ ��¥, �ݾ�
	public static Dictionary<int, int> PayMoneyDate = new Dictionary<int, int>();	// ������ ��¥, ���� �ݾ�
	public static int SumBorrow = 0;    // �� ���� �ݾ�
	public static float PlusMoney = 1.1f;   // ���� ���� ���� 
	public static int NowDate = 1;  // ��¥
	public static int ClearMoney = 0;	// ����Ʈ ������ �޴� ��
	public MoneyStore()
	{
		NpcTextStrArr = new string[57]
        {
			"�մ�~ ����� ���̴� ���� �ϰ� �ֳ׿�.",	// 0
			"��. ���� �� ���ó���� �Ⱥ��̴µ�?",	// 1
			"���� ������ �Խ��ϴ�.",	// 2
			"(����.)",	// 3
			"���̰� �մ� �󸶳� ������ �Խ��ϱ�?\n(���� �Ϸ� �� ���� �� �ִ� �ݾ� $$$��)",		// 4
			"(100���� ������.)",	// 5
			"(200���� ������.)",	// 6
			"(300���� ������.)",	// 7
			"(400���� ������.)",	// 8
			"(ù ��ȭ�� ���ư���.)�����غ��� �� ������ �� �� �����ϴ�.",	// 9
			"���� ��ü�� ������ּż� �����ϰ��, ���̰�. ���ڴ�\n ������ ���� 1%�� �����Ѵٴ� �� �˾��ּ���~",		// 10
			"(ù ��ȭ�� ���ư���.)�� �˰ڽ��ϴ�.)",	// 11
			"���� �׷� �̾߱� ��������.",	// 12
			"�� �� �����ڽ��ϴ�.",	// 13
			"�մ�. ���� ���ļ� ����մϱ�?",	// 14
			"(�ֻ��� �� 15�̻�)�׷������� ���� ����ּ���.",		// 15
			"(ù ��ȭ�� ���ư���.)�˼��մϴ�. �ѹ� �����غþ��.",	// 16
			"�մ�~���� �ؽó�..���ƿ�. ���� 0.5% ������ �����Կ�.",	// 17
			"(ü�� 40 �Ҹ�, ü���� 40���ϸ� 1�� ����� ���� �Ҹ�)\n�մ�~�ʹ� �����ϸ� �� �´� ���� �־��?",	// 18
			"���� ������ �Խ��ϴ�.",	// 19
			"���̰� �մ�~ �󸶳� ������ �Գ���?(���ƾ� �� �ݾ� $$$��)",	// 20
			"(100���� ���´�.)",	// 21
			"(200���� ���´�.)",	// 22
			"(400���� ���´�.)",	// 23
			"(ù ��ȭ�� ���ư���.)�����غ��� �� ���Ƶ� �� �� �����ϴ�.",	// 24
			"(���� �ݾ� ���� ���´�.)",	// 25
			"�մ�~���ǵ� �Ͻó�. �� �� ������ ��ŵ� �Ǵµ�",	// 26
			"(ù ��ȭ�� ���ư���.)",	// 27
			"�� �� ������ �ֽ��ϴ�.",	// 28
			"�� ����� �� �ι��� �����ΰ���?",	// 29
			"���� �� �β�����. ���� ��ӴϿ���.",		// 30
			"ȿ���� �����ϳ׿�.",	// 31
			"��..�׷��� �ص� �˾��ֽ��� ������ ��..��� ���� ��Ӵϰ� �� �ٹ濡�� \n������ �Ͻðŵ��. �ٵ� ���� ��� �Ⱦ��Ͻô���..",	// 32
			"������ �ֽ��ϱ�?",		// 33
			"���� ����. ���� ���ڲ��� �帮�µ�. �����ٰ� ������ ġ��ġ�� \n�޾Ƴ����ż�..�˷����� ������ ��ó�� �����ϴ�.",		// 34
			"���� ���͵帮�ڽ��ϴ�.",	// 35
			"����? ��ʸ� �ٶ�°���? ���� ������ ���ۿ� ������...�󸶸� ����?",	// 36
			"(100������ �䱸�Ѵ�.)",	// 37
			"(�ֻ��� �� 4 �̻�)(200������ �䱸�Ѵ�.)",	// 38
			"(�ֻ��� �� 8 �̻�)(400������ �䱸�Ѵ�.)",	// 39
			"(�ֻ��� �� 16 �̻�)(800������ �䱸�Ѵ�.)",		// 40
			"(�ֻ��� �� 30 �̻�)(1600������ �䱸�Ѵ�.)",	// 41
			"���ƿ�. �׷��� �ҰԿ�.",		// 42
			"�׷��� ���� ���� �� �� �����. 100�������� ��������.",	// 43
			"��Ӵ� ���� �̾߱��Դϴ�.",	// 44
			"���� �ذ����� ���߳���? �ƴϸ� ���� ���Ѱǰ���?",		// 45
			"������. ���п� ��Ӵϸ� ��⸸�� ���� ���� ��..\n��ʱ��� ���� �־��. ��������.",	// 46
			"(ù ��ȭ�� ���ư���.)�� �� ��ٷ��ּ���.",		// 47
			"(ù ��ȭ�� ���ư���.)������ �ްڽ��ϴ�.",		// 48
			"������ ��� ��������.",	// 49
			"���п� ȭ���ϰ� ������ ����~.������ �Ļ絵 �ϰ��. \n��Ӵϰ� ������ �����ϴ��� �����ٴϱ��.",	// 50
			"(ù ��ȭ�� ���ư���.)�����̳׿�.",	// 51
			"���� ��Ģ�� ��� �ǳ���?",	// 52
			"����~����� �Ϸ翡 3õ�������� ������ �� �ְ��. \n�ִ� �ѵ��� 5õ�����Դϴ�. �׸���..",		// 53
			"�׸���?",	// 54
			"���� �Ⱓ ���� 1���� �ð��� �帱�ٵ�..\n�Ⱓ�� ������ �մ��� ���� ���� ���� �� �������� ���������ٴ� �� ���� ������.",		// 55
			"(ù ��ȭ�� ���ư���.)�˰ڽ��ϴ�.",	// 56
        };

		if (Constant.NowDate != NowDate)
		{
			Constant.NowDate = NowDate;
			List<int> li = new List<int>();
			foreach (var key in PayMoneyDate.Keys)
			{
				PayMoneyDate[key]++;
				li.Add(key);
			}
			for (int i = 0; i < li.Count; i++)
			{
				PayMoneyDate[li[i]] = (int)(PayMoneyDate[li[i]] * PlusMoney);
			}
		}

		TextList = new List<TextNodeC>();
		InitTextList();
		InitStartText();
	}

	protected override void InitStartText()
    {
		if (GameManager.Instance.Money < 20000000)
        {
			startText = new int[1] { 0 };
        }
		else if (GameManager.Instance.Money >= 20000000)
        {
			startText = new int[1] { 1 };
        }
    }
	/// <summary>
	/// �÷��̾��� ���¿� ���� ��ȭ ���� ����
	/// </summary>
	/// <param name="num"></param>
	/// <returns></returns>
	protected override bool Condition(int num)
	{
		if (num == 5)
        {
			if (!BorrowMoneyDate.ContainsKey(Constant.NowDate))
			{
				if (SumBorrow <= 50000000 - 30000000) { return true; }
				else
				{
					if (50000000 - SumBorrow <= 1000000) { return true; }
					else { return false; }
				}
			}
			else
			{
				if (SumBorrow <= 50000000 - 30000000)
				{
					if (30000000 - BorrowMoneyDate[Constant.NowDate] <= 1000000) { return true; }
					else { return false; }
				}
				else
				{
					if ((50000000 - SumBorrow) - BorrowMoneyDate[Constant.NowDate] <= 1000000) { return true; }
					else { return false; }
				}
			}
		}
		else if (num == 6)
        {
			if (!BorrowMoneyDate.ContainsKey(Constant.NowDate))
			{
				if (SumBorrow <= 50000000 - 30000000) { return true; }
				else
				{
					if (50000000 - SumBorrow <= 2000000) { return true; }
					else { return false; }
				}
			}
			else
			{
				if (SumBorrow <= 50000000 - 30000000)
				{
					if (30000000 - BorrowMoneyDate[Constant.NowDate] <= 2000000) { return true; }
					else { return false; }
				}
				else
				{
					if ((50000000 - SumBorrow) - BorrowMoneyDate[Constant.NowDate] <= 2000000) { return true; }
					else { return false; }
				}
			}
		}
		else if (num == 7)
        {
			if (!BorrowMoneyDate.ContainsKey(Constant.NowDate))
			{
				if (SumBorrow <= 50000000 - 30000000) { return true; }
				else
				{
					if (50000000 - SumBorrow <= 3000000) { return true; }
					else { return false; }
				}
			}
			else
			{
				if (SumBorrow <= 50000000 - 30000000)
				{
					if (30000000 - BorrowMoneyDate[Constant.NowDate] <= 3000000) { return true; }
					else { return false; }
				}
				else
				{
					if ((50000000 - SumBorrow) - BorrowMoneyDate[Constant.NowDate] <= 3000000) { return true; }
					else { return false; }
				}
			}
		}
		else if (num == 8)
        {
			if (!BorrowMoneyDate.ContainsKey(Constant.NowDate))
			{
				if (SumBorrow <= 50000000 - 30000000) { return true; }
				else
				{
					if (50000000 - SumBorrow <= 4000000) { return true; }
					else { return false; }
				}
			}
			else
			{
				if (SumBorrow <= 50000000 - 30000000)
				{
					if (30000000 - BorrowMoneyDate[Constant.NowDate] <= 4000000) { return true; }
					else { return false; }
				}
				else
				{
					if ((50000000 - SumBorrow) - BorrowMoneyDate[Constant.NowDate] <= 4000000) { return true; }
					else { return false; }
				}
			}
		}
		else if (num == 12)
        {
			if (!IsTalkOneChanceDiscount)
            {
				return true;
            }
			else
            {
				return false;
            }
        }
		else if (num == 21)
        {
			int money = 0;
			foreach (var key in PayMoneyDate.Keys)
            {
				money += PayMoneyDate[key];
            }

			if (money >= 1000000 && GameManager.Instance.Money >= 1000000)
            {
				return true;
            }
			else
            {
				return false;
            }
        }
		else if (num == 22)
        {
			int money = 0;
			foreach (var key in PayMoneyDate.Keys)
			{
				money += PayMoneyDate[key];
			}

			if (money >= 2000000 && GameManager.Instance.Money >= 2000000)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if (num == 23)
        {
			int money = 0;
			foreach (var key in PayMoneyDate.Keys)
			{
				money += PayMoneyDate[key];
			}

			if (money >= 4000000 && GameManager.Instance.Money >= 4000000)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if (num == 25)
        {
			int money = 0;
			foreach (var key in PayMoneyDate.Keys)
			{
				money += PayMoneyDate[key];
			}

			if (money <= GameManager.Instance.Money)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if (num == 29)
		{
			if (!StartSonQuest)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if (num == 44)
        {
			if (StartSonQuest && !OneChanceClearSon)
            {
				return true;
            }
			else
            {
				return false;
            }
        }
		else if (num == 49)
		{
			if (LuckyStore.ClearSonQuest && StartSonQuest)
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
	/// ���ǿ� ���� ��ȭ �б���
	/// </summary>
	/// <param name="tem"></param>
	/// <returns></returns>
	protected override int Bifurcation(List<TextNodeC> tem)
    {
		int index = -1;
		temInt = tem[0].NowTextNum;

		if (temInt == 2)
        {
			if (!BorrowMoneyDate.ContainsKey(Constant.NowDate))
            {
				if (SumBorrow <= 50000000 - 30000000)
                {
					SettingConversation(Findidx(2, new int[1] { 4 }), 30000000);
                }
				else
                {
					SettingConversation(Findidx(2, new int[1] { 4 }), (50000000 - SumBorrow) );
                }
            }
			else
            {
				if (SumBorrow <= 50000000 - 30000000)
                {
					SettingConversation(Findidx(2, new int[1] { 4 }), 30000000 - BorrowMoneyDate[Constant.NowDate]);
                }
				else
                {
					SettingConversation(Findidx(2, new int[1] { 4 }), (50000000 - SumBorrow) - BorrowMoneyDate[Constant.NowDate]);
                }
            }
			index = -100;
        }
		else if (temInt == 5)
        {
			if (BorrowMoneyDate.ContainsKey(Constant.NowDate))
            {
				BorrowMoneyDate[Constant.NowDate] += 1000000;
            }
			else
            {
				BorrowMoneyDate.Add(Constant.NowDate, 1000000);
            }
			SettingConversation(Findidx(5, new int[1] { 10 }));
			index = -100;
        }
		else if (temInt == 6)
        {
			if (BorrowMoneyDate.ContainsKey(Constant.NowDate))
			{
				BorrowMoneyDate[Constant.NowDate] += 2000000;
			}
			else
			{
				BorrowMoneyDate.Add(Constant.NowDate, 2000000);
			}
			SettingConversation(Findidx(6, new int[1] { 10 }));
			index = -100;
		}
		else if (temInt == 7)
        {
			if (BorrowMoneyDate.ContainsKey(Constant.NowDate))
			{
				BorrowMoneyDate[Constant.NowDate] += 3000000;
			}
			else
			{
				BorrowMoneyDate.Add(Constant.NowDate, 3000000);
			}
			SettingConversation(Findidx(7, new int[1] { 10 }));
			index = -100;
		}
		else if (temInt == 8)
        {
			if (BorrowMoneyDate.ContainsKey(Constant.NowDate))
			{
				BorrowMoneyDate[Constant.NowDate] += 4000000;
			}
			else
			{
				BorrowMoneyDate.Add(Constant.NowDate, 4000000);
			}
			SettingConversation(Findidx(8, new int[1] { 10 }));
			index = -100;
		}
		else if (temInt == 9)
        {
			SettingConversation(Findidx(9, new int[1] { -1 }));
			index = -100;
        }
		else if (temInt == 15)
        {
			IsTalkOneChanceDiscount = true;
            DiceRoll(15);
			index = -100;
        }
		else if (temInt == 19)
        {
			int n = 0;
			foreach (var key in PayMoneyDate.Keys)
            {
				n += PayMoneyDate[key];
            }

			SettingConversation(Findidx(19, new int[1] { 20 }), n);
			index = -100;
        }
		else if (temInt == 21)
        {
			int m = 1000000;
			while (true)
            {
				int k = 0;
				foreach (var key in PayMoneyDate.Keys)
                {
					k = key;
					break;
                }
				if (PayMoneyDate[k] <= m)
                {
					m -= PayMoneyDate[k];
					PayMoneyDate.Remove(k);

					if (m == 0) { break; }
                }
				else
                {
					PayMoneyDate[k] -= m;
					break;
                }
            }

			SettingConversation(Findidx(21, new int[26]));
			index = -100;
        }
		else if (temInt == 22)
        {
			int m = 2000000;
			while (true)
			{
				int k = 0;
				foreach (var key in PayMoneyDate.Keys)
				{
					k = key;
					break;
				}
				if (PayMoneyDate[k] <= m)
				{
					m -= PayMoneyDate[k];
					PayMoneyDate.Remove(k);

					if (m == 0) { break; }
				}
				else
				{
					PayMoneyDate[k] -= m;
					break;
				}
			}

			SettingConversation(Findidx(22, new int[26]));
			index = -100;
		}
		else if (temInt == 23)
        {
			int m = 4000000;
			while (true)
			{
				int k = 0;
				foreach (var key in PayMoneyDate.Keys)
				{
					k = key;
					break;
				}
				if (PayMoneyDate[k] <= m)
				{
					m -= PayMoneyDate[k];
					PayMoneyDate.Remove(k);

					if (m == 0) { break; }
				}
				else
				{
					PayMoneyDate[k] -= m;
					break;
				}
			}

			SettingConversation(Findidx(23, new int[26]));
			index = -100;
		}
		else if (temInt == 24)
        {
			SettingConversation(Findidx(24, new int[1] { 26 }));
        }
		else if (temInt == 25)
		{
			int m = 0;
			foreach (var key in PayMoneyDate.Keys)
            {
				m += PayMoneyDate[key];
            }
			while (true)
			{
				int k = 0;
				foreach (var key in PayMoneyDate.Keys)
				{
					k = key;
					break;
				}
				if (PayMoneyDate[k] <= m)
				{
					m -= PayMoneyDate[k];
					PayMoneyDate.Remove(k);

					if (m == 0) { break; }
				}
				else
				{
					PayMoneyDate[k] -= m;
					break;
				}
			}

			SettingConversation(Findidx(25, new int[1] { 26 }));
			index = -100;
		}
		else if (temInt == 35)
        {
			StartSonQuest = true;
			SettingConversation(Findidx(35, new int[1] { 36 }));
			index = -100;
        }
		else if (temInt == 37)
        {
			ClearMoney = 1000000;
			SettingConversation(Findidx(37, new int[1] { 42 }));
			index = -100;
        }
		else if (temInt == 38)
        {
			DiceRoll(4);
			index = -100;
        }
		else if (temInt == 39)
        {
			DiceRoll(8);
			index = -100;
        }
		else if (temInt == 40)
        {
			DiceRoll(16);
			index = -100;
        }
		else if (temInt == 41)
        {
			DiceRoll(30);
			index = -100;
        }
		else if (temInt == 44)
        {
			if (LuckyStore.ClearSonQuest)
            {
				index = Findidx(44, new int[1] { 45 });
            }
			else
            {
				index = Findidx(44, new int[1] { 46 });
            }
        }
		else if (temInt == 48)
        {
			GameManager.Instance.Money += ClearMoney;
			SettingConversation(Findidx(48, new int[1] { -1 }));
			index = -100;
        }

		return index;
	}
	/// <summary>
	/// �ֻ��� ����� ���� ��ȭ �б���
	/// </summary>
	/// <param name="bo"></param>
	public override void DiceResult(bool bo)
    {
		if (temInt == 15)
        {
			if (bo)
            {
				PlusMoney = 0.5f;
				SettingConversation(Findidx(15, new int[1] { 17 }));
            }
			else
            {
				if (PlayerStat.HP < 40) { PlayerStat.HP = 1; }
				else { PlayerStat.HP -= 40; }
				SettingConversation(Findidx(15, new int[1] { 18 }));
            }
        }
		else if (temInt == 38)
        {
			if (bo)
            {
				ClearMoney = 2000000;
				SettingConversation(Findidx(38, new int[1] { 42 }));
            }
			else
            {
				ClearMoney = 1000000;
				SettingConversation(Findidx(38, new int[1] { 43 }));
            }
        }
		else if (temInt == 39)
        {
			if (bo)
			{
				ClearMoney = 4000000;
				SettingConversation(Findidx(39, new int[1] { 42 }));
			}
			else
			{
				ClearMoney = 1000000;
				SettingConversation(Findidx(39, new int[1] { 43 }));
			}
		}
		else if (temInt == 40)
        {
			if (bo)
			{
				ClearMoney = 8000000;
				SettingConversation(Findidx(40, new int[1] { 42 }));
			}
			else
			{
				ClearMoney = 1000000;
				SettingConversation(Findidx(40, new int[1] { 43 }));
			}
		}
		else if (temInt == 41)
        {
			if (bo)
			{
				ClearMoney = 16000000;
				SettingConversation(Findidx(41, new int[1] { 42 }));
			}
			else
			{
				ClearMoney = 1000000;
				SettingConversation(Findidx(41, new int[1] { 43 }));
			}
		}
    }
	/// <summary>
	/// �ؽ�Ʈ���� �����ؼ� �׷����� ����
	/// </summary>
	/// ���� ���� �̹���  0 : ���� 1 : ���� 2 : �ɱ���� 3 : ȭ��
	protected override void InitTextList()
    {
		startText = new int[1] { 0 };

		nowTextNum = -1; nextTextNum = new int[7] { 52, 49, 29, 44, 2, 19, 3}; nextTextIsAble = new bool[7] { true, false, false, false, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 700 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 2; nextTextNum = new int[5] { 5,6,7,8,9 }; nextTextIsAble = new bool[5] { false, false, false, false, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 4 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 3; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { true  };
		methodSArr = new MethodS[1]
		{
			new MethodS(MethodEnum.ENDPANEL, new int[1] { -1 })
		};
		AddTextList();
		nowTextNum = 5; nextTextNum = new int[3] { 12, 11, 13 }; nextTextIsAble = new bool[3] { false, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 10 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } )
		};
		AddTextList();
		nowTextNum = 6; nextTextNum = new int[3] { 12, 11, 13 }; nextTextIsAble = new bool[3] { false, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 10 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 7; nextTextNum = new int[3] { 12, 11, 13 }; nextTextIsAble = new bool[3] { false, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 10 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 8; nextTextNum = new int[3] { 12, 11, 13 }; nextTextIsAble = new bool[3] { false, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 10 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 9; nextTextNum = new int[7] { 52, 49, 29, 44, 2, 19, 3 }; nextTextIsAble = new bool[7] { true, false, false, false, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 700 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum =11; nextTextNum = new int[7] { 52, 49, 29, 44, 2, 19, 3 }; nextTextIsAble = new bool[7] { true, false, false, false, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 700 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 12; nextTextNum = new int[2] { 15, 16 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 14 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 })
		};
		AddTextList();
		nowTextNum = 13; nextTextNum = new int[5] { 5, 6, 7, 8, 9 }; nextTextIsAble = new bool[5] { false, false, false, false, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 4 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 15; nextTextNum = new int[1] { 11 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 17 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 }),
		};
		AddTextList();
		nowTextNum = 15; nextTextNum = new int[1] { 3 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 18 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 }),
		};
		AddTextList();
		nowTextNum = 16; nextTextNum = new int[7] { 52, 49, 29, 44, 2, 19, 3 }; nextTextIsAble = new bool[7] { true, false, false, false, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 700 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 19; nextTextNum = new int[5] { 21, 22, 23, 24, 25 }; nextTextIsAble = new bool[5] { false, false, false, true, false};
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 20 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 21; nextTextNum = new int[2] { 27, 28 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 26 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
		};
		AddTextList();
		nowTextNum = 22; nextTextNum = new int[2] { 27, 28 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 26 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
		};
		AddTextList();
		nowTextNum = 23; nextTextNum = new int[2] { 27, 28 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 26 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
		};
		AddTextList();
		nowTextNum = 24; nextTextNum = new int[7] { 52, 49, 29, 44, 2, 19, 3 }; nextTextIsAble = new bool[7] { true, false, false, false, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 700 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 25; nextTextNum = new int[2] { 27, 28 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 26 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
		};
		AddTextList();
		nowTextNum = 27; nextTextNum = new int[7] { 52, 49, 29, 44, 2, 19, 3 }; nextTextIsAble = new bool[7] { true, false, false, false, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 700 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 28; nextTextNum = new int[5] { 21, 22, 23, 24, 25 }; nextTextIsAble = new bool[5] { false, false, false, true, false };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 20 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 29; nextTextNum = new int[1] { 31 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 30 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 31; nextTextNum = new int[1] { 33 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 32 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 33; nextTextNum = new int[1] { 35 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 34 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[0])
		};
		AddTextList();
		nowTextNum = 35; nextTextNum = new int[5] { 37, 38, 39, 40, 41 }; nextTextIsAble = new bool[5] { true, true, true, true, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 36 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 500 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 37; nextTextNum = new int[1] { 27 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 42 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 38; nextTextNum = new int[1] { 27 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 42 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 38; nextTextNum = new int[1] { 27 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 43 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 39; nextTextNum = new int[1] { 27 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 42 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 39; nextTextNum = new int[1] { 27 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 43 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 40; nextTextNum = new int[1] { 27 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 42 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 40; nextTextNum = new int[1] { 27 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 43 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 41; nextTextNum = new int[1] { 27 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 42 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 41; nextTextNum = new int[1] { 27 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 43 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 44; nextTextNum = new int[1] { 47 }; nextTextIsAble = new bool[1] { false };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 45 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 44; nextTextNum = new int[1] { 48 }; nextTextIsAble = new bool[1] { false };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 46 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 47; nextTextNum = new int[7] { 52, 49, 29, 44, 2, 19, 3 }; nextTextIsAble = new bool[7] { true, false, false, false, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 700 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 48; nextTextNum = new int[7] { 52, 49, 29, 44, 2, 19, 3 }; nextTextIsAble = new bool[7] { true, false, false, false, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 700 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 49; nextTextNum = new int[1] { 51 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 50 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 51; nextTextNum = new int[7] { 52, 49, 29, 44, 2, 19, 3 }; nextTextIsAble = new bool[7] { true, false, false, false, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 700 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 52; nextTextNum = new int[1] { 54 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 53 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 54; nextTextNum = new int[1] { 56 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 55 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 56; nextTextNum = new int[7] { 52, 49, 29, 44, 2, 19, 3 }; nextTextIsAble = new bool[7] { true, false, false, false, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 700 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
	}
}
