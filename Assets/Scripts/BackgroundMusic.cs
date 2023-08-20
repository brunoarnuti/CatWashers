using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play(); // Play the music
    }

    public void PauseMusic()
    {
        audioSource.Pause(); // Pause the music
    }

    public void ResumeMusic()
    {
        audioSource.UnPause(); // Resume the music
    }

    public void ChangeTrack(AudioClip newTrack)
    {
        audioSource.clip = newTrack; // Change the music track
        audioSource.Play(); // Play the new track
    }
}
