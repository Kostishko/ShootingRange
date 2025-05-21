using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockProjectile_sc : projectile_sc
{

    public GameObject collisionObject;
    public ParticleSystem explosionEffect; // Reference to the explosion effect prefab

    private void Start()
    {
        collisionObject.SetActive(false);
    }
    public override void OnCollisionEnter(Collision collision)
    {
        speed = 0; // Stop the projectile on collision
        // Check if the object hit is on the specified layers
        if(collisionObject != null)
        {
            collisionObject.SetActive(true); // Activate the collision object
        }
        
        explosionEffect.Play(); // Play the explosion effect

        Destroy(this.gameObject, 0.3f); // Destroy the projectile after 1 second

    }


}
