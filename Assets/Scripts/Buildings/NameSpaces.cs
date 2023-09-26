using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ѽ�ȣ �ۼ�

namespace ClerkNS
{
    public enum Tier{ ONE = -1, TWO = 1, THREE = 3, FOUR = 6 };
    // ���߿� Ŭ������ �ٲܵ�
    public struct ClerkS
	{
        public Tier Handicraft;  // ������
        public Tier Agility; // ���߷�
        public Tier Career;  // ���
        public Tier Creativity;  // â�Ƿ�
        public int Stress;  // ��Ʈ����
        public int Pay; // �ֱ�

        public ClerkS (Tier Handicraft, Tier Agility, Tier Career, Tier Creativity, int Stress, int Pay)
		{
            this.Handicraft = Handicraft;
            this.Agility = Agility;
            this.Career = Career;
            this.Creativity = Creativity;
            this.Stress = Stress;
            this.Pay = Pay;
		}
    }
}

namespace BuildingNS
{
    public enum BuildingShape {SQUARE, WIDTHLONG, LENGTHLONG, COMPOSITE }; 
    
    namespace HouseNS
    {
        public enum HouseType { NONE, PIZZASTORE, HOUSE};
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
            NONE, MOVING, STOP, INSPECTING, DESTROY
        };
    }
}