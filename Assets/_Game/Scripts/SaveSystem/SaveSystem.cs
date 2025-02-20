using System.IO;
using UnityEngine;

public class SaveSystem
{
    private static string saveFilePath = Application.persistentDataPath + "/saveData.json";

    public static void Save(GameData data)
    {
        try
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(saveFilePath, json);
            Debug.Log("Game Saved at: " + saveFilePath);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to save data: " + ex.Message);
        }
    }

    public static GameData Load()
    {
        if (File.Exists(saveFilePath))
        {
            try
            {
                string json = File.ReadAllText(saveFilePath);
                return JsonUtility.FromJson<GameData>(json) ?? new GameData();
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Failed to load data: " + ex.Message);
            }
        }

        // If file doesn't exist, return default data
        Debug.LogWarning("No save file found, returning default data.");
        return new GameData();
    }

    public static void ResetSave()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Save data reset. All progress deleted.");
        }
        else
        {
            Debug.LogWarning("No save file found to reset.");
        }
    }
}
