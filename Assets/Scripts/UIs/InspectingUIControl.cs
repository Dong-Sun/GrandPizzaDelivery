using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �Ѽ�ȣ �ۼ�

public class InspectingUIControl : MonoBehaviour
{
    [SerializeField] private Text policeText;

    private string[] inspectingPoliceTextStart = new string[3] {
        "��� ������ ����?",
        "��� �ҽɰ˹��� �ְڽ��ϴ�. �׷��� �μ� ����.",
        "���� ������ ���� �� ������ ��.",
    };

    private void OnEnable()
    {
        policeText.text = "";
    }
}
