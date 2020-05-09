using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static bool previousDataExists()
    {
        string path = Path.Combine(Application.persistentDataPath, "saveDay.day");
        if (File.Exists(path))
        {
            return true;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return false;
        }
    }
    
    public static void SaveDay(EndDay.InGameDay saveDay)
    {
        BinaryFormatter dayFormatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, "saveDay.day");
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData dayData = new SaveData(saveDay);

        dayFormatter.Serialize(stream, dayData);
        stream.Close();

        Debug.Log("Save created in " + path);
    }

    public static SaveData LoadDay()
    {
        string path = Path.Combine(Application.persistentDataPath, "saveDay.day");
        if (File.Exists(path))
        {
            BinaryFormatter dayFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData dayData = dayFormatter.Deserialize(stream) as SaveData;
            stream.Close();

            return dayData;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
