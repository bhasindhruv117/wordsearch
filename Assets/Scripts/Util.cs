using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Util
{
    public static void WriteTextToFile(string fileName, string dataString)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        try
        {
            File.WriteAllText(filePath,dataString);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            throw;
        }
    }

    public static string LoadTextFromFile(string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        string data = string.Empty;
        if (File.Exists(filePath))
        {
            data = File.ReadAllText(filePath);
        }

        return data;
    }
}
