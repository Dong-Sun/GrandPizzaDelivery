using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player; // �÷��̾�(Transform)�� �����մϴ�.
    public List<Transform> Destination = new List<Transform>(); // ������(Transform)�� �����մϴ�.
    public List<RectTransform> destinationIcon = new List<RectTransform>();
    public RectTransform PlayerIcon;
    

    public void CreateDestination(Request SDR)
    {
        Destination.Add(SDR.AddressS.IHouse.GetLocation());
        ResetDestinationIcon();
    }
    /// <summary>
    /// �������� �����Ѵ�.
    /// </summary>
    /// <param name="destination">������ �� TransformŸ��</param>
    public void DeleteDestination(Transform destination)
    {
        destinationIcon[Destination.Count - 1].gameObject.SetActive(false);
        Destination.Remove(destination);
    }

    private void ResetDestinationIcon()
    {
        for(int i = 0; i < destinationIcon.Count; i++)
        {
            if(i < Destination.Count)
                destinationIcon[i].gameObject.SetActive(true);
            else
                destinationIcon[i].gameObject.SetActive(false);
        }
    }
    void Update()
    {
        if (Destination.Count <= 0)
            return;
        for(int i = 0; i < Destination.Count; i++)
        {
            Vector2 change = new Vector2((Destination[i].position.x - player.position.x) * 10, (Destination[i].position.y - player.position.y) * 10);
            if (change.x < -190)
                change.x = -190;
            if (change.x > 190)
                change.x = 190;
            if (change.y < -190)
                change.y = -190;
            if (change.y > 190)
                change.y = 190;
            destinationIcon[i].anchoredPosition = change;
        }
        
    }
}
