using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolicePathNS;

// �Ѽ�ȣ �ۼ�

interface IAddress
{
    public void InitAddress(int number);
    public int GetAddress();
}

interface IBuilding
{
    public bool GetIsPoliceCar();
    public void SetIsPoliceCar(bool b);
    public Vector2 GetpoliceCarDis();
    public Vector2 GetBuildingPos();
    public List<PolicePath> GetPolicePath();
}

interface IPoliceCar
{
    public void InitPoliceCarPath(List<PolicePath> policePathList);
}
// ������ ��� ���� �������̽�
public interface IPoliceCarControl
{
    public void SetIsBehaviour(bool bo);
    public int GetPoliceCarCode();
}
