using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine;

public class WaterPressureManager : MonoBehaviour
{
    [Header("Pressure Configuration")]
    public float initialPressure = 100f;
    public float decreaseRate = 100f;
    public float minimumPressure = 10f;
    public float maximumRange = 10f;
    public float minimumRange = 1f;
    public float pressureExponent = 2f; // Exponent for the exponential curve

    [Header("Pumping Configuration")]
    public float pressurePerPump = 5f;
    public float comboInterval = 0.5f; // Time window for consecutive pumps to count as a combo
    public float comboMultiplierIncrement = 0.1f; // Increment to the multiplier for each combo level

    private float currentPressure;
    private float lastPumpTime = 0f;
    private int pumpCombo = 0;

    private void Start()
    {
        currentPressure = initialPressure;
    }

    public void Pump()
    {
        HandlePumping();
    }

    public void Activate()
    {
        InvokeRepeating("DecreasePressure", 0f, 1f); // Decrease pressure every second
    }

    public void Deactivate()
    {
        CancelInvoke("DecreasePressure");
    }

    public float GetCurrentRange()
    {
        float rangePercentage = (currentPressure - minimumPressure) / (initialPressure - minimumPressure);
        return Mathf.Lerp(minimumRange, maximumRange, rangePercentage);
    }

    public float GetCurrentPressure()
    {
        return currentPressure;
    }

    public int GetPumpCombo()
    {
        return pumpCombo;
    }

    private void HandlePumping()
    {
        float timeSinceLastPump = Time.time - lastPumpTime;

         if (timeSinceLastPump < comboInterval)
         {
             pumpCombo++; // Increment the combo if within the interval
         }
         else
         {
             pumpCombo = 0; // Reset the combo if outside the interval
         }

         float comboMultiplier = 1f + pumpCombo * comboMultiplierIncrement; // Calculate the multiplier
         float pressureToAdd = pressurePerPump * comboMultiplier;

         // Apply the piecewise function
         float pressurePercentage = currentPressure / initialPressure;
         if (pressurePercentage < 0.1f)
         {
             pressureToAdd *= 0.5f; // Slow growth near 0%
         }
         else if (pressurePercentage < 0.65f)
         {
             pressureToAdd *= 5f; // Very fast growth in the 10% to 65% range
             pressureToAdd *= Mathf.Clamp(1f + (comboInterval - timeSinceLastPump) * 2f, 1f, 3f); // Faster growth for faster hits within this range
         }
         else if (pressurePercentage < 0.75f)
         {
             pressureToAdd *= 1f; // Moderate growth near 65%
         }
         else if (pressurePercentage < 0.85f)
         {
             pressureToAdd *= 0.5f; // Slow growth near 75%
         }
         else
         {
             pressureToAdd *= 0.1f; // Very slow growth near 85%, making 100% nearly impossible
         }

         currentPressure += pressureToAdd; // Add the pressure
         currentPressure = Mathf.Min(currentPressure, initialPressure); // Cap the pressure

         lastPumpTime = Time.time; // Update the last pump time
    }

    private void DecreasePressure()
    {
        currentPressure -= decreaseRate;
        currentPressure = Mathf.Max(currentPressure, minimumPressure);
    }
}


