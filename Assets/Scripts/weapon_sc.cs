using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public abstract class weapon_sc : MonoBehaviour
{
    public WeaponStats weaponStats; // Reference to the ScriptableObject
    public ParticleSystem muzzleFlash; // Visual effect for the weapon's muzzle flash
    public string weaponName; // Name of the weapon
    public string description; // Description of the weapon
    public float damage; // Damage dealt by the weapon
    public float fireRate; // Rate of fire (shots per second)
    public float range; // Range of the weapon (useful for raycasting weapons)
    public int magazineSize; // Magazine size
    public int currentAmmo; // Current ammo count
    public float reloadTime; // Reload time for the weapon
    public float reloadTimer;
    public bool isReloading;
    public Transform firePoint; // Point from which the weapon fires (e.g., muzzle)   

    public Transform weaponHolder; // Transform of the character's weapon holder

    private float nextFireTime = 0f; // Time until the weapon can fire again

    

    protected virtual void OnEnable()
    {
        if (weaponStats != null)
        {
            // Apply data from the ScriptableObject
            weaponName = weaponStats.weaponName;
            description = weaponStats.description;
            damage = weaponStats.damage;
            fireRate = weaponStats.fireRate;
            magazineSize = weaponStats.magazineSize;
            reloadTime = weaponStats.reloadTime; // Reload time for the weapon
            currentAmmo = magazineSize; // Initialize current ammo to magazine size
            // You can add more fields here if needed      
            reloadTimer = 0;

        }

        
    }

    // Method to fire the weapon
    public virtual void Fire()
    {
        if (currentAmmo <= 0)
        {
            if (!isReloading)
            { 
                reloadTimer = reloadTime; // Start the reload timer
                isReloading = true; // Set reloading state
            }

        }
        else
        {

            if (Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + 1f / fireRate;
                if (muzzleFlash != null)
                {
                    muzzleFlash.Play(); // Play the muzzle flash effect
                }
                currentAmmo--; // Decrease ammo count
                PerformAttack();
            }
        }

    }

    public virtual void Update()
    {
        // Match the position and rotation of the weapon holder
        transform.position = weaponHolder.position;
        transform.rotation = weaponHolder.rotation;

        if(reloadTimer > 0)
        {
            reloadTimer -= Time.deltaTime;
            if(reloadTimer <= 0)
            {
                currentAmmo = magazineSize; // Reload the weapon
                reloadTimer = 0;
                isReloading = false; // Reset reloading state
            }
        }

        //if(Input.GetKeyDown(KeyCode.R) && currentAmmo<magazineSize)
        //{
        //    currentAmmo = 0; // Empty the magazine
        //    reloadTimer = reloadTime; // Start the reload timer
        //    isReloading = true; // Set reloading state
        //}
    }

    // Abstract method for performing the attack
    // This will be implemented differently for projectile and raycast weapons
    protected abstract void PerformAttack();

}
