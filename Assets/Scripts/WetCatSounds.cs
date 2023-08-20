using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class WetCatSounds : MonoBehaviour
{
    public AudioClip[] wetCatSounds; // Array of wet cat sound clips
    private AudioSource audioSource;
    private int lastSoundIndex = -1; // Index of the last sound played

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomWetCatSound()
    {
        if (wetCatSounds.Length <= 1) return; // No sounds to play or only one sound

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, wetCatSounds.Length);
        } while (randomIndex == lastSoundIndex); // Keep generating a new index until it's different from the last one

        lastSoundIndex = randomIndex; // Remember the index of the sound played
        audioSource.clip = wetCatSounds[randomIndex];
        audioSource.Play();
    }
}
