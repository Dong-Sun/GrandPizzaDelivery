using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingAddressNS;
using ClerkNS;

// �Ѽ�ȣ �ۼ�

public class MakingPizza : MonoBehaviour
{
    [SerializeField] private GameObject[] makingPizzaPanelArr; // ����� �ִ�, Ȥ�� ���� ���ڸ� �����ֱ� ���� �гε�
    [SerializeField] private GameObject uiControl;

    private List<Request> pizzaRequestList = new List<Request>();   // �������� ���� ����Ʈ

    private List<Pizza> completePizzaList = new List<Pizza>();  // �ϼ��� ���� ����Ʈ

    private RectTransform[] makingPizzaPanelRect;
    private MakingPizzaPanel[] makingPizzaPanelClass;
    private Coroutine makingPizzaCoroutine; // ���ڸ� ����� �ڷ�ƾ

    private Vector3 initMakingPizzaPanelVec = new Vector3(-600, 400);

    private IAlarmMessagePanel iAlarmMessagePanel;
    private void Awake()
	{
        Constant.ClerkList.Add(new ClerkC(47, Tier.THREE, Tier.ONE, Tier.FOUR, 0, 20000));  // ���Ƿ� ���� ����
        iAlarmMessagePanel = uiControl.GetComponent<IAlarmMessagePanel>();
        InitArr();
    }
	void Start()
    {
        makingPizzaCoroutine = StartCoroutine(Making());
    }
    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    private void InitArr()
	{
        makingPizzaPanelRect = new RectTransform[makingPizzaPanelArr.Length];
        makingPizzaPanelClass = new MakingPizzaPanel[makingPizzaPanelArr.Length];

        for (int i = 0; i < makingPizzaPanelRect.Length; i++)
        {
            makingPizzaPanelRect[i] = makingPizzaPanelArr[i].GetComponent<RectTransform>();
            makingPizzaPanelClass[i] = makingPizzaPanelArr[i].GetComponent<MakingPizzaPanel>();
        }
    }
    /// <summary>
    /// �ֹ��� �����Ѵ�.
    /// </summary>
    /// <param name="request"></param>
    public void AddRequestPizza(Request request)
	{
        Request re = new Request(request.Pizza, request.Accept);
        re.AddressS = request.AddressS;
        pizzaRequestList.Add(re);
	}
    /// <summary>
    /// ���ξ��� ���� �ֹ��� �����Ѵ�.
    /// </summary>
    /// <param name="request"></param>
    public void AddRequestPineapplePizza(Request request)
	{
        if (Constant.PineappleCount <= 0) { return; }
        pizzaRequestList.Add(request);
	}
    /// <summary>
    /// �ڵ����� ���� ���� ���ڸ� ��� ����ϴ�. 
    /// </summary>
    /// <returns></returns>
    private IEnumerator Making()
	{
        int makeTime = 0;
        float makingRate = 0;
        int panelIndex = -1;
        while (true)
        {
            // ���� ���ڸ� ���ϴ�.
            if (pizzaRequestList.Count <= 0) 
            {
                for (int i = 0; i < 5; i++)
                {
                    yield return Constant.OneTime;
                }
                continue; 
            }

            // ���� ����µ� �ɸ��� �ð��� ����Ѵ�.
            makeTime = 80;
            for (int i = 0; i < Constant.ClerkList.Count; i++)
            {
                makeTime -= (60 + (int)Constant.ClerkList[i].Agility);
            }
            // ���� ����������.

            // ���ڸ� ���� �غ��մϴ�.
            // ��Ȱ��ȭ�� �г��� ã���ϴ�.
            for (int i = 0; i < makingPizzaPanelArr.Length; i++)
			{
                if (!makingPizzaPanelArr[i].activeSelf)
				{
                    panelIndex = i;
                    break;
				}
			}
            // �г��� ���ְ� ���� ��ġ�� ����ش�.
            makingPizzaPanelArr[panelIndex].SetActive(true);
            makingPizzaPanelRect[panelIndex].localPosition = initMakingPizzaPanelVec;
            makingPizzaPanelClass[panelIndex].SetPizza(pizzaRequestList[0].Pizza);

            // �г��� �̵���Ų��.
            while(makingPizzaPanelRect[panelIndex].localPosition.x < 0)
			{
                makingPizzaPanelRect[panelIndex].localPosition += new Vector3(5, 0);
                yield return Constant.OneTime;
			}

            // ����� ��.
            for (int i = 0; i < makeTime * 10; i++)
            {
                // ��ŭ ����������� �ۼ�Ʈ�� �����ݴϴ�.
                makingRate = (100f / (makeTime * 10f)) * i;
                // �ۼ�Ʈ ������ ����.
                makingPizzaPanelClass[panelIndex].SetMainPanelRect(makingRate);
                // ���� �ð� ���
                for (int j = 0; j < 5; j++)
                {
                    yield return Constant.OneTime;
                }
            }
            // ���ڰ� �ϼ��Ǿ���. �ϼ��� ���ڴ� ������ �κ��� ����.
            completePizzaList.Add(pizzaRequestList[0].Pizza);
            // ���ξ��� ���ڿ��ٸ� ���ξ����� �ϳ� �پ���.
            if (pizzaRequestList[0].Pizza.Ingreds.FindIndex(a => a.Equals(PizzaNS.Ingredient.PINEAPPLE)) != -1)
			{
                Constant.PineappleCount--;
			}

            // �˸��� ���.
            iAlarmMessagePanel.ControlAlarmMessageUI(true, $"{pizzaRequestList[0].Pizza.Name}�� �ϼ��Ǿ����ϴ�.");

            // ���� �гε��� �Ʒ��� ������.
            for (int i = 0; i < 7; i++)
			{
                for (int j = 0; j < makingPizzaPanelArr.Length; j++)
                {
                    if (makingPizzaPanelArr[j].activeSelf)
                    {
                        makingPizzaPanelRect[j].localPosition -= new Vector3(0, 20);
                    }
                }
                yield return Constant.OneTime;
			}
            
            // �ϼ��� ���ڴ� ����Ʈ���� ����
            pizzaRequestList.RemoveAt(0);


        }
	}
    /// <summary>
    /// �������� �ִ� ���ڸ� �������� �Լ�. 
    /// </summary>
    /// <param name="index">������ ���ڸ���Ʈ�� �ε���. ���� �ʴ´ٸ� �� ���ڸ� ��ȯ�Ѵ�.</param>
    /// <returns></returns>
    public Pizza GetInvenPizzaList(int index)
	{
        // ������ �� ���� ������ �ִٸ� �� ���ڸ� ��ȯ��
        if (completePizzaList.Count <= index) { return new Pizza(); }

        // ���ڸ� �ӽ÷� �����Ѵ�.
        Pizza temPizza = completePizzaList[index];
        // �������� �ִ� ���ڵ� ��ܿ��� �����Ѵ�.
        //Debug.Log(temPizza.Name);
        completePizzaList.RemoveAt(index);
        Debug.Log(index + " " + completePizzaList.Count);
        // ��ܿ��� ���������Ƿ�, ������ ���� �г��� ���ش�.
        for (int i = 0; i < makingPizzaPanelArr.Length; i++)
		{
            if (makingPizzaPanelClass[i].ComparePizza(temPizza) && makingPizzaPanelClass[i].gameObject.activeSelf)
			{
                makingPizzaPanelClass[i].SetMainPanelRect(0f);
                makingPizzaPanelClass[i].gameObject.SetActive(false);
                break;
			}
		}
        // ���� �г��� ��ġ�� �������Ѵ�.
        int ind = 0;
        for (int i = 0; i < makingPizzaPanelArr.Length; i++)
		{
            if (makingPizzaPanelArr[i].activeSelf && makingPizzaPanelArr[i].transform.GetChild(1).GetComponent<RectTransform>().rect.width == 0f)
			{
                makingPizzaPanelRect[i].localPosition = new Vector3(0, 260 - (140 * ind));
                ind++;
			}
		}

        // �ӽ÷� �����ص� ���ڸ� �����Ѵ�.
        return temPizza;
	}

}
