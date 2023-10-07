using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoliceNS.PoliceStateNS;

// �������� ������ �������� �� ��Ҵ�.
public abstract class Police : MonoBehaviour
{
    [Range(0f, 1000f)] public float PoliceHp;    // ������ ü��

    protected ISetTransform smokeEffectTrans;
    protected IStop iStop;

    protected SuperPoliceState spState;

    protected Coroutine smokeEffectCoroutine;

	protected virtual void Start()
	{
        smokeEffectCoroutine = StartCoroutine(PoliceSmokeCoroutine());
    }

    public void SetSmokeEffectTrans(ISetTransform iSetTransform)
	{
        smokeEffectTrans = iSetTransform;
	}
    /// <summary>
    /// �������� ü���� Ȯ���ϰ� ���������� ���⸦ ���հ����� ���ϴ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    protected IEnumerator PoliceSmokeCoroutine()
    {
        var time = new WaitForSeconds(0.01f);
        int r = 0;
        while (true)
        {
            r = Random.Range(5, 15);

            if (PoliceHp < 70f)
            {
                smokeEffectTrans.SetTransform(this.transform);
            }
            // ������ ü���� 0�� �Ǹ� rigidbody-constrait�� �����ϰ� 10�� �� �����ϵ�����.
            if (PoliceHp <= 0f && spState != SuperPoliceState.DESTROY)
            {
                if (this.GetComponent<Rigidbody2D>() != null)
                {
                    this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                }
                // �ı� ���·� ����
                ChangeSuperPoliceState(SuperPoliceState.DESTROY);
                // ���¿� ���� �ݶ��̴� �ʱ�ȭ
                InitState(false);
                // �ı�
                //Destroy(this.gameObject, 10f);
                //StopCoroutine(smokeEffectCoroutine);
                Invoke("AddForceCar", 9f);
            }

            for (int i = 0; i < r; i++)
            {
                yield return time;
            }
        }
    }
    protected virtual void InitState(bool bo)
	{

	}
    protected virtual void ChangeSuperPoliceState(SuperPoliceState spState)
	{
        this.spState = spState;
	}
    protected void AddForceCar()
	{
        if (spState == SuperPoliceState.DESTROY)
        {
            if (this.GetComponent<Rigidbody2D>() != null)
            {
                this.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0, 10f), Random.Range(0, 10f)), ForceMode2D.Impulse);
            }
        }

        Invoke("DestroyCar", 5f);
    }

    protected void DestroyCar()
    {
        StopCoroutine(smokeEffectCoroutine);
        DestroyPolice();
        Destroy(this.gameObject);
    }
    protected virtual void DestroyPolice() { }

    public void SetMap(IStop iStop)
    {
        this.iStop = iStop;
    }
}
