using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager2 : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] Vector2 spawnArea;
    [SerializeField] float spawnTimer;
    [SerializeField] GameObject player;
    float timer;
    [SerializeField] LayerMask obstacleLayer; // Layer containing the obstacles
    [SerializeField] float obstacleCheckRadius = 0.5f; // Radius to check for obstacles

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f )
        {
            SpawnEnemy();
            timer = spawnTimer;
        }
    }
    
    private void SpawnEnemy()
    {
        Vector3 position = GenerateRandomPosition();

        // Check for obstacles at the generated position
        while (Physics2D.OverlapCircle(position + player.transform.position, obstacleCheckRadius, obstacleLayer) != null)
        {
            // If an obstacle is found, regenerate the position
            position = GenerateRandomPosition();
        }

        position += player.transform.position;

        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = position;
        newEnemy.GetComponent<catEnemy>().SetTarget(player);
        newEnemy.transform.parent = transform;
    }

    private Vector3 GenerateRandomPosition()
    {

        Vector3 position = new Vector3();

        float f = UnityEngine.Random.value > 0.5f ? -1f : 1f;
        if (UnityEngine.Random.value > 0.5f)
        {
            position.x = UnityEngine.Random.Range(-spawnArea.x, spawnArea.x);
            position.y = spawnArea.y * f;
        }
        else
        {
            position.y = UnityEngine.Random.Range(-spawnArea.y, spawnArea.y);
            position.x = spawnArea.x * f;

        }

        position.z = 0;

        return position;
    }
}
