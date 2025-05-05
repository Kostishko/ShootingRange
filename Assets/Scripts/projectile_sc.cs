using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class projectile_sc : MonoBehaviour
{
    public float speed = 20f; // Speed of the projectile
    public float lifetime = 5f; // Time before the projectile is destroyed
    public float damage; // Damage dealt by the projectile
    public LayerMask hitLayers; // Layers the projectile can hit

    private void Start()
    {
        // Destroy the projectile after its lifetime expires
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Move the projectile forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object hit is on the specified layers
        if (((1 << other.gameObject.layer) & hitLayers) != 0)
        {
            // Try to get the Target script on the object
            target_sc target = other.GetComponent<target_sc>();
            if (target != null)
            {
                // Apply damage to the target
                target.TakeDamage(damage);
            }

            // Destroy the projectile after hitting something
            Destroy(gameObject);
        }
    }
}
