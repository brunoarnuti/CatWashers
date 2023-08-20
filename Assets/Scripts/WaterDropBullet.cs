using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;


public class WaterDrop : MonoBehaviour
{
    public float speed = 5f;
    public float dispersionAngle = 5f; // Angle of dispersion

    private Vector3 direction;
    private float range;
    private Vector3 startPosition;
    public int damage = 1; // Damage dealt by the water drop

    public void Initialize(Vector3 direction, float range)
    {
        this.direction = direction.normalized;
        this.range = range;
        this.startPosition = transform.position; // Store the starting position
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger detected with: " + collision.gameObject.name);
        catEnemy cat = collision.GetComponent<catEnemy>();
        if (cat != null)
        {
            cat.ApplyDamage(damage);
            Destroy(gameObject); // Destroy the water drop after hitting the cat
        }
    }
    

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // Calculate the distance traveled
        float distanceTraveled = Vector3.Distance(transform.position, startPosition);

        // Calculate the dispersion factor based on the distance
        float angle = dispersionAngle * distanceTraveled / range;

        // Apply the dispersion angle to the direction
        Vector3 dispersedDirection = Quaternion.Euler(0, 0, angle) * direction;

        // Move the droplet in the dispersed direction
        transform.position += dispersedDirection * speed * Time.deltaTime;

        // Reduce the range
        range -= speed * Time.deltaTime;

        // Destroy the droplet if it has reached its maximum range
        if (range <= 0)
        {
            Destroy(gameObject);
        }
    }
}



// public class WaterDrop : MonoBehaviour
// {
//     public float speed = 5f;
//     public float dispersionAngle = 5f;
//
//     private Vector3 direction;
//     private float range;
//
//     public void Initialize(Vector3 direction, float range)
//     {
//         // Add a slight random dispersion to the direction
//         float angle = Random.Range(-dispersionAngle, dispersionAngle);
//         this.direction = Quaternion.Euler(0, 0, angle) * direction.normalized;
//         this.range = range;
//     }
//
//     private void Update()
//     {
//         Move();
//     }
//
//     private void Move()
//     {
//         // Move the droplet in the given direction
//         transform.position += direction * speed * Time.deltaTime;
//
//         // Reduce the range
//         range -= speed * Time.deltaTime;
//
//         // Destroy the droplet if it has reached its maximum range
//         if (range <= 0)
//         {
//             Destroy(gameObject);
//         }
//     }
//
//     // private void OnTriggerEnter2D(Collider2D other)
//     // {
//     //     // Handle collision with enemies
//     //     if (other.CompareTag("Enemy"))
//     //     {
//     //         HandleCollision(other.gameObject);
//     //     }
//     // }
//     //
//     // private void HandleCollision(GameObject enemy)
//     // {
//     //     // Implement logic to handle collision with an enemy
//     //     // E.g., reduce enemy health, play a sound effect, etc.
//     //
//     //     // Destroy the droplet
//     //     Destroy(gameObject);
//     // }
// }
