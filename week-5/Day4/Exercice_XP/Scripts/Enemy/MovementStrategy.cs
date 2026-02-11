using UnityEngine;

/// <summary>
/// Strategy Pattern - Movable interface
/// Different movement strategies can be swapped without changing Enemy code
/// </summary>
public interface IMovementStrategy
{
    void Move(Transform transform, Transform target, float speed);
}

/// <summary>
/// Chase movement strategy
/// </summary>
public class ChaseMovementStrategy : IMovementStrategy
{
    public void Move(Transform transform, Transform target, float speed)
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            transform.position += (Vector3)direction * speed * Time.deltaTime;
        }
    }
}

/// <summary>
/// Patrol movement strategy (random)
/// </summary>
public class PatrolMovementStrategy : IMovementStrategy
{
    private Vector2 currentDirection;
    private float changeTime;

    public void Move(Transform transform, Transform target, float speed)
    {
        changeTime -= Time.deltaTime;
        if (changeTime <= 0)
        {
            currentDirection = Random.insideUnitCircle.normalized;
            changeTime = Random.Range(1f, 3f);
        }

        transform.position += (Vector3)currentDirection * speed * Time.deltaTime;
    }
}
