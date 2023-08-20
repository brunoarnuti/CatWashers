using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UI;

public class XPBarManager : MonoBehaviour
{
    public Image xpBarFill; // Reference to the fill image of the XP bar
    private int[] levelThresholds = { 100, 200, 300, 600, 800, 1200 }; // Thresholds for each level
    private int currentLevel = 0;
    private int totalCatsKilled = 0;

    public void AddCatKill()
    {
        totalCatsKilled++;

        // Check if the player has reached the next level
        if (currentLevel < levelThresholds.Length && totalCatsKilled >= levelThresholds[currentLevel])
        {
            currentLevel++;
        }

        // Update the XP bar based on the current level
        float progress = (float)currentLevel / levelThresholds.Length;
        xpBarFill.fillAmount = progress;
    }
}


// public class XPBarManager : MonoBehaviour
// {
//     public Image fillBar; // Reference to the fill bar UI Image
//     public int[] levelThresholds; // Array of experience thresholds for each level
//
//     private int currentLevel = 0;
//     private int currentXP = 0;
//
//     private void Start()
//     {
//         UpdateXPBar();
//     }
//
//     public void AddXP(int amount)
//     {
//         currentXP += amount;
//
//         // Check if the player has reached a new level
//         while (currentLevel < levelThresholds.Length && currentXP >= levelThresholds[currentLevel])
//         {
//             LevelUp();
//         }
//
//         UpdateXPBar();
//     }
//
//     private void LevelUp()
//     {
//         currentLevel++;
//         Debug.Log("Level Up! Current Level: " + currentLevel);
//     }
//
//     private void UpdateXPBar()
//     {
//         // Calculate the fill amount of the bar based on the current XP and level threshold
//         float fillAmount = 0f;
//         if (currentLevel < levelThresholds.Length)
//         {
//             int previousThreshold = currentLevel > 0 ? levelThresholds[currentLevel - 1] : 0;
//             fillAmount = (float)(currentXP - previousThreshold) / (levelThresholds[currentLevel] - previousThreshold);
//         }
//         else
//         {
//             fillAmount = 1f; // Max level reached
//         }
//
//         fillBar.fillAmount = fillAmount;
//     }
// }

