using Newtonsoft.Json;
using System.IO;
using UnityEngine;

/// <summary>
/// Json �����͸� �����ϱ� ���� ���� Ŭ����
/// </summary>
/// <typeparam name="T">�����ϰ��� �ϴ� �������� Ÿ��</typeparam>
public static class JsonManager<T>
{
    private static string path = Application.dataPath + "/StreamingAssets/Json";    // Json ���� ���

    /// <summary>
    /// Json �����͸� �����ϴ� �Լ�
    /// </summary>
    /// <param name="userData">�ش� Ÿ���� ������</param>
    /// <param name="fileName">������ ���� �̸�</param>
    public static void Save(T userData, string fileName)
    {
        // �ش� ��ΰ� ������ ���� ��� ����
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        // �ش� Ÿ���� �����͸� Json �������� ���� �� ���ڿ��� ����
        string _saveJson = JsonConvert.SerializeObject(userData);

        // ���ϰ��/�����̸�.json ���� ��� ����
        string _filePath = path + "/" + fileName + ".json";

        // �ش� ������ �������� ���� �ۼ�
        File.WriteAllText(_filePath, _saveJson);
    }

    /// <summary>
    /// Json �����͸� �ҷ����� �Լ�
    /// </summary>
    /// <param name="_fileName">�ҷ��� ���� �̸�</param>
    /// <returns>�ҷ��� ������</returns>
    public static T Load(string _fileName)
    {
        // ���ϰ��/�����̸�.json ���� ��� ����
        string _filePath = path + "/" + _fileName + ".json";

        // �ش� ��ΰ� ������ ���� ǥ�� �� ���׸� Ÿ�� default ��ȯ
        if (!File.Exists(_filePath))
        {
            Debug.LogError("No such saveFile exists");
            return default(T);
        }

        // ���� �ؽ�Ʈ �ҷ�����
        string saveFile = File.ReadAllText(_filePath);

        // �ҷ��� ���ڿ��� �ش� Ÿ������ ��ȯ �� ��ȯ
        T _userData = JsonConvert.DeserializeObject<T>(saveFile);
        return _userData;
    }

    /// <summary>
    /// Json �����͸� �����ϴ� �Լ�
    /// </summary>
    /// <param name="_fileName">������ ���� �̸�</param>
    public static void Delete(string _fileName)
    {
        // ���ϰ��/�����̸�.json ���� ��� ����
        string _filePath = path + "/" + _fileName + ".json";

        // �ش� ��ΰ� ������ ���� ǥ�� �� �Լ� ����
        if (!File.Exists(_filePath))
        {
            Debug.LogError("No such saveFile exists");
            return;
        }

        // ���� ����
        File.Delete(_filePath);
    }
}