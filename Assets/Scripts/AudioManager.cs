using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : GenericSingleton<AudioManager>
{
    [SerializeField] AudioClip mainMenuTheme;
    [SerializeField] AudioClip levelTheme;

    [SerializeField] AudioSource myAudioSource;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        myAudioSource = GetComponent<AudioSource>();
        PlayMenuTheme();
    }

    public void VolumeDown()
    {
        myAudioSource.volume = .25f;
    }

    public void RestoreVolume()
    {
        myAudioSource.volume = .5f;
    }

    public void PlayMenuTheme()
    {
        RestoreVolume();
        myAudioSource.clip = mainMenuTheme;
        myAudioSource.Play();
    }

    public void PlayLevelTheme()
    {
        RestoreVolume();
        myAudioSource.clip = levelTheme;
        myAudioSource.Play();
    }
}
