using System.Collections;
using System.Collections.Generic;
using UnityEngine;
class AudioData
{
    public string Name;         // �� �̸�
    public decimal BPM;         // �� BPM
    public decimal Length;      // �� ����
    public bool[] IsNote;     // ��Ʈ ���� �ð�

    public AudioData()
    {
        IsNote = new bool[100];
        for (int i = 0; i < IsNote.Length; i++)
            IsNote[i] = true;
    }
}
