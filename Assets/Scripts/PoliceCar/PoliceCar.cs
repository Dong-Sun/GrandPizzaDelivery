using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoliceNS.PolicePathNS;
using PoliceNS.PoliceStateNS;

// �Ѽ�ȣ �ۼ�

public class PoliceCar : MonoBehaviour, IPoliceCar, IMovingPoliceCarControl, IInspectingPoliceCarControl, IEndInspecting
{
    [Range(0f, 100f)] public float PoliceHp;

    [SerializeField] private GameObject checkColObj;
    [SerializeField] private GameObject stopCheckColObj;
    public static bool IsInspecting { get; private set; }

    private static List<int> policeCarCodeList = new List<int>();

    private PoliceState policeState;

    private IPoliceSmokeEffect iPoliceSmokeEffect;

    // ��θ� ���ʴ�� ��� �ִ�.
    private List<PolicePath> policePathList = new List<PolicePath>();

    private PlayerMove playerMove;
    private Transform trans;
    private Coroutine smokeEffectCoroutine;

    private Vector3 temRotate;
    private Vector3 temPosition;

    private float speed;    // �ڵ����� �ӵ�
    private float rotate;   // �÷��̾�� �ش� ����ŭ z�� ������ ������ �մϴ�.
    private int hp;
    private int index;
    private int policeCarCode;  // �ڵ��� ������ȣ
    private bool nextBehaviour;
    private bool isBehaviour;   // �ֺ��� ���� �ִ��� ���ο� ���� �ൿ�� ������ �� �ְ� ���ش�.
    private bool isLock = false;
    private bool isRight = false;   // �������� ������ �������� ������������ Ȯ�����ش�. 
    private bool isExplosion = false;
    void Awake()
    {
        PoliceHp = 100;

        trans = this.transform;
        if (checkColObj != null)
        {
            checkColObj.GetComponent<PoliceCarCollisionCheck>().SetIPoliceCarIsBehaviour(this);
        }
        if (stopCheckColObj != null)
        {
            stopCheckColObj.GetComponent<StopPoliceCarCollisionCheck>().SetIInspectingPoliceCarControl(this);
            stopCheckColObj.GetComponent<StopPoliceCarCollisionCheck>().SetIPoliceCarIsBehaviour(this);
            stopCheckColObj.GetComponent<StopPoliceCarCollisionCheck>().SetIEndInspecting(this);

        }
        InitValue();
    }

    private void Start()
    {
        smokeEffectCoroutine = StartCoroutine(PoliceSmokeCoroutine());
    }

    public void SetPlayerMove(PlayerMove playerMove)
	{
        this.playerMove = playerMove;
	}

    private void InitValue()
    {
        InitState(true);

        isRight = Random.Range(0, 2) == 0 ? true : false;
        //isLeft = false;
        trans.eulerAngles = new Vector3(0,0, isRight ? 0 : 180);
        speed = Random.Range(1,10); // �ӵ��� �������� ��
        hp = 100;   // �ڵ����� �⺻ ü���� 100
        rotate = 0;
        index = 0;
        while(true)
        {
            policeCarCode = Random.Range(0, 1000);
            if (policeCarCodeList.FindIndex(a => a.Equals(policeCarCode)) == -1)
            {
                policeCarCodeList.Add(policeCarCode);
                break;
            }
        }
        
        isBehaviour = true;
    }
    // ���¸� �ʱ�ȭ���ش�.
    private void InitState(bool bo)
    {
        if (bo)
        {
            // �������� ���¸� �������� �����ش�.
            policeState = (PoliceState)Random.Range(1, 3);
        }
        // �������� ���¿� ���� ������� �ݶ��̴��� �ٸ���.
        if (policeState == PoliceState.MOVING)
        {
            checkColObj.SetActive(true);    // ���ش�.
            stopCheckColObj.SetActive(false);   // ���ش�.
        }
        else if (policeState == PoliceState.STOP)
        {
            stopCheckColObj.SetActive(true);    // ���ش�.
            checkColObj.SetActive(false);   // ���ش�.
        }
        else if (policeState == PoliceState.INSPECTING)
        {
            IsInspecting = true;
            if (playerMove != null)
            {
                playerMove.Stop = true;
            }
            stopCheckColObj.SetActive(false);
            checkColObj.SetActive(false);
        }
        else if (policeState == PoliceState.DESTROY)
        {
            stopCheckColObj.SetActive(false);
            checkColObj.SetActive(false);
        }
    }

