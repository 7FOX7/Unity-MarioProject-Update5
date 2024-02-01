using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// using binary gives us more functionality; 
using System.IO;
using System.Runtime.Serialization.Formatters.Binary; 


public class DataManagement : MonoBehaviour
{
    public static DataManagement data;
    public int highScore;
    private string path = "C:\\Users\\ekhe1\\CheckFolder"; 

    private void Awake()
    {
        if (data == null)
        {
            DontDestroyOnLoad(gameObject);
            data = this; 
        } 
        else if (data != this)
        {
            Destroy(gameObject); 
        }
    }

    // Methods below can be accessed anywhere from the game: 
    public void SaveData()
    {
        BinaryFormatter BinFor = new BinaryFormatter(); // creates a binary; 
        FileStream file = File.Create(path + "\\gameInfo.dat"); // creates a  file; 
        GameData data = new GameData(); // creates a container for data; 
        data.highScore = highScore;
        BinFor.Serialize(file, data); // serializes 
        file.Close();
    }

    public void LoadData()
    {
        if(File.Exists (path + "\\gameInfo.dat"))
        {
            BinaryFormatter BinFor = new BinaryFormatter();
            FileStream file = File.Open(path + "\\gameInfo.dat", FileMode.Open);
            GameData data = (GameData)BinFor.Deserialize(file);
            file.Close();
            highScore = data.highScore; 
        }
    }
}

// when the data is saved (player reahes the end level, the data from the class below is actually saved): 
[Serializable]
class GameData
{
    public int highScore; 
}
