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

    public void PlayMenuTheme()
    {
        myAudioSource.clip = mainMenuTheme;
        myAudioSource.Play();
    }

    public void PlayLevelTheme()
    {
        myAudioSource.clip = levelTheme;
        myAudioSource.Play();
    }
}
