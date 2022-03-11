using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem
{
    private const string KEY_MUSIC_VOLUME = "key_music_volume";
    private const string KEY_SOUND_EFFECT_VOLUME = "key_sound_effect_volume";
    private const string KEY_TARGET_FRAMERATE = "key_target_framerate";
    private const string KEY_PROGRESS_AT_LEVEL = "key_progress_at_level_{0}";
    private const string KEY_SKIN_COLOR = "key_skin_color";
    private const string KEY_STARS_COUNT = "key_stars_count";
    private const string KEY_VIBRATIONS_ENABLED = "key_vibrations_enabled";
    private const string KEY_PREMIUM_CURRENCY = "key_premium_currency";
    private const string KEY_IS_TUTORIAL_COMPLETED = "key_is_tutorial_completed";

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

    public static void SetPremiumCurrencyCount(int value)
    {
        int current = GetPremiumCurrencyCount();
        PlayerPrefs.SetInt(KEY_PREMIUM_CURRENCY, current  + value);
    }

    public static int GetObtainedPointsFromLevel(int levelIndex)
    {
        string levelString = string.Format(KEY_PROGRESS_AT_LEVEL, levelIndex);
        int pointsObtained = PlayerPrefs.GetInt(levelString, 0);
        return pointsObtained;          
    }

    public static void SaveObtainedPointsFromLevel(int levelIndex, int points)
    {
        string levelString = string.Format(KEY_PROGRESS_AT_LEVEL, levelIndex);
        PlayerPrefs.SetInt(levelString, points);
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

    public static void AddToOwnedStars(int count)
    {
        int current = PlayerPrefs.GetInt(KEY_STARS_COUNT, 0);
        PlayerPrefs.SetInt(KEY_STARS_COUNT, current + count);
    }

    public static int GetOwnedStars()
    {
        return PlayerPrefs.GetInt(KEY_STARS_COUNT);
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