//
// public class WaterPressureManager : MonoBehaviour
// {
//     public float initialPressure = 100f;
//     public float decreaseRate = 100f;
//     public float minimumPressure = 10f;
//     public float maximumRange = 10f;
//     public float minimumRange = 1f;
//     public float pressureExponent = 2f; // Exponent for the exponential curve
//
//     public float pressurePerPump = 5f;
//     
//     private float lastPumpTime = 0f;
//     private int pumpCombo = 0;
//     public float comboInterval = 0.5f; // Time window for consecutive pumps to count as a combo
//     public float comboMultiplierIncrement = 0.1f; // Increment to the multiplier for each combo level
//
//     private float currentPressure;
//
//     private void Start()
//     {
//         currentPressure = initialPressure;
//     }
//     
//     public void Pump()
//     {
//         float timeSinceLastPump = Time.time - lastPumpTime;
//
//         if (timeSinceLastPump < comboInterval)
//         {
//             pumpCombo++; // Increment the combo if within the interval
//         }
//         else
//         {
//             pumpCombo = 0; // Reset the combo if outside the interval
//         }
//
//         float comboMultiplier = 1f + pumpCombo * comboMultiplierIncrement; // Calculate the multiplier
//         float pressureToAdd = pressurePerPump * comboMultiplier;
//
//         // Apply the piecewise function
//         float pressurePercentage = currentPressure / initialPressure;
//         if (pressurePercentage < 0.1f)
//         {
//             pressureToAdd *= 0.5f; // Slow growth near 0%
//         }
//         else if (pressurePercentage < 0.65f)
//         {
//             pressureToAdd *= 5f; // Very fast growth in the 10% to 65% range
//             pressureToAdd *= Mathf.Clamp(1f + (comboInterval - timeSinceLastPump) * 2f, 1f, 3f); // Faster growth for faster hits within this range
//         }
//         else if (pressurePercentage < 0.75f)
//         {
//             pressureToAdd *= 1f; // Moderate growth near 65%
//         }
//         else if (pressurePercentage < 0.85f)
//         {
//             pressureToAdd *= 0.5f; // Slow growth near 75%
//         }
//         else
//         {
//             pressureToAdd *= 0.1f; // Very slow growth near 85%, making 100% nearly impossible
//         }
//
//         currentPressure += pressureToAdd; // Add the pressure
//         currentPressure = Mathf.Min(currentPressure, initialPressure); // Cap the pressure
//
//         lastPumpTime = Time.time; // Update the last pump time
//     }
//
//
//
//
//     
//     public int GetPumpCombo()
//     {
//         return pumpCombo;
//     }
//
//
//     public void Activate()
//     {
//         InvokeRepeating("DecreasePressure", 0f, 1f); // Decrease pressure every second
//     }
//
//     public void Deactivate()
//     {
//         CancelInvoke("DecreasePressure");
//     }
//
//     private void DecreasePressure()
//     {
//         currentPressure -= decreaseRate;
//         currentPressure = Mathf.Max(currentPressure, minimumPressure);
//         Debug.Log("Current Pressure: " + currentPressure); // Log the current pressure
//     }
//
//     public float GetCurrentRange()
//     {
//         float rangePercentage = (currentPressure - minimumPressure) / (initialPressure - minimumPressure);
//         return Mathf.Lerp(minimumRange, maximumRange, rangePercentage);
//     }
//     
//     public float GetCurrentPressure()
//     {
//         return currentPressure;
//     }
//
// }
//

// public class WaterPressureManager : MonoBehaviour
// {
//     public float initialPressure = 100f;
//     public float decreaseRate = 1f;
//     public float minimumPressure = 10f;
//     public float maximumRange = 10f;
//     public float minimumRange = 1f;
//
//     private float currentPressure;
//
//     private void Start()
//     {
//         currentPressure = initialPressure;
//     }
//
//     public void Activate()
//     {
//         InvokeRepeating("DecreasePressure", 0f, 1f); // Decrease pressure every second
//     }
//
//     public void Deactivate()
//     {
//         CancelInvoke("DecreasePressure");
//     }
//
//     private void DecreasePressure()
//     {
//         currentPressure -= decreaseRate;
//         currentPressure = Mathf.Max(currentPressure, minimumPressure);
//         Debug.Log("Current Pressure: " + currentPressure); // Log the current pressure
//     }
//
//     public float GetCurrentRange()
//     {
//         float rangePercentage = (currentPressure - minimumPressure) / (initialPressure - minimumPressure);
//         return Mathf.Lerp(minimumRange, maximumRange, rangePercentage);
//     }
//     
//     public float GetCurrentPressure()
//     {
//         return currentPressure;
//     }
// }
//
