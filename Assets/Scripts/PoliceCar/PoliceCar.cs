using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoliceNS.PolicePathNS;
using PoliceNS.PoliceStateNS;

// �Ѽ�ȣ �ۼ�

public class PoliceCar : Police, IPoliceCar, IMovingPoliceCarControl, IInspectingPoliceCarControl, IEndConversation, IPriorityCode
{
    [SerializeField] private GameObject checkColObj;    // �̵� �� �浹�� �����ϱ� ���� �ݶ��̴�
    [SerializeField] private GameObject stopCheckColObj;    // ���� ��, �ҽɰ˹��� ���� �ݶ��̴�
    public static bool IsInspecting { get; private set; }   // �÷��̾ �ҽɰ˹� ������ Ȯ���ϴ� ���� ����

    private static List<int> policeCarCodeList = new List<int>();  // ������ ������ȣ ����Ʈ

    // ��θ� ���ʴ�� ��� �ִ�.
    private List<PolicePath> policePathList = new List<PolicePath>();

    private GameObject banana;
    private PlayerMove playerMove;
    private Transform trans;
    private Coroutine shootBananaCoroutine;
    private Rigidbody2D rigid2D;
    
    private Vector3 temRotate;
    private Vector3 temPosition;

    public float Speed;    // �ڵ����� �ӵ�
    private float rotate;   // �÷��̾�� �ش� ����ŭ z�� ������ ������ �մϴ�.
    private int hp; // �������� ü��
    private int index;  // ��� ����Ʈ�� �ε���. ������������ ������ ������������ �������� ���� �����ִ� ���� ��ȣ�� �ٸ���.
    private int policeCarCode;  // ������ ������ȣ. �������鳢�� �켱������ ���ϴµ� ����Ѵ�.
    private bool nextBehaviour;
    private bool isBehaviour;   // �ֺ��� ���� �ִ��� ���ο� ���� �ൿ�� ������ �� �ְ� ���ش�.
    private bool isLock = false;
    private bool isRight = false;   // �������� ������ �������� ������������ Ȯ�����ش�.
    private bool isStop = false;    // ������ ���� ����
    protected override void Awake()
    {
        base.Awake();
        PoliceHp = 100; // ������ �ʱ� ü�� ����

        trans = this.transform; // transform ĳ��
        if (checkColObj != null)
        {
            // �ʿ��� �������̽����� �־���
            checkColObj.GetComponent<PoliceCarCollisionCheck>().SetIPoliceCarIsBehaviour(this);
            checkColObj.GetComponent<PoliceCarCollisionCheck>().SetIPriority(this);
        }
        if (stopCheckColObj != null)
        {
            // �ʿ��� �������̽����� �־���
            stopCheckColObj.GetComponent<StopPoliceCarCollisionCheck>().SetIInspectingPoliceCarControl(this);
            stopCheckColObj.GetComponent<StopPoliceCarCollisionCheck>().SetIPoliceCarIsBehaviour(this);
            stopCheckColObj.GetComponent<StopPoliceCarCollisionCheck>().SetIEndInspecting(this);
            stopCheckColObj.GetComponent<StopPoliceCarCollisionCheck>().SetIPriority(this);
        }
        if (this.GetComponent<Rigidbody2D>() != null)
		{
            // rigidbody2d ĳ��
            rigid2D = this.GetComponent<Rigidbody2D>();
        }

        InitValue();    // �ʱ⿡ �����Ǿ�� �ϴ� ��
    }

    protected override void Start()
    {
        base.Start();
        shootBananaCoroutine = StartCoroutine(ShootBananaTermCoroutine());
    }

    public void SetPlayerMove(PlayerMove playerMove)
	{
        this.playerMove = playerMove;
	}
    public void SetBanana(GameObject banana)
	{
        this.banana = banana; 
	}

