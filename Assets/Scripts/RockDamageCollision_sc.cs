using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDamageCollision_sc : MonoBehaviour
{
    public projectile_sc projectile; // Reference to the projectile script

    public AudioClip hitSound; // Sound effect for hitting the target

    private bool isHitted = false; // Flag to check if the projectile has already hit something



    private void OnCollisionEnter(Collision collision)
    {
      
        if (collision.gameObject.GetComponentInParent<target_sc>() != null)
        {
            // Try to get the Target script on the object
            target_sc target = collision.collider.GetComponentInParent<target_sc>();
            if (target != null)
            {
                if (collision.collider.gameObject == target.critZone)
                {
                    target.TakeDamage(projectile.damage * 2); // Double damage for critical hit
                    Debug.Log("Critical hit!");
                }
                else if (collision.collider.gameObject == target.hitZone)
                {
                    target.TakeDamage(projectile.damage);  // damage for hit zone
                    Debug.Log("Hit zone hit!");
                }
            }


        }
        // Destroy the projectile after hitting something
        if (!isHitted)
        {
            isHitted = true; // Set the flag to true to prevent multiple hits
            SoundFXManager_sc.instance.PlaySoundFXClip(hitSound, this.transform, 1f); // Play the hit sound
        }

        Destroy(gameObject, 0.2f);
    }
}
