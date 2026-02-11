using UnityEngine;
using System;

/// <summary>
/// Reusable health component for player and enemies
/// Handles health logic without coupling to specific entity types
/// </summary>
public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    public event Action<float> OnHealthChanged;
    public event Action OnDeath;

    private void Awake()
    {
        if (maxHealth <= 0)
        {
            Debug.LogError($"{gameObject.name} has invalid maxHealth: {maxHealth}");
            maxHealth = 100f;
        }
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (!IsAlive())
            return;

        currentHealth -= damage;
        OnHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        OnHealthChanged?.Invoke(currentHealth);
    }

    public float GetHealthPercent() => currentHealth / maxHealth;
    public bool IsAlive() => currentHealth > 0;

    private void Die()
    {
        OnDeath?.Invoke();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth);
    }
}
