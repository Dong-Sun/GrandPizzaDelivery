using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ѽ�ȣ �ۼ�

public class PoliceCarCollisionCheck : MonoBehaviour
{    
    private List<IPriorityCode> priorityList = new List<IPriorityCode>();

    private IMovingPoliceCarControl iPoliceCarControl;
    private IPriorityCode iPriorityCode;
    //�������� �ٸ� ���������� �浹�� ����� �ִ��� üũ�Ѵ�.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�浹�� ����� �ִٸ� �ڵ����� �ൿ�� �����Ѵ�.
        if (collision.gameObject.GetComponent<IPriorityCode>() != null)
        {
            priorityList.Add(collision.gameObject.GetComponent<IPriorityCode>());
            // ��� �켱������ ����Ѵ�.
            CheckPriority();
        }
    }

    // ��ó�� �ִ� ��������� �浹���� �ʰԲ��ϱ� ���� �켱������ ���� ���� ������ �ڵ����� �����ִ� �Լ��̴�.
    private void CheckPriority()
    {
        if (iPoliceCarControl == null) { return; }
        if (priorityList == null) { return; }

        if (priorityList.FindIndex(a => a.GetPriorityCode() > iPriorityCode.GetPriorityCode()) != -1)
        {
            iPoliceCarControl.SetIsBehaviour(false);
        }
        else
        {
            iPoliceCarControl.SetIsBehaviour(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IMovingPoliceCarControl>() != null)
        {
            priorityList.Remove(collision.gameObject.GetComponent<IPriorityCode>());
            // 1�� �Ŀ� �켱������ ����Ѵ�.
            Invoke("CheckPriority", 1f);
        }
    }

    public void SetIPoliceCarIsBehaviour(IMovingPoliceCarControl iPoliceCarIsBehaviour)
    {
        this.iPoliceCarControl = iPoliceCarIsBehaviour;
    }

    public void SetIPriority(IPriorityCode iPriorityCode)
    {
        this.iPriorityCode = iPriorityCode;
    }
}
