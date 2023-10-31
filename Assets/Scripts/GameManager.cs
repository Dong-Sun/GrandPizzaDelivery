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
    public List<Pizza> PizzaMenu = new List<Pizza>() { new Pizza("CheesePizza", 60, 5000, 10000, 800, new List<Ingredient>() { Ingredient.CHEESE }, 250) };
    public List<Pizza> PineapplePizzaMenu = new List<Pizza>();
    public List<Slot> InventorySlotList = new List<Slot>();

    public bool isDarkDelivery = false;

    public TestMoneyText MoneyText;

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
        PineapplePizzaMenu.Add(Constant.PineapplePizza);

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
            MoneyText.CreateMoneyEffect(value - money);
            money = value;
        }
    }

    

    public void PlayerDead()
    {
        LoadScene.Instance.LoadNextDay("InGameScene");
        isDarkDelivery = false;
        time = 32400;
    }

    public void NextDay()
    {
        LoadScene.Instance.ActiveTrueFade("InGameScene");
        isDarkDelivery = false;
        time = 32400;
    }

    public void HospitalRespawn()
    { 
        //��ȣ���� �ִ� ����
    }
    private void TimeSkip()
    {
        if(Input.GetKeyDown(KeyCode.Backspace))
            time = 82800;
    }
    private void Update()
    {
        TimeSkip();
        if (!Constant.StopTime)
        {
            time += Time.deltaTime * timeSpeed; //���ӱ���1�� = ���ǽð�2��
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            time = 14400;
        }
        if (Input.GetKeyDown(KeyCode.F2))
		{
            time = 32400;
		}
        //����1�� * timeSpeed = ���ǽð�1��
        //TimeText.GetComponent<Text>().text = (int)time/3600 + " : " + (int)(time / 60 % 60);
    }
}
