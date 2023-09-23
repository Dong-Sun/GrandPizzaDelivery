using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player; // �÷��̾�(Transform)�� �����մϴ�.
    public List<Vector3> destination = new List<Vector3>(); // ������(Transform)�� �����մϴ�.
    public RectTransform playerIcon; // PlayerIcon GameObject�� �����մϴ�.
    public RectTransform destinationIcon; // DestinationIcon GameObject�� �����մϴ�.
    public SendDeliveryRequest SDR;

    void Update()
    {
        /*for(int i = 0; i < SDR.RequestList.Count; i++)
        {
            if (SDR.RequestList[i].Accept)
            {
                destination[i] = SDR.RequestList[i].AddressS.IHouse.GetLocation();
            }
        }*/
        for (int i = 0; i < SDR.RequestList.Count; i++)
        {
            if (SDR.RequestList[i].Accept)
            {
                destination.Add(SDR.RequestList[i].AddressS.IHouse.GetLocation());
            }
        }
        if (destination.Count <= 0)
            return;
        Vector2 distince = new Vector2(destination[0].x, destination[0].y) - new Vector2(player.position.x, player.position.y);
        Vector2 change = new Vector2(player.position.x, player.position.y) + distince * 10;
        // �÷��̾�� ������ ��ġ�� MiniMap�� �����Ͽ� �°� �����Ͽ� �������� ��ġ�� ������Ʈ�մϴ�.
        //playerIcon.anchoredPosition = new Vector2(player.position.x, player.position.y) * 6;
        if (change.x < -190)
        {
            destinationIcon.anchoredPosition = new Vector2(-190, change.y);
        }
        else if (change.x > 190)
        {
            destinationIcon.anchoredPosition = new Vector2(190, change.y);
        }
        else if (change.y < -190)
        {
            destinationIcon.anchoredPosition = new Vector2(change.x, -190);
        }
        else if (change.y > 190)
        {
            destinationIcon.anchoredPosition = new Vector2(change.x, 190);
        }
        else
        {
            destinationIcon.anchoredPosition = change;
        }
    }
}
