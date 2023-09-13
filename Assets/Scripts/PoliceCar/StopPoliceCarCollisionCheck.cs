using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoliceNS.PoliceStateNS;
// �Ѽ�ȣ �ۼ�

public class StopPoliceCarCollisionCheck : MonoBehaviour
{
    private IInspectingPanelControl iInspectingPanelControl;
    private IInspectingPoliceCarControl iInspectingPoliceCarControl;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �±װ� �÷��̾��� �������� �ҽɰ˹��� ���¿� ����.
        if (collision.tag == "Player")
        {
            // ���¸� �ҽɰ˹������� �ٲ۴�.
            iInspectingPoliceCarControl.SetPoliceState(PoliceState.INSPECTING);
            // �÷��̾ �����.

            // ī�޶� �ش� ������������ Ȯ�� �� �̵���Ű��, �ҽɰ˹��� â�� ����.
            iInspectingPanelControl.ControlInspectUI(true);
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
}
