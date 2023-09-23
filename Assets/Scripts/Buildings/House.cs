using BuildingAddressNS;
using BuildingNS.HouseNS;
using PizzaNS;
using PizzaNS.CustomerNS;
using System.Collections.Generic;
using UnityEngine;

// �Ѽ�ȣ �ۼ�

// ������ �Ѹ��� �մ��� �����ϸ�, �մ��� ������ ���� �������̴�. ��, �ѹ� ������ ������ �ٲ��� �ʴ´�.
public class House : MonoBehaviour, IAddress, IHouse, IActiveHouse
{
    // 0 = ��, 1 = ��, 2 = ��, 3 = ��
    [SerializeField] private int direction = 0;
    [SerializeField] private GameObject goalObj;
    [SerializeField] private GameObject activeObj;

    static private Color activeColor = new Color(248/255f, 70/255f, 6/255f);   // Ȱ��ȭ ��(����ؾ� �ϴ� ������ �˸�)

    private SpriteRenderer spriteRenderer;
    private Transform goalTrans;

    private IMap iMap;
    private IDeliveryPanelControl iDeliveryPanelControl;
    private IHouseActiveUIControl iHouseActiveUIControl;

    private HouseType houseType;
    
    private CustomerS customerS;
    private AddressS houseAddress;  // ���ּ�

    private float spendingTime; // ��޿� �ҿ��� �ð�
    private int houseNumber;    // �ǹ� ������ �� ��ȣ
    private bool isEnable = false;  // �ش� ���� �ֹ��� �ؾߵǴ��� ����
    private bool inHouse = false;
	private void Awake()
	{
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        goalTrans = goalObj.transform;
        Vector3 vec = Vector3.zero;
        if (direction == 0) { vec = new Vector3(0, 1); }
        else if (direction == 1) { vec = new Vector3(0, -1); }
        else if (direction == 2) { vec = new Vector3(-1, 0); }
        else if (direction == 3) { vec = new Vector3(1, 0); }
        goalObj.transform.position += vec;
        activeObj.transform.position += vec;

        activeObj.GetComponent<HouseActiveCheck>().SetIActiveHouse(this);
        houseType = HouseType.HOUSE;
        SetCustomer();
	}
    // �� ����
    private void SetCustomer()
    {
        List<Ingredient> ing = new List<Ingredient>();
        int r = 0;
        while (ing.Count < 2)
        {
            r = Random.Range(0, System.Enum.GetValues(typeof(Ingredient)).Length);
            if (ing.FindIndex(a => a.Equals((Ingredient)r)) == -1 && r != 0)
            {
                ing.Add((Ingredient)r);
            }
        }
    }
	public void InitAddress(int number, List<AddressS> addressSList)
    {
        houseNumber = number % 1000;
        houseAddress = new AddressS(number / 1000, houseNumber, this);

        addressSList.Add(houseAddress);
    }
    public void SetIMap(IMap iMap)
	{
        this.iMap = iMap;
	}
    public int GetAddress()
    {
        return houseNumber;
    }

    // ���� ��¦�̸� Ȱ��ȭ�ȴ�.
    // Ȱ��ȭ�Ǹ� �ʿ� �ش� ���� ǥ�õǸ�, Map Ŭ�������� �ð��� ��� �����Ѵ�.
    public void EnableHouse()
	{
        spriteRenderer.color = activeColor;
        isEnable = true;
        goalObj.SetActive(true);
        iMap.AddAddress(houseAddress);
	}

    // ���� ����� �������� ��
    // ����� ���� �� �ɸ� �ð�, ���� ���� ����ü �������� ��� �����Ѵ�.
    public void DisableHouse()
	{
        spriteRenderer.color = Color.white;
        isEnable = false;
        goalObj.SetActive(false);
        spendingTime = iMap.RemoveAddress(houseAddress);
	}

    public bool GetIsEnable()
	{
        return isEnable;
	}

    public void SetIDeliveryPanelControl(IDeliveryPanelControl iDeliveryPanelControl)
    {
        this.iDeliveryPanelControl = iDeliveryPanelControl;
        goalObj.GetComponent<GoalCheckCollider>().SetIDeliveryPanelControl(iDeliveryPanelControl, this);
    }
    public void SetIHouseActiveUIControl(IHouseActiveUIControl iHouseActiveControl)
    {
        this.iHouseActiveUIControl = iHouseActiveControl;
        activeObj.GetComponent<HouseActiveCheck>().SetIHouseActiveUIControl(iHouseActiveControl);

    }
    // Ư�� ������ �ΰ� ���δ�.
    public void SetHouseType(Sprite mark, HouseType houseType)
    {
        GameObject obj = new GameObject();
        obj.transform.parent = this.transform;
        obj.transform.localPosition = Vector3.zero;
        obj.AddComponent<SpriteRenderer>();
        obj.GetComponent<SpriteRenderer>().sprite = mark;
        obj.GetComponent<SpriteRenderer>().sortingOrder = 200;
        this.houseType = houseType;
    }
    public void IntoHouse(bool bo)
    {
        if (bo)
        {
            iHouseActiveUIControl.SetHouseType(houseType);
        }
        else
        {
            iHouseActiveUIControl.SetHouseType(HouseType.NONE);
        }
    }
    public HouseType GetHouseType()
    {
        return houseType;
    }
    public bool ActiveHouse(bool bo)
    {
        if (houseType != HouseType.NONE && houseType != HouseType.HOUSE)
        {
            SetInHouse(bo);
            ChangeColor(bo);
            return true;
        }
        else
        {
            return false;
        }
    }
    private  void SetInHouse(bool bo)
    {
        inHouse = bo;
    }
    // �� ��ó�� ���� ���� �� �ٲ��ֱ�.
    private void ChangeColor(bool bo)
    {
        if (!isEnable)
        {
            if (bo)
            {
                spriteRenderer.color = Color.cyan;
            }
            else
            {
                spriteRenderer.color = Color.white;
            }
        }
    }

	public Transform GetLocation()
	{
        return this.transform;
	}
}
