using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�Ѽ�ȣ �ۼ�
public class Fade : MonoBehaviour
{
    public static Fade Instance = null;

    public int AlphaTerm = 5;

    private UnityEngine.UI.Image img;

    private string loadSceneName;
    private int alpha = 255;
    private bool inOut = true;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null) //instance�� null. ��, �ý��ۻ� �����ϰ� ���� ������
        {
            Instance = this; //���ڽ��� instance�� �־��ݴϴ�.
            DontDestroyOnLoad(gameObject); //OnLoad(���� �ε� �Ǿ�����) �ڽ��� �ı����� �ʰ� ����
        }
        else
        {
            if (Instance != this) //instance�� ���� �ƴ϶�� �̹� instance�� �ϳ� �����ϰ� �ִٴ� �ǹ�
            {
                Destroy(this.gameObject); //�� �̻� �����ϸ� �ȵǴ� ��ü�̴� ��� AWake�� �ڽ��� ����
            }
        }
        img = this.gameObject.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>();
        this.gameObject.SetActive(false);
    }
	private void OnEnable()
	{
        alpha = 0;
	}
    public void SetLoadSceneName(string str)
	{
        loadSceneName = str;
	}
    // Update is called once per frame
    void Update()
    {
        if (inOut)
        {
            alpha += AlphaTerm;
            img.color = new Color(0f, 0f, 0f, alpha / 255f);

            if (alpha >= 255)
            {
                inOut = false;
                LoadScene.Instance.LoadS(loadSceneName);
            }
        }
        else
		{
            alpha -= AlphaTerm;
            img.color = new Color(0f, 0f, 0f, alpha / 255f);

            if (alpha <= 0)
			{
                inOut = true;
                this.gameObject.SetActive(false);
			}
        }
    }
}
