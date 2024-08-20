using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem
{
    // Lưu dữ liệu vào playerData ở đây, khi cần sửa thì sửa ở đây rồi gọi SavePlayer là được
    public static PlayerData playerData;

    public static void SavePlayer()
    {
        // nếu chưa có file sẽ tạo mới, có rồi sẽ ghi đè
        // C:\Users\Admin\AppData\LocalLow\DefaultCompany\Red Story
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.data";
        FileStream fileStream = new FileStream(path, FileMode.Create);

        formatter.Serialize(fileStream, playerData);
        fileStream.Close();
        
    }

    public static PlayerData loadPlayerData()
    {
        string path = Application.persistentDataPath + "/player.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);
            PlayerData playerData2 = formatter.Deserialize(fileStream) as PlayerData;
            fileStream.Close();

            SaveSystem.playerData = playerData2;
            return playerData2;
        }
        else
        {
            Debug.Log("file not found"+ path);
            return null;
        }
    }

    public static bool FileSaveExist()
    {
        string path = Application.persistentDataPath + "/player.data";
        if (File.Exists(path))
            return true;
        return false;
    }
}
