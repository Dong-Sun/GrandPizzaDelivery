using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPhone : MonoBehaviour
{
    public GameObject DeliveryAppButton;
    public GameObject DeliveryScreen;
    DeliveryScreen DSscript;

    private void Start()
    {
        DSscript = DeliveryScreen.GetComponent<DeliveryScreen>();
    }
    public void DeliveryOnClick()//��޾� ��ư ��������
    {
        DeliveryAppButton.SetActive(false);
        DeliveryScreen.SetActive(true);
        DSscript.TextUpdate();
    }

    public void HomeButtonOnClick()//Ȩ��ư ��������
    {
        DeliveryAppButton.SetActive(true);
        DeliveryScreen.SetActive(false);
    }
    
}
