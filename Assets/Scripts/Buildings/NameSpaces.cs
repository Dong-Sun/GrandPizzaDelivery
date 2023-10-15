using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ѽ�ȣ �ۼ�

namespace ClerkNS
{
    public enum Tier{ ONE = -1, TWO = 1, THREE = 3, FOUR = 6 };
    // ���߿� Ŭ������ �ٲܵ�
    public class ClerkC
    {
        public Tier Agility { get; private set; } // ���߷�
        public Tier Career { get; private set; }  // ���
        public Tier Creativity { get; private set; }  // â�Ƿ�
        public int Handicraft { get; private set; }  // ������
        public int Stress { get; set; } // ��Ʈ����
        public int Pay { get; private set; } // �ֱ�
        public int Max { get; private set; }    // ���� �ɷ�ġ �ִ�ġ
        public int Min { get; private set; }    // ���� �ɷ�ġ �ּ�ġ
        public ClerkC (int Handicraft, Tier Agility, Tier Career, Tier Creativity, int Stress, int Pay)
		{
            this.Handicraft = Handicraft;
            this.Agility = Agility;
            this.Career = Career;
            this.Creativity = Creativity;
            this.Stress = Stress;
            this.Pay = Pay;

            Max = (this.Handicraft + 8) + (int)Creativity;
            Min = (this.Handicraft - 8) + (int)Career;

            this.Pay = this.Handicraft + (int)this.Career + (int)this.Creativity + (int)this.Agility + Random.Range(-10, 11);
		}
    }
}

namespace BuildingNS
{
    public enum BuildingShape {SQUARE, WIDTHLONG, LENGTHLONG, COMPOSITE }; 
    
    namespace HouseNS
    {
        public enum HouseType { NONE, PIZZASTORE, HOUSE, DICESTORE };
    }
}
namespace BuildingAddressNS
{
    public struct AddressS
	{
        /// <summary>
        /// �ǹ��� �ּ�
        /// </summary>
        public int BuildingAddress;
        /// <summary>
        /// �ǹ��� �����ϴ� �� ���� �ּ�
        /// </summary>
        public int HouseAddress;

        public IHouse IHouse;
        public AddressS(int BuildingAddress, int HouseAddress, IHouse iHouse)
		{
            this.BuildingAddress = BuildingAddress;
            this.HouseAddress = HouseAddress;
            this.IHouse = iHouse;
		}
	}
}

namespace PizzaNS
{
    public enum Ingredient { NONE, TOMATO, CHEESE, BASIL, POTATO, BACON, CORN, JALAPENO, CHICKEN, MEAT };

    public struct IngredientS
    {
        public Ingredient Ingred;   // ���
        public int Attractiveness;  // �ŷµ�
        public int DeclineAt;    // �ŷ� �϶���
        public int IngredientPrice; // ��ᰪ

        public IngredientS(Ingredient ingred, int attractiveness, int declineAt, int ingredientPrice)
		{
            this.Ingred = ingred;
            this.Attractiveness = attractiveness;
            this.DeclineAt = declineAt;
            this.IngredientPrice = ingredientPrice;
		}
    }

    public struct PizzaExplain
	{
        public List<Ingredient> Ingreds;
        public int TotalDeclineAt;

		public PizzaExplain(List<Ingredient> Ingreds, int TotalDeclineAt)
		{
			this.Ingreds = new List<Ingredient>();
			for (int i = 0; i < Ingreds.Count; i++)
			{
				this.Ingreds.Add(Ingreds[i]);
			}
            this.TotalDeclineAt = TotalDeclineAt; 
		}
	}

	namespace CustomerNS
    {
        public struct CustomerS
        {
            //��ȣ ���
            public List<Ingredient> IngredList;
            //���� �ϼ��� ĿƮ����
            public int PizzaCutLine;

            public CustomerS(int pizzaCutLine, List<Ingredient> ingredList)
            {
                PizzaCutLine = pizzaCutLine;

                IngredList = new List<Ingredient>();
                for (int i = 0; i < ingredList.Count; i++)
                {
                    IngredList.Add(ingredList[i]);
                }
            }
        }
    }
}
namespace PoliceNS
{
    namespace PolicePathNS
    {
        public struct PolicePath
        {
            public int Behaviour;
            public float Value;

            public PolicePath(int behaviour, float value)
            {
                this.Behaviour = behaviour;
                this.Value = value;
            }
        }
    }

    namespace PoliceStateNS
    {
        // ���ʷ� ����, �̵���, �ҽɰ˹��� ���� ����, �ҽɰ˹��� �̴�.
        public enum PoliceState
        {
            NONE, MOVING, STOP, INSPECTING, DESTROY, SPUERCHASE, AUTOMOVE, OUTMAP
        };
    }
}

namespace ConversationNS
{
    public class TextNodeC
    {
        public int NowTextNum;  // ���� �ؽ�Ʈ ��ȣ
        public int[] NextTextNum;  // ���� �ؽ�Ʈ ��ȣ ������ ����Ǿ� �ִ� �ؽ�Ʈ ��ȣ��
        public MethodS[] MethodSArr;    // ���� �ؽ�Ʈ ����� ���ÿ� ����Ǿ�� �ϴ� �Լ���� ���ڰ��� �־���� ����ü�� �迭
        public bool[] NextTextIsAble;   // ���� �ؽ�Ʈ ��ȣ ������ ����Ǿ� �ִ� �ؽ�Ʈ�� ���� ����

        public TextNodeC (int nowTextNum, int[] nextTextNum, MethodS[] methodSArr, bool[] nextTextIsAble)
        {
            NowTextNum = nowTextNum;
            NextTextNum = nextTextNum;
            MethodSArr = methodSArr;
            NextTextIsAble = nextTextIsAble;
        }
    }

    public struct MethodS
    {
        public MethodEnum MethodNum;
        public int[] MethodParameter;

        public MethodS (MethodEnum methodNum, int[] methodParameter)
        {
            MethodNum = methodNum;
            MethodParameter = methodParameter;
        }
    }

    public enum MethodEnum { NONE, SETSIZECONTENTS, CHANGENPCIMAGE, CHANGEPLAYERIMAGE, SETRANDNPCTEXT, ENDPANEL, SPAWNPOLICE, OPENSTORE, SAVETEXTINDEX, SETISCONDITION };
}

namespace StoreNS
{
    public enum ItemType { NONE, DICE };

    public struct ItemS
	{
        public ItemType Type;
        public int MaxCnt;
        public string Name;
        public string Explain;
        public int ItemNumber;

        public ItemS (ItemType type, int maxCnt, string name, string explain, int itemNumber)
		{
            Type = type;
            MaxCnt = maxCnt;
            Name = name;
            Explain = explain;
            ItemNumber = itemNumber;
		}
	}
}