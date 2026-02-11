using UnityEngine;

/// <summary>
/// Receives damage and delegates to HealthComponent
/// Decouples damage dealing from health logic
/// </summary>
public class DamageReceiver : MonoBehaviour
{
    private HealthComponent health;

    private void Start()
    {
        health = GetComponent<HealthComponent>();
    }

    public void ReceiveDamage(float damage)
    {
        if (health != null)
        {
            health.TakeDamage(damage);
        }
    }

    public bool IsAlive() => health.IsAlive();
}
