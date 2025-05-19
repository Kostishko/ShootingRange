using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicWand_sc : weapon_sc
{
    public Animator wandAnimator;


    public LayerMask hitLayers; // Layers that the raycast can hit
    public GameObject hitEffectPrefab; // Prefab for the hit effect (optional)

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
       
        
        if (Input.GetButtonDown("Fire1"))
        {
           
            wandAnimator.SetTrigger("ShootTrigger"); // Trigger the fire animation
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


        // Handle reload input
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < magazineSize)
        {
            currentAmmo = 0;
            reloadTimer = reloadTime;
            isReloading = true;
            wandAnimator.SetTrigger("isReloadingAnim"); // Trigger the reload animation
        }

        if (reloadTimer > 0)
        {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0)
            {
                currentAmmo = magazineSize; // Reload the weapon
                reloadTimer = 0;
                isReloading = false; // Reset reloading state
            }
        }

        if (!isReloading)
        {
            wandAnimator.SetBool("isReloadingAnim", false); // Stop the reload animation
        }
    }

    protected override void PerformAttack()
    {

        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hit;

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

        GameObject effect = Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal)); // Instantiate hit effect
        Destroy(effect, 2f); // Destroy the effect after 2 seconds
    }


}
