using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ѽ�ȣ �ۼ�


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
/*
������ ������ �������� �մԵ��� �����Ѵ�. �ѹ� ������ ������ �մԵ��� �� �����ȴ�.
�������� �÷��̾ �߰��Ѵ�.(����)
�ӵ��踦 �����.
������ �����. ��Ḷ�� �ŷµ��� �ٸ�. �ŷµ� �϶���ġ�� �ٸ�
���� ���� ȭ���� �����.
������ ����(�߰��̺�Ʈ���� ���� �ȵ�)
������ �ٳ���
�մ� ����
*/
namespace PizzaNS
{
    public enum Ingredient { TOMATO, CHEESE, BASIL, POTATO, BACON, CORN, JALAPENO, CHICKEN };

    public struct IngredientS
    {
        public Ingredient Ingred;   // ���
        public int Attractiveness;  // �ŷµ�
        public int DeclineAt;    // �ŷ� �϶���
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