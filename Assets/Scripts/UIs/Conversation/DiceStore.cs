using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConversationNS;

// �Ѽ�ȣ �ۼ�
public class DiceStore : Conversation , ICloseStore
{
	public static bool isOneDayDiceStore = false;
	
	private int DiceLuck = -1;
	private int ItemSumCost = -1;
	private bool isDiscount = true;
	public DiceStore()
	{
		store = new DiceStoreItem(this);

		NpcTextStrArr = new string[39]
		{
			"�λ��� �ֻ����� ���� �� ���� ���̾�...",	// 0
			"�츮 ���� ���� �ֻ����� �������..\n���� �ֻ����� �λ��� �ɰ� �־�.",	// 1
			"������ ��� 6. ���� �ʴ� ���� �ູ�� �ް� �ֱ�.",	// 2
			"������ ��� 5. �ູ���� �Ϸ簡 �ǰھ�.",	// 3
			"������ ��� 4. ���� ���ٰ� �ڸ����� ����.",	// 4
			"������ ��� 3. ���� �Ϸ翡 ����� ��.",	// 5
			"������ ��� 2. ������, �ʿ��� ������ ����� �帥��.",	// 6
			"������ ��� 1. ���� ��� ���ư�. ������ ����� �������ھ�.",	// 7
			"(���ư���.)",	// 8
			"� ������ �Ȱ� �ֳ���?",	// 9
			"��ǰ�� ���� �ͽ��ϴ�.",	// 10
			"(�ֻ��� �� 9 �̻�)������ ���� ��ģ��.",	// 11
			"(����.)",	// 12
			"�ֻ����� �Ȱ� �־�. � ������� ���̿� �Ұ��� �� �ִ� �̰��� \n���������� ��ư��� �� �־� �߿��� ������ ������ ����.",	// 13
			"���� ���� ���� �͵��̾�. \n���� ����� �ñ� �ֻ����� �� �����غ�",	// 14
			"�ƹ��͵� �ƴմϴ�.",	// 15
			"��. ��� ���� �߳�?",	// 16
			"�� ���� �ּ��̰� ������ ��� �־���. �� ����.",	// 17
			"(��������.)",	// 18
			"�� $$$���̾�.",	// 19
			"�� $$$���̾�. ���� ����� �ְ���?",	// 20
			"�� $$$���̾�. �ʹ� �������� �ʾ����� �ϴ±�.",	// 21
			"(���� �����Ѵ�.)",	// 22
			"�˼��մϴ�. ���� �����ϳ׿�.",	// 23
			"������ �� ����൵ �ɱ��?",	//24
			"������ �� ���ڰ�.",	// 25
			"���� ���� ��� �Ǽ��� �ϴϱ�. �ٽ� �� �����غ�.",	// 26
			"����. ��, �̰� �����ϴ� �� �ֻ����� �ɰž�. �� ������.",	// 27
			"�� ���� �ݾ� ����� ���� ������ ���� ���� �־�. ��, �̰� �����ϴ� �� �ֻ����� �ɰž�. \n �� ���� ����� ���� �� �ϴ� ������ �ϵ� �ƴϰ���?",	// 28
			"(����)",	// 29
			"�ٽ� ��󺼰Կ�.",	// 30
			"(�ֻ��� �� 10 �̻�)(�ֻ����� ������.)",	// 31
			"���� �� �þ�. ���� ����.",	// 32
			"�ƽ��� �ƾ�. ��¼�� ��� �߸��� �ɱ�?",	// 33
			"�� ��� �ֽ��ϴ�.",	// 34
			"�ʿ��� ����� �����±�. ���� $$$���� ����.",	// 35
			"�ƽ��� �ƾ�. ������ ���� �ɷ� ����. $$$���̾�.",	// 36
			"(���Ÿ� ����Ѵ�.)",	// 37
			"(�ֻ��� �� 8 �̻�)�ѹ� �غ��ڽ��ϴ�."	// 38
		};

		TextList = new List<TextNodeC>();
		InitTextList();
		InitStartText();
	}
	protected override void InitStartText()
	{
		if (isOneDayDiceStore)
		{
			startText = new int[3] { 0, 1, DiceLuck };
		}
		else
		{
			isDiscount = true;
			//Debug.Log("��ī��Ʈ ��ȭ?");
			DiceLuck = Random.Range(2, 8);
			startText = new int[1] { DiceLuck };
			isOneDayDiceStore = true;
		}
	}
	public override void JumpConversation(int num)
	{
		int index = -1;
		// �÷��̾ ������ ���� �����ϱ⸦ ������ ��
		if (temInt == -5)
		{
			ItemSumCost = num;
			if (num <= 5000)
			{
				index = Findidx(-5, new int[1] { 19 });
			}
			else if (num > 5000 && num <= 30000)
			{
				index = Findidx(-5, new int[1] { 20 });
			}
			else if (num > 30000)
			{
				index = Findidx(-5, new int[1] { 21 });
			}
			SettingConversation(index, ItemSumCost);
			return;
		}
		
	}
	public override void JumpConversation()
	{
		int index = -1;
		// �÷��̾ �� ������ ����ϰ� ���� �г��� �ݾ��� ��
		if (temInt == -5)
		{
			ItemSumCost = -1;
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
		if (temInt == 11)
		{
			DiceRoll(9);
			index = -100;
		}
		else if (temInt == 22)
		{
			// �� ���ǵ� �κ��� ��
			AddPlayerItemDic();
			// �ݾ�����
			GameManager.Instance.Money -= ItemSumCost;
			ItemSumCost = -1;
			SettingConversation(Findidx(22, new int[1] { 25 }));
			index = -100;
		}
		else if (temInt == 23)
		{
			// ���� ���� ������ ������ 10% �ʰ��� ��
			if (GameManager.Instance.Money * 1.1f < ItemSumCost)
			{
				index = Findidx(23, new int[1] { 26 });
			}
			// ���� ���� ������ 10% ���Ϸ� ��� ��, ������ ��� 6�� ������ ��
			else if (GameManager.Instance.Money * 1.1f >= ItemSumCost && GameManager.Instance.Money < ItemSumCost && DiceLuck == 2)
			{
				index = Findidx(23, new int[1] { 28 });
			}
			else
			{
				index = Findidx(23, new int[1] { 26 });
			}
			SettingConversation(index);
			index = -100;
		}
		else if (temInt == 24)
		{
			index = Findidx(24, new int[1] { 27 });
			SettingConversation(index);
			index = -100;
		}
		else if (temInt == 31)
		{
			DiceRoll(10);
			index = -100;
		}
		else if (temInt == 38)
		{
			DiceRoll(8);
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
		if (num == 22)
		{
			if (GameManager.Instance.Money >= ItemSumCost)
			{
				SetISCondition();
				return true;
			}
		}
		else if (num == 23)
		{
			if (GameManager.Instance.Money < ItemSumCost)
			{
				return true;
			}
		}
		else if (num == 24)
		{
			if (isDiscount)
			{
				return true;
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
				index = Findidx(11, new int[1] { 16 });
			}
			else
			{
				index = Findidx(11, new int[1] { 17 });
			}
		}
		else if (temInt == 31)
		{
			if (bo)
			{
				index = Findidx(31, new int[1] { 35 });
				if (ItemSumCost > 2000)
				{
					ItemSumCost -= 2000;
				}
				else
				{
					ItemSumCost = 0;
				}
			}
			else
			{
				index = Findidx(31, new int[1] { 36 });
				isDiscount = false;
			}
			SettingConversation(index, ItemSumCost);
			return;
		}
		else if (temInt == 38)
		{
			if (bo)
			{
				index = Findidx(38, new int[1] { 32 });
				if (ItemSumCost > 2000)
				{
					ItemSumCost -= 2000;
				}
				else
				{
					ItemSumCost = 0;
				}
				GameManager.Instance.Money -= ItemSumCost;
			}
			else
			{
				index = Findidx(38, new int[1] { 33 });
			}
		}
		SettingConversation(index);
	}
	/// <summary>
	/// �ؽ�Ʈ�� �����ؼ� �׷����� ����
	/// </summary>
	/// �������� �̹��� 0 : ���� , 1 : ���� , 2 : ���� , 3 : ȭ��
	protected override void InitTextList()
	{
		startText = new int[6] { 2, 3, 4, 5, 6, 7 };
		nowTextNum = -5; nextTextNum = new int[1] { 8 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = -5; nextTextNum = new int[3] { 22, 23, 24 }; nextTextIsAble = new bool[3] { false, false, false };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 19 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = -5; nextTextNum = new int[3] { 22, 23, 24 }; nextTextIsAble = new bool[3] { false, false, false };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 20 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = -5; nextTextNum = new int[3] { 22, 23, 24 }; nextTextIsAble = new bool[3] { false, false, false };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 21 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = -1; nextTextNum = new int[4] { 9, 10, 11, 12 }; nextTextIsAble = new bool[4] { true, true, true, true };
		methodSArr = new MethodS[3]
		{
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 } )
		};
		AddTextList();
		nowTextNum = 8; nextTextNum = new int[4] { 9, 10, 11, 12 }; nextTextIsAble = new bool[4] { true, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 9; nextTextNum = new int[1] { 8 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 13 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] {1, 100 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 10; nextTextNum = new int[0]; nextTextIsAble = new bool[0];
		methodSArr = new MethodS[6]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 14 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 0 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 }),
			new MethodS(MethodEnum.SAVETEXTINDEX, new int[1] { -5}),
			new MethodS(MethodEnum.OPENSTORE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 11; nextTextNum = new int[1] { 15 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 16 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } )
		};
		AddTextList();
		nowTextNum = 11; nextTextNum = new int[1] { 18 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 17 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 }),
			new MethodS(MethodEnum.SPAWNPOLICE, new int[1] { 4 })
		};
		AddTextList();
		nowTextNum = 12; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { false };
		methodSArr = new MethodS[1]
		{
			new MethodS(MethodEnum.ENDPANEL, new int[1] { -1 } )
		};
		AddTextList();
		nowTextNum = 15; nextTextNum = new int[4] { 9, 10, 11, 12 }; nextTextIsAble = new bool[4] { true, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 18; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { false };
		methodSArr = new MethodS[1]
		{
			new MethodS(MethodEnum.ENDPANEL, new int[1] { - 1} )
		};
		AddTextList();
		nowTextNum = 22; nextTextNum = new int[2] { 29, 34 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 25 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 23; nextTextNum = new int[3] { 30, 8, 12 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 26 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 23; nextTextNum = new int[2] { 38, 12 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 28 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 24; nextTextNum = new int[1] { 31 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 27 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 29; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { false };
		methodSArr = new MethodS[1]
		{
			new MethodS(MethodEnum.ENDPANEL, new int[1] { - 1} )
		};
		AddTextList();
		nowTextNum = 30; nextTextNum = new int[0]; nextTextIsAble = new bool[0];
		methodSArr = new MethodS[6]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 14 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 0 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 }),
			new MethodS(MethodEnum.SAVETEXTINDEX, new int[1] { -5 }),
			new MethodS(MethodEnum.OPENSTORE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 31; nextTextNum = new int[3] { 22, 23, 24 }; nextTextIsAble = new bool[3] { false, false, false };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 35 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 31; nextTextNum = new int[3] { 22, 23, 24 }; nextTextIsAble = new bool[3] { false, false, false };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 36 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 34; nextTextNum = new int[0]; nextTextIsAble = new bool[0];
		methodSArr = new MethodS[6]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 14 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 0 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 }),
			new MethodS(MethodEnum.SAVETEXTINDEX, new int[1] { -5 }),
			new MethodS(MethodEnum.OPENSTORE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 38; nextTextNum = new int[2] { 34, 29 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 32 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 }),
		};
		AddTextList();
		nowTextNum = 38; nextTextNum = new int[3] { 30, 8, 12 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 33 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 }),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 }),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 }),
		};
		AddTextList();

	}
}
