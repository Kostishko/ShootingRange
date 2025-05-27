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

            target_sc target = hit.collider.GetComponentInParent<target_sc>();
            if(target != null)
            {
                if(hit.collider.gameObject == target.critZone)
                {
                    target.TakeDamage (damage *2); // Double damage for critical hit
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

    public override void StopMe()
    {
        throw new System.NotImplementedException();
    }
}
