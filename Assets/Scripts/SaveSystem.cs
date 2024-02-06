using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    CoreData coreDataScript;

    void Start()
    {
        // Assuming these scripts are attached to the same GameObject
        coreDataScript = GetComponent<CoreData>();
    }

    public void SaveData()
    {
        string path = Application.persistentDataPath + "/coreData.txt";

        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(coreDataScript.DateventureCounter.ToString());
            }
        }
    }


    public int LoadData()
    {
        string path = Application.persistentDataPath + "/coreData.txt";

        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                // Read the data from the file
                string data = reader.ReadLine();

                if (int.TryParse(data, out int dateventureCounter))
                {
                    // Successfully parsed the data, you can assign it to your CoreDataScript
                    coreDataScript.DateventureCounter = dateventureCounter;
                    return dateventureCounter;
                }
                else
                {
                    Debug.LogError($"Error parsing data from file: {path}");
                    return 0;
                }
            }
        }
        else
        {
            Debug.LogError($"Save file not found in: {path}");
            return 0;
        }
    }
}