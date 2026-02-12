using UnityEngine;

/// <summary>
/// Difficulty configuration - scales game difficulty
/// Data-driven design for tuning gameplay without code changes
/// </summary>
[CreateAssetMenu(fileName = "DifficultyConfig", menuName = "Gameplay/Difficulty Config")]
public class DifficultyConfig : ScriptableObject
{
    [SerializeField] public int maxEnemies = 10;
    [SerializeField] public float spawnRate = 2f; // enemies per second
    [SerializeField] public float enemyHealthMultiplier = 1f;
    [SerializeField] public float enemyDamageMultiplier = 1f;
}
