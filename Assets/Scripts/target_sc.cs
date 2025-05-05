using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target_sc : MonoBehaviour
{
    public float health = 100f; // Total health of the target

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
        Destroy(gameObject);
    }
}
