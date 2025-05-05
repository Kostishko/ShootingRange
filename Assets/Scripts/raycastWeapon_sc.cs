using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycastWeapon_sc : weapon_sc
{
    public LayerMask hitLayers; // Layers that the raycast can hit
    public GameObject hitEffectPrefab; // Prefab for the hit effect (optional)

    protected override void PerformAttack()
    {
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range, hitLayers))
        {
            // Apply damage to the hit object if it has a health component
            Debug.Log($"Hit {hit.collider.name} and dealt {damage} damage.");
        }

        GameObject effect = Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal)); // Instantiate hit effect
        Destroy(effect, 2f); // Destroy the effect after 2 seconds

    }
}
