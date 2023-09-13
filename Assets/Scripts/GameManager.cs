using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public List<Pizza> PizzaMenu = new List<Pizza>();
    
    
    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }
    public Pizza CheesePizza;
    private void Awake()
    {
        CheesePizza = new Pizza("cheesePizza", 60, 5000, 10000);
        PizzaMenu.Add(CheesePizza);

        if (_instance == null)
        {
            _instance = this;
        }else if(_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        
    }

    public float time;
    private float timeSpeed = 60; //�Ϸ���ؽð�

    private int money = 0;
    public int Money
    {
        get {
            return money;
        }
        set {
            //���߿� �����ݾ׵����ϸ� �ص� ȭ�鰡�� �Լ� ¥��
            money = value;
        }
    }

    
    
    private void Update()
    {
        time += Time.deltaTime * timeSpeed; //���ӱ���1�� = ���ǽð�2��
        //����1�� * timeSpeed = ���ǽð�1��
    }
}
