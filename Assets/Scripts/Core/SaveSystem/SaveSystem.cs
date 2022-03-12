using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class SaveSystem
{
    private const string KEY_MUSIC_VOLUME = "key_music_volume";
    private const string KEY_SOUND_EFFECT_VOLUME = "key_sound_effect_volume";
    private const string KEY_TARGET_FRAMERATE = "key_target_framerate";
    private const string KEY_SKIN_COLOR = "key_skin_color";
    private const string KEY_STARS_COUNT = "key_stars_count";
    private const string KEY_VIBRATIONS_ENABLED = "key_vibrations_enabled";
    private const string KEY_PREMIUM_CURRENCY = "key_premium_currency";
    private const string KEY_IS_TUTORIAL_COMPLETED = "key_is_tutorial_completed";
    private const string KEY_OWNED_STARS_DATA = "owned_stars_data";

    private static Dictionary<string, int> m_EarnedStars = new Dictionary<string, int>();

    public static bool IsTutorialCompleted
    {
        get { return PlayerPrefs.GetInt(KEY_IS_TUTORIAL_COMPLETED, 0) == 1; }
        set { PlayerPrefs.SetInt(KEY_IS_TUTORIAL_COMPLETED, value ? 1 : 0 ); }
    }

    public static void SetTutorialCompleted(bool completed)
    {
        PlayerPrefs.SetInt(KEY_IS_TUTORIAL_COMPLETED, completed ? 1 : 0);
    }

    public static Color GetColorData()
    {       
        Color color;
        string htlmColorString = PlayerPrefs.GetString(KEY_SKIN_COLOR, "#FFFFFF");
        ColorUtility.TryParseHtmlString(htlmColorString, out color);

        return color;
    }

    public static int GetPremiumCurrencyCount()
    {
        return PlayerPrefs.GetInt(KEY_PREMIUM_CURRENCY, 0);
    }

    public static void AddPremiumCurrencyCount(int value)
    {
        int current = GetPremiumCurrencyCount();
        PlayerPrefs.SetInt(KEY_PREMIUM_CURRENCY, current  + value);
    }

    public static void LoadPlayerProgress()
    {
        string loadedDictionary = PlayerPrefs.GetString(KEY_OWNED_STARS_DATA);

        if(loadedDictionary == "")
        {
            m_EarnedStars = new Dictionary<string, int>();
            return;
        }

        m_EarnedStars = JsonConvert.DeserializeObject<Dictionary<string, int>>(loadedDictionary);
    }

    public static int GetObtainedPointsFromLevel(int levelIndex)
    {
        string levelName = string.Format(LevelLoader.LEVEL_NAME_FORMAT, levelIndex.ToString("D2"));

        return m_EarnedStars.ContainsKey(levelName) ? m_EarnedStars[levelName] : 0;
    }

    public static void SaveObtainedStarsFromLevel(int levelIndex, int starsCount)
    {
        string levelName = string.Format(LevelLoader.LEVEL_NAME_FORMAT, levelIndex.ToString("D2"));

        if (m_EarnedStars.ContainsKey(levelName))
        {
            m_EarnedStars[levelName] = starsCount;
        }
        else
        {
            m_EarnedStars.Add(levelName, starsCount);
        }

        string jsonString = JsonConvert.SerializeObject(m_EarnedStars);
        PlayerPrefs.SetString(KEY_OWNED_STARS_DATA, jsonString);
    }

    public static int GetSumOfStars()
    {
        int sum = 0;

        foreach(string key in m_EarnedStars.Keys)
        {
            sum += m_EarnedStars[key];
        }

        return sum;
    }

    public static int GetTargetFramerate()
    {
        return PlayerPrefs.GetInt(KEY_TARGET_FRAMERATE, 30);
    }

    public static void SaveTargetFramerate(int framerate)
    {
        PlayerPrefs.SetInt(KEY_TARGET_FRAMERATE, framerate);
    }

    public static float GetSFXVolume()
    {
        return PlayerPrefs.GetFloat(KEY_SOUND_EFFECT_VOLUME, 0.5f);
    }

    public static void SaveSFXVolume(float value)
    {
        PlayerPrefs.SetFloat(KEY_SOUND_EFFECT_VOLUME, value);
    }

    public static float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat(KEY_MUSIC_VOLUME, 0.5f);
    }

    public static void SaveMusicVolume(float value)
    {
        PlayerPrefs.SetFloat(KEY_MUSIC_VOLUME, value);
    }

    public static void SaveColorData(string color)
    {
        PlayerPrefs.SetString(KEY_SKIN_COLOR, color);
    }    

    public static bool AreVibrationsEnabled()
    {
        return PlayerPrefs.GetInt(KEY_VIBRATIONS_ENABLED, 1) == 1;
    }

    public static void SetVibrationsEnabled(bool value)
    {
        PlayerPrefs.SetInt(KEY_VIBRATIONS_ENABLED, value ? 1 : 0);
    }   
}


