/*
 * @Author: Tzu-Ting Wu 
 * @Date: 2021-02-28 18:24:25 
 * @Last Modified by: Tzu-Ting Wu
 * @Last Modified time: 2021-03-14 23:53:11
 */
using System;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    private static string path;

    public static void SavePlayer(PlayerBehaviour player, string slot, DateTime saveTime) {
        path = Application.persistentDataPath + "/player" + slot;
        BinaryFormatter formatter = new BinaryFormatter(); // used to turn our files into binary
        FileStream stream = new FileStream(path, FileMode.Create);
        
        PlayerData data = new PlayerData(player);
        data.saveTime = DateTime.Now;
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer(string slot) {
        path = Application.persistentDataPath + "/player" + slot;
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
