using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoliceNS.PoliceStateNS;
// �Ѽ�ȣ �ۼ�

public class StopPoliceCarCollisionCheck : MonoBehaviour
{
    private List<IPriorityCode> priorityList = new List<IPriorityCode>();

    private IMovingPoliceCarControl iPoliceCarControl;  // ������ ��� ���� �������̽�
    private IPriorityCode iPriorityCode;    // ������ �켱���� ��ȣ�� ���� �������̽�
    private IConversationPanelControl iInspectingPanelControl;  // ��ȭâ UI�� �����ϱ� ���� �������̽�
    private IInspectingPoliceCarControl iInspectingPoliceCarControl;    // �������� ���¿� ���� �ҽɰ˹� ���θ� ������ ���� �������̽�
    private IEndConversation iEndInspecting;    // ��ȭâ UI�� ������ �� ������ �ϵ��� �ٷ�� �Լ��� ��� �������̽�

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���������� �ҽɰ˹� ������ �ߺ��Ǿ��� �� �� �������� �ҽɰ˹��� �ߵ���Ű���� �Ѵ�. 
        if (collision.gameObject.GetComponent<IPriorityCode>() != null)
        {
            // �켱���� ����Ʈ�� �켱���� ��ȣ�� ������ �� �ִ� �������̽��� �ִ´�.
            priorityList.Add(collision.gameObject.GetComponent<IPriorityCode>());
            // �켱������ ����Ͽ� ���� �켱������ ���� �������� ������ �������� �ҽɰ˹��� ���´�.
            if (!CheckPriority())
			{
                return;
			}
        }

        // �±װ� �÷��̾��� �������� �ҽɰ˹��� ���¿� ����.
        if (collision.tag == "Player")
        {

            // �ҽɰ˹� ���� �� �ٸ� ���������� �ߺ��ؼ� �ҽɰ˹��� ���� ���ϵ��� �Ѵ�.
            if (!PoliceCar.IsInspecting)
            {
                // ���¸� �ҽɰ˹������� �ٲ۴�.
                iInspectingPoliceCarControl.SetPoliceState(PoliceState.INSPECTING);

                // ī�޶� �ش� ������������ Ȯ�� �� �̵���Ű��, �ҽɰ˹��� â�� ����.
                iInspectingPanelControl.ControlConversationUI(true, iEndInspecting, 1);
            }
        }
    }

    public void SetIInspectingPoliceCarControl(IInspectingPoliceCarControl iInspectingPoliceCarControl)
    {
        this.iInspectingPoliceCarControl = iInspectingPoliceCarControl;
    }

    public void SetIInspectingPanelControl(IConversationPanelControl iInspectingPanelControl)
    {
        this.iInspectingPanelControl = iInspectingPanelControl;
    }

    // ��ó�� �ִ� ���������� �ҽɰ˹� ������ �ߺ��Ǿ��� �� ��� ������ �ҽɰ˹��� �ؾ����� �����ִ� �Լ��̴�.
    private bool CheckPriority()
    {
        if (iPoliceCarControl == null) { return false; }
        if (priorityList == null) { return false; }

        if (priorityList.FindIndex(a => a.GetPriorityCode() > iPriorityCode.GetPriorityCode()) != -1)
        {
            // �� �������� �ҽɰ˹��� �� �� ����.
            return false;
        }
        else
        {
            // �� �������� �ҽɰ˹��� �� �� �ִ�.
            return true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Ż���� �ݶ��̴��� �Ϲ� �������� ���
        if (collision.gameObject.GetComponent<IMovingPoliceCarControl>() != null)
        {
            // �켱���� ����Ʈ���� �ش� �ݶ��̴��� �������̽��� �����Ѵ�.
            priorityList.Remove(collision.gameObject.GetComponent<IPriorityCode>());
        }
    }

    public void SetIPoliceCarIsBehaviour(IMovingPoliceCarControl iPoliceCarIsBehaviour)
    {
        this.iPoliceCarControl = iPoliceCarIsBehaviour;
    }
    
    public void SetIEndInspecting(IEndConversation iEndInspecting)
	{
        this.iEndInspecting = iEndInspecting;
	}
    public void SetIPriority(IPriorityCode iPriorityCode)
    {
        this.iPriorityCode = iPriorityCode;
    }
}
