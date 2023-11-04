using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoliceNS.PoliceStateNS;

// �Ѽ�ȣ �ۼ�
// �������� ������ �������� �� ��Ҵ�.
public abstract class Police : MonoBehaviour
{
    [Range(0f, 1000f)] public float PoliceHp;    // ������ ü��
    public ISpawnCar SpawnCar { get; set; }    // �߰����� ��ȯ�ϱ� ���� �������̽�

    protected ISetTransform smokeEffectTrans;
    protected IStop iStop;

    //protected SuperPoliceState spState;
    protected PoliceState policeState;    //  �������� ����
    protected PoliceType policeType;    // ������ Ÿ��
    protected Coroutine smokeEffectCoroutine;   // ���� ���� �� ����� ���� �ڷ�ƾ
    protected Coroutine damagedCoroutine;   // ���� ������ ���� ������ �ٲ�� ����Ʈ �ڷ�ƾ
    protected SpriteRenderer spr;

    protected  virtual void Awake()
	{
        if (this.GetComponent<SpriteRenderer>() != null)
		{
            spr = this.GetComponent<SpriteRenderer>();
		}
	}

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
            if (PoliceHp <= 0f && policeState != PoliceState.DESTROY)
            {
                if (this.GetComponent<Rigidbody2D>() != null)
                {
                    this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                }
                // �ı� ���·� ����
                policeState = PoliceState.DESTROY;
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
    protected IEnumerator Damaged()
	{
        spr.color = Color.red;
        yield return Constant.OneTime;
        spr.color = Color.white;
        yield return Constant.OneTime;
        spr.color = Color.green;
        yield return Constant.OneTime;
        spr.color = Color.white;
        yield return Constant.OneTime;
        spr.color = Color.red;
        yield return Constant.OneTime;
        spr.color = Color.white;
        yield return Constant.OneTime;
    }
    protected virtual void InitState(bool bo)
	{
        
	}
    protected virtual void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject.tag.Equals("Player"))
        {
            // ũ��Ƽ�� 1.5��
            PoliceHp -= Mathf.Abs(collision.gameObject.GetComponent<PlayerMove>().Speed) * 7f * Random.Range(1.0f, 1.5f);

            if (PoliceHp < 0f) { PoliceHp = 0f; }


            if (damagedCoroutine != null)
            {
                StopCoroutine(damagedCoroutine);
            }
            damagedCoroutine = StartCoroutine(Damaged());
        }
    }
    protected void AddForceCar()
	{
        if (policeState == PoliceState.DESTROY)
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
        if (policeType == PoliceType.NORMAL)
        {
            SpawnCar.SpawnCar(1);
        }
        DestroyPolice();
        Destroy(this.gameObject);
    }
    protected virtual void DestroyPolice() { }

    public void SetMap(IStop iStop)
    {
        this.iStop = iStop;
    }

    public virtual void PausePoliceCar(bool bo)
    {

    }
}
