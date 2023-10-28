using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ѽ�ȣ �ۼ�
/// <summary>
/// ������ ���õ� ���ӽ����̽�
/// </summary>
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
        public int Pay { get; set; } // �ֱ�
        public int Max { get; private set; }    // ���� �ɷ�ġ �ִ�ġ
        public int Min { get; private set; }    // ���� �ɷ�ġ �ּ�ġ
        public string Name { get; private set; } // �̸�
        public int MinPayScale { get; private set; }
        public int MaxPayScale { get; private set; }
        public int MaxStress { get; private set; }
        public ClerkC (int Handicraft, Tier Agility, Tier Career, Tier Creativity, int Stress, int Pay, string Name)
		{
            this.Handicraft = Handicraft;
            this.Agility = Agility;
            this.Career = Career;
            this.Creativity = Creativity;
            this.Stress = Stress;
            this.Pay = Pay;
            this.Name = Name;

            Max = (this.Handicraft + 8) + (int)Creativity;
            Min = (this.Handicraft - 8) + (int)Career;

            MinPayScale = Handicraft - (int)Agility + (int)Creativity + (int)Career - 10;
            MaxPayScale = Handicraft - (int)Agility + (int)Creativity + (int)Career + 10;

            MaxStress = 150;
        }
    }
}
/// <summary>
/// ���� ���¿� ���� ���ӽ����̽�
/// </summary>
namespace BuildingNS
{
    public enum BuildingShape {SQUARE, WIDTHLONG, LENGTHLONG, COMPOSITE }; 
    /// <summary>
    /// �� ���¿� ���� ���ӽ����̽�
    /// </summary>
    namespace HouseNS
    {
        public enum HouseType { NONE, PIZZASTORE, HOUSE, DICESTORE, PINEAPPLESTORE, INGREDIENTSTORE, PINEAPPLESTORETWO, GUNSTORE };
    }
}
/// <summary>
/// �� �ּҿ� ���� ���ӽ����̽�
/// </summary>
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
/// <summary>
/// ���� ��ῡ ���� ���ӽ����̽�
/// </summary>
namespace PizzaNS
{
    public enum Ingredient { PINEAPPLE, TOMATO, CHEESE, BASIL, POTATO, BACON, CORN, JALAPENO, CHICKEN, MEAT, APPLE, CARROT, BIGGREENONION };
    /// <summary>
    /// ��� ���ӽ����̽�
    /// </summary>
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
    /// <summary>
    /// �ϼ��� ������ ������ ���� ����ü
    /// </summary>
    public struct PizzaExplain
	{
        public List<Ingredient> Ingreds;    // ���ڿ� �� ����
        public int TotalDeclineAt;  // �� �ŷ��϶��� 

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
    /// <summary>
    /// ���� ���õ� ���ӽ����̽�
    /// </summary>
	namespace CustomerNS
    {
        public struct CustomerS
        {
            public List<Ingredient> IngredList;  //��ȣ ���
            public int PizzaCutLine;    //���� �ϼ��� ĿƮ����

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
/// <summary>
/// ������ ���õ� ���ӽ����̽�
/// </summary>
namespace PoliceNS
{
    namespace PolicePathNS
    {
        public struct PolicePath
        {
            public int Behaviour;   // ������ �ൿ ��ȣ
            public float Value; // �ൿ�� ���õ� ��(��. �̵��Ÿ�, ȸ�� ���� ��)

            public PolicePath(int behaviour, float value)
            {
                this.Behaviour = behaviour;
                this.Value = value;
            }
        }
    }
    /// <summary>
    /// �������� ���¸� ������ ���� ���ӽ����̽��̴�.
    /// </summary>
    namespace PoliceStateNS
    {
        // ���ʷ� ����, �̵���, �ҽɰ˹��� ���� ����, �ҽɰ˹���, �ı���, ������ �÷��̾� �Ѿƿ�, �ڵ� ����, �� ������ Ż�� �̴�.
        public enum PoliceState
        {
            NONE, MOVING, STOP, INSPECTING, DESTROY, SPUERCHASE, AUTOMOVE, OUTMAP
        };
    }
}
/// <summary>
/// ��ȭ�� ���� ���ӽ����̽��̴�.
/// </summary>
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
    /// <summary>
    /// ��ȭ �������� �ϳ� �� ������ ����Ǿ�� �ϴ� �޼ҵ忡 ���� ����ü�̴�.
    /// </summary>
    public struct MethodS
    {
        public MethodEnum MethodNum;    // ������ �޼ҵ� ��ȣ
        public int[] MethodParameter;   // ������ �޼ҵ��� �Ķ���� ��

        public MethodS (MethodEnum methodNum, int[] methodParameter)
        {
            MethodNum = methodNum;
            MethodParameter = methodParameter;
        }
    }
    /// <summary>
    /// ������ �޼ҵ��� ����
    /// </summary>
    public enum MethodEnum { NONE, SETSIZECONTENTS, CHANGENPCIMAGE, CHANGEPLAYERIMAGE, SETRANDNPCTEXT, ENDPANEL, SPAWNPOLICE, OPENSTORE, SAVETEXTINDEX, SETISCONDITION };
}
/// <summary>
/// ���Կ� ���õ� ���ӽ����̽��̴�.
/// </summary>
namespace StoreNS
{
    /// <summary>
    /// ���� ����
    /// </summary>
    public enum ItemType { NONE, DICE, GUN };
    /// <summary>
    /// ������ ����ü
    /// </summary>
    public struct ItemS
	{
        public ItemType Type;   // ������ Ÿ��
        public int MaxCnt;  // ������ �ִ� ���� ���� 
        public string Name; // ������ �̸�
        public string Explain;  // ������ ����
        public int ItemNumber;  // ������ ��ȣ

        public ItemS (ItemType type, int maxCnt, string name, string explain, int itemNumber)
		{
            Type = type;
            MaxCnt = maxCnt;
            Name = name;
            Explain = explain;
            ItemNumber = itemNumber;
		}
	}
    /// <summary>
    /// �ֻ��� ������ ����ü
    /// </summary>
    public struct DiceS
	{
        public int DiceCnt; // �ֻ����� �� ����
        public int[] DiceArr;   // �鸶�� �����ִ� ��ȣ
        public string Path; // �ֻ��� �̹����� ���

        public DiceS(int diceCnt, int[] diceArr, string path)
		{
            DiceCnt = diceCnt;
            DiceArr = diceArr;
            Path = path;
        }
	}
    /// <summary>
    /// ���� ������� ������
    /// </summary>
    public enum LoadEnum : short { NONE, AUTO, SEMIAUTO, MANUAL }
    /// <summary>
    /// �� ������ ����ü
    /// </summary>
    public struct GunS
	{
        public LoadEnum LoadType;   // ���� �������
        public float Speed; // �߻�ӵ�
        public short Damage;    // �߻� �����
        public short Accuracy;  // �߻� ��Ȯ��
        public short Magazine; // ��ź��
        public short ReloadSpeed; //������ �ӵ�
        public string Path; //�� �̹��� ���

        public GunS(LoadEnum loadType, float speed, short damage, short accuracy, short magazine, short reloadSpeed, string path)
		{
            LoadType = loadType;
            Speed = speed;
            Damage = damage;
            Accuracy = accuracy;
            Magazine = magazine;
            ReloadSpeed = reloadSpeed;
            Path = path;
		}
	}
}