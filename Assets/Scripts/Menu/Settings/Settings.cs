using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] Slider musicVolume;
    [SerializeField] Slider sfxVolume;

    [SerializeField] Slider moveSpeed;
    [SerializeField] TextMeshProUGUI moveSpeedText;
    [SerializeField] Slider jumpHeight;
    [SerializeField] TextMeshProUGUI jumpHeightText;
    [SerializeField] Slider jumpApex;
    [SerializeField] TextMeshProUGUI jumpApexText;
    [SerializeField] Slider jumpNormalMulti;
    [SerializeField] TextMeshProUGUI jumpNormalMultiText;
    [SerializeField] Slider jumpShortMulti;
    [SerializeField] TextMeshProUGUI jumpShortMultiText;
    [SerializeField] Slider wallSlideSpeed;
    [SerializeField] TextMeshProUGUI wallSlideSpeedText;
    [SerializeField] Slider wallJumpOffForce;
    [SerializeField] TextMeshProUGUI walljumpoffText;
    [SerializeField] Slider wallJumpHeight;
    [SerializeField] TextMeshProUGUI walljumpHeightText;
    [SerializeField] Toggle toggle60FPS;

    private void Start()
    {
        musicVolume.value = ShadowRunApp.Instance.SaveSystem.GetMusicVolume();
        sfxVolume.value = ShadowRunApp.Instance.SaveSystem.GetSFXVolume();
        toggle60FPS.isOn = ShadowRunApp.Instance.SaveSystem.GetTargetFramerate() == 60 ? true : false;
    }

    public void SaveSettings()
    {
        ShadowRunApp.Instance.SaveSystem.SaveMusicVolume(musicVolume.value);
        ShadowRunApp.Instance.SaveSystem.SaveSoundFXVolume(sfxVolume.value);

        if (toggle60FPS.isOn)
        {
            ShadowRunApp.Instance.SaveSystem.SaveTargetFramerate(60);
        }
        else
        {
            ShadowRunApp.Instance.SaveSystem.SaveTargetFramerate(30);
        }

        ShadowRunApp.Instance.SaveSystem.Save();
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

    public void SetMoveSpeedText()
    {
        moveSpeedText.text = $"Speed : {moveSpeed.value}";
    }

    public void SetWallJumpHeightText()
    {
        walljumpHeightText.text = $"Walljump Height : {wallJumpHeight.value}";
    }

    public void SetJumpHeightText()
    {
        jumpHeightText.text = $"Jump Height : {jumpHeight.value}";
    }

    public void SetTimeToApexText()
    {
        jumpApexText.text = $"Time to Jump Apex : {jumpApex.value}";
    }

    public void SetNormalJumpMultiText()
    {
        jumpNormalMultiText.text = $"Normal jump fall multi : {jumpNormalMulti.value}";
    }

    public void SetShortJumpMultiText()
    {
        jumpShortMultiText.text = $"Short jump fall multi : {jumpShortMulti.value}";
    }

    public void SetWallJumpOffForceText()
    {
        walljumpoffText.text = $"Wall Jump off Force : {wallJumpOffForce.value}";
    }

    public void SetWallslideSpeedText()
    {
        wallSlideSpeedText.text = $"Wall slide speed : {wallSlideSpeed.value}";
    }

    

}
