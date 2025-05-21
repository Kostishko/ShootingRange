using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicWand_sc : weapon_sc
{
    public Animator wandAnimator;


    public LayerMask hitLayers; // Layers that the raycast can hit
    public GameObject hitEffectPrefab; // Prefab for the hit effect (optional)

    public AudioClip shootWaveSound;

    protected override void OnEnable()
    {
        base.OnEnable();
        // Initialize the wand animator if needed
        if (wandAnimator == null)
        {
            wandAnimator = GetComponent<Animator>();
        }
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
            wandAnimator.SetTrigger("ReloadingAnim"); // Trigger the reload animation
            SoundFXManager_sc.instance.PlaySoundFXClip(reloadSound, this.transform, 1f); // Play the reload sound
            isReloading = true; // Set reloading state
        }

   
    }

    protected override void PerformAttack()
    {

        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hit;
        if (muzzleFlash != null)
        {
            muzzleFlash.Play(); // Play the muzzle flash effect
        }
        if (Physics.Raycast(ray, out hit, range, hitLayers))
        {
            // Apply damage to the hit object if it has a health component
            Debug.Log($"Hit {hit.collider.name} and dealt {damage} damage.");

            target_sc target = hit.collider.GetComponentInParent<target_sc>();
            if (target != null)
            {
                if (hit.collider.gameObject == target.critZone)
                {
                    target.TakeDamage(damage * 2); // Double damage for critical hit
                    Debug.Log("Critical hit!");
                }
                else if (hit.collider.gameObject == target.hitZone)
                {
                    target.TakeDamage(damage);  // damage for hit zone
                    Debug.Log("Hit zone hit!");
                }
            }

        }
        wandAnimator.SetTrigger("FinShoot"); // Trigger the fire animation
        SoundFXManager_sc.instance.PlaySoundFXClip(shootSound, this.transform, 1f); // Play the shooting sound
        currentAmmo--; // Decrease ammo count
        isShooting = false; // Reset shooting state
        GameObject effect = Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal)); // Instantiate hit effect
        Destroy(effect, 2f); // Destroy the effect after 2 seconds
    }

    public void HandleAttack()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            if (!isReloading && !isShooting)
            {
                if (currentAmmo <= 0)
                {
                    wandAnimator.SetTrigger("ReloadingAnim"); // Trigger the reload animation
                    SoundFXManager_sc.instance.PlaySoundFXClip(reloadSound, this.transform, 1f); // Play the reload sound
                    isReloading = true; // Set reloading state
                }
                else
                {

                    if (Time.time >= nextFireTime)
                    {
                        nextFireTime = Time.time + 1f / fireRate;
                        wandAnimator.SetTrigger("ShootTrigger"); // Trigger the fire animation
                        SoundFXManager_sc.instance.PlaySoundFXClip(shootWaveSound, this.transform, 1f); // Play the shooting sound
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
        isReloading = false; // Reset reloading state
        wandAnimator.SetTrigger("FinReloading"); // Trigger the reload finish animation
        isReloading = false; // Reset reloading state
    }




}
