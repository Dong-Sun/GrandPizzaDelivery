using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingAddressNS;
using BuildingNS.HouseNS;

// �Ѽ�ȣ �ۼ�


//�ʿ� �����ؾ��� ������Ʈ���� ��ġ�ϰ�, �ǹ����� �ּҸ� �ٿ������ν� ���� �����մϴ�. 
public class Map : MonoBehaviour, IMap, IStop
{
    [SerializeField] private GameObject uiControlObj;
    [SerializeField] private GameObject policeCar;
    [SerializeField] private GameObject effectControl;
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private Sprite[] houseMarkArr;

    // addressList�� ���� ������ �ּҸ� �ʱ�ȭ�ϰų� �޾ƿ� �� �ֽ��ϴ�.
    private List<IAddress> addressList = new List<IAddress>();
    // �� �ǹ��� �������� �����ϱ� ���� ����Ʈ�Դϴ�.
    private List<IBuilding> buildingList = new List<IBuilding>();
    private List<IPoliceCar> policeList = new List<IPoliceCar>();

    private List<AddressS> houseAddressList = new List<AddressS>();
    private List<AddressS> temHouseAddressList = new List<AddressS>();
    private Dictionary<AddressS, float> deliveryTimeDic = new Dictionary<AddressS, float>();
    void Awake()
    {
        // �ǹ��� �ּҸ� �ٿ��ݴϴ�.
        int n = 0;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).GetComponent<IAddress>() != null)
            {
                GameObject ob = this.transform.GetChild(i).gameObject;
                addressList.Add(ob.GetComponent<IAddress>());
                addressList[n].InitAddress(n, houseAddressList);
                addressList[n].SetIMap(this);
                addressList[n].SetIDeliveryPanelControl(uiControlObj.GetComponent<IDeliveryPanelControl>());
                addressList[n].SetIHouseActiveUIControl(uiControlObj.GetComponent<IHouseActiveUIControl>());
                buildingList.Add(ob.GetComponent<IBuilding>());
                n++;
            }
        }
        uiControlObj.GetComponent<UIControl>().SetIStop(this);

    }

    private void Start()
    {   
        // �������� ��ũ�� ���δ�.
        houseAddressList[36].IHouse.SetHouseType(houseMarkArr[0], HouseType.PIZZASTORE);

        MakeAPoliceCar(45);
    }
    private void test()
    {
        List<AddressS> ad = GetRandAddressSList(5);
        for (int i = 0; i < ad.Count; i++)
        {
            ad[i].IHouse.EnableHouse();
        }
    }

    // �������� ������ �ǹ����� �������ִ� �Լ��Դϴ�.
    private void MakeAPoliceCar(int cnt)
    {
        if (cnt >= buildingList.Count) { cnt = buildingList.Count; }
        // �ǹ����� �������� �ؼ� �������� ��ȯ�մϴ�. �������� ��ȯ�Ǵ� �ǹ��� �����Դϴ�.
        while (cnt > 0)
        {
            // �������� �ǹ��� �߿� �ϳ��� ���ϴ�.
            int ran = Random.Range(0, buildingList.Count);
            // �ش� �ǹ��� �������� �̹� �����Ǿ� �ִ��� Ȯ���ϰ� �����Ǿ� ���� �ʾƾ� �������� ������ �� ������ ���ǹ����� ǥ���մϴ�.
            if (!buildingList[ran].GetIsPoliceCar())
            {
                // �������� �����ϱ⿡ �ռ� �ǹ��� ���� �ǹ��� ��ġ�� Ȯ���մϴ�.
                // �ǹ��� ��翡 ���� �������� ��ġ�� �޶�����.
                GameObject policeCar = Instantiate(this.policeCar);
                policeCar.transform.position = buildingList[ran].GetpoliceCarDis() + buildingList[ran].GetBuildingPos();

                if (policeCar.GetComponent<IPoliceCar>() != null)
                {
                    policeList.Add(policeCar.GetComponent<IPoliceCar>());
                    policeCar.GetComponent<IPoliceCar>().SetPlayerMove(playerMove);
                    policeCar.GetComponent<IPoliceCar>().SetPoliceSmokeEffect(effectControl.GetComponent<IPoliceSmokeEffect>());
                    policeCar.GetComponent<IPoliceCar>().SetMap(this);
                    // �� ���������� �ǹ��� �´� ��Ʈ�� ¥�� �Ѱܾ��Ѵ�.
                    if (buildingList[ran].GetPolicePath().Count != 0)
                    {
                        policeCar.GetComponent<IPoliceCar>().InitPoliceCarPath(buildingList[ran].GetPolicePath());
                    }
                    policeCar.GetComponent<IPoliceCar>().SetIInspectingPanelControl(uiControlObj.GetComponent<IInspectingPanelControl>());
                }
                // �������� �����Ǿ����Ƿ� cnt�� �ϳ� ������, �������� �����Ǿ����� �ǹ�(Building)�� �˸��ϴ�.
                buildingList[ran].SetIsPoliceCar(true);
                cnt--;
            }
        }
    }
    public void AddAddress(AddressS addressS)
    {
        deliveryTimeDic.Add(addressS, GameManager.Instance.time);
    }
    // ����� ���� �ش� �ּҿ����� �ð���⸦ ������ �ҿ��� ��޽ð��� ��ȯ�մϴ�.
    public float RemoveAddress(AddressS addressS)
    {
        foreach (var addr in deliveryTimeDic.Keys)
        {
            if (addr.BuildingAddress == addressS.BuildingAddress 
                && addr.HouseAddress == addressS.HouseAddress
                && addr.IHouse == addressS.IHouse)
            {
                float f = GameManager.Instance.time - deliveryTimeDic[addr]; 
                deliveryTimeDic.Remove(addr);
                return f;
            }
        }
        return -1f;
    }
	/// <summary>
	/// ������ ���ּ� ���� ����  �˷��ش�.
	/// </summary>
	/// <param name="n">���ּҵ��� �����̴�.</param>
	/// <returns>��ȯ������ List<AddressS> �����̴�. </returns>
	public List<AddressS> GetRandAddressSList(int n)
	{
        if (houseAddressList.Count == 0) { return null; }

        temHouseAddressList.Clear();
        for (int i = 0; i < houseAddressList.Count; i++)
		{
            temHouseAddressList.Add(houseAddressList[i]);
		}

        List<AddressS> list = new List<AddressS>();
        int r = 0;
        for (int i = 0; i < n; i++)
		{
            while (true)
            {
                r = Random.Range(0, temHouseAddressList.Count);
                if (temHouseAddressList[r].IHouse.GetHouseType() == HouseType.HOUSE)
                {
                    break;
                }
            }
            list.Add(temHouseAddressList[r]);
            temHouseAddressList.RemoveAt(r);
		}

        return list;
	}
    /// <summary>
    /// ������ �� �ּ� 1���� �˷��ش�. �ּҴ� ��Ȱ��ȭ�� �͵��߿��� ����.
    /// </summary>
    /// <returns>��ȯ ������ AddressS�̴�.</returns>
    public AddressS GetRandAddressS()
	{
        int r = 0;
        while (true)
        {
            r = Random.Range(0, houseAddressList.Count);

            if (!houseAddressList[r].IHouse.GetIsEnable() && houseAddressList[r].IHouse.GetHouseType() == HouseType.HOUSE)
            {
                Debug.Log(houseAddressList[r].IHouse.GetLocation());
                return houseAddressList[r];
            }
        }

    }

    public void StopMap(bool bo) 
    {
        int n = 0;

        if (bo)
        {
            while(true)
            {
                n = policeList.Count;
                for (int i = 0; i < n; i++)
                {
                    if (n != policeList.Count) { break; }
                    policeList[i].SetIsStop(true);
                }
                break;
            }
            playerMove.Stop = true;
        }
        else
        {
            while (true)
            {
                n = policeList.Count;
                for (int i = 0; i < n; i++)
                {
                    if (n != policeList.Count) { break; }
                    policeList[i].SetIsStop(false);
                }
                break;
            }
            playerMove.Stop = false;
        }
    }

    public void RemovePoliceList(IPoliceCar iPoliceCar)
    {
        policeList.Remove(iPoliceCar);
    }
}
