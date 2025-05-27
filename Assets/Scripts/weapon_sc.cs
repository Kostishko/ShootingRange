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
    public bool isShooting; 
    public Transform firePoint; // Point from which the weapon fires (e.g., muzzle)   

    public Transform weaponHolder; // Transform of the character's weapon holder

    protected float nextFireTime = 0f; // Time until the weapon can fire again

    //Sounds
    public AudioClip shootSound; // Sound effect for shooting
    public AudioClip reloadSound; // Sound effect for reloading

    public void Start()
    {
        // Initialize ammo count
        currentAmmo = magazineSize; // Start with a full magazine
        isReloading = false; // Not reloading at the start
        isShooting = false; // Not shooting at the start
    }

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
          
            // You can add more fields here if needed      
            reloadTimer = 0;

        }

        
    }

    public virtual void Update()
    {
        // Match the position and rotation of the weapon holder
        transform.position = weaponHolder.position;
        transform.rotation = weaponHolder.rotation;
    }

    // Abstract method for performing the attack
    // This will be implemented differently for projectile and raycast weapons
    protected abstract void PerformAttack();

    public abstract void StopMe();

}