    // �������� ��θ� �����ִ� �Լ��̴�.
    public void InitPoliceCarPath(List<PolicePath> policePathList)
    {
        // ��θ� ����ϰ� �� �����ش�.
        this.policePathList.Clear();
        for (int i = 0; i < policePathList.Count; i++)
        {
            this.policePathList.Add(policePathList[i]);
        }
        // �������� �������� �ٶ󺸰� �ִٸ� 0���� ��θ� �����ϰ�, ������ �ٶ󺸰� �ִٸ� ����ȣ���� ��θ� �����Ѵ�.
        if (isRight) { index = 0; }
        else { index = this.policePathList.Count - 1; }
    }

    // ���������� � �ൿ�� �ϰ� ���� ����� �����ϴ�.
    private void PoliceCarBehaviour(int choice, float value)
    {
        switch(choice)
        {
            case 1:
                Straight(value);
                // ���� �� ����Ȯ���� �ҽɰ˹�(STOP) ���·� �ٲ�ϴ�.
                if (Random.Range(0,1000) < 2) { InitState(true); }
                break;
            case 2:
                Turn(value);
                TurnStraight(value);
                break;
        }
    }
    // �ڵ����� ȸ�� �� ȸ���� �������� �����Ÿ� �����ϰԲ� ���ݴϴ�.
    private void TurnStraight(float value)
    {
        int n = 1;
        if (value > 0) { n = -1; }
        trans.position += transform.right * ((Mathf.PI * speed) / (2 * value * (-1) * n));
    }
    // �ٶ󺸴� �������� �����մϴ�.
    private void Straight(float dis)
    {
        if (!isLock)
        {
            temPosition = trans.position + (transform.right * dis);
            isLock = true;
        }
        
        if (Vector3.SqrMagnitude(trans.position - temPosition)
            <= Vector3.SqrMagnitude((trans.position + transform.right * speed * Time.deltaTime) - temPosition))
        {
            trans.position = temPosition;
            
            //Debug.Log($"{trans.position.x}  {trans.position.y}");
            nextBehaviour = true;
            isLock = false;
        }
        else
        {
            trans.position += transform.right * speed * Time.deltaTime;
        }
    }
    // ȸ���մϴ�. �ش� �Լ��� Mathf.Abs(rotate)����ŭ ȣ��ǰ� ���� ������� �Ѿ�� �մϴ�.
    private void Turn(float rotate)
    {
        // this.rotate�� ���� �������ݴϴ�.
        if (this.rotate == 0f)
        {
            // ������ ���� �������� ��� ������ �ٶ󺸰� �������� ���� �ٸ��ϴ�.
            this.rotate = rotate * (isRight ? 1 : -1);
            // ������ �ٲٱ��� �������� z�� rotation���� temRotate�� �����մϴ�.
            temRotate = trans.eulerAngles;
        }
        if (rotate * (isRight ? 1 : -1) > 0)
        {
            this.rotate += (-1) * speed;
            trans.Rotate(new Vector3(0, 0, speed));
            if (this.rotate < 0)
            {
                this.rotate = 0f;
            }
        }
        else if (rotate * (isRight ? 1 : -1) < 0)
        {
            this.rotate += speed;
            trans.Rotate(new Vector3(0, 0, (-1) * speed));
            if (this.rotate > 0)
            {
                this.rotate = 0f;
            }
        }

        // this.rotate���� 0�� �Ǹ� ȸ���� �������� �ǹ��մϴ�.
        if (this.rotate == 0f)
        {
            // �������� ������ ������ ���� �Ϳ� ����Ͽ� ȸ���� ���� �� ��Ȯ�� ���Ⱚ�� �ٽ� �־��ݴϴ�.
            trans.eulerAngles = temRotate + new Vector3(0,0, rotate * (isRight ? 1 : -1));
            // ���� ����� �θ� �� �ֵ��� nextBehaviour���� true�� �ٲߴϴ�.
            nextBehaviour = true;
        }
    }

