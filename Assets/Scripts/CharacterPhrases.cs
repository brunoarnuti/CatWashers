using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CharacterPhrases : MonoBehaviour
{
    public AudioClip[] characterPhrases; // Array of character phrase sound clips
    public AudioClip hitSound; // Sound effect for hit
    private AudioSource audioSource;
    private int lastSoundIndex = -1; // Index of the last sound played

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("PlayRandomCharacterPhrase", 7f, 7f);
    }


    public void PlayRandomCharacterPhrase()
    {
        if (characterPhrases.Length <= 1) return; // No sounds to play or only one sound

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, characterPhrases.Length);
        } while (randomIndex == lastSoundIndex); // Keep generating a new index until it's different from the last one

        lastSoundIndex = randomIndex; // Remember the index of the sound played
        audioSource.clip = characterPhrases[randomIndex];
        audioSource.Play();
    }

    public void PausePhrases()
    {
        audioSource.Pause();
    }

    public void ResumePhrases()
    {
        audioSource.UnPause();
    }
    
    public void PauseAndPlayHitSound()
    {
        audioSource.Stop(); // Stop any ongoing sound
        audioSource.clip = hitSound; // Set the hit sound
        audioSource.volume = 1f; // Set the volume to maximum
        audioSource.Play(); // Play the hit sound
    }
}


// [RequireComponent(typeof(AudioSource))]
// public class CharacterPhrases : MonoBehaviour
// {
//     public AudioClip[] characterPhrases; // Array of character phrase sound clips
//     private AudioSource audioSource;
//     private int lastSoundIndex = -1; // Index of the last sound played
//
//     private void Start()
//     {
//         audioSource = GetComponent<AudioSource>();
//         InvokeRepeating("PlayRandomCharacterPhrase", 7f, 7f); // Play a random phrase every 3 seconds
//     }
//
//     public void PlayRandomCharacterPhrase()
//     {
//         if (characterPhrases.Length <= 1) return; // No sounds to play or only one sound
//
//         int randomIndex;
//         do
//         {
//             randomIndex = Random.Range(0, characterPhrases.Length);
//         } while (randomIndex == lastSoundIndex); // Keep generating a new index until it's different from the last one
//
//         lastSoundIndex = randomIndex; // Remember the index of the sound played
//         audioSource.clip = characterPhrases[randomIndex];
//         audioSource.Play();
//     }
// }

