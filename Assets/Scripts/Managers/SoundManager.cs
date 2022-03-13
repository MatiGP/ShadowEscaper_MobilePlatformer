using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource m_SoundEffectSource = null;
        [SerializeField] private AudioSource m_MainMenuMusic = null;
        [SerializeField] private AudioSource m_GameplayMusic = null;

        [SerializeField] private AudioClip[] SFXSounds;

        private float m_SFXVolume = 1f;
        private float m_MusicVolume = 1f;
        public void Initialize()
        {
            m_SFXVolume = SaveSystem.GetSFXVolume();
            m_SoundEffectSource.volume = m_SFXVolume;

            m_MusicVolume = SaveSystem.GetMusicVolume();
            m_GameplayMusic.volume = m_MusicVolume;
            m_MainMenuMusic.volume = m_MusicVolume;
        }

        public void PlaySoundEffect(ESoundType soundType)
        {
            m_SoundEffectSource.PlayOneShot(SFXSounds[(int)soundType]);
        }

        public void PauseMainMenuMusic()
        {
            m_MainMenuMusic.Pause();
            if (!m_GameplayMusic.isPlaying)
            {
                m_GameplayMusic.Play();
            }

        }

        public void PauseGameplayMusic()
        {
            m_MainMenuMusic.Play();
            m_GameplayMusic.Pause();
        }

        public void ApplySoundFXVolume()
        {
            m_SoundEffectSource.volume = SaveSystem.GetSFXVolume();
        }

        public void ApplyMusicVolume()
        {
            m_MainMenuMusic.volume = SaveSystem.GetMusicVolume();
            m_GameplayMusic.volume = SaveSystem.GetMusicVolume();
        }
    }


    public enum ESoundType
    {
        COIN_COLLECT,
        GAME_WIN,
        KEY_COLLECT,
        PLAYER_LANDING,
        PLAYER_SLIDE,
        ROCKETLAUNCHER_DESTROY,
        ROCKETLAUNCHER_LAUNCH,
        ROCKETLAUNCHER_PLAYERDETECTED,
        SPIKES_ACTIVATE,
        BUTTON_PRESSED
    }
}