    private void InitValue()
    {
        InitState(true);

        isRight = Random.Range(0, 2) == 0 ? true : false;
        trans.eulerAngles = new Vector3(0,0, isRight ? 0 : 180);
        Speed = Random.Range(1,10); // �ӵ��� �������� ��
        hp = 100;   // �ڵ����� �⺻ ü���� 100
        rotate = 0;
        index = 0;
        while(true)
        {
            policeCarCode = Random.Range(0, 1000) + 100000000;
            if (policeCarCodeList.FindIndex(a => a.Equals(policeCarCode)) == -1)
            {
                policeCarCodeList.Add(policeCarCode);
                break;
            }
        }
        
        isBehaviour = true;
    }
    /// <summary>
    /// ���¿� ���� �ݶ��̴��� ���ְų� ���ش�. 
    /// </summary>
    /// <param name="bo">true�� �������� ���¸� �������� �����ָ�, false�� �������� ���¸� �������� �ʴ´�.</param>    
    protected override void InitState(bool bo)
    {
        if (bo)
        {
            // ���� ����
            if (GameManager.Instance.isDarkDelivery)
            {
                // �������� ���¸� �������� �����ش�.
                policeState = (PoliceState)Random.Range(1, 3);
            }
            else
            {
                policeState = PoliceState.MOVING;
            }
        }
        // �������� ���¿� ���� ������� �ݶ��̴��� �ٸ���.
        if (policeState == PoliceState.MOVING)  // �������� �̵� ������ ��
        {
            checkColObj.SetActive(true);    // ���ش�.
            stopCheckColObj.SetActive(false);   // ���ش�.
        }
        else if (policeState == PoliceState.STOP)   // �������� ���� ������ ��(�ҽɰ˹�)
        {
            stopCheckColObj.SetActive(true);    // ���ش�.
            checkColObj.SetActive(false);   // ���ش�.
        }
        else if (policeState == PoliceState.INSPECTING) // �������� �ҽɰ˹� ���� ������ ��
        {
            IsInspecting = true;    // true�� �÷��̾ �ҽɰ˹� ������ ��Ÿ��
            if (playerMove != null)
            {
                playerMove.Stop = true; // �÷��̾��� �̵��� �����Ѵ�.
            }
            stopCheckColObj.SetActive(false);   // ���ش�.
            checkColObj.SetActive(false);   // ���ش�.
        }
        else if (policeState == PoliceState.DESTROY)    // �������� �ı��� ������ ��
        {
            stopCheckColObj.SetActive(false);   // ���ش�.
            checkColObj.SetActive(false);   // ���ش�.
        }
    }
    /// <summary>
    /// �������� ��θ� �����ִ� �Լ��̴�.
    /// </summary>
    public void InitPoliceCarPath(List<PolicePath> policePathList)
    {
        // ��θ� ����ϰ� �� �����ش�.
        this.policePathList.Clear();
        // ��θ� ���ʴ�� �־��ش�.
        for (int i = 0; i < policePathList.Count; i++)
        {
            this.policePathList.Add(policePathList[i]);
        }
        // �������� �������� �ٶ󺸰� �ִٸ� 0���� ��θ� �����ϰ�, ������ �ٶ󺸰� �ִٸ� ����ȣ���� ��θ� �����Ѵ�.
        if (isRight) { index = 0; }
        else { index = this.policePathList.Count - 1; }
    }

