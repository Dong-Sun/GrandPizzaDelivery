public class PlayerData
{
    public int Money;
    public PlayerData()
    {
        Money = 0;
    }
    public PlayerData(PlayerData data)
    {
        // �ɹ� ���� �ʱ�ȭ
        Money = data.Money;
    }
    public PlayerData(int money)
    {
        Money = money;
    }
}
