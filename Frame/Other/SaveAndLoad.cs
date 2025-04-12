using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameSave
{
    public static void SaveByJson(string saveFilename, object data)
    {
        var json = JsonUtility.ToJson(data);
        var path = Path.Combine(Application.persistentDataPath, saveFilename);
        try
        {
            File.WriteAllText(path, json);
            Debug.Log($"�ɹ��洢��{path}");
        }
        catch (System.Exception exception)
        {
            Debug.Log($"��{path}�洢ʧ��\n {exception}");
        }
    }
    public static T LoadByJson<T>(string saveFilename)
    {
        var path = Path.Combine(Application.persistentDataPath, saveFilename);

        try
        {
            var json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<T>(json);
            Debug.Log($"�ɹ���{path}��ȡ");
            return data;
        }
        catch (System.Exception exception)
        {
            Debug.Log($"��{path}��ȡʧ��\n {exception}");
            return default;
        }
    }

    public static void DeleteFile(string saveFilename)
    {
        var path = Path.Combine(Application.persistentDataPath, saveFilename);
        try { File.Delete(path); }
        catch (System.Exception exception)
        {
            Debug.Log($"��{path}ɾ��ʧ��\n {exception}");
        }
    }
}
