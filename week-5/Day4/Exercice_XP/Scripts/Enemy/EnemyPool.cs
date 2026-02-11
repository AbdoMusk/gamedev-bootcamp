using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Object Pooling Pattern - Reuses enemy objects
/// Zero instantiation during gameplay for performance
/// </summary>
public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance { get; private set; }

    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private int initialPoolSize = 20;

    private Queue<Enemy> availableEnemies = new Queue<Enemy>();
    private HashSet<Enemy> activeEnemies = new HashSet<Enemy>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("EnemyPool: Enemy prefab not assigned!");
            return;
        }
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            Enemy enemy = Instantiate(enemyPrefab);
            enemy.gameObject.SetActive(false);
            availableEnemies.Enqueue(enemy);
        }
    }

    public Enemy GetEnemy()
    {
        Enemy enemy;

        if (availableEnemies.Count > 0)
        {
            enemy = availableEnemies.Dequeue();
        }
        else
        {
            enemy = Instantiate(enemyPrefab);
        }

        enemy.gameObject.SetActive(true);
        activeEnemies.Add(enemy);
        return enemy;
    }

    public void ReturnEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        activeEnemies.Remove(enemy);
        availableEnemies.Enqueue(enemy);
    }

    public int GetActiveEnemyCount() => activeEnemies.Count;
}
