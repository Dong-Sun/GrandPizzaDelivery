using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player; // �÷��̾�(Transform)�� �����մϴ�.
    public Transform destination; // ������(Transform)�� �����մϴ�.
    public RectTransform playerIcon; // PlayerIcon GameObject�� �����մϴ�.
    public RectTransform destinationIcon; // DestinationIcon GameObject�� �����մϴ�.

    void Update()
    {
        Vector2 distince = destination.position - player.position;
        Vector2 change = new Vector2(player.position.x, player.position.y) + distince * 10;
        // �÷��̾�� ������ ��ġ�� MiniMap�� �����Ͽ� �°� �����Ͽ� �������� ��ġ�� ������Ʈ�մϴ�.
        //playerIcon.anchoredPosition = new Vector2(player.position.x, player.position.y) * 6;
        if (change.x < -200)
        {
            destinationIcon.anchoredPosition = new Vector2(-200, change.y);
        }else if (change.x > 200)
        {
            destinationIcon.anchoredPosition = new Vector2(200, change.y);
        }else if(change.y < -200)
        {
            destinationIcon.anchoredPosition = new Vector2(change.x, -200);
        }else if (change.y > 200)
        {
            destinationIcon.anchoredPosition = new Vector2(change.x, 200);
        }else
        {
            destinationIcon.anchoredPosition = change;
        }
    }
}
