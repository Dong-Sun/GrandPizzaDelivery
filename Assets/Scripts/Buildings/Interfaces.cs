using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoliceNS.PolicePathNS;
using PoliceNS.PoliceStateNS;
using BuildingAddressNS;
using BuildingNS.HouseNS;
using StoreNS;
// �Ѽ�ȣ �ۼ�
/// <summary>
/// �ǹ�, �� �ּҿ� ���õ� �������̽�
/// </summary>
public interface IAddress
{
    public void InitAddress(int number, List<AddressS> addressSList);
    public int GetAddress();
    public void SetIMap(IMap iMap);
    public void SetIDeliveryPanelControl(IDeliveryPanelControl iDeliveryPanelControl);
    public void SetIHouseActiveUIControl(IHouseActiveUIControl iHouseActiveControl);
}
/// <summary>
/// �ǹ��� �������� �Ҵ��ϰ� ��θ� �����ϱ� ���� �������̽�
/// </summary>
public interface IBuilding
{
    public bool GetIsPoliceCar();
    public void SetIsPoliceCar(bool b);
    public Vector2 GetpoliceCarDis();
    public Vector2 GetBuildingPos();
    public List<PolicePath> GetPolicePath();
}
/// <summary>
/// �������� �Ӽ��� ���õ� �������̽�
/// </summary>
public interface IPoliceCar
{
    public void InitPoliceCarPath(List<PolicePath> policePathList);
    public void SetIInspectingPanelControl(IConversationPanelControl iInspectingPanelControl);
    public void SetPlayerMove(PlayerMove playerMove);
    public Rigidbody2D GetRigidBody2D();
    public float GetSpeed();
    public PoliceState GetPoliceState();
    public void SetMap(IStop iStop);
    public void SetIsStop(bool bo);

    public void SetBanana(GameObject banana);

}
/// <summary>
/// ������ ��� ���� �������̽�
/// </summary>
public interface IMovingPoliceCarControl
{
    public void SetIsBehaviour(bool bo);
    //public int GetPoliceCarCode();
}
/// <summary>
/// ������ �켱���� ��ȣ�� ���õ� �������̽�
/// </summary>
public interface IPriorityCode
{
    public int GetPriorityCode();
}

