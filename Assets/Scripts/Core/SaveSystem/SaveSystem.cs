using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem
{   
    private string m_JsonSave = null;

    private SaveData m_CurrentSave = null;

    private const string SAVE_NAME = "saveData";

    public void Initialize()
    {
        TryReadingJSONSave();
    }

    private void TryReadingJSONSave()
    {
        try
        {
            m_JsonSave = File.ReadAllText(Application.persistentDataPath + $"/{SAVE_NAME}.json");
        }
        catch
        {
            // Launching game for the very first time.
            string saveData = JsonUtility.ToJson(new SaveData());
            File.WriteAllText(Application.persistentDataPath + $"/{SAVE_NAME}.json", saveData);
        }
        finally
        {
            m_JsonSave = File.ReadAllText(Application.persistentDataPath + $"/{SAVE_NAME}.json");
            m_CurrentSave = JsonUtility.FromJson<SaveData>(m_JsonSave);
        }
    }

    public void Save()
    {
        string saveData = JsonUtility.ToJson(m_CurrentSave);
        File.WriteAllText(Application.persistentDataPath + $"/{SAVE_NAME}.json", saveData);
    }

    public Color GetColorData()
    {       
        Color c;
        ColorUtility.TryParseHtmlString(m_CurrentSave.m_Color, out c);

        return c;
    }

    public int GetObtainedPointsFromLevel(int levelIndex)
    {       
        return m_CurrentSave.m_PointsGained[levelIndex];          
    }

    public int GetTargetFramerate()
    {
        return m_CurrentSave.m_FrameRate;
    }

    public float GetSFXVolume()
    {
        return m_CurrentSave.m_SoundFXVolume;
    }

    public float GetMusicVolume()
    {
        return m_CurrentSave.m_MusicVolume;
    }

    public void SaveColorData(string color)
    {
        m_CurrentSave.m_Color = color;

        string saveData = JsonUtility.ToJson(m_CurrentSave);
        File.WriteAllText(Application.persistentDataPath + $"/{SAVE_NAME}.json", saveData);
    } 

    public void SaveSoundFXVolume(float v)
    {
        m_CurrentSave.m_SoundFXVolume = v;
    }

    public void SaveMusicVolume(float v)
    {
        m_CurrentSave.m_MusicVolume = v;
    }

    public void SaveTargetFramerate(int framerate)
    {
        m_CurrentSave.m_FrameRate = framerate;  
    }

    public void SaveLevelProgress(int earnedPoints, int levelIndex)
    {
        m_CurrentSave.m_PointsGained[levelIndex] = earnedPoints;
        Save();
    }
}

[System.Serializable]
public class SaveData
{
    public int[] m_PointsGained = new int[25];
    public string m_Color = "#FFFFFF";
    public int m_FrameRate = 30;
    public float m_SoundFXVolume = 1f;
    public float m_MusicVolume = 1f;   
}


