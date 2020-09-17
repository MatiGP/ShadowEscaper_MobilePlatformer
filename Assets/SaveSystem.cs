using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public SaveSystem instance;

    [SerializeField] int[] pointsObtainedOnEachLevel = new int[25];

    private void Awake()
    {
        instance = this;
        Load();
        
    }

    void Save()
    {
        string saveData = JsonUtility.ToJson(new SaveData(pointsObtainedOnEachLevel));
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

    public int[] GetObtainedPointsFromEachLevel()
    {
        return pointsObtainedOnEachLevel;
    }
}
[System.Serializable]
public class SaveData
{
    public int[] pointsGained;

    public SaveData(int[] points)
    {
        pointsGained = points;
    }
}
