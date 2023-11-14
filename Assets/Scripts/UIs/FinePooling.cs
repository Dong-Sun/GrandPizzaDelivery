using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinePooling : MonoBehaviour
{
    [SerializeField] private GameObject finePrefab;

    private List<GameObject> poolingObj = new List<GameObject>();
    private Queue<string> objText = new Queue<string>();
    
    private Transform trans;
    private Coroutine moveCoroutine;

    private Vector3 originPosition = new Vector3(1780, 600);

    private int upPoint = 0;
    private bool isTrue = true;
    public void Awake()
    {
        trans = this.transform;
        moveCoroutine = StartCoroutine(Move());
    }
    /// <summary>
    ///  ������Ʈ Ǯ���� �̿��� ���� �ΰ� UI ����
    /// </summary>
    /// <param name="fine">���� ��</param>
    public void AddFine(int fine)
    {
        upPoint++;
        objText.Enqueue($"������ �ΰ��Ǿ����ϴ� \n : { fine } ��");
        Constant.Fine += fine;
    }
    IEnumerator Move()
    {
        while (true)
        {
            if (upPoint > 0)
            {
                isTrue = true;
                int index = 0;

                for (int i = 0; i < poolingObj.Count; i++)
                {
                    if (!poolingObj[i].activeSelf)
                    {
                        isTrue = false;
                        index = i;
                        break;
                    }
                }
                if (isTrue)
                {
                    GameObject obj = Instantiate(finePrefab, originPosition, Quaternion.identity, this.gameObject.transform);
                    obj.GetComponent<RectTransform>().position = originPosition;
                    obj.GetComponent<FineMessage>().SetText(objText.Dequeue());
                    poolingObj.Add(obj);
                }
                else
                {
                    poolingObj[index].SetActive(true);
                    poolingObj[index].GetComponent<RectTransform>().position = originPosition;
                    poolingObj[index].GetComponent<FineMessage>().SetText(objText.Dequeue());
                }

                for (int i = 0; i < 100; i++)
                {
                    trans.position += Vector3.up;
                    yield return Constant.OneTime;
                }
                upPoint--;
            }
            yield return Constant.OneTime;
        }
    }
}
