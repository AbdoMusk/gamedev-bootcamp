using UnityEngine;

/// <summary>
/// Handles player health and damage
/// Communicates via events only
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    private HealthComponent health;

    private void Start()
    {
        health = GetComponent<HealthComponent>();
        if (health == null)
        {
            Debug.LogError("Player missing HealthComponent!");
            return;
        }
        health.OnDeath += OnPlayerDeath;
        health.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(float currentHealth)
    {
        EventManager.InvokePlayerDamaged(currentHealth);
    }

    private void OnPlayerDeath()
    {
        EventManager.InvokePlayerDeath();
    }

    private void OnDestroy()
    {
        if (health != null)
        {
            health.OnDeath -= OnPlayerDeath;
            health.OnHealthChanged -= OnHealthChanged;
        }
    }
}
