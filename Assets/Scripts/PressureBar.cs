using UnityEngine;
using UnityEngine.UI;

public class PressureBar : MonoBehaviour
{
    public WaterPressureManager waterPressureManager; // Reference to the WaterPressureManager
    public Image pressureBarFill; // Reference to the PressureBarFill Image

    private void Update()
    {
        float pressurePercentage = waterPressureManager.GetCurrentPressure() / waterPressureManager.initialPressure;
        pressureBarFill.fillAmount = pressurePercentage; // Set the fill amount based on the current pressure
    }
}
