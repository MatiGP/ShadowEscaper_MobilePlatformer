using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{   
    [SerializeField] private AudioSource m_SoundEffectSource;

    [SerializeField] private AudioClip[] SFXSounds;

    float sfxVolume;
    float musicVolume;

    public void PlaySoundEffect(SoundType soundType)
    {
        m_SoundEffectSource.PlayOneShot(SFXSounds[(int)soundType], sfxVolume);
    }
}

public enum SoundType { 
    Coin_Collect,
    Game_Win,
    Key_Collect,
    Player_Landing,
    Player_Slide,
    RocketLauncher_Destroy,
    RocketLauncher_Launch,
    RocketLauncher_PlayerDetected,
    Spikes_Activate,
    Button_Pressed

}
