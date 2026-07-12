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
        SoundFXManager_sc.instance.PlaySoundFXClip(shootSound, this.transform, 1f); // Play the shooting sound
        currentAmmo--; // Decrease ammo count
        isShooting = false; // Reset shooting state
        if (currentAmmo <= 0)
        {
            wandAnimator.SetTrigger("Reload"); // Trigger the reload animation
            SoundFXManager_sc.instance.PlaySoundFXClip(reloadSound, this.transform, 1f); // Play the reload sound
            isReloading = true; // Set reloading state
            reloadTimer = reloadTime;
        }

    }

    public override void Update()
    {
        base.Update();

        if(gameManager_sc.currentGameState == gameManager_sc.GameState.Gameplay || gameManager_sc.currentGameState == gameManager_sc.GameState.Waiting)
        {
            HandleAttack();
            ReloadLogic();
        }

        // Handle reload input
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < magazineSize)
        {
            currentAmmo = 0;
            wandAnimator.SetTrigger("Reload"); // Trigger the reload animation
            SoundFXManager_sc.instance.PlaySoundFXClip(reloadSound, this.transform, 1f); // Play the reload sound
            isReloading = true; // Set reloading state

            
        }

    }

    public void HandleAttack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("You pressed Fire while handle the projectile weapon");

            if (!isReloading && !isShooting)
            {
                Debug.Log("The projectile weapon is not reloading or shooting currently");
                if (currentAmmo <= 0)
                {
                    wandAnimator.SetTrigger("Reload"); // Trigger the reload animation
                    SoundFXManager_sc.instance.PlaySoundFXClip(reloadSound, this.transform, 1f); // Play the reload sound
                    isReloading = true; // Set reloading state
                    reloadTimer = reloadTime;
                    Debug.Log("Current ammo was smaller than zero, so it went to reload");
                }
                else
                {
                    Debug.Log("There were ammo, so should be a shot");


                    if (Time.time >= nextFireTime)
                    {
                        Debug.Log("And even the time for fire time is good enough to make the shot");
                        nextFireTime = Time.time + 1f / fireRate;
                        wandAnimator.SetTrigger("Shoot"); // Trigger the fire animation
                        isShooting = true; // Set shooting state
                        
                    }
                }
            }
        }

    }

 public void ReloadLogic()
    {
        
        if (isReloading)
        {
            if(reloadTime>0)
            {
                reloadTimer -= Time.deltaTime;
            }
            else
            {
                currentAmmo = magazineSize; // Reload the weapon
                reloadTimer = 0;
                wandAnimator.SetTrigger("FinReload"); // Trigger the reload finish animation

                isReloading = false; // Ensure reloading state is reset after reload completes
            }
        }
    }

    public void ReloadFinLogic()
    {
        if (isReloading)
        {
            currentAmmo = magazineSize; // Reload the weapon
            reloadTimer = 0;
            wandAnimator.SetTrigger("FinReload"); // Trigger the reload finish animation
            isReloading = false;
        }
    }

    public override void StopMe()
    {
        if (isReloading)
        {
            isReloading = false; // Reset reloading state
            wandAnimator.SetTrigger("FinReloading"); // Trigger the reload finish animation
        }
        if (isShooting)
        {
            isShooting = false; // Reset shooting state
            wandAnimator.SetTrigger("FinShoot"); // Trigger the fire finish animation
        }
    }

}
