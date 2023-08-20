using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catEnemy : MonoBehaviour
{
    Transform targetDestination;
    [SerializeField] float speed;
    [SerializeField] private int health = 2; // Health of the cat
    GameObject targetGameObject;
    Rigidbody2D catRigibody2D;
    private WetCatSounds wetCatSounds;

    private void Awake()
    {
        catRigibody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        wetCatSounds = FindObjectOfType<WetCatSounds>();
    }

    public void SetTarget(GameObject target)
    {
        targetGameObject = target;
        targetDestination = target.transform;
    }
    
    private void PlayWetCatSound()
    {
        if (wetCatSounds != null)
        {
            wetCatSounds.PlayRandomWetCatSound();
        }
    }

    private void FixedUpdate()
    {
        Vector3 direction = (targetDestination.position - transform.position).normalized;
        catRigibody2D.velocity = direction * speed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == targetGameObject) //TODO: ver si hay que suplantar con otro script
        {
            Attack();
        }
    }

    private void Attack()
    {
        
    }
    
    public void ApplyDamage(int damage)
    {
        if (health <= 0)
        {
            wetCatSounds.PlayRandomWetCatSound(); // Play the wet cat sound
            Destroy(gameObject);
        }
        
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Cat has been defeated!");
        XPBarManager xpBarManager = FindObjectOfType<XPBarManager>();
         if (xpBarManager != null)
         {
             xpBarManager.AddCatKill(); // Add 100 XP for defeating a cat
         }
        Destroy(gameObject);
    }

}
