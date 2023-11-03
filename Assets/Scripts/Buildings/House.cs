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

    public static Color activeColor = new Color(248/255f, 70/255f, 6/255f);   // Ȱ��ȭ ��(����ؾ� �ϴ� ������ �˸�)

    private SpriteRenderer spriteRenderer;
    private Transform goalTrans;

    private IMap iMap;
    private IDeliveryPanelControl iDeliveryPanelControl;
    private IHouseActiveUIControl iHouseActiveUIControl;

    private HouseType houseType;

    private Color houseColor;

    private CustomerS customerS;
    private AddressS houseAddress;  // ���ּ�

    private float spendingTime; // ��޿� �ҿ��� �ð�
    private int houseNumber;    // �ǹ� ������ �� ��ȣ
    private bool isEnable = false;  // �ش� ���� �ֹ��� �ؾߵǴ��� ����
    private bool inHouse = false;
    
	private void Awake()
	{
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        houseColor = Color.HSVToRGB(Random.Range(0, 361f) / 360f, 12 / 100f, 100 / 100f);
        spriteRenderer.color = houseColor;
        goalTrans = goalObj.transform;
        Vector3 vec = Vector3.zero;
        if (direction == 0) { vec = new Vector3(0, 1); }
        else if (direction == 1) { vec = new Vector3(0, -1); }
        else if (direction == 2) { vec = new Vector3(-1, 0); }
        else if (direction == 3) { vec = new Vector3(1, 0); }
        goalObj.transform.position += vec;
        activeObj.transform.position += vec;

        customerS = new CustomerS(Random.Range(1, 101), Random.Range(60, 240), Random.Range(0, 4), Random.Range(200, 2000));
        activeObj.GetComponent<HouseActiveCheck>().SetIActiveHouse(this);
        houseType = HouseType.HOUSE;
        SetCustomer();
	}
    /// <summary>
    /// �� ����
    /// </summary>
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
    /// <summary>
    /// �ּ� �ʱ�ȭ
    /// </summary>
    /// <param name="number"></param>
    /// <param name="addressSList"></param>
	public void InitAddress(int number, List<AddressS> addressSList)
    {
        houseNumber = number % 1000;
        houseAddress = new AddressS(number / 1000, houseNumber, this);
        goalObj.GetComponent<GoalCheckCollider>().addr = houseAddress;

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
    /// <summary>
    /// ���� ��¦�̸� Ȱ��ȭ�ȴ�.
    /// Ȱ��ȭ�Ǹ� �ʿ� �ش� ���� ǥ�õǸ�, Map Ŭ�������� �ð��� ��� �����Ѵ�.
    /// </summary>
    public void EnableHouse()
	{
        spriteRenderer.color = activeColor;
        isEnable = true;
        goalObj.SetActive(true);
        iMap.AddAddress(houseAddress);
    }
    /// <summary>
    /// ���� ����� �������� ��
    /// ����� ���� �� �ɸ� �ð�, ���� ���� ����ü �������� ��� �����Ѵ�.
    /// </summary> 
    public void DisableHouse(Pizza pizza)
	{
        // ���޹��� ���ڸ� �մ��� ����� ���ؼ� ���� �󸶳� ���� ���ϰ�, ������ �󸶳� ���� ���Ѵ�.
        spriteRenderer.color = houseColor;
        isEnable = false;
        goalObj.SetActive(false);
        spendingTime = iMap.RemoveAddress(houseAddress);
	}

    public bool GetIsEnable()
	{
        return isEnable;
	}
    /// <summary>
    /// ��� ���� ���θ� ������ �������̽� ����.
    /// </summary>
    /// <param name="iDeliveryPanelControl"></param>
    public void SetIDeliveryPanelControl(IDeliveryPanelControl iDeliveryPanelControl)
    {
        this.iDeliveryPanelControl = iDeliveryPanelControl;
        goalObj.GetComponent<GoalCheckCollider>().SetIDeliveryPanelControl(iDeliveryPanelControl, this);
    }
    /// <summary>
    /// �� ��ó���� ������ �� �ִ� ����� ������ �гο� ���� �������̽� ����
    /// </summary>
    /// <param name="iHouseActiveControl"></param>
    public void SetIHouseActiveUIControl(IHouseActiveUIControl iHouseActiveControl)
    {
        this.iHouseActiveUIControl = iHouseActiveControl;
        activeObj.GetComponent<HouseActiveCheck>().SetIHouseActiveUIControl(iHouseActiveControl);

    }
    /// <summary>
    /// Ư�� ������ �ΰ� ���δ�.
    /// </summary>
    /// <param name="mark">�ΰ� ��������Ʈ</param>
    /// <param name="houseType">�� Ÿ��</param>
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
    /// <summary>
    /// �� ��ó�� ���� �� Ÿ�Կ� ���� ������ ������ �ٸ��� ��
    /// </summary>
    /// <param name="bo"></param>
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
    /// <summary>
    /// �Ϲ� ���� ��쿡�� �� ��ó�� ���� ���� ������ ������ ����
    /// </summary>
    /// <param name="bo"></param>
    /// <returns></returns>
    public bool ActiveHouse(bool bo)
    {
        if (houseType != HouseType.NONE && houseType != HouseType.HOUSE)
        {
            SetInHouse(bo);
            // ���� ������ �ٲ���
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
    /// <summary>
    /// �� ��ó�� ���� ���� �� �ٲ���
    /// </summary>
    /// <param name="bo"></param>
    private void ChangeColor(bool bo)
    {
        if (!isEnable)
        {
            if (bo)
            {
                spriteRenderer.color = Color.red;
            }
            else
            {
                spriteRenderer.color = houseColor;
            }
        }
    }

	public Transform GetLocation()
	{
        return this.transform;
	}
}
