using UnityEngine;

public class EnemyThrower : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float throwInterval = 2f;
    [SerializeField] private float swordSpeed = 5f;
    [SerializeField] private float swordSpeedVariation = 1.5f;

    private Transform player;
    private Animator animator;
    private bool isDead = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        animator = GetComponent<Animator>();
        SetIdle(true);
        InvokeRepeating(nameof(ThrowSword), throwInterval, throwInterval);
    }

    private void ThrowSword()
    {
        if (isDead || player == null || swordPrefab == null || firePoint == null) return;

        SetAttacking(true);

        GameObject sword = Instantiate(swordPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = sword.GetComponent<Rigidbody2D>();
        
        if (rb != null)
        {
            float randomSpeed = swordSpeed + Random.Range(-swordSpeedVariation, swordSpeedVariation);
            Vector2 direction = (player.position - firePoint.position).normalized;
            rb.linearVelocity = direction * randomSpeed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            sword.transform.rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
        }

        Invoke(nameof(ResetToIdle), 0.2f);
    }

    private void ResetToIdle()
    {
        SetAttacking(false);
        SetIdle(true);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.point.y > transform.position.y + 0.2f) 
                {
                    isDead = true;
                    SetDead();
                    Rigidbody2D rb = GetComponent<Rigidbody2D>();   
                    Collider2D col = GetComponent<Collider2D>();
                    if (col != null)
                    {
                        col.isTrigger = true;
                    }
                    if (rb != null)
                    {
                        rb.bodyType = RigidbodyType2D.Static;
                    }
                    SpriteRenderer sr = GetComponent<SpriteRenderer>();
                    if (sr != null)
                    {
                        Color color = sr.color;
                        color.a = 0.5f;
                        sr.color = color;
                    }
                    Destroy(gameObject, 1f);
                    break;
                }
            }
        }
    }

    private void SetIdle(bool value)
    {
        if (animator != null)
        {
            animator.SetBool("isIdle", value);
            if (value) animator.SetBool("isAttacking", false);
        }
    }

    private void SetDead()
    {
        if (animator != null)
        {
            animator.SetBool("isDead", true);
            animator.SetBool("isIdle", false);
            animator.SetBool("isAttacking", false);
        }
    }

    private void SetAttacking(bool value)
    {
        if (animator != null)
        {
            animator.SetBool("isAttacking", value);
            if (value) animator.SetBool("isIdle", false);
        }
    }
}
