using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Object Pooling - Minimal pool of size 1 for this assignment
/// Guarantees no instantiation during gameplay
/// </summary>
public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance { get; private set; }

    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private int poolSize = 1; // Exactly 1 enemy for this assignment

    private readonly Queue<Enemy> availableEnemies = new Queue<Enemy>();
    private bool isInitialized;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Ensure pool is ready before any Start() calls (e.g., GameFacade.Start)
        TryInitializePool();
    }

    private void Start() { }

    private void TryInitializePool()
    {
        if (isInitialized) return;

        if (enemyPrefab == null)
        {
            Debug.LogError("EnemyPool: Prefab not assigned! Assign Enemy.prefab in the inspector.");
            return;
        }

        InitializePool();
        isInitialized = true;
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            Enemy enemy = Instantiate(enemyPrefab);
            enemy.gameObject.SetActive(false);
            availableEnemies.Enqueue(enemy);
        }
    }

    public Enemy GetEnemy()
    {
        if (!isInitialized)
        {
            TryInitializePool();
        }

        if (availableEnemies.Count > 0)
        {
            Enemy enemy = availableEnemies.Dequeue();
            return enemy;
        }
        
        Debug.LogError("EnemyPool: No available enemies!");
        return null;
    }

    public void ReturnEnemy(Enemy enemy)
    {
        if (enemy == null) return;

        // Reset state when returning to pool
        enemy.gameObject.SetActive(false);

        // Guard against double-enqueue (pool size is 1, so O(n) is fine)
        foreach (Enemy queued in availableEnemies)
        {
            if (queued == enemy) return;
        }

        availableEnemies.Enqueue(enemy);
    }
}

