using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{   
    [SerializeField] private AudioSource m_SoundEffectSource;

    [SerializeField] private AudioClip[] SFXSounds;

    float sfxVolume = 1f;
    float musicVolume;

    public void PlaySoundEffect(ESoundType soundType)
    {
        m_SoundEffectSource.PlayOneShot(SFXSounds[(int)soundType], sfxVolume);
    }
}

public enum ESoundType { 
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
