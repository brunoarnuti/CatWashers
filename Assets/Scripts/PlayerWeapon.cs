using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerWeapon : MonoBehaviour
{

    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        //public Vector3 shootPosition;
    }
    
    public GameObject waterDropPrefab; // Reference to the WaterDrop prefab
    public float fireRate = 0.1f; // Time between shots
    private bool isFiring = false; // Track whether the gun is firing
    public WaterPressureManager waterPressureManager; // Reference to the WaterPressureManager
    
    private Transform aimTransform;
    private Transform weaponShootPoint;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
        weaponShootPoint = aimTransform.Find("ShootingPoint");
    }

    private void Update()
    {
        HandleAiming();
        HandleShooting();
        
    
    
    }

    private void HandleAiming()
    {
        Vector3 mousePosition = Utils.GetMouseWorldPosition();

        Vector3 aimDirection = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    
        // This part handles when the weapon rotates
        Vector3 localScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            localScale.y = -1f;
        }
        else
        {
            localScale.y = +1f;
        }

        aimTransform.localScale = localScale;
    }

    // private void HandleShooting()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         Vector3 mousePosition = Utils.GetMouseWorldPosition();
    //         OnShoot?.Invoke(this, new OnShootEventArgs()
    //         {
    //             gunEndPointPosition = weaponShootPoint.position,
    //             shootPosition = mousePosition
    //         });   
    //     }
    // }
    
    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isFiring = !isFiring;
            if (isFiring)
            {
                waterPressureManager.Activate(); // Activate pressure management
                StartCoroutine(FireWaterGun());
            }
            else
            {
                waterPressureManager.Deactivate(); // Deactivate pressure management
                StopCoroutine(FireWaterGun());
            }
        }
    }
    
    private IEnumerator FireWaterGun()
    {
        while (isFiring)
        {
            // Get the direction of the shot
            Vector3 mousePosition = Utils.GetMouseWorldPosition();
            Vector3 direction = (mousePosition - weaponShootPoint.position).normalized;

            // Instantiate the water droplet
            GameObject waterDrop = Instantiate(waterDropPrefab, weaponShootPoint.position, Quaternion.identity);

            // Get the current range from the WaterPressureManager
            float currentRange = waterPressureManager.GetCurrentRange();
            Debug.Log("Current Range: " + currentRange); // Log the current range

            // Initialize the water droplet with the direction and current range
            waterDrop.GetComponent<WaterDrop>().Initialize(direction, currentRange);

            yield return new WaitForSeconds(fireRate);
        }
    }

}
