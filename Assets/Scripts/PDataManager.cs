using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PDataManager : MonoBehaviour
{
    static PData currentData = null;

    public string FilePath
    {
        get
        {
            return Application.persistentDataPath + "/" + fileName;
        }
    }

    public string fileName = "pdata.bin";

    // This will clear the player's data
    public void Clear()
    {
        if (File.Exists(FilePath))
        {
            File.Delete(FilePath);
        }
    }

    // This will load a player's data from disk, and return it
    public void Load()
    {
        if (File.Exists(FilePath))
        {
            FileStream stream = new FileStream(FilePath, FileMode.Open);
            
            BinaryFormatter bin = new BinaryFormatter();
            // The binary formatter stores objects as bits, and therefore
            // must be explicitly casted back to the correct object
            try
            {
                currentData = (PData)bin.Deserialize(stream);
            }
            catch
            {
                Debug.Log("PDATA: Failed to load data (Not necessarily an issue)");
                currentData = new PData(); // Load failsafe data
            }
        }
        else
        {
            currentData = new PData();
        }

        PlayerLevelManager.instance.stats = currentData.stats; // Load the player's stats from the PData object
    }

    // This will take a player's data, and save it to disk
    public void Save()
    {
        currentData = new PData();

        currentData.stats = PlayerLevelManager.instance.stats;

        FileStream stream = new FileStream(FilePath, FileMode.Create);

        BinaryFormatter bin = new BinaryFormatter();
        // This will convert our object into binary data
        // and allow it to be written to a file
        bin.Serialize(stream, currentData);
        stream.Close();
    }
}
