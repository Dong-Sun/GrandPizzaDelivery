using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ѽ�ȣ �ۼ�

public class PoliceCarCollisionCheck : MonoBehaviour
{
    private IMovingPoliceCarControl iPoliceCarControl;
    private List<IMovingPoliceCarControl> otherIPoliceCarIsBehaviourList = new List<IMovingPoliceCarControl>();
    //�������� �ٸ� ���������� �浹�� ����� �ִ��� üũ�Ѵ�.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�浹�� ����� �ִٸ� �ڵ����� �ൿ�� �����Ѵ�.
        if (collision.gameObject.GetComponent<IMovingPoliceCarControl>() != null)
        {
            otherIPoliceCarIsBehaviourList.Add(collision.gameObject.GetComponent<IMovingPoliceCarControl>());
            // ��� �켱������ ����Ѵ�.
            CheckPriority();
        }
    }

    // ��ó�� �ִ� ��������� �浹���� �ʰԲ��ϱ� ���� �켱������ ���� ���� ������ �ڵ����� �����ִ� �Լ��̴�.
    private void CheckPriority()
    {
        if (iPoliceCarControl == null) { return; }

        if (otherIPoliceCarIsBehaviourList.FindIndex(a => a.GetPoliceCarCode() > iPoliceCarControl.GetPoliceCarCode()) != -1)
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
            otherIPoliceCarIsBehaviourList.Remove(collision.gameObject.GetComponent<IMovingPoliceCarControl>());
            // 1�� �Ŀ� �켱������ ����Ѵ�.
            Invoke("CheckPriority", 1f);
        }
    }

    public void SetIPoliceCarIsBehaviour(IMovingPoliceCarControl iPoliceCarIsBehaviour)
    {
        this.iPoliceCarControl = iPoliceCarIsBehaviour;
    }
}
