using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem
{   
    private string json = null;

    private SaveData currentSave = null;

    public void Initialize()
    {
        TryReadingJSONSave();
    }

    private void TryReadingJSONSave()
    {
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

    public void CompleteLevel(int levelIndex, int pointsObtained)
    {
        currentSave.pointsGained[levelIndex] = pointsObtained;
        Save();
    }

    public Color GetColorData()
    {       
        Color c;
        ColorUtility.TryParseHtmlString(currentSave.color, out c);

        return c;
    }

    public int GetObtainedPointsFromLevel(int levelIndex)
    {       
        return currentSave.pointsGained[levelIndex];          
    }

    public int GetTargetFramerate()
    {
        return currentSave.frameRate;
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
    public int[] pointsGained = new int[25];
    public string color = "#FFFFFF";
    public int frameRate = 30;
    public float soundFXVolume = 1;
    public float musicVolume = 1;   
}


