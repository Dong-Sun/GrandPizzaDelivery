using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ѽ�ȣ �ۼ�

public class PoliceCarCollisionCheck : MonoBehaviour
{    
    private List<IPriorityCode> priorityList = new List<IPriorityCode>();

    private IMovingPoliceCarControl iPoliceCarControl;  // ������ ���� �������̽�
    private IPriorityCode iPriorityCode;    // ������ �켱���� �������̽�

    /// <summary>
    /// �������� �ٸ� ���������� �浹�� ����� �ִ��� üũ�Ѵ�.
    /// </summary>
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

    /// <summary>
    /// ��ó�� �ִ� ��������� �浹���� �ʰԲ��ϱ� ���� �켱������ ���� ���� ������ �ڵ����� �����ִ� �Լ��̴�.
    /// </summary>
    private void CheckPriority()
    {

        if (iPoliceCarControl == null) { return; }
        if (priorityList == null) { return; }
        // ���� �ݶ��̴��� �����ִ� ���������� �켱���� �߿��� �ش� �������� ���� �켱������ ������ Ȯ���ϴ� �����̴�.
        if (priorityList.FindIndex(a => a.GetPriorityCode() > iPriorityCode.GetPriorityCode()) != -1)
        {
            // ���� �������� �ൿ�� �����.
            iPoliceCarControl.SetIsBehaviour(false);
        }
        else
        {
            // ���� �������� �ൿ�� �簳�Ѵ�.
            iPoliceCarControl.SetIsBehaviour(true);
        }
    }
    /// <summary>
    /// �ٸ� �������� �ϳ� �������� 1�� �Ŀ� �켱������ �ٽ� ����Ͽ� �������� ���¸� ������.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IMovingPoliceCarControl>() != null)
        {
            priorityList.Remove(collision.gameObject.GetComponent<IPriorityCode>());
            // 1�� �Ŀ� �켱������ ����Ѵ�.
            Invoke("CheckPriority", 1f);
        }
    }
    /// <summary>
    /// ������ ��� ���� �������̽��� �����´�.
    /// </summary>
    /// <param name="iPoliceCarIsBehaviour"></param>
    public void SetIPoliceCarIsBehaviour(IMovingPoliceCarControl iPoliceCarIsBehaviour)
    {
        this.iPoliceCarControl = iPoliceCarIsBehaviour;
    }
    /// <summary>
    /// ������ �켱���� ��ȣ�� ���õ� �������̽��� �����´�.
    /// </summary>
    /// <param name="iPriorityCode"></param>
    public void SetIPriority(IPriorityCode iPriorityCode)
    {
        this.iPriorityCode = iPriorityCode;
    }
}
