using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StoreNS;
// �Ѽ�ȣ �ۼ�
public class StoreManager : MonoBehaviour, IInitStore
{
    [SerializeField] private GameObject[] itemPanelObjArr;
    [SerializeField] private Text[] itemSelectCountArr;
    [SerializeField] private GameObject storePanel;
    private List<int> selectItemCnt = new List<int>();  // ������ ������ ����
    private Dictionary<ItemS,int> selectItemDic = new Dictionary<ItemS,int>(); // ������ ������ ������ ����
    private ItemPanelArr[] itemPanelArr;
   
    private Store nowStore; // ���� �� ���� 

    private int nowPage = 0;
    private bool isSelectItemList;   // ��ٱ��� Ŭ�� ����

    // Start is called before the first frame update
    void Awake()
    {
        itemPanelArr = new ItemPanelArr[itemPanelObjArr.Length];
        
        for (int i = 0; i < itemPanelObjArr.Length; i++)
		{
            itemPanelArr[i] = itemPanelObjArr[i].GetComponent<ItemPanelArr>();
        }
        
    }
    /// <summary>
    /// ��ǰ �ѷ����⸦ ������.
    /// </summary>
    public void CancelStore()
	{
        InitSelectItemCnt();

        nowStore.CloseStore.CloseStore(-999, null);
        // ���� ����
        storePanel.SetActive(false);
    }
    /// <summary>
    /// ������ ��ǰ ���� ���� ���ϰ� �ٽ� ��ȭ�� ���ư���.
    /// </summary>
    public void ResultStore()
	{
        int sum = 0;
        for (int i = 0; i <nowStore.StoreItemList.Count; i++)
		{
            sum += nowStore.StoreItemList[i].Cost * selectItemCnt[i];
		}
        // �� ������ �������ο��� �ٽ� ������
        nowStore.CloseStore.CloseStore(sum, selectItemDic);
        //InitSelectItemCnt();
        // ���� ����
        storePanel.SetActive(false);

    }
    private void InitPage()
	{
        nowPage = 0;
	}
    /// <summary>
    /// ������ �������� ���� �� ����ϰ�, �������� 0�������� �ǵ�����.
    /// </summary>
    public void InitSelectItemCnt()
    {
        InitPage();

        selectItemCnt.Clear();
        selectItemDic.Clear();
        if (nowStore != null)
        {
            for (int i = 0; i < nowStore.StoreItemList.Count; i++)
            {
                selectItemCnt.Add(0);
            }
        }
        for (int i = 0; i < itemPanelObjArr.Length; i++)
        {
            itemSelectCountArr[i].text = 0.ToString();
        }
        InitItemPanel();
    }
    /// <summary>
    /// �� ������ �г� �ʱ�ȭ
    /// </summary>
    private void InitItemPanel()
	{
        for (int i = 0; i < itemPanelArr.Length; i++)
        {
            if (!isSelectItemList)
            {
                if (nowPage * itemPanelArr.Length + i < nowStore.StoreItemList.Count)
                {
                    itemPanelArr[i].Item = nowStore.StoreItemList[nowPage * itemPanelArr.Length + i].Item;
                    itemPanelArr[i].ItemCost = nowStore.StoreItemList[nowPage * itemPanelArr.Length + i].Cost;
                    itemPanelArr[i].SetItemName(nowStore.StoreItemList[nowPage * itemPanelArr.Length + i].Item.Name);
                    itemSelectCountArr[i].text = selectItemCnt[nowPage * itemPanelArr.Length + i].ToString();
                }
                else
                {
                    itemPanelArr[i].Item = null;
                    itemPanelArr[i].ItemCost = -999;
                    itemPanelArr[i].SetItemName("");
                    itemSelectCountArr[i].text = "0";
                }
            }
            else
			{
                if (nowPage * itemPanelArr.Length + i < selectItemDic.Count && selectItemDic.CheckIndexDic(nowPage * itemPanelArr.Length + i))
				{
                    itemPanelArr[i].Item = selectItemDic.FindKeyForIndex(nowPage * itemPanelArr.Length + i).Value;
                    int n = nowStore.StoreItemList.FindIndex(a => a.Item.Equals(selectItemDic.FindKeyForIndex(nowPage * itemPanelArr.Length + i).Value));
                    if (n != -1)
                    {
                        itemPanelArr[i].ItemCost = nowStore.StoreItemList[n].Cost;
                        itemSelectCountArr[i].text = selectItemCnt[n].ToString();
                    }
                    itemPanelArr[i].SetItemName(selectItemDic.FindKeyForIndex(nowPage * itemPanelArr.Length + i).Value.Name);
                }
                else
				{
                    itemPanelArr[i].Item = null;
                    itemPanelArr[i].ItemCost = -999;
                    itemPanelArr[i].SetItemName("");
                    itemSelectCountArr[i].text = "0";
                }
            }
        }
	}
    /// <summary>
    /// ������ �������� ���� �ϳ� �߰�
    /// </summary>
    /// <param name="index"></param>
    public void PlusCount(int index)
    {

        if ((nowPage * itemPanelArr.Length + index < nowStore.StoreItemList.Count && !isSelectItemList))
        {
            int cnt = Constant.PlayerItemDIc.ContainsKey(itemPanelArr[index].Item.Value) ? Constant.PlayerItemDIc[itemPanelArr[index].Item.Value] : 0; 

            if (itemPanelArr[index].Item.Value.MaxCnt - cnt > selectItemCnt[nowPage * itemPanelArr.Length + index])
            {
                if (selectItemCnt[nowPage * itemPanelArr.Length + index] == 0)
                {
                    selectItemDic.Add(itemPanelArr[index].Item.Value, 1);
                }
                else
                {
                    selectItemDic[itemPanelArr[index].Item.Value]++;
                }

                selectItemCnt[nowPage * itemPanelArr.Length + index]++;
                itemSelectCountArr[index].text = selectItemCnt[nowPage * itemPanelArr.Length + index].ToString();
            }
        }
        else if (nowPage * itemPanelArr.Length + index < selectItemDic.Count && isSelectItemList)
		{
            int n = nowStore.StoreItemList.FindIndex(a => a.Item.Equals(selectItemDic.FindKeyForIndex(nowPage * itemPanelArr.Length + index).Value));

            int cnt = Constant.PlayerItemDIc.ContainsKey(nowStore.StoreItemList[n].Item) ? Constant.PlayerItemDIc[nowStore.StoreItemList[n].Item] : 0;
            if (selectItemCnt[n] - cnt < itemPanelArr[index].Item.Value.MaxCnt)
			{
                if (selectItemCnt[n] == 0)
				{
                    selectItemDic.Add(itemPanelArr[index].Item.Value, 1);
				}
                else
				{
                    selectItemDic[itemPanelArr[index].Item.Value]++;
				}

                selectItemCnt[n]++;
                itemSelectCountArr[index].text = selectItemCnt[n].ToString();
			}
        }

    }
    /// <summary>
    /// �������� ���� �������� ���� �ϳ� ����
    /// </summary>
    /// <param name="index"></param>
    public void MinusCount(int index)
	{
        if ((nowPage * itemPanelArr.Length + index < nowStore.StoreItemList.Count && !isSelectItemList))
		{
            if (selectItemCnt[nowPage * itemPanelArr.Length + index] > 0)
			{
                selectItemCnt[nowPage * itemPanelArr.Length + index]--;
                itemSelectCountArr[index].text = selectItemCnt[nowPage * itemPanelArr.Length + index].ToString();

                if (selectItemCnt[nowPage * itemPanelArr.Length + index] == 0)
				{
                    selectItemDic.Remove(itemPanelArr[index].Item.Value);
                    InitItemPanel();
				}
                else
				{
                    selectItemDic[itemPanelArr[index].Item.Value]--;
				}
			}
		}
        else if (nowPage * itemPanelArr.Length + index < selectItemDic.Count && isSelectItemList)
		{
            int n = nowStore.StoreItemList.FindIndex(a => a.Item.Equals(selectItemDic.FindKeyForIndex(nowPage * itemPanelArr.Length + index).Value));
            if (selectItemCnt[n] > 0)
			{
                selectItemCnt[n]--;
                itemSelectCountArr[index].text = selectItemCnt[n].ToString();

                if (selectItemCnt[n] == 0)
				{
                    selectItemDic.Remove(itemPanelArr[index].Item.Value);
                    InitItemPanel();
				}
				else 
                {
                    selectItemDic[itemPanelArr[index].Item.Value]--;
                }
			}
        }

    }
    public void NextPage()
    {
        if (!isSelectItemList)
        {
            if (nowPage < nowStore.StoreItemList.Count / itemPanelArr.Length)
            {
                nowPage++;
                InitItemPanel();
            }
        }
        else
		{
            if (nowPage < selectItemDic.Count / itemPanelArr.Length)
			{
                nowPage++;
                InitItemPanel();
			}
		}
    }
    public void BackPage()
	{
        if (nowPage > 0)
		{
            nowPage--;
            InitItemPanel();
		}
	}
    public void TrueIsSelectItemList()
	{
        isSelectItemList = true;
        InitPage();
        InitItemPanel();
	}
    public void FalseIsSelectItemList()
	{
        isSelectItemList = false;
        InitPage();
        InitItemPanel();
	}
    public void InitStore(Store store)
    {
        nowStore = store;

        InitSelectItemCnt();
    }
    public void OpenStore()
	{
        storePanel.SetActive(true);
	}
}
