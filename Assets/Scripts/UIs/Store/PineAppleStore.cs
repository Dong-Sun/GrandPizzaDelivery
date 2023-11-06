using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConversationNS;

// �Ѽ�ȣ �ۼ�
public class PineAppleStore : Conversation
{
	public static bool isFirstTime = true;
	public static bool isFineapple = true;

	private int goodPoint = 0;
	private int requireMoney = 0;
	public PineAppleStore()
	{
		NpcTextStrArr = new string[42]
		{
			"���� �ϴ� �Ͽ� �߸��� �� ����.",	// 0
			"����. ���� ����..",	// 1
			"'�� ����'�� ��޸��ص� �������ٴ� �̰� ���� ���� �����ΰ�?",	// 2
			"�ȳ��ϼ���.",	// 3
			"�ȳ�, ó������ ���ε�. ��� ���� ã�ƿ���?",	// 4
			"�� �Ա���. �ٵ� ���� ������ ������ �ʾҾ�. �̾��ϰ� �Ʊ�.",	// 5
			"�Ա���. ��ٸ��� �־���.",	// 6
			"���̿��� �Ұ��޾ҽ��ϴ�.",	// 7
			"��. �ʱ���. �� ���Ը� �������� ������.",	// 8
			"�׷��� ���� ���� �����ΰ���?",	// 9
			"�˰�ʹ�? ���� Ư���� �� �Ȱ� �־�. �������� ���ϴ� �ҹ���������.",	// 10
			"�׷� �� ���� ���ص� �Ǵ� �ǰ���?",	// 11
			"����..�׷�����.",	// 12
			"Ȥ�� '���ξ���'�� ���ϴ� �̴ϱ�?",	// 13
			"���� ������. ������ �ʵ� �� �����ϰ� �ɰž�. \n���� �ִٰ� ����µ� ��Ե� ���ƾ����� �ʰڴ�?",	// 14
			"ħ���ϱ���. �ƴϸ� �ͼ��Ѱǰ�? Ȥ�� �Ծ�� �־�? '���ξ���'",	// 15
			"���� ����. �� �ܾ ���� ���ϴٴ�. ������ ��� �༮�̾�. \nȤ�� �Ծ�� �ִ�? '���ξ���'",	// 16
			"������ ���ξ��� ���ڸ� ����� �ȶ�� �ǰ���?",	// 17
			"����� �����ϰ� �־����ϴ�...���� �������� ���� �ؾߴϱ��.",	// 18
			"�Ծ�� �����ϴ�. �����ε� ���� ������ �����.",	// 19
			"�Ծ�� �����ϴ�. ������ ��ȸ�� �Ǹ� �Ծ�� �ͳ׿�.",	// 20
			"������ ����, ���� ������ �� �ܰ� ���̾���. \n���Ը� �����޾����� �ʰ� ����ؾ����� �ʰڴ�?",	// 21
			"���� �� ���ؼ� �����̳�. �׷� �� �غ� �� ����?",	// 22
			"���� ������ �ִ°ǰ�? �ƴ� ���� �߿����� �ʰ���. \n�׷��� �� �غ�� �Ǿ�����?",	// 23
			"�ʿʹ� �� ���� �� ����. �׷� ���� ��� ����?",	// 24
			"���Դϱ�?",	// 25
			"60�����̾�.",	// 26
			"40�����̾�. �� ������ ������� �̹��� �������ִ� �ž�.",	// 27
			"20������ �ٰ�. �ʿʹ� ���� �˰����� �� �ϴ� �̹��� �̷��� �ȵ��� ����.",	// 28
			"�� �ʰ��� �༮�� ������. �̹��� ��¥�� �ٰ�. ������.",	// 29
			"���� ���׿�. ������ �ðԿ�.",	// 30
			"(�ݾ��� �����Ѵ�.)",	// 31
			"�׷����� ��",	// 32
			"���� ����. \n ��ǰ�� �Ժη� ��� �ٴϸ� ������ �ɸ��״� ���� ���� ���Կ� �����ٰ�.",	// 33
			"(����)",	// 34
			"�����Ϸ� �Խ��ϴ�. '���ξ���'",	// 35
			"�׷�. 60�����̾�.",	// 36
			"������. ���� ���׿�.",	// 37
			"(�ݾ��� �����Ѵ�.)",	// 38
			"�ƽ���. ������ ����",	// 39
			"�׻� ����.",	// 40
			"(����)"	// 41
		};

		if (Constant.NowDate == 1 && GameManager.Instance.time >= 32400 && GameManager.Instance.time <= 32500)
        {
			isFirstTime = true;
			isFineapple = true;
		}

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

		if (temInt == 3)
		{
			// �ʸ��� ���
			if (isFirstTime)
			{
				index = Findidx(3, new int[1] { 4 });
				isFirstTime = false;
			}
			// �����ΰ��
			else
			{
				// ���ξ����� ���°��
				if (!isFineapple)
				{
					index = Findidx(3, new int[1] { 5 });
				}
				// ���ξ����� �ִ� ���
				else
				{
					index = Findidx(3, new int[1] { 6 });
				}
			}
		}
		else if (temInt == 11)
		{
			SettingConversation(Findidx(11, new int[1] { 14 }));
			index = -100;
		}
		else if (temInt == 12)
		{
			SettingConversation(Findidx(12, new int[1] { 15 }));
			goodPoint += 1;
			index = -100;
		}
		else if (temInt == 13)
		{
			SettingConversation(Findidx(13, new int[1] { 16 }));
			goodPoint += 2;
			index = -100;
		}
		else if (temInt == 17)
		{
			SettingConversation(Findidx(17, new int[1] { 21 }));
			index = -100;
		}
		else if (temInt == 18)
		{
			SettingConversation(Findidx(18, new int[1] { 22 }));
			goodPoint += 1;
			index = -100;
		}
		else if (temInt == 19)
		{
			SettingConversation(Findidx(19, new int[1] { 23 }));
			index = -100;
		}
		else if (temInt == 20)
		{
			SettingConversation(Findidx(20, new int[1] { 24 }));
			goodPoint += 1;
			index = -100;
		}
		else if (temInt == 25)
		{
			if (goodPoint == 0)
			{
				index = Findidx(25, new int[1] { 26 });
				requireMoney = 600000;
			}
			else if (goodPoint == 1)
			{
				index = Findidx(25, new int[1] { 27 });
				requireMoney = 400000;
			}
			else if (goodPoint == 2)
			{
				index = Findidx(25, new int[1] { 28 });
				requireMoney = 200000;
			}
			else if (goodPoint >= 3)
			{
				index = Findidx(25, new int[1] { 29 });
				requireMoney = 0;
			}
		}
		else if (temInt == 30)
		{
			SettingConversation(Findidx(30, new int[1] { 32 }));
			index = -100;
		}
		else if (temInt == 31)
		{
			GameManager.Instance.Money -= requireMoney;
			Constant.PineappleCount++;
			isFineapple = false;
			SettingConversation(Findidx(31, new int[1] { 33 }));
			index = -100;
		}
		else if (temInt == 37)
		{
			SettingConversation(Findidx(37, new int[1] { 39 }));
			index = -100;
		}
		else if (temInt == 38)
		{
			GameManager.Instance.Money -= 600000;
			Constant.PineappleCount++;
			isFineapple = false;
			SettingConversation(Findidx(38, new int[1] { 40 }));
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
		if (num == 30)
		{
			if (requireMoney > GameManager.Instance.Money)
			{
				return true;
			}
		}
		else if (num == 31)
		{
			if (requireMoney <= GameManager.Instance.Money)
			{
				return true;
			}
		}
		else if (num == 37)
		{
			if (600000 > GameManager.Instance.Money)
			{
				return true;
			}
		}
		else if (num == 38)
		{
			if (600000 <= GameManager.Instance.Money)
			{
				return true;
			}
		}

		return false;
	}
	/// <summary>
	/// �ؽ�Ʈ���� �����ؼ� �׷����� ����
	/// </summary>
	/// ���� ���� �̹���  0 : ������� 1 : ���ɾ��� 2 : ���� 3 : �ǹ̽���
	protected override void InitTextList()
	{
		startText = new int[3] { 0, 1, 2 };

		nowTextNum = -1; nextTextNum = new int[1] { 3 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[3]
		{
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 } )
		};
		AddTextList();
		nowTextNum = 3;	nextTextNum = new int[1] { 7 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 4 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 3; nextTextNum = new int[1] { 41 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 5 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 3; nextTextNum = new int[1] { 35 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 6 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 7; nextTextNum = new int[1] { 9 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 8 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 9; nextTextNum = new int[3] { 11, 12, 13 }; nextTextIsAble = new bool[3] { true, true, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 10 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])

		};
		AddTextList();
		nowTextNum = 11; nextTextNum = new int[2] { 17, 18 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 14 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 12; nextTextNum = new int[2] { 19, 20 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 15 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 13; nextTextNum = new int[2] { 19, 20 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 16 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 17; nextTextNum = new int[1] { 25 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 21 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 18; nextTextNum = new int[1] { 25 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 22 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 19; nextTextNum = new int[1] { 25 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 23 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 20; nextTextNum = new int[1] { 25 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 24 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 25; nextTextNum = new int[2] { 30, 31 }; nextTextIsAble = new bool[2] { false, false };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 26 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 25; nextTextNum = new int[2] { 30, 31 }; nextTextIsAble = new bool[2] { false, false };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 27 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 25; nextTextNum = new int[2] { 30, 31 }; nextTextIsAble = new bool[2] { false, false };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 28 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 25; nextTextNum = new int[2] { 30, 31 }; nextTextIsAble = new bool[2] { false, false };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 29 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 30; nextTextNum = new int[1] { 34 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 32 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 31; nextTextNum = new int[1] { 34 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 33 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 34; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { false };
		methodSArr = new MethodS[1]
		{
			new MethodS(MethodEnum.ENDPANEL, new int[1] { -1 } )
		};
		AddTextList();
		nowTextNum = 35; nextTextNum = new int[2] { 37, 38 }; nextTextIsAble = new bool[2] { false, false };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 36 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 37; nextTextNum = new int[1] { 41 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 39 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 38; nextTextNum = new int[1] { 41 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 40 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 41; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { false };
		methodSArr = new MethodS[1]
		{
			new MethodS(MethodEnum.ENDPANEL, new int[1] { -1 } )
		};
		AddTextList();
	}
}
