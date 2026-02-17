using UnityEngine;
using UnityEngine.AI;

public class GuardAI_Advanced : MonoBehaviour
{
    public enum Mode { Patrol, Alert, Combat }
    public Mode currentMode;

    [Header("References")]
    public Transform player;
    public Transform[] patrolPoints;
    private NavMeshAgent agent;

    [Header("Vision")]
    public float visionRange = 12f;
    public float visionAngle = 60f;

    [Header("Hearing")]
    public float hearingRadius = 10f;

    [Header("Combat")]
    public float attackRange = 2f;
    public float health = 100f;

    [Header("Memory")]
    public float memoryTime = 5f;

    private Vector3 lastKnownPosition;
    private float memoryTimer;
    private int patrolIndex;

    private float reactionDelay = 0.4f;
    private float reactionTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentMode = Mode.Patrol;
    }

    void Update()
    {
        HandleVision();
        HandleModes();
    }

    // =========================
    // VISION SYSTEM (Fair AI)
    // =========================
    void HandleVision()
    {
        Vector3 dirToPlayer = (player.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, player.position);

        float angle = Vector3.Angle(transform.forward, dirToPlayer);

        if (distance < visionRange && angle < visionAngle)
        {
            Ray ray = new Ray(transform.position + Vector3.up, dirToPlayer);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, visionRange))
            {
                if (hit.transform == player)
                {
                    reactionTimer += Time.deltaTime;

                    if (reactionTimer >= reactionDelay)
                    {
                        lastKnownPosition = player.position;
                        memoryTimer = memoryTime;
                        currentMode = Mode.Combat;
                    }
                }
            }
        }
        else
        {
            reactionTimer = 0;
        }
    }
        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, visionRange);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, hearingRadius);
        }


    // =========================
    // MODE HANDLING
    // =========================
    void HandleModes()
    {
        switch (currentMode)
        {
            case Mode.Patrol:
                PatrolMode();
                break;

            case Mode.Alert:
                AlertMode();
                break;

            case Mode.Combat:
                CombatMode();
                break;
        }
    }

    // =========================
    // PATROL MODE (BT Style)
    // =========================
    void PatrolMode()
    {
        if (patrolPoints.Length == 0) return;

        agent.speed = 3.5f;
        agent.SetDestination(patrolPoints[patrolIndex].position);

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }

        // If heard noise (example: player running close)
        if (Vector3.Distance(transform.position, player.position) < hearingRadius)
        {
            lastKnownPosition = player.position;
            currentMode = Mode.Alert;
        }
    }

    // =========================
    // ALERT MODE (Investigate/Search)
    // =========================
    void AlertMode()
    {
        agent.speed = 4f;
        agent.SetDestination(lastKnownPosition);

        memoryTimer -= Time.deltaTime;

        if (memoryTimer <= 0)
        {
            currentMode = Mode.Patrol;
        }

        if (Vector3.Distance(transform.position, player.position) < visionRange)
        {
            currentMode = Mode.Combat;
        }
    }

    // =========================
    // COMBAT MODE (Chase/Attack/Retreat)
    // =========================
    void CombatMode()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        memoryTimer -= Time.deltaTime;

        if (health < 30)
        {
            // Retreat behavior
            Vector3 retreatDir = (transform.position - player.position).normalized;
            agent.SetDestination(transform.position + retreatDir * 5f);
            return;
        }

        if (distance > attackRange)
        {
            agent.speed = 5.5f;
            agent.SetDestination(player.position);
        }
        else
        {
            agent.ResetPath();
            Debug.Log("Attack!");
        }

        if (distance > visionRange)
        {
            if (memoryTimer <= 0)
            {
                currentMode = Mode.Alert;
            }
        }
    }
}
