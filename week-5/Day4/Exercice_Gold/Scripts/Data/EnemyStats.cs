using UnityEngine;

/// <summary>
/// Flyweight Pattern - Shared enemy stats data
/// Multiple enemies can use the same stats asset
/// Only runtime values are stored per-instance in Enemy script
/// </summary>
[CreateAssetMenu(fileName = "EnemyStats", menuName = "Gameplay/Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    [SerializeField] public float maxHealth = 30f;
    [SerializeField] public float moveSpeed = 2f;
    [SerializeField] public float attackDamage = 5f;
    [SerializeField] public float attackRange = 1f;
    [SerializeField] public float attackCooldown = 1.5f;
    [SerializeField] public int scoreValue = 10;
}
