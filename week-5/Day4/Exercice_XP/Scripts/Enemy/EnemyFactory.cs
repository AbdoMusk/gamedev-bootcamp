using UnityEngine;

/// <summary>
/// Factory Pattern - Creates enemies without specifying types
/// Designers can add new enemy types via prefabs and stats without code changes
/// </summary>
public class EnemyFactory
{
    public static Enemy CreateEnemy(EnemyStats stats, Vector3 spawnPosition, Transform player)
    {
        Enemy enemy = EnemyPool.Instance.GetEnemy();
        enemy.transform.position = spawnPosition;
        enemy.Initialize(stats, player);
        return enemy;
    }
}
