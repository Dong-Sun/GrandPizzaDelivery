using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConversationNS;

// �Ѽ�ȣ �ۼ�
public class IngredientStore : Conversation
{
	public static int Contract = 0;
	public static bool Hint = false;
	public static bool OneChance = true;
	public IngredientStore()
	{
		NpcTextStrArr = new string[41]
		{
			"��ä �缼��. �缼��. ���� �ʾƵ� ǰ���� �����ϴ�.",	// 0
			"������ �Ȱ� �ֳ���?",	// 1
			"��� �����ؼ� �� ���� �ֽ��ϴ�.",	// 2
			"(����� ����.)",	// 3
			"���, ��� ��..���ĸ� �Ȱ� �־�.",	// 4
			"(���ư���.) �׷�����.",	// 5
			"���߿� ����� ���� �ƴѰ���?",	// 6
			"��¼�ٺ���..�׺��� ��ž� ���ž�?",	// 7
			"(���ư���.)",	// 8
			"��ǰ ����� �ϰ� �ͽ��ϴ�.",	// 9
			"��ǰ ����� �����ϰ� �ͽ��ϴ�.",	// 10
			"�׷�. ��� ����ҷ�?",	// 11
			"��� �����ҷ�?",	// 12
			"(��� �� ���� ��� �� 20����)����� ����ϰ� �ͽ��ϴ�.",	// 13
			"(��� �� ���� ��� �� 17����)����� ����ϰ� �ͽ��ϴ�.",	// 14
			"(��� �� ���� ��� �� 14����)���ĸ� ����ϰ� �ͽ��ϴ�.",	// 15
			"����. ��� ���, ���� ���� ������ ��ᰪ�� \n����� ������ ���� ��ٴ� �� ������.",	// 16
			"(���ư���.)������. ���� ���׿�.",	//17
			"�˰ڽ��ϴ�.(���ư���.)",	// 18
			"(����.)",	// 19
			"����� �����ϰ� �ͽ��ϴ�.",	// 20
			"����� �����ϰ� �ͽ��ϴ�.",	// 21
			"���ĸ� �����ϰ� �ͽ��ϴ�.",	// 22
			"(���ư���.)�����غ��� ���� ���ص� �� �� �����ϴ�.",	// 23
			"�׷�. �ƽ����� ��. ���߿� ����ϰ� ������ �ٽ� ������.",	// 24
			"�׷�, ���Դ� �� �����ϰ� �ִ�?",	// 25
			"�׷����� �������Դϴ�.",	// 26
			"���� �� ����� ��Ȳ�̿���.",	// 27
			"�׷� �����̳�",	// 28
			"(���ư���.)",	// 29
			"����..�ϱ� �������..���� �˷���� �ϳ�..",	// 30
			"��..�� �� �ܰ�մ��� �Ǿ��شٸ� ���� ������ �ٰ�",	// 31
			"(���ư���.)�˰ڽ��ϴ�.",	// 32
			"(�ֻ��� �� 10 �̻�) �ε� �˷��ּ���..!",	// 33
			"����. �׷� ����ص�. '������ ���Ϸ�' \n �� �̻��� �˾Ƽ� ã�ƺ�.",	// 34
			"�ȵ� �ȵ�. �ʿ��� ���� �̸��� ����.",	// 35
			"����ϰ� ����? '������ ���Ϸ�'��. �̰� ������ �� �Ŷ��.",	// 36
			"(���ư���.)�˰ڽ��ϴ�.",	// 37
			"���� �ִٸ� �˰� �ͽ��ϴ�.",	// 38
			"���� �Ҹ�?",	// 39
			"���� ������ ���̴°�?"	// 40
		};

		if (Constant.NowDate == 1)
        {
			Contract = 0;
			Hint = false;
			OneChance = true;
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
		if (temInt == 13)
		{
			if (GameManager.Instance.Money >= 200000)
			{
				GameManager.Instance.Money -= 200000;
				Constant.UsableIngredient.Add(10);
				Contract++;
				index = Findidx(13, new int[1] { 16 });
			}
			else
			{
				index = Findidx(13, new int[1] { 40 });
			}
		}
		else if (temInt == 14)
		{
			if (GameManager.Instance.Money >= 170000)
			{
				GameManager.Instance.Money -= 170000;
				Constant.UsableIngredient.Add(11);
				Contract++;
				index = Findidx(14, new int[1] { 16 });
			}
			else
			{
				index = Findidx(14, new int[1] { 40 });
			}
		}
		else if (temInt == 15)
		{
			if (GameManager.Instance.Money >= 140000)
			{
				GameManager.Instance.Money -= 140000;
				Constant.UsableIngredient.Add(12);
				Contract++;
				index = Findidx(15, new int[1] { 16 });
			}
			else
			{
				index = Findidx(15, new int[1] { 40 });
			}
		}
		else if (temInt == 20)
		{
			Constant.UsableIngredient.Remove(10);
			Contract--;
			SettingConversation(Findidx(20, new int[1] { 24 }));
			index = -100;
		}
		else if (temInt == 21)
		{
			Constant.UsableIngredient.Remove(11);
			Contract--;
			SettingConversation(Findidx(21, new int[1] { 24 }));
			index = -100;
		}
		else if (temInt == 22)
		{
			Constant.UsableIngredient.Remove(12);
			Contract--;
			SettingConversation(Findidx(22, new int[1] { 24 }));
			index = -100;
		}
		else if (temInt == 23)
		{
			SettingConversation(Findidx(23, new int[1] { -1 }));
			index = -100;
		}
		else if (temInt == 27)
		{
			if (!Hint)
			{
				if (Contract >= 2)
				{
					Hint = true;
					index = Findidx(27, new int[1] { 30 });
				}
				else if (Contract < 2)
				{
					index = Findidx(27, new int[1] { 31 });
				}
			}
			else
			{
				index = Findidx(27, new int[1] { 36 });
			}
		}
		else if (temInt == 33)
		{
			OneChance = false;
			DiceRoll(10);
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
		int index = -1;
		if (temInt == 33)
		{
			if (bo)
			{
				index = Findidx(33, new int[1] { 34 });
			}
			else
			{
				index = Findidx(33, new int[1] { 35 });
			}
		}

		SettingConversation(index);
	}
	/// <summary>
	/// �÷��̾��� ���¿� ���� ��ȭ ���� ����
	/// </summary>
	/// <param name="num"></param>
	/// <returns></returns>
	protected override bool Condition(int num)
	{
		if (num == 13)
		{
			if (Constant.UsableIngredient.FindIndex(a => a == 10) != -1)
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
			if (Constant.UsableIngredient.FindIndex(a => a == 11) != -1)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		else if (num == 15)
		{
			if (Constant.UsableIngredient.FindIndex(a => a == 12) != -1)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		else if (num == 20)
		{
			if (Constant.UsableIngredient.FindIndex(a => a == 10) != -1)
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
			if (Constant.UsableIngredient.FindIndex(a => a == 11) != -1)
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
			if (Constant.UsableIngredient.FindIndex(a => a == 12) != -1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else if (num == 33)
		{
			if (OneChance)
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

		nowTextNum = -1; nextTextNum = new int[4] { 1, 2, 19, 3 }; nextTextIsAble = new bool[4] { true, true, true, true };
		methodSArr = new MethodS[3]
		{
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 } )
		};
		AddTextList();
		nowTextNum = 1; nextTextNum = new int[2] { 5, 6 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 4 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 2; nextTextNum = new int[2] { 9, 10 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 39 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 3; nextTextNum = new int[2] { 26, 27 }; nextTextIsAble = new bool[2] { true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 25 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 5; nextTextNum = new int[4] { 1, 2, 19, 3 }; nextTextIsAble = new bool[4] { true, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 6; nextTextNum = new int[1] { 8 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 7 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 8; nextTextNum = new int[4] { 1, 2, 19, 3 }; nextTextIsAble = new bool[4] { true, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 9; nextTextNum = new int[4] { 13, 14, 15, 17 }; nextTextIsAble = new bool[4] { false, false, false, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 11 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 10; nextTextNum = new int[4] { 20, 21, 22, 23 }; nextTextIsAble = new bool[4] { false, false, false, true };
		methodSArr = new MethodS[5]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 12 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 }),
			new MethodS(MethodEnum.SETISCONDITION, new int[0])
		};
		AddTextList();
		nowTextNum = 13; nextTextNum = new int[1] { 18 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 16 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 13; nextTextNum = new int[1] { 17 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 40 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 14; nextTextNum = new int[1] { 18 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 16 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 14; nextTextNum = new int[1] { 17 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 40 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 15; nextTextNum = new int[1] { 18 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 16 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 15; nextTextNum = new int[1] { 17 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 40 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 17; nextTextNum = new int[4] { 1, 2, 19, 3 }; nextTextIsAble = new bool[4] { true, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 18; nextTextNum = new int[4] { 1, 2, 19, 3 }; nextTextIsAble = new bool[4] { true, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 19; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { false };
		methodSArr = new MethodS[1]
		{
			new MethodS(MethodEnum.ENDPANEL, new int[1] { -1 } )
		};
		AddTextList();
		nowTextNum = 20; nextTextNum = new int[1] { 18 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 24 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 21; nextTextNum = new int[1] { 18 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 24 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 22; nextTextNum = new int[1] { 18 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 24 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 23; nextTextNum = new int[4] { 1, 2, 19, 3 }; nextTextIsAble = new bool[4] { true, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 26; nextTextNum = new int[1] { 29 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 28 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 27; nextTextNum = new int[1] { 38 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 30 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 27; nextTextNum = new int[2] { 32, 33 }; nextTextIsAble = new bool[2] { true, false };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 31 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 27; nextTextNum = new int[1] { 37 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 36 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 29; nextTextNum = new int[4] { 1, 2, 19, 3 }; nextTextIsAble = new bool[4] { true, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 32; nextTextNum = new int[4] { 1, 2, 19, 3 }; nextTextIsAble = new bool[4] { true, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 })
		};
		AddTextList();
		nowTextNum = 33; nextTextNum = new int[1] { 18 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 34 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 33; nextTextNum = new int[1] { 32 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 35 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
		};
		AddTextList();
		nowTextNum = 37; nextTextNum = new int[4] { 1, 2, 19, 3 }; nextTextIsAble = new bool[4] { true, true, true, true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { -1 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
		nowTextNum = 38; nextTextNum = new int[1] { 18 }; nextTextIsAble = new bool[1] { true };
		methodSArr = new MethodS[4]
		{
			new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 34 } ),
			new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
			new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
			new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 })
		};
		AddTextList();
	}
}
