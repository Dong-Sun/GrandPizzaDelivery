using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConversationNS;
public class PoliceInspecting : Conversation
{
    public PoliceInspecting()
    {
        NpcTextStrArr = new string[24]
        {
            "��� ������ ����?",   // 0
            "�ű� ���� �ֽǱ�?",   // 1
            "�̰� ���� ������?",   // 2
            "�������ΰ���?",  // 3
            "�����Ѵ�.",    // 4
            "���ڹ�޺� ������...�ű� ���� �������.",  // 5
            "��� �˹��� �ؾ߰ھ�.", // 6
            "���� ������ ���°� ������? �˹��� �ؾ߰ھ�.",    // 7
            "�ѹ� ���캸����. �ҹ� ���İ��� �� �����.", // 8
            "(���� �ֻ��� 7 �̻�) ���� �̻��� �Ŷ� ���������? �̺���, �� ���ÿ� �������� �� �� ���̰�, \n����� �������� �ֹ� ������ ������ �ǻ簡 �־��. �׷��� �ҽÿ� �̷� ���� ���� �����ּ���.",   // 9
            "(20000���� �ش�.)��...�׸� ������ �ǰڽ��ϱ�?",   // 10
            "�̷� ���ξ��� �����ݾ� ! �̷� �ҹ������� �����ϰ� �ִٴ�...�̰� �м���. \n������ ���� ���Կ� �������״� �������� �̷� �� ������ !",  // 11
            "����. ������ ����.",  // 12
            "Ȯ���� �� ���� �±�. ������ ����.", // 13
            "���� �ʹ� ���. �ܸ����� �� ���̳� �������.",   // 14
            "����. �̹��� �������ָ�. �������� �����ϵ���",    // 15
            "��..�� ���� �� ������� ������.",  // 16
            "(20000���� �ش�.) �׷��� ����. ��� �ȵǰڽ��ϱ�?",    // 17
            "������ �༮. ������ �� �� ���� ����.",   // 18
            "��������.", // 19
            "�ð��� �����߱�. ���� ü���ϰھ� !",  // 20
            "(����.)", // 21
            "(�˹��� �޴´�.)",  // 22
            "(����ϰ� ����.)"    // 23
        };

        TextList = new List<TextNodeC>();
        InitTextList();
    }

    private void InitTextList()
    {
        startText = new int[3] { 0, 1, 2 };

        nowTextNum = -1; nextTextNum = new int[2] { 3, 4 }; nextTextIsAble = new bool[2] { true, true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[3] { 0, 1, 2} ),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 } )
        };
        AddTextList();
        nowTextNum = 3; nextTextNum = new int[4] { 8, 9, 10, 4}; nextTextIsAble = new bool[4] { true, true, false, true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[3] { 5, 6, 7 } ),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 400 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 })
        };
        AddTextList();
        nowTextNum = 4; nextTextNum = new int[1] { 19 }; nextTextIsAble = new bool[1] { true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 20 } ),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 } )
        };
        AddTextList();
        nowTextNum = 8; nextTextNum = new int[1] { 21 }; nextTextIsAble = new bool[1] { true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 12 } ),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 0 } )
        };
        AddTextList();
        nowTextNum = 8; nextTextNum = new int[1] { 23 }; nextTextIsAble = new bool[1] { true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 11 } ),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 2 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } )
        };
        AddTextList();
        nowTextNum = 9; nextTextNum = new int[1] { 21 }; nextTextIsAble = new bool[1] { true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 13 } ),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 } )
        };
        AddTextList();
        nowTextNum = 9; nextTextNum = new int[1] { 22 }; nextTextIsAble = new bool[1] { true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 14 } ),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 1 } )
        };
        AddTextList();
        nowTextNum = 10; nextTextNum = new int[1] { 21 }; nextTextIsAble = new bool[1] { true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 15 } ),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } )
        };
        AddTextList();
        nowTextNum = 10; nextTextNum = new int[2] { 17, 18 }; nextTextIsAble = new bool[2] { true, true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 16 } ),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } )
        };
        AddTextList();
        nowTextNum = 17; nextTextNum = new int[1] { 21 }; nextTextIsAble = new bool[1] { true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 15 } ),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 0 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } )
        };
        nowTextNum = 17; nextTextNum = new int[2] { 17, 18 }; nextTextIsAble = new bool[2] { true, true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 16 } ),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 200 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 1 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 3 } )
        };
        AddTextList();
        nowTextNum = 18; nextTextNum = new int[1] { 19 }; nextTextIsAble = new bool[2] { true, true };
        methodSArr = new MethodS[4]
        {
            new MethodS(MethodEnum.SETRANDNPCTEXT, new int[1] { 20 } ),
            new MethodS(MethodEnum.SETSIZECONTENTS, new int[2] { 1, 100 } ),
            new MethodS(MethodEnum.CHANGENPCIMAGE, new int[1] { 3 } ),
            new MethodS(MethodEnum.CHANGEPLAYERIMAGE, new int[1] { 2 } )
        };
        AddTextList();
        nowTextNum = 19; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[2] { true, true };
        methodSArr = new MethodS[2]
        {
            new MethodS(MethodEnum.ENDPANEL, new int[1] { -1 } ),
            new MethodS(MethodEnum.SPAWNPOLICE, new int[1] { 4 } )
        };
        AddTextList();
        nowTextNum = 21; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { false };
        methodSArr = new MethodS[1]
        {
            new MethodS(MethodEnum.ENDPANEL, new int[1] { -1 } )
        };
        AddTextList();
        nowTextNum = 23; nextTextNum = new int[1] { -1 }; nextTextIsAble = new bool[1] { false };
        methodSArr = new MethodS[1]
        {
            new MethodS(MethodEnum.ENDPANEL, new int[1] { -1 } )
        };
        AddTextList();
    }

}
