using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{   
    [SerializeField] int[] pointsObtainedOnEachLevel = new int[50];
    public static SaveSystem instance;
    string colorData;

    private void Awake()
    {
        instance = this;
        Load();       
    }

    void Save()
    {
        string saveData = JsonUtility.ToJson(new SaveData(pointsObtainedOnEachLevel, colorData));
        File.WriteAllText(Application.persistentDataPath + "/completedLevelsData.json", saveData);
    }

    public void Load()
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/completedLevelsData.json");
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        pointsObtainedOnEachLevel = data.pointsGained;
    }

    public void CompleteLevel(int levelIndex, int pointsObtained)
    {
        pointsObtainedOnEachLevel[levelIndex] = pointsObtained;
        Save();
    }

    public Color GetColorData()
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/completedLevelsData.json");
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        Color c;
        ColorUtility.TryParseHtmlString(data.color, out c);

        return c;
    }

    public int[] GetObtainedPointsFromEachLevel()
    {
        return pointsObtainedOnEachLevel;
    }

    public void SaveColorData(string color)
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/completedLevelsData.json");
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        data.color = color;

        string saveData = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/completedLevelsData.json", saveData);

    }

}
[System.Serializable]
public class SaveData
{
    public int[] pointsGained;
    public string color;

    public SaveData(int[] points, string color)
    {
        pointsGained = points;
    }

    
}
