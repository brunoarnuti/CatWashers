using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameOver : MonoBehaviour
{
    public GameObject gameOverPanel;

    public void Start()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        player.OnGameOver += HandleGameOver;
    }

    public void HandleGameOver()
    {
        Debug.Log("Game Over");
        gameOverPanel.SetActive(true);
    }
}
