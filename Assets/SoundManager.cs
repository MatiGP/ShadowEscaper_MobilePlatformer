using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource soundEffectSource;

    public AudioClip[] SFXSounds;

    private void Awake()
    {
        instance = this;
    }

    public void PlaySoundEffect(SoundType soundType)
    {
        soundEffectSource.PlayOneShot(SFXSounds[(int)soundType]);
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
    Spikes_Activate

}
