using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inventory;
using PizzaNS;
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public Pizza?[] PizzaInventoryData = new Pizza?[5];
    public List<Pizza> PizzaMenu = new List<Pizza>();
    public List<Slot> InventorySlotList = new List<Slot>();
    public GameObject TimeText;
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
    private void Awake()
    {
        
        for (int i = 0; i < 5; i++)
        {
            List<Ingredient> ing = new List<Ingredient>();
            ing.Add(Ingredient.CHEESE);
            GameManager.Instance.PizzaMenu.Add(new Pizza("CheesePizza5", 60, 5000, 10000, Random.Range(0, 500) + 500, ing, Random.Range(0, 100) + 200));
        }
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
        //TimeText.GetComponent<Text>().text = (int)time/3600 + " : " + (int)(time / 60 % 60);
    }
}
