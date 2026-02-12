using UnityEngine;
using System.Collections;

/// <summary>
/// Facade - Only orchestration, NO Update() logic
/// Spawn, damage, respawn are called by UI buttons only
/// </summary>
public class GameFacade : MonoBehaviour
{
    public static GameFacade Instance { get; private set; }

    [SerializeField] private EnemyData_SO enemyData;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private EnemyPool enemyPool;

    [Header("Optional")]
    [SerializeField] private Transform followTarget;

    [Header("Damage Simulation")]
    [SerializeField] private float simulatedDamage = 5f;

    private Enemy currentEnemy;
    private Coroutine respawnCoroutine;

    private void OnEnable()
    {
        EventManager.OnEnemyDied += HandleEnemyDied;
    }

    private void OnDisable()
    {
        EventManager.OnEnemyDied -= HandleEnemyDied;
    }

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
        // Validate all required fields
        if (enemyData == null)
        {
            Debug.LogError("GameFacade: EnemyData_SO not assigned!");
            return;
        }
        if (spawnPoint == null)
        {
            Debug.LogError("GameFacade: SpawnPoint not assigned!");
            return;
        }
        if (enemyPool == null)
        {
            enemyPool = FindObjectOfType<EnemyPool>();
            if (enemyPool == null)
            {
                Debug.LogError("GameFacade: EnemyPool not found in scene!");
                return;
            }
        }

        // Initialize first spawn
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        if (enemyPool == null)
        {
            Debug.LogError("GameFacade: EnemyPool is null!");
            return;
        }

        // Pool size is 1: always request the instance from pool
        currentEnemy = enemyPool.GetEnemy();

        if (currentEnemy == null)
        {
            Debug.LogError("GameFacade: Pool returned null!");
            return;
        }

        currentEnemy.transform.position = spawnPoint.position;
        currentEnemy.Initialize(enemyData);

        if (followTarget != null)
        {
            currentEnemy.SetFollowTarget(followTarget);
        }

        currentEnemy.Activate();

        // Stop any pending respawn
        if (respawnCoroutine != null)
        {
            StopCoroutine(respawnCoroutine);
        }
    }

    public void DamageEnemy(float damage)
    {
        // Event-driven: Facade publishes, Enemy reacts (no direct call)
        EventManager.InvokeEnemyDamageRequested(damage);
    }

    // Recommended for UI button: avoids hard-coded values in code/OnClick params
    public void SimulateDamage()
    {
        DamageEnemy(simulatedDamage);
    }

    private void HandleEnemyDied()
    {
        if (enemyPool != null && currentEnemy != null)
        {
            enemyPool.ReturnEnemy(currentEnemy);
            currentEnemy = null;
        }

        if (respawnCoroutine != null)
        {
            StopCoroutine(respawnCoroutine);
        }

        respawnCoroutine = StartCoroutine(RespawnDelay());
    }

    private IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(enemyData.respawnDelay);
        SpawnEnemy();
    }
}
