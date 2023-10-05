using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingAddressNS;

// �Ѽ�ȣ �ۼ�

public class GoalCheckCollider : MonoBehaviour, IPriorityCode
{
    private List<IPriorityCode> priorityList = new List<IPriorityCode>();

    private IDeliveryPanelControl iDeliveryPanelControl;
    public IHouse iHouse;
    public AddressS addr;
    // ��ǥ������ ���޽�, ��� �г��� ����.
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<IPriorityCode>() != null)
        {
            priorityList.Add(collision.gameObject.GetComponent<IPriorityCode>());
            // �켱������ ����Ͽ� ���� �켱������ ���� ���� ������ �������� Ʈ���� �ߵ� �� �ؾ��ϴ� ������ ���´�.
            if (!CheckPriority())
            {
                return;
            }
        }
        //Debug.Log("�۵�");

        if (collision.tag.Equals("Player"))
        {
            if (iDeliveryPanelControl == null) { return; }
            iDeliveryPanelControl.SetIHouseDeliveryUI(iHouse);
            iDeliveryPanelControl.ControlDeliveryUI(true);
            GameObject.Find("InventoryManager").GetComponent<InventoryManager>().GoalAddressS = this.GetComponent<GoalCheckCollider>();
        }
    }
    private bool CheckPriority()
    {
        if (priorityList.FindIndex(a => a.GetPriorityCode() > GetPriorityCode()) != -1)
        {
            // ���� �Ұ�
            return false;
        }
        else
        {
            // ���� ����
            return true;
        }
    }
    public void GetAddress(AddressS addressS)
    {
        addr = addressS;
    }


    public int GetPriorityCode()
    {
        return (addr.BuildingAddress * 1000 + addr.HouseAddress);
    }

    public void SetIDeliveryPanelControl(IDeliveryPanelControl iDeliveryPanelControl, IHouse iHouse)
    {
        this.iDeliveryPanelControl = iDeliveryPanelControl;
        this.iHouse = iHouse;
    }
}
