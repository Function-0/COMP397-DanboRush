/*
 * @Author: Tzu-Ting Wu 
 * @Date: 2021-02-28 18:24:25 
 * @Last Modified by: Tzu-Ting Wu
 * @Last Modified time: 2021-02-28 19:28:04
 */
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    private static string path = Application.persistentDataPath + "/player";

    public static void SavePlayer(PlayerBehaviour player) {
        BinaryFormatter formatter = new BinaryFormatter(); // used to turn our files into binary
        FileStream stream = new FileStream(path, FileMode.Create);
        
        PlayerData data = new PlayerData(player);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer() {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        } else
        {
            Debug.LogError("Can't find the save file in " + path);
            return null;
        }
    }
}
