using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RechargeController : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Recharge();
            PlayRechargeSound();
        }
    }

    private void Recharge()
    {
        // Your existing code to handle the recharge action
    }

    private void PlayRechargeSound()
    {
        audioSource.Play();
    }
}