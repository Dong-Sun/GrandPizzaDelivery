using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// �Ѽ�ȣ �ۼ�
public class Calculate : MonoBehaviour
{
    [SerializeField] private Text contentsText;
    [SerializeField] private Text sumText;
    [SerializeField] private GameObject nextButton;
    private Coroutine calCoroutine;
    private int moneyTime = 7;  // ���� ���ƾ� �Ǳ���� ��ٷ� �ִ� �ð�
    private int temMoney;
    private int temFine;
    private int temDept;
    private int temPizzaIngMoney;
    private int temClerkMoney;
    private bool temIsDead;
    public Dictionary<int, Dictionary<int, int>> temPayMoneyDate = new Dictionary<int, Dictionary<int, int>>();

    // Start is called before the first frame update
    void Start()
    {
        List<int> li = new List<int>();
        foreach (var key in Constant.PayMoneyDate.Keys)
        {
            li.Add(key);
        }
        for (int i = 0; i < li.Count; i++)
        {
            for (int i2 = 0; i2 < Constant.MoneyStoreCode.Length; i2++)
            {
                Constant.PayMoneyDate[li[i]][i2] = (int)(Constant.PayMoneyDate[li[i]][i2] * Constant.DeptMulitplex[i2]);
            }
        }

        for (int i = 0; i < EmployeeFire.WorkingDay[(int)Constant.NowDay].Count; i++)
        {
            Constant.ClerkMoney += EmployeeFire.WorkingDay[(int)Constant.NowDay][i].Pay;
        }

        temMoney = GameManager.Instance.Money;
        temFine = Constant.Fine;
        temDept = Constant.Dept;
        temPizzaIngMoney = Constant.PizzaIngMoney;
        temClerkMoney = Constant.ClerkMoney;
        temIsDead = Constant.IsDead;
        
        foreach (var k1 in Constant.PayMoneyDate.Keys)
        {
            temPayMoneyDate.Add(k1, new Dictionary<int, int>());

            foreach (var k2 in Constant.PayMoneyDate[k1].Keys)
            {
                temPayMoneyDate[k1].Add(k2, Constant.PayMoneyDate[k1][k2]);
            }
        }


        calCoroutine = StartCoroutine(Cal());    
    }
    /// <summary>
    /// ���� ����
    /// ���⼭ ����� ���� ���࿡�� ���� ���� ��.
    /// </summary>
    /// <returns></returns>
    IEnumerator Cal()
    {
        int t1 = 0;
        int t2 = 0;
        int t3 = 0;
        int t4 = 0;
        int t5 = 0;
        int t6 = 0;
        int t7 = 0;
        while (true)
        {
            contentsText.gameObject.SetActive(true);
            while (t1 < Constant.Fine)
            {
                contentsText.text = $"���� : {t1}��";
                t1+= Random.Range(1,10000);
                
                yield return Constant.OneTime;
                continue;
            }
            t1 = Constant.Fine;
            if (GameManager.Instance.Money > t1)
            {
                GameManager.Instance.Money -= t1;
            }
            else
            {
                t1 -= GameManager.Instance.Money;
                Constant.Dept += t1;
                t1 += GameManager.Instance.Money;
                GameManager.Instance.Money = 0;
            }
            while (t2 < Constant.PizzaIngMoney)
            {
                contentsText.text = $"���� : {t1}�� \n�Ҹ�� ���� ��� �� : {t2}��";
                t2 += Random.Range(1, 5000);
                yield return Constant.OneTime;
                continue;
            }
            t2 = Constant.PizzaIngMoney;
            if (GameManager.Instance.Money > t2)
            {
                GameManager.Instance.Money -= t2;
            }
            else
            {
                t2 -= GameManager.Instance.Money;
                Constant.Dept += t2;
                t2 += GameManager.Instance.Money;
                GameManager.Instance.Money = 0;
            }


            while (t3 < Constant.ClerkMoney)
            {
                contentsText.text = $"���� : {t1}�� \n�Ҹ�� ���� ��� �� : {t2}�� \n���� �ϱ� : {t3}��";
                t3+= Random.Range(1, 20000); ;
                yield return Constant.OneTime;
                continue;
            }
            t3 = Constant.ClerkMoney;
            if (GameManager.Instance.Money > t3)
            {
                GameManager.Instance.Money -= t3;
            }
            else
            {
                t3 -= GameManager.Instance.Money;
                Constant.Dept += t3;
                t3 += GameManager.Instance.Money;
                GameManager.Instance.Money = 0;
            }
            while (Constant.IsDead)
            {
                contentsText.text = $"���� : {t1}�� \n�Ҹ�� ���� ��� �� : {t2}�� \n���� �ϱ� : {t3}�� \n��Ȱ�� : {t4}��";
                if (t4 >= 300000) { break; }
                t4 += Random.Range(1, 10000); ;
                yield return Constant.OneTime;
                continue;
            }
            if (Constant.IsDead)
            {
                t4 = 300000;
                contentsText.text = $"���� : {t1}�� \n�Ҹ�� ���� ��� �� : {t2}�� \n���� �ϱ� : {t3}�� \n��Ȱ�� : {t4}��";
                if (GameManager.Instance.Money > t4)
                {
                    GameManager.Instance.Money -= t4;
                }
                else
                {
                    t4 -= GameManager.Instance.Money;
                    Constant.Dept += t4;
                    t4 += GameManager.Instance.Money;
                    GameManager.Instance.Money = 0;
                }
            }
            else
			{
                t4 = 0;
                contentsText.text = $"���� : {t1}�� \n�Ҹ�� ���� ��� �� : {t2}�� \n���� �ϱ� : {t3}�� \n��Ȱ�� : {t4}��";
            }
            List<int> li = new List<int>();
            List<int> li2 = new List<int>();
            foreach (var key in Constant.PayMoneyDate.Keys)
            {
                if (key <= Constant.NowDate - moneyTime)
                {
                    li.Add(key);
                }
            }

            if (li.Count > 0)
            {
                contentsText.text = $"���� : {t1}�� \n�Ҹ�� ���� ��� �� : {t2}�� \n���� �ϱ� : {t3}�� \n��Ȱ�� : {t4}�� \n...";
                yield return Constant.OneTime;
                yield return Constant.OneTime;
                yield return Constant.OneTime;

                contentsText.text = $"���� : {t1}�� \n�Ҹ�� ���� ��� �� : {t2}�� \n���� �ϱ� : {t3}�� \n��Ȱ�� : {t4}�� \n...\n��! �뿩�ڵ��� ���̴��ƴ�!";
                yield return Constant.OneTime;
                yield return Constant.OneTime;
                yield return Constant.OneTime;

                for (int i = 0; i < li.Count; i++)
                {
                    foreach (var key in Constant.PayMoneyDate[li[i]].Keys)
                    {
                        li2.Add(key);
                    }

                    for (int i2 = 0; i2 < li2.Count; i2++)
                    {
                        if (Constant.PayMoneyDate[li[i]][li2[i2]] <= GameManager.Instance.Money)
                        {
                            GameManager.Instance.Money -= Constant.PayMoneyDate[li[i]][li2[i2]];
                            t5 += Constant.PayMoneyDate[li[i]][li2[i2]];
                            Constant.PayMoneyDate[li[i]].Remove(li2[i2]);
                            if (Constant.PayMoneyDate[li[i]].Count == 0)
                            {
                                Constant.PayMoneyDate.Remove(li[i]);
                            }
                        }
                        else
                        {
                            Constant.PayMoneyDate[li[i]][li2[i2]] -= GameManager.Instance.Money;
                            t5 += GameManager.Instance.Money;
                            GameManager.Instance.Money = 0;
                        }
                    }
                }

                int n5 = 0;
                while (n5 < t5)
                {
                    contentsText.text = $"���� : {t1}�� \n�Ҹ�� ���� ��� �� : {t2}�� \n���� �ϱ� : {t3}�� \n��Ȱ�� : {t4}�� \n...\n��! �뿩�ڵ��� ���̴��ƴ�!\n�뿩�ڵ鿡�� ���� �� : {n5}��";
                    n5 += Random.Range(1, 10000);
                    yield return Constant.OneTime;
                    continue;
                }
                n5 = t5;
                contentsText.text = $"���� : {t1}�� \n�Ҹ�� ���� ��� �� : {t2}�� \n���� �ϱ� : {t3}�� \n��Ȱ�� : {t4}�� \n...\n��! �뿩�ڵ��� ���̴��ƴ�!\n�뿩�ڵ鿡�� ���� �� : {n5}��";

            }
            yield return Constant.OneTime;
            yield return Constant.OneTime;
            yield return Constant.OneTime;
            sumText.gameObject.SetActive(true);

            while (t6 < GameManager.Instance.Money)
            {
                sumText.text = $"���� ���� �� : {t6}��";
                if (GameManager.Instance.Money >= 100000000)
                {
                    t6 += Random.Range(1, 4000000);
                }
                else if (GameManager.Instance.Money >= 10000000)
                {
                    t6 += Random.Range(1, 2500000);
                }
                else if (GameManager.Instance.Money >= 1000000)
                {
                    t6 += Random.Range(1, 300000);
                }
                else
                {
                    t6 += Random.Range(1, 30000);
                }
                yield return Constant.OneTime;
                continue;
            }
            t6 = GameManager.Instance.Money;

            int n7 = Constant.Dept;

            foreach (var key in Constant.PayMoneyDate.Keys)
            {
                foreach (var key2 in Constant.PayMoneyDate[key].Keys)
                {
                    n7 += Constant.PayMoneyDate[key][key2];
                }
            }
            Constant.Dept = n7;

            while (t7 < n7)
            {
                sumText.text = $"���� ���� �� : {t6}�� \n���� �� : {t7}��";
                if (n7 > 10000000)
                {
                    t7 += Random.Range(1, 3000000);
                }
                else
                {
                    t7 += Random.Range(1, 300000); ;
                }
                yield return Constant.OneTime;
                continue;
            }
            t7 = n7;
            sumText.text = $"���� ���� �� : {t6}�� \n���� �� : {t7}��";

            Constant.Fine = 0;
            Constant.PizzaIngMoney = 0;
            Constant.ClerkMoney = 0;
            Constant.IsDead = false;

            nextButton.SetActive(true);
            break;
        }
    }

