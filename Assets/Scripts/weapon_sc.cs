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
    public Transform firePoint; // Point from which the weapon fires (e.g., muzzle)   

    public Transform weaponHolder; // Transform of the character's weapon holder (e.g., hand or weapon slot)

    private float nextFireTime = 0f; // Time until the weapon can fire again

    private void OnEnable()
    {
        if (weaponStats != null)
        {
            // Apply data from the ScriptableObject
            weaponName = weaponStats.weaponName;
            description = weaponStats.description;
            damage = weaponStats.damage;
            fireRate = weaponStats.fireRate;
            // You can add more fields here if needed      
            

        }
    }

    // Method to fire the weapon
    public void Fire()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;
            if(muzzleFlash != null)
            {
                muzzleFlash.Play(); // Play the muzzle flash effect
            }
            PerformAttack();
        }


    }

    public void Update()
    {
        // Match the position and rotation of the weapon holder
        transform.position = weaponHolder.position;
        transform.rotation = weaponHolder.rotation;
    }

    // Abstract method for performing the attack
    // This will be implemented differently for projectile and raycast weapons
    protected abstract void PerformAttack();

}
