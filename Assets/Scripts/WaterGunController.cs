using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WaterGunController : MonoBehaviour
{
    private AudioSource splashAudioSource;
    private bool isWaterGunActive = false;

    private void Start()
    {
        splashAudioSource = GetComponent<AudioSource>();
        splashAudioSource.loop = true; // Ensure the audio source is set to loop
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
        {
            ToggleWaterGun();
        }
    }

    private void ToggleWaterGun()
    {
        isWaterGunActive = !isWaterGunActive;

        if (isWaterGunActive)
        {
            splashAudioSource.Play();
        }
        else
        {
            splashAudioSource.Stop();
        }
    }
}
