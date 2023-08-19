using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    public float speed = 5f;
    public float dispersionAngle = 5f;

    private Vector3 direction;
    private float range;

    public void Initialize(Vector3 direction, float range)
    {
        // Add a slight random dispersion to the direction
        float angle = Random.Range(-dispersionAngle, dispersionAngle);
        this.direction = Quaternion.Euler(0, 0, angle) * direction.normalized;
        this.range = range;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // Move the droplet in the given direction
        transform.position += direction * speed * Time.deltaTime;

        // Reduce the range
        range -= speed * Time.deltaTime;

        // Destroy the droplet if it has reached its maximum range
        if (range <= 0)
        {
            Destroy(gameObject);
        }
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     // Handle collision with enemies
    //     if (other.CompareTag("Enemy"))
    //     {
    //         HandleCollision(other.gameObject);
    //     }
    // }
    //
    // private void HandleCollision(GameObject enemy)
    // {
    //     // Implement logic to handle collision with an enemy
    //     // E.g., reduce enemy health, play a sound effect, etc.
    //
    //     // Destroy the droplet
    //     Destroy(gameObject);
    // }
}
