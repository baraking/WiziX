using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public AudioClip spellCastFX;
    public AudioClip spellHitSFX;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.clip = spellCastFX;
        audioSource.Play();
    }

    public void PlayHitBySpellSFX()
    {
        audioSource.clip = spellHitSFX;
        audioSource.Play();
    }
}
