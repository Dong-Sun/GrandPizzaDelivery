using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingNS;
using PoliceNS.PolicePathNS;
using BuildingAddressNS;

// �Ѽ�ȣ �ۼ�

public class Building : MonoBehaviour, IAddress, IBuilding
{
    [SerializeField] private BuildingShape buildingShape;   // ������ ���
    [SerializeField] private Vector2 compositePos;  // �������� ���·� ���� �������� �� �������� ���� ��ġ�� ���Ƿ� ���ϱ� ���� ����
    [SerializeField] private float[] policeCarValue;    // �������� �����ؾ� �ϴ� �ൿ�� �Ÿ�, Ȥ�� ȸ�� ��
    [SerializeField] private int[] policeCarBehaviour;  // �������� �����ؾ� �ϴ� �ൿ ��ȣ

    private List<PolicePath> pathList = new List<PolicePath>(); // ������ �����ϴ� ��� ����Ʈ

    private Vector2 policeCarDis; // �������� ������ �� �� �ǹ��� �������� �Ÿ��Դϴ�.
    private Vector2 buildingPos;    // ������ Position��

    private int buildingNumber; // ������ ���� ��ȣ
    private bool isPoliceCar;   // �� ������ �������� �����ؾ��ϴ��� ����

    public void Awake()
    {

        buildingPos = this.gameObject.transform.position;
        // ������ ��翡 ���� �������� ���� ��ġ�� �޶�����.
        if (buildingShape == BuildingShape.SQUARE)
        {
            policeCarDis = new Vector2(-2, 5);
        }
        else if (buildingShape == BuildingShape.WIDTHLONG)
        {
            policeCarDis = new Vector2(-1, 5);
        }
        else if (buildingShape == BuildingShape.LENGTHLONG)
        {
            policeCarDis = new Vector2(-2, 7);
        }
        else if (buildingShape == BuildingShape.COMPOSITE)
        {
            policeCarDis = compositePos;
        }

        for (int i = 0; i < policeCarBehaviour.Length; i++)
        {
            // �� �������� ������ �� �ִ� ��� ���� ��� ����Ʈ�� �־��ش�.
            pathList.Add(new PolicePath(policeCarBehaviour[i], policeCarValue[i]));
        }
    }
    /// <summary>
    /// �ּҰ� �ʱ�ȭ
    /// </summary>
    /// <param name="number">������ȣ</param>
    /// <param name="addressSList">�� �ּ� ���</param>
    public void InitAddress(int number, List<AddressS> addressSList)
    {
        buildingNumber = number;
        int n = 0;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).GetComponent<IAddress>() != null)
            {
                this.transform.GetChild(i).GetComponent<IAddress>().InitAddress(number * 1000 + n, addressSList);
                n++;
            }
        }
    }
    /// <summary>
    /// �������� IMap �������̽��� �����Ͽ� �� �ּҸ� �����ϱ� ����
    /// </summary>
    /// <param name="iMap"></param>
    public void SetIMap(IMap iMap)
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).GetComponent<IAddress>() != null)
            {
                this.transform.GetChild(i).GetComponent<IAddress>().SetIMap(iMap);
            }
        }
    }
    /// <summary>
    /// ���� ��ȣ ��ȯ
    /// </summary>
    /// <returns></returns>
    public int GetAddress()
    {
        return buildingNumber;
    }
    /// <summary>
    /// �ش� ������ �������� �ִ��� ���� ��ȯ
    /// </summary>
    /// <returns></returns>
    public bool GetIsPoliceCar()
    {
        return isPoliceCar;
    }
    /// <summary>
    /// �ش� ������ ���ƴٴϴ� �������� ��� ��ȯ
    /// </summary>
    /// <returns></returns>
    public List<PolicePath> GetPolicePath()
    {
        return pathList;
    }
    /// <summary>
    /// �ش� ������ �������� ���ƴٴϴ� �� �ƴ��� ����
    /// </summary>
    /// <param name="b"></param>
    public void SetIsPoliceCar(bool b)
    {
        isPoliceCar = b;
    }
    /// <summary>
    /// �������� ������ġ���� �ǹ������� �Ÿ� ��ȯ
    /// </summary>
    /// <returns></returns>
    public Vector2 GetpoliceCarDis()
    {
        return policeCarDis;
    }
    /// <summary>
    /// ���� ��ġ ��ȯ
    /// </summary>
    /// <returns></returns>
    public Vector2 GetBuildingPos()
    {
        return buildingPos;
    }
    /// <summary>
    /// ������ ���� �� ��� ���� UI�� �� �� �ֵ��� �������̽��� �������� ���� 
    /// </summary>
    /// <param name="iDeliveryPanelControl"></param>
    public void SetIDeliveryPanelControl(IDeliveryPanelControl iDeliveryPanelControl)
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).GetComponent<IAddress>() != null)
            {
                this.transform.GetChild(i).GetComponent<IAddress>().SetIDeliveryPanelControl(iDeliveryPanelControl);
            }
        }
    }
    /// <summary>
    /// �������� �������̽��� ����
    /// </summary>
    /// <param name="iHouseActiveControl"></param>
    public void SetIHouseActiveUIControl(IHouseActiveUIControl iHouseActiveControl)
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).GetComponent<IAddress>() != null)
            {
                this.transform.GetChild(i).GetComponent<IAddress>().SetIHouseActiveUIControl(iHouseActiveControl);
            }
        }
    }
}
