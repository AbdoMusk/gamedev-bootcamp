using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    [Header("Respawn Settings")]
    [SerializeField] private float respawnDelay = 1.5f;
    [Header("Death Y Threshold")]
    [SerializeField] private float deathY = -8f;
    private Vector2 lastCheckpointPos;
    private Rigidbody2D rb;

    [Header("UI")]
    [SerializeField] private TMPro.TextMeshProUGUI coinText;
    private int coins = 0;
    
    private Vector2 initialPosition;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lastCheckpointPos = transform.position;
        initialPosition = transform.position;
    }

    private void Start()
    {
        UpdateCoinUI();
    }

    private void Update()
    {
        if (transform.position.y < deathY)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpecialCheckpoint special = collision.GetComponent<SpecialCheckpoint>();
        if (special != null)
        {
            switch (special.type)
            {
                case SpecialCheckpoint.CheckpointType.Normal:
                    lastCheckpointPos = special.transform.position;
                    break;
                case SpecialCheckpoint.CheckpointType.Teleport:
                    if (special.teleportTarget != null)
                    {
                        transform.position = special.teleportTarget.position;
                    }
                    break;
                case SpecialCheckpoint.CheckpointType.Final:
                    GameUIManager ui = FindFirstObjectByType<GameUIManager>();
                    if (ui != null)
                    {
                        ui.ShowWinPanel();
                    }
                    break;
            }
            return;
        }
        if (collision.CompareTag("coin"))
        {
            coins += 10;
            collision.gameObject.SetActive(false);
            UpdateCoinUI();
        }
        if (collision.CompareTag("200dh"))
        {
            coins += 200;
            collision.gameObject.SetActive(false);
            UpdateCoinUI();
        }
        if (collision.CompareTag("100dh"))
        {
            coins += 100;
            collision.gameObject.SetActive(false);
            UpdateCoinUI();
        }
        
        if (collision.CompareTag("checkpoint"))
        {
            lastCheckpointPos = collision.transform.position;
        }

        if (collision.CompareTag("sword"))
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("sword"))
        {
            Die();
        }
    }

    public void Die()
    {
        SetDead();
        StartCoroutine(Respawn());
    }

    public void ResetPlayer()
    {
        coins = 0;
        lastCheckpointPos = initialPosition;
        transform.position = initialPosition;
        UpdateCoinUI();
        rb.simulated = true;
        SetIdle();
    }

    private IEnumerator Respawn()
    {
        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;
        
        yield return new WaitForSeconds(respawnDelay);

        transform.position = lastCheckpointPos;
        rb.simulated = true;
        SetIdle();
    }

    private void SetIdle()
    {
        if (animator != null)
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isDead", false);
        }
    }

    private void SetDead()
    {
        if (animator != null)
        {
            animator.SetBool("isDead", true);
            animator.SetBool("isIdle", false);
        }
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text =  coins + " Darham";
        }
    }
}
