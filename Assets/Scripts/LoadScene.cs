using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DayNS;

// �Ѽ�ȣ �ۼ�
public class LoadScene : MonoBehaviour
{
    /* // �̱��� //
* instance��� ������ static���� ������ �Ͽ� �ٸ� ������Ʈ ���� ��ũ��Ʈ������ instance�� �ҷ��� �� �ְ� �մϴ� 
*/
    public static LoadScene Instance = null;

    private void Awake()
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
    }
    /// <summary>
    /// ���̵� ������ �����ְ� ���� �ҷ��ɴϴ�.
    /// </summary>
    /// <param name="str">�ҷ��� ���� �̸��Դϴ�.</param>
    public void ActiveTrueFade(string str)
	{
        Fade.Instance.gameObject.SetActive(true);
        Fade.Instance.SetLoadSceneName(str);
	}
    public void LoadNextDay(string str)
	{
        if (Constant.NowDay != DayEnum.SUNDAY)
        {
            Constant.NowDay++;
        }
        else
		{
            Constant.NowDay = DayEnum.MONDAY;
		}
        Constant.NowDate++;
        ActiveTrueFade("InGameScene");
	}
    public void LoadS(string str)
	{
		SceneManager.LoadScene(str);
	}
	public void LoadRhythm()
	{
		if (Constant.ChoiceIngredientList.Count > 0)
		{
            ActiveTrueFade("SelectScene");
		}
	}
    public void LoadPrologueToInGameScene()
    {
        Constant.isStartGame = true;
        ActiveTrueFade("InGameScene");
    }

    public void LoadPizzaMenu()
	{
        Constant.IsMakePizza = true;
        Constant.DevelopPizza.Add(new Pizza("Pizza" + System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"), Random.Range(20,80) + 20, Constant.ProductionCost
            ,Random.Range(5000,30000) + 10000,Constant.PizzaAttractiveness, Constant.ingreds, Constant.TotalDeclineAt));
        ActiveTrueFade("InGameScene");
	}
}
