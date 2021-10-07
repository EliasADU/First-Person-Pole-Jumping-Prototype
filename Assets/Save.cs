using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Save : MonoBehaviour
{
    public static void SavePlayer(Player player)
    {
    BinaryFormatter formatter = new BinaryFormatter();

    string path = Application.persistentDataPath + "/player.fun";
    FileStream stream = new FileStream(path, FileMode.Create);

    Data data = new Data(player);

        formatter.Serialize(stream, data);
    stream.Close();
}

public static Data LoadPlayer()
{
    string path = Application.persistentDataPath + "/player.fun";
    if(File.Exists(path))
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);

        Data data = formatter.Deserialize(stream) as Data;
        stream.Close();

        return data;
    }
    else
    {
        Debug.LogError("Save file not found in " + path);
        return null;
    }
}
}