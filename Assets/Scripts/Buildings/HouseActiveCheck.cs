using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ѽ�ȣ �ۼ�

public class HouseActiveCheck : MonoBehaviour
{
    private IActiveHouse iActiveHouse;
    private IHouseActiveUIControl iHouseActiveUIControl;
    private bool isIn = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.tag.Equals("Player")) { return; }
        // �ǹ� Ȱ��ȭ
        if (iActiveHouse.ActiveHouse(true))
        {
            // ����Ű ���� �г��� ���ش�.
            iHouseActiveUIControl.ActiveTrueKeyExplainPanel(true);
            // HouseŸ���� UIControl�� �����Ѵ�.
            iActiveHouse.IntoHouse(true);
            isIn = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.tag.Equals("Player")) { return; }

        if (isIn)
        {
            iActiveHouse.IntoHouse(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.tag.Equals("Player")) { return; }

        if (iActiveHouse.ActiveHouse(false))
        {
            // ����Ű ���� �г��� ���ش�.
            iHouseActiveUIControl.ActiveTrueKeyExplainPanel(false);
            // UIContorl�� houseType�� �ʱ�ȭ���ش�.
            iActiveHouse.IntoHouse(false);
            isIn = false;
        }
    }

    public void SetIActiveHouse(IActiveHouse iActiveHouse)
    {
        this.iActiveHouse = iActiveHouse;
    }
    public void SetIHouseActiveUIControl(IHouseActiveUIControl iHouseActiveUIControl)
    {
        this.iHouseActiveUIControl = iHouseActiveUIControl;
    }
}
