public class TotalData
{
    public int TotalMoney;

    public TotalData()
    {
        TotalMoney = 0;
    }

    public TotalData(TotalData data)
    {
        // �ɹ� ���� �ʱ�ȭ
        TotalMoney = data.TotalMoney;
    }

    public TotalData(int totalMoney)
    {
        TotalMoney = totalMoney;
    }
}
