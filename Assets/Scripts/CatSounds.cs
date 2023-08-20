using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CatSounds : MonoBehaviour
{
    public AudioClip[] catSounds; // Array of cat sound clips
    private AudioSource audioSource;
    private int lastSoundIndex = -1; // To track the last sound played

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("PlayRandomCatSound", 3f, 3f); // Call PlayRandomCatSound every 3 seconds
    }

    private void PlayRandomCatSound()
    {
        if (catSounds.Length == 0) return; // No sounds to play

        int randomIndex;

        // Ensure a different sound is selected
        do
        {
            randomIndex = Random.Range(0, catSounds.Length);
        } while (randomIndex == lastSoundIndex && catSounds.Length > 1);

        lastSoundIndex = randomIndex;
        audioSource.clip = catSounds[randomIndex];
        audioSource.Play();
    }
}
