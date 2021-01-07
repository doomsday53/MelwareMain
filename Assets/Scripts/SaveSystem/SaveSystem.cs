using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Assets.Scripts.SaveSystem;

public class SaveSystem
{
    public static void Save(GameData gameData, string filename)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + filename + ".MWD";
        Debug.Log(path);
        FileStream fileStream = new FileStream(path, FileMode.Create);
        binaryFormatter.Serialize(fileStream, gameData);
        fileStream.Close();
    }
    public static GameData Load(string filename)
    {
        string path = Application.persistentDataPath + "/" + filename + ".MWD";
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);
            GameData gameData = binaryFormatter.Deserialize(fileStream) as GameData;
            fileStream.Close();
            return gameData;
        }
        else
        {
            return null;
        }
    }
}
