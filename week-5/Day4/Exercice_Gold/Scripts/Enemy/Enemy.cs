using UnityEngine;

/// <summary>
/// Event-Driven Enemy - No Update() logic
/// State transitions happen only via events
/// Uses existing HealthComponent for damage tracking
/// </summary>
public class Enemy : MonoBehaviour
{
    private IEnemyState currentState;
    private HealthComponent healthComponent;
    private EnemyData_SO enemyData;
    private bool isInitialized;
    private bool isSubscribed;

    [SerializeField] private Transform followTarget;
    private Coroutine followCoroutine;

    private void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
        if (healthComponent == null)
        {
            healthComponent = gameObject.AddComponent<HealthComponent>();
        }
    }

    public void Initialize(EnemyData_SO data)
    {
        if (data == null)
        {
            Debug.LogError("Enemy.Initialize: data is null!");
            return;
        }

        enemyData = data;

        if (healthComponent == null)
        {
            Debug.LogError("Enemy.Initialize: HealthComponent missing!");
            return;
        }

        // Data-driven health
        healthComponent.SetMaxHealth(enemyData.maxHealth, resetCurrentHealth: true);

        // Subscribe once (pool reuse calls Initialize repeatedly)
        if (!isSubscribed)
        {
            healthComponent.OnDeath += OnEnemyDied;
            healthComponent.OnHealthChanged += OnHealthChanged;
            isSubscribed = true;
        }

        isInitialized = true;
        SetState(new InactiveState(this));
    }

    // Optional: if you have a player in-scene, assign it here (or via inspector)
    public void SetFollowTarget(Transform target)
    {
        followTarget = target;
    }

    public void SetState(IEnemyState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void TakeDamage(float damage)
    {
        if (!isInitialized) return;
        if (healthComponent == null) return;

        healthComponent.TakeDamage(damage);
        EventManager.InvokeEnemyDamaged(damage);
    }

    public void ResetHealth()
    {
        if (!isInitialized) return;
        if (healthComponent == null || enemyData == null) return;

        healthComponent.SetMaxHealth(enemyData.maxHealth, resetCurrentHealth: true);
    }

    public void Activate()
    {
        ResetHealth();
        // Transition to Active via event only
        EventManager.InvokeEnemySpawnRequested(this);
    }

    private void OnEnemyDied()
    {
        EventManager.InvokeEnemyDied();
        SetState(new DeadState(this));
    }

    private void OnHealthChanged(float currentHealth)
    {
        EventManager.InvokeEnemyHealthChanged(currentHealth);
    }

    private void OnDestroy()
    {
        if (healthComponent != null)
        {
            healthComponent.OnDeath -= OnEnemyDied;
            healthComponent.OnHealthChanged -= OnHealthChanged;
        }
    }

    public void StartFollowing()
    {
        if (followTarget == null) return;
        if (enemyData == null) return;

        StopFollowing();
        followCoroutine = StartCoroutine(FollowLoop());
    }

    public void StopFollowing()
    {
        if (followCoroutine != null)
        {
            StopCoroutine(followCoroutine);
            followCoroutine = null;
        }
    }

    private System.Collections.IEnumerator FollowLoop()
    {
        while (true)
        {
            if (followTarget == null || enemyData == null)
            {
                yield return null;
                continue;
            }

            Vector3 currentPosition = transform.position;
            Vector3 targetPosition = followTarget.position;
            transform.position = Vector3.MoveTowards(currentPosition, targetPosition, enemyData.moveSpeed * Time.deltaTime);

            yield return null;
        }
    }

    // Damage player on contact (works with either trigger or collision)
    private void OnTriggerEnter2D(Collider2D other)
    {
        TryDamagePlayer(other.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryDamagePlayer(collision.gameObject);
    }

    private void TryDamagePlayer(GameObject other)
    {
        if (enemyData == null) return;

        if (other == null) return;

        // Expect player to be tagged "Player"
        if (!other.CompareTag("Player")) return;

        DamageReceiver receiver = other.GetComponent<DamageReceiver>();
        if (receiver != null)
        {
            receiver.ReceiveDamage(enemyData.attackDamage);
            // knockback away from player
            StartKnockbackFrom(other.transform.position);
        }
    }

    private void StartKnockbackFrom(Vector3 sourcePosition)
    {
        StopFollowing();
        if (followCoroutine != null)
        {
            StopCoroutine(followCoroutine);
            followCoroutine = null;
        }

        StartCoroutine(KnockbackRoutine(sourcePosition));
    }

    private System.Collections.IEnumerator KnockbackRoutine(Vector3 sourcePosition)
    {
        if (enemyData == null)
        {
            yield break;
        }

        Vector3 dir = (transform.position - sourcePosition);
        if (dir == Vector3.zero) dir = (transform.position - (sourcePosition + Vector3.up * 0.1f));
        dir.Normalize();

        Vector3 target = transform.position + dir * enemyData.knockbackDistance;

        float remaining = Vector3.Distance(transform.position, target);
        while (remaining > 0.01f)
        {
            float step = enemyData.knockbackSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            remaining = Vector3.Distance(transform.position, target);
            yield return null;
        }

        // resume following if a follow target exists
        if (followTarget != null)
        {
            StartFollowing();
        }
    }
}
