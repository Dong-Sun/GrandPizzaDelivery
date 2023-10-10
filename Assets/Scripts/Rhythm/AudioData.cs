using System.Collections.Generic;

public enum NoteType { None = 0, Normal, Hold }

/// <summary>
/// �� �����͸� ��� �ִ� Ŭ����
/// </summary>
public class AudioData
{
    public string Name;                         // �� �̸�
    public float BPM;                           // �� BPM
    public float Length;                        // �� ����
    public float Sync;                          // �� ��ũ
    public SortedList<int, NoteType> Notes;       // ��Ʈ ���� �ð�

    public AudioData()
    {
        Name = "no title";
        BPM = 60f;
        Length = 0f;
        Sync = 0f;
        Notes = new SortedList<int, NoteType>();
    }

    public AudioData(string fileName)
    {
        AudioData data = JsonManager<AudioData>.Load(fileName);
        if (data == null)
        {
            data = new AudioData();
        }
        Name = data.Name;
        BPM = data.BPM;
        Length = data.Length;
        Sync = data.Sync;
        Notes = data.Notes;
    }
}