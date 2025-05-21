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
        transform.parent.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        speed = 0; // Stop the projectile on collision
        // Check if the object hit is on the specified layers
        if (collision.gameObject.GetComponentInParent<target_sc>()!=null)
        {
            // Try to get the Target script on the object
            target_sc target = collision.collider.GetComponentInParent<target_sc>();
            if (target != null)
            {
                if (collision.collider.gameObject == target.critZone)
                {
                    target.TakeDamage(damage * 2); // Double damage for critical hit
                    Debug.Log("Critical hit!");
                }
                else if (collision.collider.gameObject == target.hitZone)
                {
                    target.TakeDamage(damage);  // damage for hit zone
                    Debug.Log("Hit zone hit!");
                }
            }


        }
        // Destroy the projectile after hitting something
        Destroy(gameObject, 1f);
    }

   
}