    private IEnumerator PoliceSmokeCoroutine()
    {
        var time = new WaitForSeconds(0.01f);
        int r = 0;
        while(true)
        {
            r = Random.Range(5, 15);

            if (PoliceHp < 70f)
            {
                iPoliceSmokeEffect.InsPoliceSmokeEfectObj(this.transform);
            }

            if (PoliceHp <= 0f && policeState != PoliceState.DESTROY)
            {
                this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                // �ı� ���·� ����
                policeState = PoliceState.DESTROY;
                // ���¿� ���� �ݶ��̴� �ʱ�ȭ
                InitState(false);
                // �ı�
                //Destroy(this.gameObject, 10f);
                //StopCoroutine(smokeEffectCoroutine);
                Invoke("AddForceCar", 9f);
            }
            
            // ������ ü���� 0�� �Ǹ� rigidbody-constrait�� �����ϰ� 10�� �� �����ϵ�����.
             

            for (int i = 0; i < r; i++)
            {
                yield return time;
            }
        }
    }

    private void AddForceCar()
    {
        // ���� �̹��� �ֱ�
        isExplosion = true;

        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0, 10f), Random.Range(0, 10f)), ForceMode2D.Impulse);
        Invoke("DestroyCar", 5f);
    }

    private void DestroyCar()
    {
        StopCoroutine(smokeEffectCoroutine);
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �±װ� �÷��̾�� ���� ��
        if (collision.gameObject.tag.Equals("Player"))
        {
            // ũ��Ƽ�� 1.5��
            PoliceHp -= Mathf.Abs(collision.gameObject.GetComponent<PlayerMove>().Speed) * 7f * Random.Range(1.0f, 1.5f);

            if (PoliceHp < 0f) { PoliceHp = 0f; }
            
        }
    }


    void FixedUpdate()
    {
        if (policeState == PoliceState.DESTROY && isExplosion)
        {

        }

        if (policeState == PoliceState.DESTROY) { return; }

        // �������� ���°� MOVING�̿��߸� ������ �����Ѵ�.
        if (policeState == PoliceState.MOVING)
        {
            // �ֺ��� ���� ���ٴ� ���� �����ϰ�, ����� ��ΰ� 0�� �ƴ϶�� ������ �����Ѵ�.
            if (policePathList.Count != 0 && isBehaviour)
            {
                // ���������� � �ൿ�� �ϰ� ���� ����� �����ϴ�.
                PoliceCarBehaviour(policePathList[index].Behaviour, policePathList[index].Value);
            }

            if (nextBehaviour && isBehaviour)
            {
                trans.position = new Vector3((float)System.Math.Round(trans.position.x, 1), (float)System.Math.Round(trans.position.y, 1));

                if (isRight)
                {
                    if (policePathList.Count - 1 == index)
                    {
                        index = 0;
                    }
                    else
                    {
                        index++;
                    }
                }
                else
                {
                    if (index == 0)
                    {
                        index = policePathList.Count - 1;
                    }
                    else
                    {
                        index--;
                    }
                }
                nextBehaviour = false;
            }
        }
        else if (policeState == PoliceState.STOP)
        {
            if (Random.Range(0,10000) < 10) { InitState(true); }
        }
    }
    public void SetIsBehaviour(bool bo)
    {
        isBehaviour = bo;
    }
    public int GetPoliceCarCode()
    {
        return policeCarCode;
    }
    public void SetPoliceState(PoliceState policeState)
    {
        this.policeState = policeState;
        InitState(false);
    }
    public void EndInspecting()
	{
        playerMove.Stop = false;
        IsInspecting = false;
        this.policeState = PoliceState.MOVING;
        InitState(false);
	}
    public void SetIInspectingPanelControl(IInspectingPanelControl iInspectingPanelControl)
    {
        stopCheckColObj.GetComponent<StopPoliceCarCollisionCheck>().SetIInspectingPanelControl(iInspectingPanelControl);
    }
    public void SetPoliceSmokeEffect(IPoliceSmokeEffect iPoliceSmokeEffect)
	{
        this.iPoliceSmokeEffect = iPoliceSmokeEffect;
    }
}
