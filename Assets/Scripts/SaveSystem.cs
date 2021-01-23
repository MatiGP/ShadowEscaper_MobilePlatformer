using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{   
    public static SaveSystem instance;
    string colorData;
    string json;

    SaveData currentSave;

    private void Awake()
    {
        instance = this;

        try
        {
            json = File.ReadAllText(Application.persistentDataPath + "/completedLevelsData.json");
        }
        catch
        {
            // Launching game for the very first time.
            string saveData = JsonUtility.ToJson(new SaveData());
            File.WriteAllText(Application.persistentDataPath + "/completedLevelsData.json", saveData);
        }
        finally
        {
            json = File.ReadAllText(Application.persistentDataPath + "/completedLevelsData.json");
            currentSave = JsonUtility.FromJson<SaveData>(json);
        }                            
             
    }

    public void Save()
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

    public int GetTargetFramerate()
    {
        return 60;//currentSave.frameRate;
    }

    public float GetSFXVolume()
    {
        return currentSave.soundFXVolume;
    }

    public float GetMusicVolume()
    {
        return currentSave.musicVolume;
    }

    public void SaveColorData(string color)
    {
        currentSave.color = color;

        string saveData = JsonUtility.ToJson(currentSave);
        File.WriteAllText(Application.persistentDataPath + "/completedLevelsData.json", saveData);
    } 

    public void SaveSoundFXVolume(float v)
    {
        currentSave.soundFXVolume = v;
    }

    public void SaveMusicVolume(float v)
    {
        currentSave.musicVolume = v;
    }

    public void SaveTargetFramerate(int framerate)
    {
        currentSave.frameRate = framerate;  
    }
}
[System.Serializable]
public class SaveData
{
    public Points[] pointsGained = new Points[50];
    public string color = "#FFFFFF";
    public int frameRate = 30;
    public float soundFXVolume = 1;
    public float musicVolume = 1;   
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


