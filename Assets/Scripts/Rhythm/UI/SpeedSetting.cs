using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������ӿ� ���Ǵ� ������ �������ִ� UI Ŭ����
/// </summary>
public class SpeedSetting : MonoBehaviour
{
    public Slider SpeedSlider;

    private RhythmManager manager;          // ���� �Ŵ��� ĳ��

    private void Start()
    {
        // ���� �Ŵ��� ĳ��
        manager = RhythmManager.Instance;

        SpeedSlider.value = manager.Speed / 5f;
        SpeedSlider.onValueChanged.AddListener(SetSpeed);
    }

    /// <summary>
    /// Slider�� �ӵ��� �����ϱ� ���� �Լ�
    /// </summary>
    /// <param name="volume">�����̴� ��</param>
    public void SetSpeed(float volume)
    {
        manager.Speed = volume * 5f;
    }
}
