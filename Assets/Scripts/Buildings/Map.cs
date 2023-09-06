using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingNS;

//�ʿ� �����ؾ��� ������Ʈ���� ��ġ�ϰ�, �ǹ����� �ּҸ� �ٿ������ν� ���� �����մϴ�. 
public class Map : MonoBehaviour
{
    [SerializeField] private GameObject policeCar;

    // addressList�� ���� ������ �ּҸ� �ʱ�ȭ�ϰų� �޾ƿ� �� �ֽ��ϴ�.
    private List<IAddress> addressList = new List<IAddress>();
    private List<IBuilding> buildingList = new List<IBuilding>();
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
                addressList[n].InitAddress(n);
                buildingList.Add(ob.GetComponent<IBuilding>());
                n++;
            }
        }
    }

    private void Start()
    {

        MakeAPoliceCar(15);
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
                // �� ���������� �ǹ��� �´� ��Ʈ�� ¥�� �Ѱܾ��Ѵ�.


                // �������� �����Ǿ����Ƿ� cnt�� �ϳ� ������, �������� �����Ǿ����� �ǹ�(Building)�� �˸��ϴ�.
                buildingList[ran].SetIsPoliceCar(true);
                cnt--;
            }
        }
    }
}
