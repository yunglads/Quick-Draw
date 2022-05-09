using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayerData(PlayerData savePlayerData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, savePlayerData);
        stream.Close();

        Debug.Log("Player Data Saved!");
    }
    public static void SaveLevelData(Level saveLevelData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, saveLevelData);
        stream.Close();

        Debug.Log("Level Data Saved!");
    }

    //public static void LoadPlayerData()
    //{
    //    string path = Application.persistentDataPath + "/player.save";
    //    if (File.Exists(path))
    //    {
    //        BinaryFormatter formatter = new BinaryFormatter();
    //        FileStream stream = File.Open(path, FileMode.Open);

    //        PlayerData saveData = (PlayerData)formatter.Deserialize(stream);

    //        stream.Close();

    //        Debug.Log("Game Loaded!");
    //    }
    //    else
    //    {
    //        Debug.LogError("Save file not found in " + path);
    //    }
    //}
}
