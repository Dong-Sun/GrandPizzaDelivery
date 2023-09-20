using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingAddressNS;

// �Ѽ�ȣ �ۼ�

// ������ �Ѹ��� �մ��� �����ϸ�, �մ��� ������ ���� �������̴�. ��, �ѹ� ������ ������ �ٲ��� �ʴ´�.
public class House : MonoBehaviour, IAddress, IHouse
{
    // 0 = ��, 1 = ��, 2 = ��, 3 = ��
    [SerializeField] private int direction = 0;
    [SerializeField] private GameObject goalObj;

    static private Color activeColor = new Color(248/255f, 70/255f, 6/255f);   // Ȱ��ȭ ��(����ؾ� �ϴ� ������ �˸�)

    private SpriteRenderer spriteRenderer;
    private Transform goalTrans;

    private IMap iMap;
    private IDeliveryPanelControl iDeliveryPanelControl;

    private AddressS houseAddress;  // ���ּ�

    private float spendingTime; // ��޿� �ҿ��� �ð�
    private int houseNumber;    // �ǹ� ������ �� ��ȣ
    private bool isEnable = false;
    
	private void Awake()
	{
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        goalTrans = goalObj.transform;

        if (direction == 0) { goalTrans.position += new Vector3(0, 1); }
        else if (direction == 1) { goalTrans.position += new Vector3(0, -1); }
        else if (direction == 2) { goalTrans.position += new Vector3(-1, 0); }
        else if (direction == 3) { goalTrans.position += new Vector3(1, 0); }
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
}
