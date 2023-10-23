using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ÿ�̸� UI ǥ�� Ŭ����
/// </summary>
public class RhythmProgress : MonoBehaviour
{
    public Image pizza;      // ���� �̹���
    private RhythmManager manager;

    private void Start()
    {
        manager = RhythmManager.Instance;
    }

    void Update()
    {
        pizza.fillAmount = (float)manager.CurrentTime / manager.Data.Length;
    }
}