    public void Skip()
    {
        StopCoroutine(calCoroutine);

        GameManager.Instance.Money = temMoney;
        Constant.Fine = temFine;
        Constant.Dept = temDept;
        Constant.PizzaIngMoney = temPizzaIngMoney;
        Constant.ClerkMoney = temClerkMoney;
        Constant.IsDead = temIsDead;

        foreach (var k1 in temPayMoneyDate.Keys)
        {
            if (!Constant.PayMoneyDate.ContainsKey(k1))
            {
                Constant.PayMoneyDate.Add(k1, new Dictionary<int, int>());
            }

            foreach (var k2 in temPayMoneyDate[k1].Keys)
            {
                if (!Constant.PayMoneyDate.ContainsKey(k1))
                {
                    Constant.PayMoneyDate[k1].Add(k2, temPayMoneyDate[k1][k2]);
                }
                else
                {
                    Constant.PayMoneyDate[k1][k2] = temPayMoneyDate[k1][k2];
                }
            }
        }

        contentsText.gameObject.SetActive(true);
        Constant.Dept = 0;
        int de = 0;
        if (GameManager.Instance.Money > Constant.Fine)
        {
            GameManager.Instance.Money -= Constant.Fine;
        }
        else
        {
            Constant.Dept += Constant.Fine;
            GameManager.Instance.Money = 0;
        }

        if (GameManager.Instance.Money > Constant.PizzaIngMoney)
        {
            GameManager.Instance.Money -= Constant.PizzaIngMoney;
        }
        else
        {
            Constant.Dept += Constant.PizzaIngMoney;
            GameManager.Instance.Money = 0;
        }

        if (GameManager.Instance.Money > Constant.ClerkMoney)
        {
            GameManager.Instance.Money -= Constant.ClerkMoney;
        }
        else
        {
            Constant.Dept += Constant.ClerkMoney;
            GameManager.Instance.Money = 0;
        }
        int t99 = 0;
        if (Constant.IsDead)
        {
            t99 = 300000;
            if (GameManager.Instance.Money > 300000)
            {
                GameManager.Instance.Money -= 300000;
            }
            else
            {
                Constant.Dept += 300000;
                GameManager.Instance.Money = 0;
            }
        }
        else
		{
            t99 = 0;
		}
        List<int> li = new List<int>();
        List<int> li2 = new List<int>();
        foreach (var key in Constant.PayMoneyDate.Keys)
        {
            if (key <= Constant.NowDate - moneyTime)
            {
                li.Add(key);
            }
        }

        if (li.Count > 0)
        {
            for (int i = 0; i < li.Count; i++)
            {
                foreach (var key in Constant.PayMoneyDate[li[i]].Keys)
                {
                    li2.Add(key);
                }

                for (int i2 = 0; i2 < li2.Count; i2++)
                {
                    if (Constant.PayMoneyDate[li[i]][li2[i2]] <= GameManager.Instance.Money)
                    {
                        GameManager.Instance.Money -= Constant.PayMoneyDate[li[i]][li2[i2]];
                        de += Constant.PayMoneyDate[li[i]][li2[i2]];
                        Constant.PayMoneyDate[li[i]].Remove(li2[i2]);
                        if (Constant.PayMoneyDate[li[i]].Count == 0)
                        {
                            Constant.PayMoneyDate.Remove(li[i]);
                        }
                    }
                    else
                    {
                        Constant.PayMoneyDate[li[i]][li2[i2]] -= GameManager.Instance.Money;
                        de += GameManager.Instance.Money;
                        GameManager.Instance.Money = 0;
                    }
                }
            }
        }

        int n7 = Constant.Dept;

        foreach (var key in Constant.PayMoneyDate.Keys)
        {
            foreach (var key2 in Constant.PayMoneyDate[key].Keys)
            {
                n7 += Constant.PayMoneyDate[key][key2];
            }
        }
        Constant.Dept = n7;

        if (li.Count > 0)
        {
            contentsText.text = $"���� : {Constant.Fine}�� \n�Ҹ�� ���� ��� �� : {Constant.PizzaIngMoney}�� \n���� �ϱ� : {Constant.ClerkMoney}�� \n��Ȱ�� : {t99}�� \n...\n��! �뿩�ڵ��� ���̴��ƴ�!\n�뿩�ڵ鿡�� ���� �� : {de}��";
        }
        else
        {
            contentsText.text = $"���� : {Constant.Fine}�� \n�Ҹ�� ���� ��� �� : {Constant.PizzaIngMoney}�� \n���� �ϱ� : {Constant.ClerkMoney}�� \n��Ȱ�� : {t99}��";
        }
        sumText.gameObject.SetActive(true);
        
        sumText.text = $"���� ���� �� : {GameManager.Instance.Money}�� \n���� �� : {Constant.Dept}��";

        nextButton.SetActive(true);

        Constant.Fine = 0;
        Constant.PizzaIngMoney = 0;
        Constant.ClerkMoney = 0;
        Constant.IsDead = false;
        
    }

    public void GoInGame()
    {
        PlayerStat.HP = PlayerStat.MaxHP;
        GameManager.Instance.time = 32400;

        if (Constant.NowDate != 8)
        {
            LoadScene.Instance.ActiveTrueFade("DateScene");
        }
        else
		{
            LoadScene.Instance.ActiveTrueFade("EndingScene");
		}
    }
}
