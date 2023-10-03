using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ѽ�ȣ �ۼ�
public class ChasePoliceCar : MonoBehaviour, ISetTransform
{
    [Range (0f,10f)]
    public float Speed;
    /// <summary>
    /// �߰� ������ ����. SPUERCHASE�� �Ÿ� ������� ������ �÷��̾� �Ѿƿ��� ����
    /// </summary>
    private enum ChaserPoliceState { NONE, SPUERCHASE, AUTOMOVE};

    private Transform playerTrans;
    private Transform myTrans;
    private Coroutine stateCoroutine;

    private RaycastHit2D hit;
    
    private ChaserPoliceState chaserPoliceState = ChaserPoliceState.SPUERCHASE;
    
    /// <summary>
    /// �÷��̾ ã�Ҵ��� ���θ� �˱����� �������̽�
    /// </summary>
    private IGetBool iGetBool;  
    private float time; // ���� ���°� �ߵ��ǰ� �ִ� �ð�
    private float oldAngle = -999f; // ���� �����ӿ��� �߰ݰ������� �÷��̾��� ���� ����
	// Start is called before the first frame update
	private void Awake()
	{
        Speed = Random.Range(3f, 10f);
        myTrans = this.transform;
        iGetBool = this.GetComponent<IGetBool>();
	}
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
    /// <summary>
    /// ������������ �÷��̾ ������� �����̴�.
    /// </summary>
    private void SpuerChase()
	{
        Debug.DrawLine(myTrans.position, myTrans.position + myTrans.right * 100f);
        Debug.DrawLine(myTrans.position, myTrans.position+ (playerTrans.position - myTrans.position).normalized * 100f);
        // 1.�켱 �������� �÷��̾� ���������� ȸ����Ŵ
        // �������� �ٶ󺸴� ������ ����
        Vector3 from = (myTrans.position + myTrans.right) - myTrans.position;
        // ���������� �÷��̾� �� ������ ����
        Vector3 to = (myTrans.position + (playerTrans.position - myTrans.position).normalized) - myTrans.position;
        // Spin�� false�� �����ϸ� �������� �÷��̾ �ٶ󺸰� ��
        if (!Spin(GetAngle(from, to)))
		{
            // ���������� �÷��̾����� �ٶ󺸴� ����
            Vector3 ve = playerTrans.position - myTrans.position;
            // ������ ���� �÷��̾��� ������ ����
            float angle = Mathf.Atan2(ve.y, ve.x) * Mathf.Rad2Deg;
            // �÷��̾ �ٶ󺸰� ������ �ٲ�
            myTrans.rotation = Quaternion.AngleAxis(angle, myTrans.forward);
        }
        // 2.�������� �ٶ󺸴� ������ ������Ų��.
        Straight();
    }
    /// <summary>
    /// �� �� ������ ������ ���Ѵ�.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    private float GetAngle(Vector3 from, Vector3 to)
	{
        Vector3 v = from - (myTrans.position + new Vector3(1,0));
        return Vector3.SignedAngle(from, to, Vector3.forward);
	}
    private void AutoMove()
	{

	}
    /// <summary>
    /// �������� ȸ����ŵ�ϴ�. angle�� ��ȣ�� ���� ȸ�������� �ٸ��ϴ�.
    /// angle�� ��ȣ�� �ٲ�� ������ ����(angle�� 0 ��ó�ϋ�) ��ǥ�� �ٶ󺸴� ������ �����ϰ� false�� ����. ȸ���� ����
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    private bool Spin(float angle)
	{
        if ((oldAngle >= 0 && angle <= 0 && oldAngle != -999 && angle != 180) || (oldAngle <= 0 && angle >= 0 && oldAngle != -999 && angle != 180))
		{
            oldAngle = angle;
            return false;
        }
        else
		{
            myTrans.localEulerAngles += new Vector3(0, 0, (angle >= 0 ? 1 : -1) * Time.deltaTime * Speed * 5f);
        }
        oldAngle = angle;
        return true;
    }
    /// <summary>
    /// �������� �ٶ󺸴� �������� �̵���Ų��.
    /// </summary>
    private void Straight()
	{
        myTrans.localPosition += myTrans.right * Time.deltaTime * Speed;
	}
    /// <summary>
    /// �÷��̾��� Ʈ�������� ������. ��ġ�� ������ �߰��ϱ� ����
    /// </summary>
    /// <param name="trans"></param>
    public void SetTransform(Transform trans)
	{
        playerTrans = trans;
	}
    /// <summary>
    /// ���¸� �����ϰ� �ð��� �ʱ�ȭ��Ŵ.
    /// </summary>
    /// <param name="state"></param>
    private void ResetState(ChaserPoliceState state)
	{
        chaserPoliceState = state;
        time = 0f;
	}

	private void FixedUpdate()
	{
        time += Time.deltaTime;

        switch (chaserPoliceState)
        {
            // �÷��̾���� �Ÿ� ������� ������ �Ѿƿ��� ������.
            case ChaserPoliceState.SPUERCHASE:
                // �÷��̾ ������ �Ѿƿ�
                SpuerChase();
                // ���� �÷��̾ �߰��Ѵٸ� �ð��� 0�ʷ� ������ SUPERCHASE ���¸� ����.
                if (iGetBool.GetBool())
                {
                    ResetState(ChaserPoliceState.SPUERCHASE);
                }
                // ���� 10�ʰ� ������ �ڵ�������� �ٲ�
                if (time >= 1200f)
                {
                    ResetState(ChaserPoliceState.AUTOMOVE);
                }
                break;
            // ���� �������� ���ƴٴϴ� ������
            case ChaserPoliceState.AUTOMOVE:
                AutoMove();
                // ���� �÷��̾ �߰��Ѵٸ� �ð��� 0�ʷ� ������ SUPERCHASE ���·� ��ȯ
                if (iGetBool.GetBool())
				{
                    ResetState(ChaserPoliceState.SPUERCHASE);
				}
                break;
        }
	}
}
