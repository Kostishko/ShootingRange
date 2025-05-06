using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileWeapon_sc : weapon_sc
{
    public GameObject projectilePrefab; // Prefab for the projectile
    public float projectileSpeed; // Speed of the projectile
    public Transform projectileSpawner;

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
    }
}
