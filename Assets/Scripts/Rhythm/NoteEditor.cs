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
    public Transform Line1;
    public Transform Line2;

    private RhythmManager manager;
    private decimal calculator;     // ��Ʈ �迭 �ε��� ����
    private int index;

    private void Start()
    {
        manager = RhythmManager.Instance;
    }

    void Update()
    {
        if (manager.CurrentTime < 0m)
            return;

        AddNote();
        RemoveNote();
        Wind();
        SetPitch();
        Playing();
        MouseInput();
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
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKey(KeyCode.W))
        {
            calculator = manager.CurrentTime / NoteSpawner.BitSlice;
            calculator = decimal.Round(calculator);
            if (!manager.Data.NoteLines[0].ContainsKey((int)calculator))
            {
                manager.Data.NoteLines[0].Add((int)calculator, (Input.GetKeyDown(KeyCode.Q)) ? NoteType.Normal : NoteType.Hold);
                Debug.Log("AddNumber : " + calculator);
            }
        }
        if (Input.GetKeyDown(KeyCode.O) || Input.GetKey(KeyCode.P))
        {
            calculator = manager.CurrentTime / NoteSpawner.BitSlice;
            calculator = decimal.Round(calculator);
            if (!manager.Data.NoteLines[1].ContainsKey((int)calculator))
            {
                manager.Data.NoteLines[1].Add((int)calculator, (Input.GetKeyDown(KeyCode.O)) ? NoteType.Normal : NoteType.Hold);
                Debug.Log("AddNumber : " + calculator);
            }
        }
    }

    /// <summary>
    /// ��Ʈ ����
    /// </summary>
    private void RemoveNote()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            calculator = manager.CurrentTime / NoteSpawner.BitSlice;
            calculator = decimal.Round(calculator);
            if (manager.Data.NoteLines[0].ContainsKey((int)calculator))
            {
                manager.Data.NoteLines[0].Remove((int)calculator);
                Debug.Log("RemoveNumber : " + calculator);
            }
        }
        if (Input.GetKey(KeyCode.M))
        {
            calculator = manager.CurrentTime / NoteSpawner.BitSlice;
            calculator = decimal.Round(calculator);
            if (manager.Data.NoteLines[1].ContainsKey((int)calculator))
            {
                manager.Data.NoteLines[1].Remove((int)calculator);
                Debug.Log("RemoveNumber : " + calculator);
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

    private void MouseInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int line;
            if (Mathf.Abs(Line1.position.y - vector.y) < 0.4f)
                line = 0;
            else if (Mathf.Abs(Line2.position.y - vector.y) < 0.4f)
                line = 1;
            else
                return;
            
            float timing = (8 + vector.x) / (manager.Speed * 5f);
            calculator = (decimal)timing / NoteSpawner.BitSlice + manager.CurrentTime / NoteSpawner.BitSlice;
            calculator = decimal.Round(calculator);

            if (!manager.Data.NoteLines[line].ContainsKey((int)calculator))
            {
                manager.Data.NoteLines[line].Add((int)calculator, (Input.GetKeyDown(KeyCode.Mouse0)) ? NoteType.Normal : NoteType.Hold);
                Debug.Log("AddNumber : " + calculator);
            }
            else
            {
                manager.Data.NoteLines[line].Remove((int)calculator);
                Debug.Log("RemoveNumber : " + calculator);
            }
        }
    }
}