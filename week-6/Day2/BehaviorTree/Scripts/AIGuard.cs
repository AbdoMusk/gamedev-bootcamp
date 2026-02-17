using UnityEngine;
using UnityEngine.AI;

public class GuardAI : MonoBehaviour
{
    public enum State
    {
        Idle,
        Patrol,
        Chase,
        Return
    }

    public State currentState;

    public Transform[] patrolPoints;
    public Transform player;

    public float detectionRadius = 8f;
    public float lostPlayerTime = 3f;
    public float idleTime = 2f;

    private NavMeshAgent agent;
    private int currentPoint;
    private float stateTimer;
    private Vector3 lastPatrolPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = State.Idle;
        stateTimer = idleTime;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Idle:
                stateTimer -= Time.deltaTime;
                if (stateTimer <= 0)
                {
                    ChangeState(State.Patrol);
                }
                break;

            case State.Patrol:
                Patrol();

                if (distance < detectionRadius)
                {
                    ChangeState(State.Chase);
                }
                break;

            case State.Chase:
                agent.SetDestination(player.position);

                if (distance > detectionRadius)
                {
                    stateTimer = lostPlayerTime;
                    ChangeState(State.Return);
                }
                break;

            case State.Return:
                stateTimer -= Time.deltaTime;

                if (stateTimer <= 0)
                {
                    ChangeState(State.Patrol);
                }
                break;
        }
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        agent.SetDestination(patrolPoints[currentPoint].position);

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }
    }

    void ChangeState(State newState)
    {
        currentState = newState;

        switch (newState)
        {
            case State.Idle:
                agent.ResetPath();
                stateTimer = idleTime;
                break;

            case State.Patrol:
                break;

            case State.Chase:
                break;

            case State.Return:
                break;
        }
    }
}
