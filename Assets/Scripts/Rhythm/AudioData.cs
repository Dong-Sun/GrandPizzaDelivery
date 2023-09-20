public class AudioData
{
    public string Name;         // �� �̸�
    public float BPM;           // �� BPM
    public float Length;        // �� ����
    public float Sync;          // �� ��ũ
    public bool[] IsNote;       // ��Ʈ ���� �ð�

    public AudioData()
    {
        Name = "no title";
        BPM = 60f;
        Length = 0f;
        Sync = 0f;
        IsNote = new bool[5000];
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
        IsNote = data.IsNote;
    }
}