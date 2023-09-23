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
        // �÷��̾�� ������ ��ġ�� MiniMap�� �����Ͽ� �°� �����Ͽ� �������� ��ġ�� ������Ʈ�մϴ�.
        playerIcon.anchoredPosition = new Vector2(player.position.x, player.position.y) * 5;
        destinationIcon.anchoredPosition = new Vector2(destination.position.x, destination.position.y) * 5;
    }
}
