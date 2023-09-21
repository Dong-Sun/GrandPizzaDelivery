using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��Ʈ ���� ���� Ŭ����
/// </summary>
public class NoteEditor : MonoBehaviour
{
    public Button FrontButton;      // �ǰ��� ��ư
    public Button BackButton;       // ���� ��ư
    public AudioSource BgSound;     // ��� ����

    private decimal calculator;     // ��Ʈ �迭 �ε��� ����
    private RhythmManager manager;

    private void Start()
    {
        manager = RhythmManager.Instance;
    }

    void Update()
    {
        AddNote();
        RemoveNote();
        Wind();
        SetPitch();
        Playing();
    }
    public void Front(int second)
    {
        BgSound.time = Mathf.Clamp(BgSound.time - second, 0f, BgSound.clip.length);
    }
    public void Back(int second)
    {
        BgSound.time = Mathf.Clamp(BgSound.time + second, 0f, (int)BgSound.clip.length);
    }
    public void Play()
    {
        if (!BgSound.isPlaying)
            BgSound.Play();
    }
    public void Pause()
    {
        BgSound.Pause();
    }
    public void Stop()
    {
        BgSound.Stop();
    }

    /// <summary>
    /// ��Ʈ �߰�
    /// </summary>
    private void AddNote()
    {
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.D))
        {
            calculator = (manager.CurrentTime - (decimal)manager.Data.Sync) / NoteSpawner.BitSlice;
            int index = calculator % NoteSpawner.BitSlice < NoteSpawner.BitSlice / 2 ? (int)calculator : (int)calculator + 1;
            if (!manager.Data.IsNote.ContainsKey(index))
                manager.Data.IsNote.Add(index, index * (float)NoteSpawner.BitSlice);
        }
    }

    /// <summary>
    /// ��Ʈ ����
    /// </summary>
    private void RemoveNote()
    {
        if (Input.GetKey(KeyCode.V))
        {
            calculator = (manager.CurrentTime - (decimal)manager.Data.Sync) / NoteSpawner.BitSlice;
            int index = calculator % NoteSpawner.BitSlice < NoteSpawner.BitSlice / 2 ? (int)calculator : (int)calculator + 1;
            if (manager.Data.IsNote.ContainsKey(index))
            {
                if (NoteSpawner.NoteLoad.Count > 0)
                    NoteSpawner.NoteClear();
                manager.Data.IsNote.Remove(index);
            }
        }
    }

    /// <summary>
    /// �� ����
    /// </summary>
    private void Wind()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            FrontButton.onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            BackButton.onClick.Invoke();
        }
    }

    /// <summary>
    /// �� ���
    /// </summary>
    private void SetPitch()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            BgSound.pitch = Mathf.Clamp(BgSound.pitch + 0.01f, 0f, 2f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            BgSound.pitch = Mathf.Clamp(BgSound.pitch - 0.01f, 0f, 2f);
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            BgSound.pitch = 1;
        }
    }

    /// <summary>
    /// �뷡 ���
    /// </summary>
    private void Playing()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (BgSound.isPlaying)
                Pause();
            else
                Play();
        }
    }
}
