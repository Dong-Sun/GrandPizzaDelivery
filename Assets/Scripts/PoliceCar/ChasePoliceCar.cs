using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ѽ�ȣ �ۼ�
public class ChasePoliceCar : MonoBehaviour, ISetTransform
{
    /// <summary>
    /// �߰� ������ ����. SPUERCHASE�� �Ÿ� ������� ������ �÷��̾� �Ѿƿ��� ����
    /// </summary>
    private enum ChaserPoliceState { NONE, SPUERCHASE};

    private Transform playerTrans;
    private Coroutine stateCoroutine;

    private RaycastHit2D hit;

    private ChaserPoliceState chaserPoliceState = ChaserPoliceState.SPUERCHASE;

    private float time;

    // Start is called before the first frame update
    void Start()
    {
        //stateCoroutine = StartCoroutine(stateMachine());
    }
    /// <summary>
    /// �߰� �������� ���� ������ ���
    /// </summary>
    /// <returns></returns>
    private IEnumerator stateMachine()
	{
        while(true)
		{
            // �÷��̾���� �Ÿ� ������� ������ �Ѿƿ��� ������.
            if (chaserPoliceState == ChaserPoliceState.SPUERCHASE)
			{
                // ���� 


                yield return Constant.OneTime;
			}
		}
	}

    private void SpuerChase()
	{

	}

    private bool FindPlayer()
	{

        return false;
	}

    /// <summary>
    /// �÷��̾��� Ʈ�������� ������. ��ġ�� ������ �߰��ϱ� ����
    /// </summary>
    /// <param name="trans"></param>
    public void SetTransform(Transform trans)
	{
        playerTrans = trans;
	}

	private void FixedUpdate()
	{
        time += Time.deltaTime;

        switch(chaserPoliceState)
		{
            // �÷��̾���� �Ÿ� ������� ������ �Ѿƿ��� ������.
            case ChaserPoliceState.SPUERCHASE:
                // �÷��̾ ������ �Ѿƿ�
                SpuerChase();
                // ���� �÷��̾ �߰��Ѵٸ�

                    break;
		}
	}
}
