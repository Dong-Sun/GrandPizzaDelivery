using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ѽ�ȣ �ۼ�


namespace BuildingNS
{
    public enum BuildingShape {SQUARE, WIDTHLONG, LENGTHLONG, COMPOSITE };  
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

        public IHouse iHouse;
        public AddressS(int BuildingAddress, int HouseAddress, IHouse iHouse)
		{
            this.BuildingAddress = BuildingAddress;
            this.HouseAddress = HouseAddress;
            this.iHouse = iHouse;
		}
	}
}
/*
������ ������ �������� �մԵ��� �����Ѵ�. �ѹ� ������ ������ �մԵ��� �� �����ȴ�.
�������� �÷��̾ �߰��Ѵ�.(����)
�ӵ��踦 �����.
������ �����.
���� ���� ȭ���� �����.
������ ����(�߰��̺�Ʈ���� ���� �ȵ�)
������ �ٳ���
�մ� ����
*/
namespace CustomerNS
{
    public struct CustomerS
	{
        //��ȣ ���
        //���� �ϼ��� ĿƮ����

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