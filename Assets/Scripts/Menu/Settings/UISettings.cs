using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Panels
{
    public class UISettings : UIPanel
    {
        [SerializeField] private Button m_BackButton = null;

        [SerializeField] private Slider m_MusicVolume;
        [SerializeField] private Slider m_SFXVolume;
        [SerializeField] private Toggle m_Toggle60FPS;
        [SerializeField] private Toggle m_ToggleVibrations;

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

        private void SetTargetFramerate(bool isOn)
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

        private void SetVibrations(bool isOn)
        {
            if (isOn)
            {
                Vibration.Vibrate();
            }

            SaveSystem.SetVibrationsEnabled(isOn);
            ShadowRunApp.Instance.SoundManager.PlaySoundEffect(ESoundType.BUTTON_PRESSED);
        }

        public void SetMusicVolume(float value)
        {
            SaveSystem.SaveMusicVolume(value);
            ShadowRunApp.Instance.SoundManager.ApplyMusicVolume();
        }

        public void SetSFXVolume(float value)
        {
            SaveSystem.SaveSFXVolume(value);
            ShadowRunApp.Instance.SoundManager.ApplySoundFXVolume();
        }

        public override void BindEvents()
        {
            m_BackButton.onClick.AddListener(ClosePanel);
            m_Toggle60FPS.onValueChanged.AddListener(SetTargetFramerate);
            m_ToggleVibrations.onValueChanged.AddListener(SetVibrations);
            m_MusicVolume.onValueChanged.AddListener(SetMusicVolume);
            m_SFXVolume.onValueChanged.AddListener(SetSFXVolume);
        }

        public override void UnBindEvents()
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