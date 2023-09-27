using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    private int hp = 200;
    public float Braking = 0.99f;
    [SerializeField]
    private float speed;
    public float MaxSpeed;
    public float acceleration = 10;

    public int HP
    {
        get { return hp; }
        set {
            if (value <= 0)
                Debug.Log("�÷��̾� ���");
            else if (value > 0)
                Debug.Log("�÷��̾� ����");
            hp = value;
        }
    }
    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            if(MaxSpeed < value)
            {
                value = MaxSpeed;
            }else if(-MaxSpeed > value)
            {
                value = -MaxSpeed;
            }
            speed = value;
        }
    }
}
