using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{   
    public static SaveSystem instance;
    string colorData;

    SaveData currentSave;

    private void Awake()
    {
        instance = this;

        string json = File.ReadAllText(Application.persistentDataPath + "/completedLevelsData.json");
        currentSave = JsonUtility.FromJson<SaveData>(json);     
    }

    void Save()
    {
        string saveData = JsonUtility.ToJson(currentSave);
        File.WriteAllText(Application.persistentDataPath + "/completedLevelsData.json", saveData);
    }

    public void CompleteLevel(int levelIndex, int[] pointsObtained)
    {
        // if repeating the level.
        if(currentSave.pointsGained[levelIndex] != null)
        {
            currentSave.pointsGained[levelIndex].points = pointsObtained;
        }
        else // if level attempted for the first time
        {
            currentSave.pointsGained[levelIndex] = new Points(pointsObtained);
        }
        
        Save();
    }

    public Color GetColorData()
    {       
        Color c;
        ColorUtility.TryParseHtmlString(currentSave.color, out c);

        return c;
    }

    public int[] GetObtainedPointsFromLevel(int levelIndex)
    {       
        return currentSave.pointsGained[levelIndex].points;          
    }

    public void SaveColorData(string color)
    {
        currentSave.color = color;

        string saveData = JsonUtility.ToJson(currentSave);
        File.WriteAllText(Application.persistentDataPath + "/completedLevelsData.json", saveData);
    } 
}
[System.Serializable]
public class SaveData
{
    public Points[] pointsGained = new Points[50];
    public string color;
    public int frameRate;
    public float soundFXVolume;
    public float musicVolume;   
}
[System.Serializable]
public class Points
{
    public int[] points;

    public Points(int[] pObtained)
    {
        points = pObtained;
    }
}


