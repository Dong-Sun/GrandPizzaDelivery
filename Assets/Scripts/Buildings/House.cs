using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingAddressNS;

// �Ѽ�ȣ �ۼ�

public class House : MonoBehaviour, IAddress, IHouse
{
    static private Color activeColor = new Color(248, 70, 6);

    private SpriteRenderer spriteRenderer;
    private IMap iMap;

    private int houseNumber;
    private bool isEnable = false;

	private void Awake()
	{
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
	}

	public void InitAddress(int number, List<AddressS> addressSList)
    {
        houseNumber = number % 1000;

        addressSList.Add(new AddressS(number / 1000, houseNumber, this));
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
	}

    public void DisableHouse()
	{
        spriteRenderer.color = Color.white;
        isEnable = false;
	}

}