/// <summary>
/// �������� ���¿� ���� �ҽɰ˹� ���θ� ������ ���� �������̽�
/// </summary>
public interface IInspectingPoliceCarControl
{
    public void SetPoliceState(PoliceState policeState);
}
/// <summary>
/// ��ȭâ UI�� �����ϱ� ���� �������̽�
/// </summary>
public interface IConversationPanelControl
{
    public void ControlConversationUI(bool isOn, IEndConversation iEndInspecting, int type);
}
/// <summary>
/// ��� �������θ� ������ �������̽�
/// </summary>
public interface IDeliveryPanelControl
{
    public void ControlDeliveryUI(bool isOn);
    public void SetIHouseDeliveryUI(IHouse iHouse);
}
/// <summary>
///  �� Ÿ�Կ� ���� �� ��ó���� ���� �� �ִ� ����Ű ���� �г��� ���ִ� �������̽�
/// </summary>
public interface IHouseActiveUIControl
{
    public void ActiveTrueKeyExplainPanel(bool bo);
    public void SetHouseType(HouseType houseType);

}
/// <summary>
/// ��ȭ ���� �������� ���� ���ִ� �������̽�
/// </summary>
public interface IInspectingUIText
{
    public void ChoiceText(int num);
}
/// <summary>
/// ��ȭâ UI�� ������ �� ������ �ϵ��� �ٷ�� �Լ��� ��� �������̽�
/// </summary>
public interface IEndConversation
{
    public void EndConversation();
}
/// <summary>
/// �� Ÿ���� �ٲ��ְų� ��� ���ο� ���� ���� ���¸� �ٲ��ֱ� ���� �������̽�
/// </summary>
public interface IHouse
{
    public void EnableHouse();
    public void DisableHouse(Pizza pizza);
    public void EndDeliveryDisableHouse();
    public bool GetIsEnable();
    public void SetHouseType(Sprite mark, HouseType houseType);
    public HouseType GetHouseType();
    public Transform GetLocation();

}
/// <summary>
/// Ȱ��ȭ�� �� ��ó�� �Դ��� ������ �׿� ���� ���¸� ��ȭ�ϱ� ���� �������̽�
/// </summary>
public interface IActiveHouse
{ 
    public bool ActiveHouse(bool bo);
    public void IntoHouse(bool bo);
}
/// <summary>
/// �ʿ� �ִ� ��� ���� �ּҸ� �����ϱ� ���� �������̽�
/// </summary>
public interface IMap
{
    public void AddAddress(AddressS addressS);
    public float RemoveAddress(AddressS addressS);
}
/// <summary>
/// ������ ���� ���߰ų�(�� �Ͻ����� ȿ��) �ı��� ������ �����ϱ� ���� �Լ��� ���� �������̽�. 
/// </summary>
public interface IStop
{
    public void StopMap(bool bo);
    public void RemovePoliceList(IPoliceCar iPoliceCar);
}
/// <summary>
/// ���� ��� �� ������ �����ߴ��� �׸��� ������ ����� ������ �����ֱ� ���� �������̽�
/// </summary>
public interface IIngredientSlot
{
    public void IngredientExplain(int ingNum);
    public void ChoiceIngredient(int ingNum, int index);
}
/// <summary>
///  ���� ������ ���� �������̽�
/// </summary>
public interface IAddPizza
{
    public void SetAddPizzaExplain(int num);
    public void SetTemSlotNumber(int num);
}
/// <summary>
/// ���������� ������ ���ڿ� ���� ������ �����ϴ� �������̽�
/// </summary>
public interface IMakingPizzaPanel
{
    public void SetPizza(Pizza pizza);
    public bool ComparePizza(Pizza pizza);
}
/// <summary>
/// ȭ�� ��ܿ� �˶��� ���� �������̽�
/// </summary>
public interface IAlarmMessagePanel
{
    /// <summary>
    /// �˸� â ����ݱ�. �˸�â�� ������ �ؽ�Ʈ�� �����ؾߵ�
    /// </summary>
    /// <param name="isOn"></param>
    /// <param name="text">��� �ؽ�Ʈ�� ���´�.</param>
    public void ControlAlarmMessageUI(bool isOn, string text);
}
/// <summary>
/// �߰� �������� ��ȯ�ϴ� �������̽�
/// </summary>
public interface ISpawnCar
{
    /// <summary>
    /// �߰� �������� count �� ��ŭ ��ȯ�մϴ�.
    /// </summary>
    /// <param name="count">��ȯ�� �߰� �������� ���Դϴ�.</param>
    public void SpawnCar(int count);
}
/// <summary>
/// Ʈ�������� �������� �������̽�
/// </summary>
public interface ISetTransform
{
    public void SetTransform(Transform trans);
}
/// <summary>
/// boolean ���� ��ȯ�ϴ� �������̽�
/// </summary>
public interface IGetBool
{
    public bool GetBool();
}
/// <summary>
/// �߰� ������ ��ó�� ���صǴ� ������Ʈ�� �ִ��� Ȯ���ϱ� ���� �������̽�
/// </summary>
public interface ICheckCol
{
    public void InitNumber(int num, IUpdateCheckList iUpdateCheckList);
}
/// <summary>
/// �߰� ������ ��ó���� Ž���� ���� ���� Ȥ�� ��������� �˸��� �������̽�
/// </summary>
public interface IUpdateCheckList
{
    public void UpdateCheck(int num, bool isAdd);
}
/// <summary>
/// �ֻ��� ������ �ڷ�ƾ�� ���� �������̽�
/// </summary>
public interface ICoroutineDice
{
    public void StartDice(int num);
}
/// <summary>
/// ���� ���� �ʱ�ȭ�ϴ� �������̽�
/// </summary>
public interface IInitStore
{
    public void InitStore(Store store);
    public void OpenStore();
    public void InitSelectItemCnt();
}
/// <summary>
/// ������ �ݴ� �������̽�
/// </summary>
public interface ICloseStore
{
    public void CloseStore(int cost, Dictionary<ItemS, int> dic);
}
/// <summary>
/// ��ȣ���� �ʷϺ����� Ȯ���ϱ� ���� �������̽�
/// </summary>
public interface ICheckIsGreen
{
    public bool CheckIsGreen();
}
/// <summary>
/// ����Ǵ� ���ڸ� ���½�Ű�� ���� �������̽�
/// </summary>
public interface IResetPizzaMaking
{
    public void ResetPizzaMaking();
}