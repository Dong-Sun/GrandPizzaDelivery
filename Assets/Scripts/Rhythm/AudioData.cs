using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioData
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

    public AudioData(string path)
    {
        AudioData data = JsonManager<AudioData>.Load(path);
        Name = data.Name;
        BPM = data.BPM;
        Length = data.Length;
        IsNote = data.IsNote;
    }
    public AudioData(AudioData data)
    {
        Name = data.Name;
        BPM = data.BPM;
        Length = data.Length;
        IsNote = data.IsNote;
    }
}
