using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileWeapon_sc : weapon_sc
{
    public GameObject projectilePrefab; // Prefab for the projectile
    public float projectileSpeed; // Speed of the projectile
    public Transform projectileSpawner;

    public Animator wandAnimator;

    protected override void OnEnable()
    {
        base.OnEnable();
        // Initialize the wand animator if needed
        if (wandAnimator == null)
        {
            wandAnimator = GetComponent<Animator>();
        }
    }

    protected override void PerformAttack()
    {
        // Instantiate the projectile at the fire point
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawner.position, firePoint.rotation);

        // Set the projectile's damage and speed
        projectile_sc projectileScript = projectile.GetComponentInChildren<projectile_sc>();
        if (projectileScript != null)
        {
            projectileScript.damage = damage;
            projectileScript.speed = projectileSpeed;
        }

        wandAnimator.SetTrigger("FinShoot"); // Trigger the fire animation
        currentAmmo--; // Decrease ammo count
        isShooting = false; // Reset shooting state
        wandAnimator.SetTrigger("Reload"); // Trigger the reload animation
        isReloading = true; // Set reloading state
    }

    public override void Update()
    {
        base.Update();

        if(gameManager_sc.currentGameState == gameManager_sc.GameState.Gameplay || gameManager_sc.currentGameState == gameManager_sc.GameState.Waiting)
        {
            HandleAttack();
        }

        // Handle reload input
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < magazineSize)
        {
            currentAmmo = 0;
            wandAnimator.SetTrigger("Reload"); // Trigger the reload animation
            isReloading = true; // Set reloading state
        }

    }

    public void HandleAttack()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            if (!isReloading && !isShooting)
            {
                if (currentAmmo <= 0)
                {
                    wandAnimator.SetTrigger("Reload"); // Trigger the reload animation
                    isReloading = true; // Set reloading state
                }
                else
                {

                    if (Time.time >= nextFireTime)
                    {
                        nextFireTime = Time.time + 1f / fireRate;
                        wandAnimator.SetTrigger("Shoot"); // Trigger the fire animation
                        isShooting = true; // Set shooting state
                    }
                }
            }
        }

    }

    public void ReloadFinLogic()
    {

        currentAmmo = magazineSize; // Reload the weapon
        reloadTimer = 0;        
        wandAnimator.SetTrigger("FinReload"); // Trigger the reload finish animation
        isReloading = false; // Reset reloading state
    }


}
