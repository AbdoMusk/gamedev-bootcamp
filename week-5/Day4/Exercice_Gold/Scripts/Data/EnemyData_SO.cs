using UnityEngine;

/// <summary>
/// Flyweight Pattern - Shared immutable enemy data
/// Only maxHealth and respawnDelay. No instances store this data.
/// </summary>
[CreateAssetMenu(fileName = "EnemyData", menuName = "Gameplay/Enemy Data")]
public class EnemyData_SO : ScriptableObject
{
    [SerializeField] public float maxHealth = 20f;
    [SerializeField] public float respawnDelay = 2f;

    // Optional (helps your "enemy follows me" expectation, still data-driven)
    [SerializeField] public float moveSpeed = 2.5f;
    [SerializeField] public float attackDamage = 5f;
    [Header("Knockback")]
    [SerializeField] public float knockbackDistance = 1.5f;
    [SerializeField] public float knockbackSpeed = 6f;
}
