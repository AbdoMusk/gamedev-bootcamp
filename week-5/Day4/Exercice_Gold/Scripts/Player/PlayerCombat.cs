using UnityEngine;

/// <summary>
/// Handles player combat (attacks)
/// Uses simple raycast for damage detection
/// </summary>
public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackCooldown = 0.5f;

    private float lastAttackTime;

    public void Attack()
    {
        // Don't attack if game is paused
        if (Time.timeScale == 0f)
            return;

        if (Time.time - lastAttackTime < attackCooldown)
            return;

        lastAttackTime = Time.time;

        // Simple circle overlap attack detection
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);

        bool hitEnemy = false;
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                Debug.Log("PlayerCombat: Attacking enemy " + hit.name);
                DamageReceiver damageReceiver = hit.GetComponent<DamageReceiver>();
                if (damageReceiver != null && damageReceiver.IsAlive())
                {
                    Debug.Log("PlayerCombat: Damaging enemy " + hit.name);
                    damageReceiver.ReceiveDamage(attackDamage);
                    hitEnemy = true;
                }
            }
        }

        if (!hitEnemy && hits.Length == 0)
        {
            Debug.LogWarning("Attack: No colliders in range. Check if enemies have CircleCollider2D and 'Enemy' tag!");
        }
    }
}
