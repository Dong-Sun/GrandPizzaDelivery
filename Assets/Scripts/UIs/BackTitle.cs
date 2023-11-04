using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PizzaNS;
using Inventory;
using UnityEngine.SceneManagement;
using ClerkNS;
using StoreNS;
using DayNS;

public class BackTitle : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;

    public void OnClickTitle()
    {
        Destroystatic("GameManager");
        PlayerStat.HP = PlayerStat.MaxHP;
        SceneManager.LoadScene("MainPage");
        Destroystatic("RhythmManager");
        // -------------------------------------static -----------------------------------------------------//
        Constant.BorrowMoneyDate = new Dictionary<int, Dictionary<int, int>>();
        Constant.PayMoneyDate = new Dictionary<int, Dictionary<int, int>>();
        Constant.NowDay = DayEnum.MONDAY;
        Constant.NowDate = 1;
        Constant.DiceBonus = 0;
        Constant.ChoiceIngredientList = new List<int>();
        Constant.ingreds = new List<Ingredient>();
        Constant.IngredientsArray = new string[16, 5]
    {
        {"0","-1","-1","-1" ,"����"},	// ����
		{"1","25","3","150","�丶��" },	// �丶��
		{"2","30","2","160","ġ��"},	// ġ��
		{"3","15","2","80","����" },	// ����
		{"4","20","1","120","����" },	// ����
		{"5","45","7","500","������" },	// ������
		{"6","27","3","140","������" },	// ������
		{"7","40","5","320","�Ҷ��Ǵ�" },	// �Ҷ��Ǵ�
		{"8","65","12","960","�߰��" },	// �߰��
		{"9","78","20","1350","�Ұ��" },    // �Ұ��
		{"10","32","4","150","���" }, // ���
		{"11","27","2","200","���" }, // ���
		{"12","17","1","100","����" },    // ����
		{"13", "34", "7", "230", "����" },    // ����
		{"14", "28", "5", "170", "����" },    // ����
		{"15", "22", "1", "210", "����" },	// ����
	};
        Constant.UsableIngredient = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        Constant.DevelopPizza = new List<Pizza>();
        Constant.menuDateDic = new Dictionary<Pizza, int>();
        Constant.IsMakePizza = false;
        Constant.isStartGame = false;
        Constant.StopTime = false;
        Constant.PineappleCount = 0;
        Constant.PineapplePizza = new Pizza("PineapplePizza", 100, 0, 2000000, 99999, new List<Ingredient>() { Ingredient.TOMATO, Ingredient.CHEESE, Ingredient.PINEAPPLE }, 0, 100, 0);
        Constant.OneTime = new WaitForSeconds(0.02f);
        Constant.ClerkList = new List<ClerkC>() { new ClerkC(47, Tier.THREE, Tier.ONE, Tier.FOUR, 0, 20000, "�����̾�", null, 0) };
        Constant.DiceItem = new ItemS[10]
    {
        new ItemS(ItemType.DICE, 2, "�� �ֻ���", "���� ���� �ֻ�����. \n �ֻ��� �� ���� 0,1,2,3,4,5 �� ��¡�Ѵ�.", 0),
        new ItemS(ItemType.DICE, 2, "�ݼ� �ֻ���", "�ݼ����� ���� �ֻ�����. \n �ֻ��� �� ���� 3,4,5,6,7,8 �� ��¡�Ѵ�.", 1),
        new ItemS(ItemType.DICE, 2, "8�� �ֻ���", "8������ �� �ֻ�����. \n �ֻ��� �� ���� 2,2,3,3,4,4,5,6 �� ��¡�Ѵ�.", 2),
        new ItemS(ItemType.DICE, 2, "12�� �ֻ���", "12������ �� �ֻ�����. \n �ֻ��� �� ���� \n1,2,3,3,4,4,5,5,6,7,8,9 �� ��¡�Ѵ�.", 3),
        new ItemS(ItemType.DICE, 2, "¦�� �ֻ���", "¦���� �����ϴ� �ֻ�����. \n �ֻ��� �� ���� 2,2,4,4,6,6 �� ��¡�Ѵ�.", 4),
        new ItemS(ItemType.DICE, 2, "Ȧ�� �ֻ���", "Ȧ���� �����ϴ� �ֻ�����. \n �ֻ��� �� ���� 1,1,3,3,5,5 �� ��¡�Ѵ�.", 5),
        new ItemS(ItemType.DICE, 2, "�Ҽ� �ֻ���", "�Ҽ��� �����ϴ� �ֻ�����. \n �ֻ��� �� ���� 2,3,5,7,11,13 �� ��¡�Ѵ�.", 6),
        new ItemS(ItemType.DICE, 2, "��� �ֻ���", "���ڰ� �� ���� �ֻ�����. \n �ֻ��� �� ���� 1,1,1,1,1,15 �� ��¡�Ѵ�.", 7),
        new ItemS(ItemType.DICE, 2, "���� �ֻ���", "������ ���� �ֻ�����. \n �ֻ��� �� ���� 2,3,4,5,6,7 �� ��¡�Ѵ�.", 8),
        new ItemS(ItemType.DICE, 2, "�ö�ƽ �ֻ���", "�ö�ƽ���� ���� �ֻ�����. \n �ֻ��� �� ���� 1,2,3,4,5,6 �� ��¡�Ѵ�.", 9)
    };
        Constant.PlayerItemDIc = new Dictionary<ItemS, int>() { { Constant.DiceItem[9], 2 } };
        Constant.DiceInfo = new DiceS[10]
    {
        new DiceS(6, new int[6] { 0, 1, 2, 3, 4, 5} , "UI/RubberDice_80_80"),
        new DiceS(6, new int[6] { 3, 4, 5, 6, 7, 8} , "UI/MetalDice_80_80"),
        new DiceS(8, new int[8] { 2, 2, 3, 3, 4, 4, 5, 6} , "UI/EightDice_80_80"),
        new DiceS(12, new int[12] { 1, 2, 3, 3, 4, 4, 5, 5, 6, 7, 8, 9} , "UI/TwelveDice_80_80"),
        new DiceS(6, new int[6] { 2, 2, 4, 4, 6, 6} , "UI/EvenDice_80_80"),
        new DiceS(6, new int[6] { 1, 1, 3, 3, 5, 5} , "UI/OddDice_80_80"),
        new DiceS(6, new int[6] { 2, 3, 5, 7, 11, 13} , "UI/PrimeDice_80_80"),
        new DiceS(6, new int[6] { 1, 1, 1, 1, 1, 15} , "UI/TwoDice_80_80"),
        new DiceS(6, new int[6] { 2, 3, 4, 5, 6, 7} , "UI/WoodDice_80_80"),
        new DiceS(6, new int[6] { 1, 2, 3, 4, 5, 6} , "UI/MiniDice_80_80")
    };
        Constant.nowDice = new int[2] { 9, 9 };
        Constant.GunItem = new ItemS[8]
    {
        new ItemS(ItemType.GUN, 1, "Glick 19","����ӵ� : �߰� \n����� : ���� \n��ź�� : 15\n�������ð� : 3", 0),
        new ItemS(ItemType.GUN, 1, "S&U m500","����ӵ� : ���� \n����� : ���� \n��ź�� : 6\n�������ð� : 3", 1),
        new ItemS(ItemType.GUN, 1, "Mi1911","����ӵ� : ���ݴ��� \n����� : �ſ���� \n��ź�� : 7\n�������ð� : 3", 2),
        new ItemS(ItemType.GUN, 1, "MiP9","����ӵ� : �ſ���� \n����� : ���� \n��ź�� : 30\n�������ð� : 3", 3),
        new ItemS(ItemType.GUN, 1, "MiPX","����ӵ� : ���� \n����� : �߰� \n��ź�� : 30\n�������ð� : 3", 4),
        new ItemS(ItemType.GUN, 1, "Pi90","����ӵ� : ���� \n����� : �߰� \n��ź�� : 50\n�������ð� : 3", 5),
        new ItemS(ItemType.GUN, 1, "Kress Victor","����ӵ� : �ſ�ſ���� \n����� : ���� \n��ź�� : 30\n�������ð� : 3", 6),
        new ItemS(ItemType.GUN, 1, "Thimpson SMG","����ӵ� : ���� \n����� : �ſ���� \n��ź�� : 30\n�������ð� : 3", 7)
    };
        Constant.GunInfo = new GunS[8]
    {
        new GunS(LoadEnum.SEMIAUTO, 0.5f, 20, 100, 15, 3, "UI/Glick19_240_120"),
        new GunS(LoadEnum.MANUAL, 0.1f, 150, 100, 6, 3, "UI/S&Um500_240_120"),
        new GunS(LoadEnum.SEMIAUTO, 0.3f, 10, 100, 7, 3, "UI/Mi1911_240_120"),
        new GunS(LoadEnum.AUTO, 0.8f, 20, 100, 30, 3, "UI/MiP9_240_120"),
        new GunS(LoadEnum.AUTO, 0.65f, 50, 100, 30, 3, "UI/MiPX_240_120"),
        new GunS(LoadEnum.AUTO, 0.65f, 50, 100, 50, 3, "UI/Pi90_240_120"),
        new GunS(LoadEnum.AUTO, 0.9f, 20, 100, 30, 3, "UI/KressVictor_240_120"),
        new GunS(LoadEnum.AUTO, 0.65f, 10, 100, 30, 3, "UI/ThimpsonSMG_240_120")
    };
        Constant.nowGun = new int[1] { -1 };

        House.activeColor = new Color(248 / 255f, 70 / 255f, 6 / 255f);

        StreetLamp.lightOnColor = new Color(255 / 255f, 177 / 255f, 0 / 255f);
        StreetLamp.lightOffColor = Color.black;

        EmployeeFire.WorkingDay = new Dictionary<int, List<ClerkC>>(); ;

        DiceStore.IsOneDayDiceStore = false;
        
        IngredientStore.Contract = 0;
        IngredientStore.Hint = false;
        IngredientStore.OneChance = true;

        IngredientStoreTwo.IsTalk = false;
        IngredientStoreTwo.IsGalicQuest = false;
        IngredientStoreTwo.OneChanceGalicClear = false;
        IngredientStoreTwo.NowDate = 1;
        IngredientStoreTwo.Ingredient = 0;
        IngredientStoreTwo.Discount = 0;
        IngredientStoreTwo.BounsDiscount = 0;
        IngredientStoreTwo.Contract = 0;

        LuckyStore.IsAngry = false;
        LuckyStore.IsLuckyTest = false;
        LuckyStore.ClearGalicQuest = false;
        LuckyStore.ClearSonQuest = false;
        LuckyStore.BigDicePlus = false;
        LuckyStore.BigDiceMinus = false;
        LuckyStore.SmallDicePlus = false;
        LuckyStore.SmallDiceMinus = false;
        LuckyStore.AngryDate = 0;
        LuckyStore.NowDate = 1;

        MoneyStore.IsTalk = false;
        MoneyStore.StartSonQuest = false;
        MoneyStore.OneChanceClearSon = false;
        MoneyStore.IsTalkOneChanceDiscount = false;

        MoneyStore.SumBorrow = 0;
        MoneyStore.PlusMoney = 1.1f;
        MoneyStore.NowDate = 1;
        MoneyStore.ClearMoney = 0;

        PineAppleStore.isFirstTime = true;
        PineAppleStore.isFineapple = true;

        PineAppleStoreTwo.isPineapple = true;
        PineAppleStoreTwo.isContract = false;
        PineAppleStoreTwo.isMeet = false;

        PizzaMenuUI.nowDate = 0;


        // -------------------------------------------------------------------------------------------------//
    }
    private void Destroystatic(string gameOB)
    {
        GameObject temporary = GameObject.Find(gameOB);
        if (temporary != null)
        {
            Destroy(temporary);
        }
    }

    public void OnClickGameQuit()
    {
        Application.Quit();
    }
}
