using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ѽ�ȣ �ۼ�

public class House : MonoBehaviour, IAddress
{
    private int houseNumber;

    public void InitAddress(int number)
    {
        houseNumber = number;
        //Debug.Log($"HouseNumber + {houseNumber}");
    }
    public int GetAddress()
    {
        return houseNumber;
    }

}
