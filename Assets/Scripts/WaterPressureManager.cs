using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPressureManager : MonoBehaviour
{
    public float initialPressure = 100f;
    public float decreaseRate = 1f;
    public float minimumPressure = 10f;
    public float maximumRange = 10f;
    public float minimumRange = 1f;

    private float currentPressure;

    private void Start()
    {
        currentPressure = initialPressure;
    }

    public void Activate()
    {
        InvokeRepeating("DecreasePressure", 0f, 1f); // Decrease pressure every second
    }

    public void Deactivate()
    {
        CancelInvoke("DecreasePressure");
    }

    private void DecreasePressure()
    {
        currentPressure -= decreaseRate;
        currentPressure = Mathf.Max(currentPressure, minimumPressure);
        Debug.Log("Current Pressure: " + currentPressure); // Log the current pressure
    }

    public float GetCurrentRange()
    {
        float rangePercentage = (currentPressure - minimumPressure) / (initialPressure - minimumPressure);
        return Mathf.Lerp(minimumRange, maximumRange, rangePercentage);
    }
}