    /// <summary>
    /// ���������� � �ൿ�� �ϰ� ���� ����� �����ϴ�.
    /// </summary>
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
                // ��ȸ��, ��ȸ���� �Ѵ�.
                Turn(value);
                TurnStraight(value);
                break;
        }
    }
    private float nn = 0f;
    /// <summary>
    /// �ڵ����� ȸ�� �� ȸ���� �������� �����Ÿ� �����ϰԲ� ���ݴϴ�.
    /// </summary>
    private void TurnStraight(float value)
    {
        
        if (nn + Speed < Mathf.Abs(value))
        {
            trans.position += transform.right * ((Mathf.PI * Speed * Time.timeScale) / (2 * Mathf.Abs(value)));
            nn += Speed;
        }
        else
        {
            trans.position += transform.right * ((Mathf.PI * (Mathf.Abs(value) - nn) * Time.timeScale) / (2 * Mathf.Abs(value)));
            nn = 0f;
        }
    }
    /// <summary>
    /// �ٶ󺸴� �������� �����մϴ�.
    /// </summary>
    private void Straight(float dis)
    {
        if (!isLock)
        {
            // temPosition�� �������� ��ǥ�� �ϰ� �ִ� �����̴�.
            // isLock���� ��ǥ������ �ѹ��� �����ش�.
            temPosition = trans.position + (transform.right * dis);
            isLock = true;
        }
        
        // �������� ��ġ�� ��ǥ������ ���� ��������� ��, �������� ��ġ�� ��ǥ�������� �ٲ� ��ġ�� ������ ���ش�.
        if (Vector3.SqrMagnitude(trans.position - temPosition)
            <= Vector3.SqrMagnitude((trans.position + transform.right * Speed * Time.deltaTime) - temPosition))
        {
            trans.position = temPosition;
            
            nextBehaviour = true;   // ��ǥ������ ���������Ƿ� ���� ����� ���� �غ� �ȴ�.
            isLock = false; // ��ǥ������ ���������Ƿ� temPosition�� ���� ȣ�� �� �ٽ� �޾ƾ��Ѵ�.
        }
        else
        {
            // �������� ��ǥ���� �������� �����Ÿ� �̵���Ų��.
            trans.position += transform.right * Speed * Time.deltaTime;
        }
    }
    /// <summary>
    /// ȸ���մϴ�. �ش� �Լ��� Mathf.Abs(rotate)����ŭ ȣ��ǰ� ���� ������� �Ѿ�� �մϴ�.
    /// </summary>
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
        // rotate�� ���� �������� ������� ���ο�, �������� ���������� ������ �ƴ����� ���� 
        // �� 4������ ��Ȳ�� �����, �׿� ���� �ٸ��� ȸ���� ������ �ʿ䰡 �ִ�.
        if (rotate * (isRight ? 1 : -1) > 0)
        {
            this.rotate += (-1) * Speed * Time.timeScale;
            trans.Rotate(new Vector3(0, 0, Speed * Time.timeScale));
            if (this.rotate < 0)
            {
                this.rotate = 0f;
            }
        }
        else if (rotate * (isRight ? 1 : -1) < 0)
        {
            this.rotate += Speed * Time.timeScale;
            trans.Rotate(new Vector3(0, 0, (-1) * Speed * Time.timeScale));
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
    /// <summary>
    /// ���� �ð����� �ٳ����� ������ �ϴ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShootBananaTermCoroutine()
	{
        var time = new WaitForSeconds(1f);
        int r = 0;
        while(true)
		{
            r = Random.Range(50, 150);

            for (int i = 0; i < r; i++)
			{
                yield return time;
			}

            ShootBanana();
		}
	}
    private void ShootBanana()
	{
        GameObject ba = Instantiate(banana);
        ba.transform.position = this.transform.position;
        ba.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0, 5f), Random.Range(0, 5f)), ForceMode2D.Impulse);
	}

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        // �÷��̾�� �浹�ϸ� �������� ������� ����.
        // �±װ� �÷��̾�� ���� ��
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
        // ���������� �浹�ص� ������� ����.
        else if (collision.gameObject.GetComponent<IPoliceCar>() != null)
        {
            // �������� ���� �ڱ� ��θ� ã�� �޸��� �ִ� ����� �� �ε����ٸ� ���ظ� ���� ����. �׷��� ���� ��쿡�� ���ظ� ����.
            if (!(collision.gameObject.GetComponent<IPoliceCar>().GetPoliceState() == PoliceState.MOVING
                && collision.gameObject.GetComponent<IPoliceCar>().GetPoliceState() == policeState))
            {
                // �ε��� �������� MOVING���°� �ƴ϶�� �������� velocity�� ������.
                if (collision.gameObject.GetComponent<IPoliceCar>().GetPoliceState() != PoliceState.MOVING)
                {
                    PoliceHp -= Mathf.Sqrt(Mathf.Pow(collision.gameObject.GetComponent<IPoliceCar>().GetRigidBody2D().velocity.x, 2)
                        + Mathf.Pow(collision.gameObject.GetComponent<IPoliceCar>().GetRigidBody2D().velocity.x, 2)) * 7f;

                }
                // �ε��� �������� MOVING���¶�� �������� speed�� ������.
                else
                {
                    PoliceHp -= collision.gameObject.GetComponent<IPoliceCar>().GetSpeed() * 7f;
                }


                if (damagedCoroutine != null)
                {
                    StopCoroutine(damagedCoroutine);
                }
                damagedCoroutine = StartCoroutine(Damaged());
            }
        }

    }
    public void SetIsStop(bool bo)
    {
        isStop = bo;
    }
    protected override void DestroyPolice()
	{
        iStop.RemovePoliceList(this);
	}
    /// <summary>
    /// ������ �Ͻ� ����. Ǯ���� �̵��Ѵ�.
    /// </summary>
    /// <param name="bo"></param>
    public override void PausePoliceCar(bool bo)
    {
        if (bo)
        {
            policeState = PoliceState.NONE;
        }
        else
        {
            policeState = PoliceState.MOVING;
        }
    }
    void FixedUpdate()
    {
        if (isStop) { return; }
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
            // ���� ����� ���� �غ� �Ǿ��ְ�, ������ �ֺ��� �ε��� ������ ���� ���
            // index�� �ٲٰ� ���� ����� ȣ���ϵ��� �Ѵ�.
            if (nextBehaviour && isBehaviour)
            {
                // �Ҽ��������� ������ ���ֱ� ���� ���� �����ǰ��� �ݿø��Ͽ� ������ �ٲ۴�.
                trans.position = new Vector3((float)System.Math.Round(trans.position.x, 1), (float)System.Math.Round(trans.position.y, 1));
                // ���������� �������� �����̸�, ��� ����Ʈ�� �ε����� 1 �ø���, �� �ݴ�� 1 ������.
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
                // ��� ����Ʈ �ε����� �ٲ����� ���� ����� ���� �� ���� ���·� �ٲ۴�.
                nextBehaviour = false;
            }
        }
        // ����Ȯ���� �ҽɰ˹� ���¿��� �ٽ� ���ο� ���·� �ʱ�ȭ�����ش�.
        else if (policeState == PoliceState.STOP)
        {
            if (Random.Range(0,10000) < 10) { InitState(true); }
        }
    }
    // �浹�ϰ� �Ǵ��� ���ο� ���� ���� �ٲ��ش�.
    public void SetIsBehaviour(bool bo)
    {
        isBehaviour = bo;
    }
    /// <summary>
    /// �������� ���� ��ȣ�� �����´�. ������ȣ�� �������鳢�� �켱������ ���ϴµ� ����Ѵ�.
    /// </summary>
    public int GetPriorityCode()
    {
        return policeCarCode;
    }
    /// <summary>
    /// �������� ���¸� �ٲ۴�. ���¸� �ʱ�ȭ������ �ʰ�,
    /// �ʿ���� �ݶ��̴��� InitState�Լ��� ���ְ�, �ʿ��� �� ���ش�.
    /// </summary>
    /// <param name="policeState"></param>
    public void SetPoliceState(PoliceState policeState)
    {
        this.policeState = policeState;
        InitState(false);
    }
    /// <summary>
    /// �ҽɰ˹��� ���°� ������ �������� ���¸� �̵� ���·� �ٲ��ش�. �� �ܿ�
    /// �÷��̾� ������̳�, �ҽɰ˹����� ���°� �ƴ��� ��Ÿ���� ���� �������� �ٲ��ش�.
    /// </summary>
    public void EndConversation()
	{
        playerMove.Stop = false;
        IsInspecting = false;
        this.policeState = PoliceState.MOVING;
        InitState(false);
	}
    public void SetIInspectingPanelControl(IConversationPanelControl iInspectingPanelControl)
    {
        stopCheckColObj.GetComponent<StopPoliceCarCollisionCheck>().SetIInspectingPanelControl(iInspectingPanelControl);
    }
    public Rigidbody2D GetRigidBody2D()
	{
        return rigid2D;
	}
    public float GetSpeed()
	{
        return Speed;
	}
    public PoliceState GetPoliceState()
	{
        return policeState;
	}
}
