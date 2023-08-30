using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

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
        if(_instance == null)
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

    public struct PiZzAtEsT
    {
        public string Name;//�����̸�
        public int Perfection;//�ϼ���
        public int Production_Cost;//������
        public int Sell_Cost;//�Ǹź��

        public PiZzAtEsT(string name, int perfection, int production_Cost, int Sell_Cost)
        {
            this.Name = name;
            this.Perfection = perfection;
            this.Production_Cost = production_Cost;
            this.Sell_Cost = Sell_Cost;
        }
    }

    private void Start()
    {
        PiZzAtEsT CheesePizza = new PiZzAtEsT("cheesePizza", 60, 5000, 10000);
    }
    private void Update()
    {
        time += Time.deltaTime * timeSpeed; //���ӱ���1�� = ���ǽð�2��
        //����60�� = ���ǽð�1�� * x
        Debug.Log((int)time/3600 + " : " + (int)(time % 3600)/60);
    }
}
