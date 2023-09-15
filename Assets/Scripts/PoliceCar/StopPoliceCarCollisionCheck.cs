using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoliceNS.PoliceStateNS;
// �Ѽ�ȣ �ۼ�

public class StopPoliceCarCollisionCheck : MonoBehaviour
{
    private List<IMovingPoliceCarControl> otherIPoliceCarIsBehaviourList = new List<IMovingPoliceCarControl>();

    private IMovingPoliceCarControl iPoliceCarControl;
    private IInspectingPanelControl iInspectingPanelControl;
    private IInspectingPoliceCarControl iInspectingPoliceCarControl;
    private IEndInspecting iEndInspecting;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���������� �ҽɰ˹� ������ �ߺ��Ǿ��� �� �� �������� �ҽɰ˹��� �ߵ���Ű���� �Ѵ�. 
        if (collision.gameObject.GetComponent<IMovingPoliceCarControl>() != null)
        {
            otherIPoliceCarIsBehaviourList.Add(collision.gameObject.GetComponent<IMovingPoliceCarControl>());
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
                // �÷��̾ �����.

                // ī�޶� �ش� ������������ Ȯ�� �� �̵���Ű��, �ҽɰ˹��� â�� ����.
                iInspectingPanelControl.ControlInspectUI(true, iEndInspecting);
            }
        }
    }

    public void SetIInspectingPoliceCarControl(IInspectingPoliceCarControl iInspectingPoliceCarControl)
    {
        this.iInspectingPoliceCarControl = iInspectingPoliceCarControl;
    }

    public void SetIInspectingPanelControl(IInspectingPanelControl iInspectingPanelControl)
    {
        this.iInspectingPanelControl = iInspectingPanelControl;
    }

    // ��ó�� �ִ� ���������� �ҽɰ˹� ������ �ߺ��Ǿ��� �� ��� ������ �ҽɰ˹��� �ؾ����� �����ִ� �Լ��̴�.
    private bool CheckPriority()
    {
        if (iPoliceCarControl == null) { return false; }

        if (otherIPoliceCarIsBehaviourList.FindIndex(a => a.GetPoliceCarCode() > iPoliceCarControl.GetPoliceCarCode()) != -1)
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
        if (collision.gameObject.GetComponent<IMovingPoliceCarControl>() != null)
        {
            otherIPoliceCarIsBehaviourList.Remove(collision.gameObject.GetComponent<IMovingPoliceCarControl>());
        }
    }

    public void SetIPoliceCarIsBehaviour(IMovingPoliceCarControl iPoliceCarIsBehaviour)
    {
        this.iPoliceCarControl = iPoliceCarIsBehaviour;
    }
    
    public void SetIEndInspecting(IEndInspecting iEndInspecting)
	{
        this.iEndInspecting = iEndInspecting;
	}
}
