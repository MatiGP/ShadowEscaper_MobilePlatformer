using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Panels
{
    public class UISettings : UIPanel
    {
        [SerializeField] protected Button m_BackButton = null;

        [SerializeField] protected Slider m_MusicVolume;
        [SerializeField] protected Slider m_SFXVolume;
        [SerializeField] protected Toggle m_Toggle60FPS;
        [SerializeField] protected Toggle m_ToggleVibrations;

        protected override void Awake()
        {
            base.Awake();

            BindEvents();
        }

        protected override void Start()
        {
            m_MusicVolume.value = SaveSystem.GetMusicVolume();
            m_SFXVolume.value = SaveSystem.GetSFXVolume();
            m_Toggle60FPS.isOn = SaveSystem.GetTargetFramerate() == 60 ? true : false;
            m_ToggleVibrations.isOn = SaveSystem.AreVibrationsEnabled();
            
        }     

        protected void SetTargetFramerate(bool isOn)
        {
            if (isOn)
            {
                Application.targetFrameRate = 60;
            }
            else
            {
                Application.targetFrameRate = 30;
            }

            SaveSystem.SaveTargetFramerate(Application.targetFrameRate);
            ShadowRunApp.Instance.SoundManager.PlaySoundEffect(ESoundType.BUTTON_PRESSED);
        }

        protected void SetVibrations(bool isOn)
        {
            if (isOn)
            {
                //Vibration.Vibrate();
            }

            SaveSystem.SetVibrationsEnabled(isOn);
            ShadowRunApp.Instance.SoundManager.PlaySoundEffect(ESoundType.BUTTON_PRESSED);
        }

        protected void SetMusicVolume(float value)
        {
            SaveSystem.SaveMusicVolume(value);
            ShadowRunApp.Instance.SoundManager.ApplyMusicVolume();
        }

        protected void SetSFXVolume(float value)
        {
            SaveSystem.SaveSFXVolume(value);
            ShadowRunApp.Instance.SoundManager.ApplySoundFXVolume();
        }

        protected virtual void BindEvents()
        {
            m_BackButton.onClick.AddListener(ClosePanel);
            m_Toggle60FPS.onValueChanged.AddListener(SetTargetFramerate);
            m_ToggleVibrations.onValueChanged.AddListener(SetVibrations);
            m_MusicVolume.onValueChanged.AddListener(SetMusicVolume);
            m_SFXVolume.onValueChanged.AddListener(SetSFXVolume);
        }

        protected virtual void UnBindEvents()
        {
            m_BackButton.onClick.RemoveListener(ClosePanel);
            m_Toggle60FPS.onValueChanged.RemoveListener(SetTargetFramerate);
            m_ToggleVibrations.onValueChanged.RemoveListener(SetVibrations);
            m_MusicVolume.onValueChanged.RemoveListener(SetMusicVolume);
            m_SFXVolume.onValueChanged.RemoveListener(SetSFXVolume);
        }

        private void OnDestroy()
        {
            UnBindEvents();
        }
    }
}