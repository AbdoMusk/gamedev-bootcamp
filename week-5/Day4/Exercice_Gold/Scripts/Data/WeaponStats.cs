using UnityEngine;

/// <summary>
/// Weapon stats data for player
/// Data-driven design - can be modified in inspector without code changes
/// </summary>
[CreateAssetMenu(fileName = "WeaponStats", menuName = "Gameplay/Weapon Stats")]
public class WeaponStats : ScriptableObject
{
    [SerializeField] public float damage = 10f;
    [SerializeField] public float range = 1f;
    [SerializeField] public float cooldown = 0.5f;
}
