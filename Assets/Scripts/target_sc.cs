using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target_sc : MonoBehaviour
{
    public float health = 100f; // Total health of the target

    public GameObject critZone; // Reference to the critical hit zone (optional)
    public GameObject hitZone; // Reference to the hit zone (optional)

    public ParticleSystem DieEffect; // Particle effect for when the target dies

    public Transform healthBarPosition; // Position of the health bar (optional)
    public Transform damageInfoPosition; // Position of the damage info (optional)

    public event EventHandler OnDestroyed; // Event triggered when the target is destroyed

    public GameObject character;

    

    public GameObject targetBody;

    public void Update()
    {
        if (this.character != null)
        {
            var lookatTarget = character.transform.position;
            lookatTarget.y = transform.position.y;
            transform.LookAt(lookatTarget);

            //gameObject.transform.rotation = new Quaternion(0, temp.rotation.y, 0, 1);


        }
    }



    public void TakeDamage(float damage)
    {
        // Reduce health by the damage amount
        health -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage. Remaining health: {health}");

        // Check if the target's health is depleted
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle the target's death (e.g., destroy the object)
        Debug.Log($"{gameObject.name} has been destroyed.");
        DieEffect.Play(); // Play the death effect
        OnDestroyed?.Invoke(this, EventArgs.Empty); // Trigger the destroyed event
        Destroy(gameObject, 0.2f);
    }


}
