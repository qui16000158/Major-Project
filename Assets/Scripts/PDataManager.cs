using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PDataManager : MonoBehaviour
{
    public static PDataManager instance;

    public string FilePath
    {
        get
        {
            return Application.persistentDataPath + "/" + fileName;
        }
    }

    public string fileName = "pdata.bin";

    // Awake is called before Start
    void Awake()
    {
        instance = this;
    }

    // This will load a player's data from disk, and return it
    public PData Load()
    {
        if (File.Exists(FilePath))
        {
            FileStream stream = new FileStream(FilePath, FileMode.Open);
            
            BinaryFormatter bin = new BinaryFormatter();
            // The binary formatter stores objects as bits, and therefore
            // must be explicitly casted back to the correct object
            return (PData)bin.Deserialize(stream);
        }
        else
        {
            return new PData();
        }
    }

    // This will take a player's data, and save it to disk
    public void Save(PData toSave)
    {
        FileStream stream = new FileStream(FilePath, FileMode.Create);

        BinaryFormatter bin = new BinaryFormatter();
        // This will convert our object into binary data
        // and allow it to be written to a file
        bin.Serialize(stream, toSave);
        stream.Close();
    }
}
