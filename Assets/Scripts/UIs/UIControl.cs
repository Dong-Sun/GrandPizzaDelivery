using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingNS.HouseNS;

// �Ѽ�ȣ �ۼ�

public class UIControl : MonoBehaviour, IInspectingPanelControl, IDeliveryPanelControl, IHouseActiveUIControl
{
    [SerializeField] private GameObject inspectingPanel;
    [SerializeField] private GameObject inspectingMaskPanel;
    [SerializeField] private GameObject deliveryPanel;
    [SerializeField] private GameObject keyExplainPanel;
    [SerializeField] private GameObject pizzaStorePanel;
    [SerializeField] private GameObject pizzaStoreMaskPanel;
    [SerializeField] private GameObject pizzaMakePanel;
    [SerializeField] private GameObject pizzaMenuPanel;
    [SerializeField] private GameObject employeeRecruitPanel;

    [SerializeField] private UnityEngine.UI.Image addPizzaImg;

    private IEndInspecting iEndInspecting;
    private IHouse iHouse;
    private IStop iStop;

    private HouseType houseType;
    
    private RectTransform inspectTrans;
    private RectTransform pizzaStoreTrans;
    private RectTransform pizzaMakeTrans;
    private RectTransform pizzaMenuTrans;
    private RectTransform employeeRecruitTrans;

    private int inspectingHeight = 0;   // �ҽɰ˹� �г�â ����
    private int pizzaStoreHeight = 0;   // ������ �г�â ����
    private int pizzaMakeWitdh = 0; // ���ڸ���� �г�â �ʺ�
    private int pizzaMenuHeight = 0;    // ���ڸ޴� �г�â ����;

    private bool isInspecting = false;  // �ҽɰ˹��� â�� �����ϴ��� ����
    private bool isPizzaStore = false;  // ������ â�� �����ϴ��� ����
    private bool isPizzaMake = false;
    private bool isPizzaMenu = false;
    private bool isPizzaAddButtonBlank = false;
    private bool isColor = false;
    void Awake()
    {
        inspectTrans = inspectingMaskPanel.GetComponent<RectTransform>();
        pizzaStoreTrans = pizzaStoreMaskPanel.GetComponent<RectTransform>();
        pizzaMakeTrans = pizzaMakePanel.GetComponent<RectTransform>();
        pizzaMenuTrans = pizzaMenuPanel.GetComponent<RectTransform>();
        employeeRecruitTrans = employeeRecruitPanel.GetComponent<RectTransform>();

        houseType = HouseType.NONE;

        if (Constant.isMakePizza)
		{
            DirectADdPizzaMenu();
        }
    }
    private void DirectADdPizzaMenu()
	{
        pizzaStorePanel.SetActive(true);
        isPizzaStore = true;
        pizzaStoreHeight = 1080;
        pizzaStoreTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, pizzaStoreHeight);

        pizzaMenuPanel.SetActive(true);
        isPizzaMenu = true;
        pizzaMenuHeight = 1080;
        pizzaMenuTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, pizzaMenuHeight);

        isPizzaAddButtonBlank = true;
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
    public void ControlPizzaStore(bool isOn)
    {
        if (isOn)
        {
            pizzaStorePanel.SetActive(isOn);
            isPizzaStore = isOn;
        }
        else
        {
            isPizzaStore = isOn;
        }
    }
    public void ControlPizzaMake(bool isOn)
    {
        if (isOn)
        {
            pizzaMakePanel.SetActive(isOn);
            isPizzaMake = isOn;
        }
        else
        {
            isPizzaMake = isOn;
        }
    }

    public void ControlPizzaMenu(bool isOn)
	{
        if (isOn)
		{
            pizzaMenuPanel.SetActive(isOn);
            isPizzaMenu = isOn;
		}
        else
		{
            isPizzaMenu = isOn;
            isPizzaAddButtonBlank = false;
            addPizzaImg.color = Color.white;
		}
	}

	public void ControlEmployeeRecruit(bool isOn)
	{
		employeeRecruitPanel.SetActive(isOn);
	}

	public void ControlDeliveryUI(bool isOn)
    {
        deliveryPanel.SetActive(isOn);
    }
    public void SetIHouseDeliveryUI(IHouse iHouse)
    {
        this.iHouse = iHouse;
    }
    public void OKDeliveryUI()
    {
        if (iHouse == null) { return; }

        iHouse.DisableHouse();
        deliveryPanel.SetActive(false);
    }
    public void NODeliveryUI()
    {
        deliveryPanel.SetActive(false);
    }

    public void ActiveTrueKeyExplainPanel(bool bo)
    {
        keyExplainPanel.SetActive(bo);
    }

    public void SetHouseType(HouseType houseType)
    {
        this.houseType = houseType;
    }

    public void SetIStop(IStop iStop)
    {
        this.iStop = iStop;
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
        
        if (isPizzaStore && pizzaStoreHeight < 1080)
        {
            pizzaStoreHeight += 40;
            pizzaStoreTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, pizzaStoreHeight);
        }
        else if (!isPizzaStore && pizzaStoreHeight >= 1080)
        {
            pizzaStoreHeight = 0;
            pizzaStoreTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, pizzaStoreHeight);
            pizzaStorePanel.SetActive(false);
            iStop.StopMap(false);
        }

        if (isPizzaMake && pizzaMakeWitdh < 1920)
        {
            pizzaMakeWitdh += 80;
            pizzaMakeTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, pizzaMakeWitdh);
        }
        else if (!isPizzaMake && pizzaMakeWitdh >= 1920)
        {
            pizzaMakeWitdh = 0;
            pizzaMakeTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, pizzaMakeWitdh);
            pizzaMakePanel.SetActive(false);
        }

        if (isPizzaMenu && pizzaMenuHeight < 1080)
        {
            pizzaMenuHeight += 40;
            pizzaMenuTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, pizzaMenuHeight);
        }
        else if (!isPizzaMenu && pizzaMenuHeight >= 1080)
        {
            pizzaMenuHeight = 0;
            pizzaMenuTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, pizzaMenuHeight);
            pizzaMenuPanel.SetActive(false);
        }

        if (isPizzaAddButtonBlank)
        {
            if (isColor)
            {
                addPizzaImg.color += new Color(5 / 255f, 0, 0, 0);
                if (addPizzaImg.color.r >= 1) { isColor = false; }
            }
            else
			{
                addPizzaImg.color -= new Color(5 / 255f, 0, 0, 0);
                if (addPizzaImg.color.r <= 0) { isColor = true; }
            }
        }
    }

    public void Update()
    {
        // Ư���� ��ҿ��� ZŰ�� ���� ��
        if (houseType != HouseType.NONE && houseType != HouseType.HOUSE
            && Input.GetKeyDown(KeyCode.Z))
        {
            switch(houseType)
            {
                case HouseType.PIZZASTORE:
                    //Debug.Log("�Ѵ� �ȉѴ�");
                    //houseType = HouseType.NONE;
                    //�÷��̾� ���߰�, ������ �������.
                    iStop.StopMap(true);
                    // ����â Ȱ��ȭ
                    ControlPizzaStore(true);
                    break;
            }
        }
    }
}
