using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������ӿ� ���Ǵ� ������ �������ִ� UI Ŭ����
/// </summary>
public class RhythmSetting : MonoBehaviour
{
    // Sliders
    public Slider MusicSlider;
    public Slider KeySlider;
    public Slider SpeedSlider;

    // InputFields
    public InputField MusicInputField;
    public InputField KeyInputField;
    public InputField SpeedInputField;

    private RhythmManager manager;          // ���� �Ŵ��� ĳ��

    private void Start()
    {
        // ���� �Ŵ��� ĳ��
        manager = RhythmManager.Instance;

        // �����̴����� ���� �Ŵ����� ������ �ʱ�ȭ
        MusicSlider.value = manager.MusicSound;
        KeySlider.value = manager.KeySound;
        SpeedSlider.value = manager.Speed / 5f;

        // �ؽ�Ʈ���� �Ŵ����� ������ �ʱ�ȭ
        MusicInputField.text = (manager.MusicSound * 10f).ToString("0.0");
        KeyInputField.text = (manager.KeySound * 10f).ToString("0.0");
        SpeedInputField.text = manager.Speed.ToString("0.0");
    }

    /// <summary>
    /// Slider�� ������� �����ϱ� ���� �Լ�
    /// </summary>
    /// <param name="volume">�����̴� ��</param>
    public void SetMusicVolume(float volume)
    {
        manager.MusicSound = volume;
        MusicVolumeSync();
    }
    /// <summary>
    /// InputField�� ������� �����ϱ� ���� �Լ�
    /// </summary>
    /// <param name="volume">�ؽ�Ʈ ��</param>
    public void SetMusicVolume(string volume)
    {
        float value;
        try
        {
            value = float.Parse(volume);
        }
        catch (FormatException e)
        {
            Debug.Log(e);
            MusicVolumeSync();
            return;
        }

        value = Mathf.Clamp(value, 0, 10) / 10f;
        manager.MusicSound = value;
        MusicVolumeSync();
    }
    /// <summary>
    /// ������� ���� Slider�� InputField�� ���� ����ȭ �ϴ� �Լ�
    /// </summary>
    private void MusicVolumeSync()
    {
        MusicSlider.value = manager.MusicSound;
        MusicInputField.text = (manager.MusicSound * 10f).ToString("0.0");
    }

    /// <summary>
    /// Slider�� Ű���� �����ϱ� ���� �Լ�
    /// </summary>
    /// <param name="volume">�����̴� ��</param>
    public void SetKeyVolume(float volume)
    {
        manager.KeySound = volume;
        KeyVolumeSync();
    }
    /// <summary>
    /// InputField�� Ű���� �����ϱ� ���� �Լ�
    /// </summary>
    /// <param name="volume">�ؽ�Ʈ ��</param>
    public void SetKeyVolume(string volume)
    {
        float value;
        try
        {
           value  = float.Parse(volume);
        }
        catch (FormatException e)
        {
            Debug.Log(e);
            KeyVolumeSync();
            return;
        }

        value = Mathf.Clamp(value, 0, 10) / 10f;
        manager.KeySound = value;
        KeyVolumeSync();
    }
    /// <summary>
    /// Ű���� ���� Slider�� InputField�� ���� ����ȭ �ϴ� �Լ�
    /// </summary>
    private void KeyVolumeSync()
    {
        KeySlider.value = manager.KeySound;
        KeyInputField.text = (manager.KeySound * 10f).ToString("0.0");
    }

    /// <summary>
    /// Slider�� �ӵ��� �����ϱ� ���� �Լ�
    /// </summary>
    /// <param name="volume">�����̴� ��</param>
    public void SetSpeed(float volume)
    {
        volume = Mathf.Clamp(volume, 0.02f, 1);
        manager.Speed = volume * 5f;
        SpeedSync();
    }
    /// <summary>
    /// InputField�� �ӵ��� �����ϱ� ���� �Լ�
    /// </summary>
    /// <param name="volume">�ؽ�Ʈ ��</param>
    public void SetSpeed(string volume)
    {
        float value;
        try
        {
            value = float.Parse(volume);
        }
        catch (FormatException e)
        {
            Debug.Log(e);
            SpeedSync();
            return;
        }
        value = Mathf.Clamp(value, 0.1f, 5);
        manager.Speed = value;
        SpeedSync();
    }
    /// <summary>
    /// �ӵ��� ���� Slider�� InputField�� ���� ����ȭ �ϴ� �Լ�
    /// </summary>
    private void SpeedSync()
    {
        SpeedSlider.value = manager.Speed / 5f;
        SpeedInputField.text = manager.Speed.ToString("0.0");
    }
}
