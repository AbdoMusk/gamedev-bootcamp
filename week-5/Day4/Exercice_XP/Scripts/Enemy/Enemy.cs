using UnityEngine;

/// <summary>
/// Enemy AI - Combines State Machine and Strategy Pattern
/// No duplicated health logic - uses HealthComponent
/// All logic goes through states, not Update
/// </summary>
public class Enemy : MonoBehaviour
{
    [HideInInspector] public EnemyStats stats;
    [HideInInspector] public IMovementStrategy movement;
    [HideInInspector] public float detectionRange = 5f;

    private EnemyState currentState;
    private HealthComponent health;
    private Transform playerTransform;

    public void Initialize(EnemyStats statsData, Transform player)
    {
        if (statsData == null || player == null)
        {
            Debug.LogError("Enemy initialization failed: statsData or player is null!");
            return;
        }

        stats = statsData;
        playerTransform = player;
        health = GetComponent<HealthComponent>();
        movement = new ChaseMovementStrategy(); // Can be changed

        if (health == null)
        {
            Debug.LogError("Enemy missing HealthComponent!");
            return;
        }

        // Reset health FIRST before subscribing to events
        health.ResetHealth();

        // Then subscribe to death event
        health.OnDeath += OnDeath;

        SetState(new IdleState(this));
    }

    public void SetState(EnemyState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }

    private void OnDeath()
    {
        SetState(new DeadState(this));
        EventManager.InvokeEnemyDeath(stats.scoreValue);
        EventManager.InvokeScoreChanged(stats.scoreValue);
        
        // Return to pool
        EnemyPool.Instance.ReturnEnemy(this);
    }

    public Transform GetPlayer() => playerTransform;
    public Vector2 GetPlayerPos() => playerTransform != null ? (Vector2)playerTransform.position : Vector2.zero;

    private void OnDestroy()
    {
        if (health != null)
        {
            health.OnDeath -= OnDeath;
        }
    }
}
