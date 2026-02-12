using UnityEngine;

/// <summary>
/// State Pattern - Each state is its own class
/// No Update() logic for decisions. Transitions via events only.
/// </summary>
public interface IEnemyState
{
    void Enter();
    void Exit();
}

/// <summary>
/// Inactive State - Enemy is pooled and waiting
/// </summary>
public class InactiveState : IEnemyState
{
    private Enemy enemy;

    public InactiveState(Enemy enemyInstance)
    {
        enemy = enemyInstance;
    }

    public void Enter()
    {
        enemy.gameObject.SetActive(false);

        // Transition to Active ONLY via event
        EventManager.OnEnemySpawnRequested += HandleSpawnRequested;
    }

    public void Exit()
    {
        EventManager.OnEnemySpawnRequested -= HandleSpawnRequested;
    }

    private void HandleSpawnRequested(Enemy requestedEnemy)
    {
        if (requestedEnemy != enemy) return;
        enemy.SetState(new ActiveState(enemy));
    }
}

/// <summary>
/// Active State - Enemy is alive and exists in scene
/// </summary>
public class ActiveState : IEnemyState
{
    private Enemy enemy;

    public ActiveState(Enemy enemyInstance)
    {
        enemy = enemyInstance;
    }

    public void Enter()
    {
        enemy.gameObject.SetActive(true);
        enemy.ResetHealth();
        EventManager.InvokeEnemySpawned(1);
        EventManager.InvokeEnemySpawnedEvent();

        // Damage comes in via event (Facade publishes)
        EventManager.OnEnemyDamageRequested += HandleDamageRequested;

        // Optional follow behavior (no Update, runs only in Active)
        enemy.StartFollowing();
    }

    public void Exit()
    {
        EventManager.OnEnemyDamageRequested -= HandleDamageRequested;
        enemy.StopFollowing();
    }

    private void HandleDamageRequested(float damage)
    {
        enemy.TakeDamage(damage);
    }
}

/// <summary>
/// Dead State - Enemy is dead, ready to respawn
/// </summary>
public class DeadState : IEnemyState
{
    private Enemy enemy;

    public DeadState(Enemy enemyInstance)
    {
        enemy = enemyInstance;
    }

    public void Enter()
    {
        enemy.gameObject.SetActive(false);
        EventManager.InvokeEnemyDied();
    }

    public void Exit()
    {
    }
}
