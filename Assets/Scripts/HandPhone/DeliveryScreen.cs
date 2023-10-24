using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryScreen : MonoBehaviour
{
    [SerializeField] private MakingPizza ma;
    [SerializeField] private GameObject[] MyChildRefreshB;
    public List<Text> RequestTextList = new List<Text>();
    public SendDeliveryRequest SDR;
    public List<GameObject> AcceptB;
    public List<GameObject> CancleB;
    public Map Map;
    public House House;
    public Minimap Minimap;
    //��޾� �ؽ�Ʈ ������Ʈ
    public void TextUpdate()
    {
        
        if(SDR.RequestList.Count > 0)
        {
            for (int i = 0; i < 5; i++)
            {
                if (i < SDR.RequestList.Count)
                {
                    RequestTextList[i].text = SDR.RequestList[i].Pizza.Name;
                    if (!SDR.RequestList[i].Accept)
                        AcceptB[i].SetActive(true);
                }
                else
                {
                    RequestTextList[i].text = "";
                    AcceptB[i].SetActive(false);
                }
            }
        }
    }
    private void Update()
    {
        TextUpdate();
    }
    public void OnClickAccept(int i)//�����ֹ�������ưŬ��
    {
        AcceptB[i].SetActive(false);
        SDR.RequestList[i].Accept = true;
        SDR.RequestList[i].AddressS = Map.GetRandAddressS();
        SDR.RequestList[i].AddressS.IHouse.EnableHouse();
        Minimap.CreateDestination(SDR.RequestList[i]);
        ma.AddRequestPizza(SDR.RequestList[i]);
        MyChildRefreshB[i].SetActive(true);
    }
    
    public void OnClickCancle(int i)//�����ֹ���ҹ�ưŬ��
    {
        MyChildRefreshB[i].SetActive(false);
        AcceptB[i].SetActive(false);
        RequestTextList[i].text = "";
        SDR.RequestList.RemoveAt(i);
    }

    public void OnClickAddRequestPizza(int i)
    {
        ma.AddRequestPizza(SDR.RequestList[i]);
    }
}
