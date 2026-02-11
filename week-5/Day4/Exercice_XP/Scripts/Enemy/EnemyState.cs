using UnityEngine;

/// <summary>
/// State Pattern - Enemy AI State Machine
/// No if statements for dead checks - states handle logic
/// </summary>
public abstract class EnemyState
{
    protected Enemy enemy;

    public virtual void Enter() { }
    public abstract void Update();
    public virtual void Exit() { }

    public EnemyState(Enemy enemy)
    {
        this.enemy = enemy;
    }
}

public class IdleState : EnemyState
{
    public IdleState(Enemy enemy) : base(enemy) { }

    public override void Update()
    {
        if (Vector2.Distance((Vector2)enemy.transform.position, enemy.GetPlayerPos()) < enemy.detectionRange)
        {
            enemy.SetState(new ChaseState(enemy));
        }
    }
}

public class ChaseState : EnemyState
{
    public ChaseState(Enemy enemy) : base(enemy) { }

    public override void Update()
    {
        Vector2 distToPlayer = enemy.GetPlayerPos() - (Vector2)enemy.transform.position;
        
        if (distToPlayer.magnitude > enemy.detectionRange)
        {
            enemy.SetState(new IdleState(enemy));
        }
        else if (distToPlayer.magnitude < enemy.stats.attackRange)
        {
            enemy.SetState(new AttackState(enemy));
        }
        else
        {
            enemy.movement.Move(enemy.transform, enemy.GetPlayer(), enemy.stats.moveSpeed);
        }
    }
}

public class AttackState : EnemyState
{
    private float lastAttackTime;

    public AttackState(Enemy enemy) : base(enemy) { }

    public override void Update()
    {
        Vector2 distToPlayer = enemy.GetPlayerPos() - (Vector2)enemy.transform.position;

        if (distToPlayer.magnitude > enemy.detectionRange)
        {
            enemy.SetState(new IdleState(enemy));
            return;
        }

        if (distToPlayer.magnitude > enemy.stats.attackRange)
        {
            enemy.SetState(new ChaseState(enemy));
            return;
        }

        if (Time.time - lastAttackTime >= enemy.stats.attackCooldown)
        {
            DamageReceiver playerDamage = enemy.GetPlayer()?.GetComponent<DamageReceiver>();
            if (playerDamage != null)
            {
                playerDamage.ReceiveDamage(enemy.stats.attackDamage);
                lastAttackTime = Time.time;
            }
        }
    }
}

public class DeadState : EnemyState
{
    public DeadState(Enemy enemy) : base(enemy) { }

    public override void Update()
    {
        // Dead state doesn't update
    }
}
