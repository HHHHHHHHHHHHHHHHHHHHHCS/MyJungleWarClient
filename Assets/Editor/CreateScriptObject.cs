using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CreateScriptObject : Editor
{
    private const string saveDataDir = "SaveData";
    private static readonly string saveDataPath = "Assets/SaveData";

    public static void CreateObj(Object obj,string saveName)
    {
        if(!Directory.Exists(saveDataPath))
        {
            Directory.CreateDirectory(saveDataPath);
        }
        int i = 1;
        string filePath = string.Format("{0}/{1}.asset", saveDataPath, saveName);
        while(File.Exists(filePath))
        {
            filePath = string.Format("{0}/{1}_{2}.asset", saveDataPath, saveName,i);
            i++;
        }
        AssetDatabase.CreateAsset(obj, filePath);
    }

    [MenuItem("SaveData/CreatePlayerData")]
    public static void CreatePlayerData()
    {
        ScriptableObject temp = CreateInstance(typeof(PlayerDataList));
        string savePath = "PlayerData";
        CreateObj(temp, savePath);
    }
}
