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

    private IEndInspecting iEndInspecting;
    private IHouse iHouse;
    private IStop iStop;

    private HouseType houseType;
    
    private RectTransform inspectTrans;
    private RectTransform pizzaStoreTrans;
    private RectTransform pizzaMakeTrans;

    private int inspectingHeight = 0;   // �ҽɰ˹� �г�â ����
    private int pizzaStoreHeight = 0;   // ������ �г�â ����
    private int pizzaMakeWitdh = 0; // ���ڸ���� �г�â �ʺ�

    private bool isInspecting = false;  // �ҽɰ˹��� â�� �����ϴ��� ����
    private bool isPizzaStore = false;  // ������ â�� �����ϴ��� ����
    private bool isPizzaMake = false;
    void Awake()
    {
        inspectTrans = inspectingMaskPanel.GetComponent<RectTransform>();
        pizzaStoreTrans = pizzaStoreMaskPanel.GetComponent<RectTransform>();
        pizzaMakeTrans = pizzaMakePanel.GetComponent<RectTransform>();
        houseType = HouseType.NONE;
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
                    Debug.Log("�Ѵ� �ȉѴ�");
                    houseType = HouseType.NONE;
                    //�÷��̾� ���߰�, ������ �������.
                    iStop.StopMap(true);
                    // ����â Ȱ��ȭ
                    ControlPizzaStore(true);
                    break;
            }
        }
    }
}
