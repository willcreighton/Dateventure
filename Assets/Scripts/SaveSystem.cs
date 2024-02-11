using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    // Define the CoreData class
    CoreData coreDataScript;

    void Start()
    {
        // Assign it from the attached component to access its contents
        coreDataScript = GetComponent<CoreData>();
    }

    public void SaveData()
    {
        // Define the file path for our data
        string path = Application.persistentDataPath + "/coreData.txt";

        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                // Save our data into the text file
                writer.Write(coreDataScript.DateventureCounter.ToString());
            }
        }
    }

    public int LoadData()
    {
        // Define the file path for our data
        string path = Application.persistentDataPath + "/coreData.txt";

        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                // Read the data from the file
                string data = reader.ReadLine();

                if (int.TryParse(data, out int dateventureCounter))
                {
                    // Successfully parsed the data and return it as an int to use
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