using UnityEngine;

/// <summary>
/// Factory Pattern - Creates enemies (kept for compatibility)
/// </summary>
public class EnemyFactory
{
    public static Enemy CreateEnemy(EnemyData_SO data, Vector3 spawnPosition, GameFacade facade)
    {
        Enemy enemy = EnemyPool.Instance.GetEnemy();
        if (enemy == null) return null;
        
        enemy.transform.position = spawnPosition;
        enemy.Initialize(data);
        return enemy;
    }
}
