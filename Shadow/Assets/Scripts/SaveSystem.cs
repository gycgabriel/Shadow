using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;        // open/save files
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static string saveFolder = "Save";

    public static void SavePlayer(Player player, int saveNum)
    {
        string saveName = "Save" + saveNum.ToString().PadLeft(2, '0') + ".bin";           // Save01.bin

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, saveFolder, saveName);

        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer(int saveNum)
    {
        string saveName = "Save" + saveNum.ToString().PadLeft(2, '0') + ".bin";           // Save01.bin
        string path = Path.Combine(Application.persistentDataPath, saveFolder, saveName);
        
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
