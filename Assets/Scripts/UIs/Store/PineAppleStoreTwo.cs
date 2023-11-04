using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConversationNS;

// �Ѽ�ȣ �ۼ�
public class PineAppleStoreTwo : Conversation
{
	public static bool isPineapple = true;
	public static bool isContract = false;
	public static bool isMeet = false;
	public PineAppleStoreTwo()
	{
		NpcTextStrArr = new string[36]
		{
			"���� ���� ���ؿ�",	// 0
			"�˼��մϴ�. ���� �����־ �����ϴ� �� �˾ҽ��ϴ�.",	// 1
			"(����.)",	// 2
			"�������. �ٵ� ���� ������ �ȵ��԰ŵ��.",	// 3
			"��! ��ħ ������ ���Դµ� �� ���̳׿�",	//	4
			"���� �ٷ� ������ �� �������?",	// 5
			"�翬����. 55�����̿���",	// 6
			"(�����Ѵ�.)",	// 7
			"������ ���� �����ϳ׿�. ������ ���ڽ��ϴ�.",	// 8
			"��, ������ �ƿ�.",	// 9
			"�����մϴ�. ������ �ƿ�!",	// 10
			"���, Ȥ�� ���ڰ�����?",	// 11
			"��, ���� ���ο� �����Դϴ�.",	// 12
			"�߸����ŵ��մϴ�.",	// 13
			"���̰�, �ƴϱ���.",	// 14
			"��, ����. ���� ����� �ϳ��� ���ŵ��.",	// 15
			"�� �ҽ��� ���Ը��� ���� ����̳׿�?",	// 16
			"���� ��鼭 �� ���ڰ��Ը� �𸣴� ����� ���ŵ��.",	// 17
			"�׳����� ����� �� �Ĵ� �����Դϱ�?",	// 18
			"���� ������..���� '�� ����'�� �Ȱ� �־��.",	// 19
			"'���ξ���'���ϴ� �ǰ���?",	// 20
			"'�� ����' �����̽ʴϱ�?",	// 21
			"������..! �׷��� ���� ��ھ��..�¾ƿ�. �װ� �Ȱ� ����.",	// 22
			"�¾ƿ�. ������ ���� ���� �Ǹ��ϰ� ����.",	// 23
			"Ȥ�� ������ ���⼭ ���� �װ��� �����Ͻ� �ǰ���?",	// 24
			"�׷���. �ܰ�մ��̾���. ���� ���ʵ� ������ �׷��ǰ���?",	// 25
			"�������. Ȥ�� ������ �������̳���?",	// 26
			"(���� 40������ �ش�.)",	// 27
			"�� �� �����غ��ڽ��ϴ�.",	// 28
			"��������.",	// 29
			"�� ������ �ٲ�ø� �������ּ���. ���� �ŷ��� ������ 40������ �� �˾��ֽð��.",	// 30
			"���׿�! 40������ ���ֽŴٸ� �ٷ� ����ص帱�Կ�.",	// 31
			"������, ���� �����ϳ׿�.",	// 32
			"�����ϰ� ����ּ���. ������ ������ �� �ƴϴϱ��.",	// 33
			"���� �޾ҽ��ϴ�! ��� ������� ����� '����' �ϳ��� �帱�Կ� \n��, �������ʹ� 55������ ���ž� �� �� �ִٴ� �� ������ּ���.",	// 34
			"(����.)�˰ڽ��ϴ�. ������ �˰ڽ��ϴ�."	// 35
		};

		if (Constant.NowDate == 1)
        {
			isPineapple = true;
			isContract = false;
			isMeet = false;
		}

		TextList = new List<TextNodeC>();
		InitTextList();
		InitStartText();
	}
	protected override void InitStartText()
	{
		if (!isMeet)
		{
			startText = new int[1] { 0 };
		}
		else
		{
			if (!isContract)
			{
				startText = new int[1] { 26 };
			}
			else
			{
				if (!isPineapple)
				{
					startText = new int[1] { 3 };
				}
				else
				{
					startText = new int[1] { 4 };
				}
			}
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
			if (!isMeet)
			{
				index = Findidx(-1, new int[1] { 0 });
			}
			else
			{
				if (!isContract)
				{
					index = Findidx(-1, new int[1] { 26 });
				}
				else
				{
					if (!isPineapple)
					{
						index = Findidx(-1, new int[1] { 3 });
					}
					else
					{
						index = Findidx(-1, new int[1] { 4 });
					}
				}
			}
		}
		else if (temInt == 2)
		{
			SettingConversation(TextList.FindIndex(a => a.NowTextNum == 2));
			index = -100;
		}
		else if (temInt == 7)
		{
			isPineapple = false;
			Constant.PineappleCount++;
			GameManager.Instance.Money -= 550000;
			index = Findidx(7, new int[1] { 10 });
		}
		else if (temInt == 8)
		{
			index = Findidx(8, new int[1] { 9 });
		}
		else if (temInt == 27)
		{
			isContract = true;
			GameManager.Instance.Money -= 400000;
			SettingConversation(Findidx(27, new int[1] { 34 }));
			index = -100;
		}
		else if (temInt == 28)
		{
			isMeet = true;
			SettingConversation(Findidx(28, new int[1] { 30 }));
			index = -100;
		}
		else if (temInt == 29)
		{
			isMeet = true;
			SettingConversation(Findidx(29, new int[1] { 31 }));
			index = -100;
		}
		else if (temInt == 32)
		{
			SettingConversation(Findidx(32, new int[1] { 33 }));
			index = -100;
		}
		else if (temInt == 35)
		{
			isPineapple = false;
			Constant.PineappleCount++;
			SettingConversation(TextList.FindIndex(a => a.NowTextNum == 35));
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
			if (GameManager.Instance.Money >= 550000)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if (num == 8)
		{
			if (GameManager.Instance.Money < 550000)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if (num == 27)
		{
			if (GameManager.Instance.Money >= 400000)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if (num == 32)
		{
			if (GameManager.Instance.Money < 400000)
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
	/// ���� ���� �̹���  0 : ������� 1 : ���ɾ��� 2 : ���� 3 : �ǹ̽���
	protected override void InitTextList()
	{
		startText = new int[1] { 0 };

		nowTextNum = -1; nextTextNum = new int[2] { 1, 2 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 0 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 } )
		};
		AddTextList();
		nowTextNum = -1; nextTextNum = new int[1] { 2 }; nextTextIsAble = new bool[1] {true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 3 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } )
		};
		AddTextList();
		nowTextNum = -1; nextTextNum = new int[2] { 5, 2 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 4 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 } )
		};
		AddTextList();
		nowTextNum = -1; nextTextNum = new int[3] { 27, 32, 2 }; nextTextIsAble = new bool[3] { false, false, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 26 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 300 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 1; nextTextNum = new int[2] { 12, 13 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 11 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } )
		};
		AddTextList();
		nowTextNum = 2; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { false };
		methodSArr = new MethodS[1]
		{
			new MethodS(MethodEnum.ENDPANEL, new int[1] { -1 })
		};
		AddTextList();
		nowTextNum = 5; nextTextNum = new int[2] { 7, 8 }; nextTextIsAble = new bool[2] { false, false };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 6 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 } )
		};
		AddTextList();
		nowTextNum = 7; nextTextNum = new int[1] { 2 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 10 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 } )
		};
		AddTextList();
		nowTextNum = 8; nextTextNum = new int[1] { 2 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 9 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } )
		};
		AddTextList();
		nowTextNum = 12; nextTextNum = new int[1] { 16 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 15 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } )
		};
		AddTextList();
		nowTextNum = 13; nextTextNum = new int[1] { 2 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 14 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 } )
		};
		AddTextList();
		nowTextNum = 16; nextTextNum = new int[1] { 18 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 17 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } )
		};
		AddTextList();
		nowTextNum = 18; nextTextNum = new int[2] { 20, 21 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 19 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 } )
		};
		AddTextList();
		nowTextNum = 20; nextTextNum = new int[1] { 24 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 22 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 } )
		};
		AddTextList();
		nowTextNum = 21; nextTextNum = new int[1] { 24 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 23 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 } )
		};
		AddTextList();
		nowTextNum = 24; nextTextNum = new int[2] { 28, 29 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 25 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 27; nextTextNum = new int[1] { 35 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 34 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 28; nextTextNum = new int[1] { 2 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 30 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 } )
		};
		AddTextList();
		nowTextNum = 29; nextTextNum = new int[2] { 27, 32 }; nextTextIsAble = new bool[2] { false, false };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 31 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 32; nextTextNum = new int[1] { 2 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 33 }),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } )
		};
		AddTextList();
		nowTextNum = 35; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { false };
		methodSArr = new MethodS[1]
		{
			new MethodS(MethodEnum.ENDPANEL, new int[1] { -1 }),
		};
		AddTextList();

	}
}
