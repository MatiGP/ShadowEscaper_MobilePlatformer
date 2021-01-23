using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] Slider musicVolume;
    [SerializeField] Slider sfxVolume;
    [SerializeField] Toggle toggle60FPS;

    private void Start()
    {
        musicVolume.value = SaveSystem.instance.GetMusicVolume();
        sfxVolume.value = SaveSystem.instance.GetSFXVolume();
        toggle60FPS.isOn = SaveSystem.instance.GetTargetFramerate() == 60 ? true : false;
    }

    public void SaveSettings()
    {
        SaveSystem.instance.SaveMusicVolume(musicVolume.value);
        SaveSystem.instance.SaveSoundFXVolume(sfxVolume.value);

        if (toggle60FPS.isOn)
        {
            SaveSystem.instance.SaveTargetFramerate(60);
        }
        else
        {
            SaveSystem.instance.SaveTargetFramerate(30);
        }

        SaveSystem.instance.Save();
    }

    public void SetTargetFramerate()
    {
        if (toggle60FPS.isOn)
        {
            Application.targetFrameRate = 60;
        }
        else
        {
            Application.targetFrameRate = 30;
        }
    }
}
