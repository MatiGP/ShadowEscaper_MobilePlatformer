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

        protected override void Start()
        {
            m_MusicVolume.value = ShadowRunApp.Instance.SaveSystem.GetMusicVolume();
            m_SFXVolume.value = ShadowRunApp.Instance.SaveSystem.GetSFXVolume();
            m_Toggle60FPS.isOn = ShadowRunApp.Instance.SaveSystem.GetTargetFramerate() == 60 ? true : false;
            
        }

        public void SaveSettings()
        {
            ShadowRunApp.Instance.SaveSystem.SaveMusicVolume(m_MusicVolume.value);
            ShadowRunApp.Instance.SaveSystem.SaveSoundFXVolume(m_SFXVolume.value);

            if (m_Toggle60FPS.isOn)
            {
                ShadowRunApp.Instance.SaveSystem.SaveTargetFramerate(60);
            }
            else
            {
                ShadowRunApp.Instance.SaveSystem.SaveTargetFramerate(30);
            }

            ShadowRunApp.Instance.SaveSystem.Save();
        }

        public void SetTargetFramerate(bool isOn)
        {
            if (isOn)
            {
                Application.targetFrameRate = 60;
            }
            else
            {
                Application.targetFrameRate = 30;
            }
        }    

        public override void Initialize()
        {
            BindEvents();
        }

        public override void BindEvents()
        {
            m_BackButton.onClick.AddListener(ClosePanel);
            m_Toggle60FPS.onValueChanged.AddListener(SetTargetFramerate);
        }

        public override void UnBindEvents()
        {
            m_BackButton.onClick.RemoveListener(ClosePanel);
            m_Toggle60FPS.onValueChanged.RemoveListener(SetTargetFramerate);
        }

        private void OnDestroy()
        {
            UnBindEvents();
        }
    }
}