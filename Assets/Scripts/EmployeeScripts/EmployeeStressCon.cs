using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeStressCon : MonoBehaviour
{
    [SerializeField] GameObject NoticeWin;
    [SerializeField] bool isMorning = false;

    private void Update()
    {
        if (isMorning)
        {
            PayStress();
        }
    }

    void PayStress()
    {
        string Message = null;

        for (int i = 0; i < Constant.ClerkList.Count; i++)
        {
            if(Constant.ClerkList[i].Pay < Constant.ClerkList[i].MinPayScale)
            {
                if (Constant.ClerkList[i].Stress < Constant.ClerkList[i].MaxStress)
                {
                    Constant.ClerkList[i].Stress += (Constant.ClerkList[i].MinPayScale - Constant.ClerkList[i].Pay);
                }
                else
                {
                    if(Message != null)
                    {
                        Message += " ,";
                    }

                    Message += Constant.ClerkList[i].Name;

                    Constant.ClerkList.RemoveAt(i); 
                }
            }
            else if(Constant.ClerkList[i].Pay > Constant.ClerkList[i].MaxPayScale)
            {
                if (Constant.ClerkList[i].Stress > 0)
                {
                    Constant.ClerkList[i].Stress -= (Constant.ClerkList[i].Pay - Constant.ClerkList[i].MaxPayScale);
                }
            }
        }

        if(Message != null) 
        {
            Message += "�� ����� �δ��� ��츦 ���� ���ϰ� �������ϴ�.";

            NoticeMessage(Message);
        }
    }

    void NoticeMessage(string Message)
    {
        NoticeWin.SetActive(true);

        NoticeWin.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = Message;
    }
}