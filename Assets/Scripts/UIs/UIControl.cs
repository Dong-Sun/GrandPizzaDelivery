using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ѽ�ȣ �ۼ�

public class UIControl : MonoBehaviour, IInspectingPanelControl
{
    [SerializeField] private GameObject inspectingPanel;
    [SerializeField] private GameObject inspectingMaskPanel;

    private IEndInspecting iEndInspecting;

    private RectTransform inspectTrans;

    private int inspectingHeight = 0;
    private bool isInspecting = false;  // �ҽɰ˹��� â�� �����ϴ��� ����
    void Awake()
    {
        inspectTrans = inspectingMaskPanel.GetComponent<RectTransform>();
    }

    public void ControlInspectUI(bool isOn, IEndInspecting iEndInspecting)
    {
        if (iEndInspecting != null)
		{
            this.iEndInspecting = iEndInspecting; 
		}

        if (isOn)
        {
            inspectingPanel.SetActive(isOn);
            isInspecting = isOn;
        }
        else if (!isOn && inspectingHeight >= 1080)
        {
            isInspecting = false;
            this.iEndInspecting.EndInspecting();
            this.iEndInspecting = null;
        }

    }

    void FixedUpdate()
    {
        if (isInspecting && inspectingHeight < 1080)
        {
            inspectingHeight += 40;
            inspectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inspectingHeight);
        }
        else if (!isInspecting && inspectingHeight >= 1080)
        {
            inspectingHeight = 0;
            inspectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inspectingHeight);
            inspectingPanel.SetActive(false);
        }
    }
}
