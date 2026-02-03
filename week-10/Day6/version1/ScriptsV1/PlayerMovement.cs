using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float acceleration = 40f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.4f;
    [SerializeField] private Vector2 groundCheckOffset = new Vector2(0f, -0.2f);
    [Header("Damping Settings")]
    [SerializeField, Range(0f, 1f)] private float damping = 0.1f;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float horizontalInput;
    private bool jumpRequested;
    private bool jumpHeld;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        moveAction?.action.Enable();
        jumpAction?.action.Enable();
    }

    private void OnDisable()
    {
        moveAction?.action.Disable();
        jumpAction?.action.Disable();
    }

    private void Update()
    {
        horizontalInput = moveAction?.action.ReadValue<Vector2>().x ?? 0f;

        if (jumpAction?.action.WasPressedThisFrame() ?? false)
        {
            jumpRequested = true;
        }
        jumpHeld = jumpAction?.action.IsPressed() ?? false;
    }

    private void FixedUpdate()
    {
        Vector2 groundCheckPos = (Vector2)transform.position + groundCheckOffset;
        isGrounded = Physics2D.OverlapCircle(groundCheckPos, groundCheckRadius, groundLayer);
        if (!isGrounded)
        {
            isGrounded = rb.IsTouchingLayers(groundLayer);
        }

        if (Mathf.Abs(horizontalInput) > 0.01f)
        {
            float force = horizontalInput * acceleration;
            rb.AddForce(new Vector2(force, 0f), ForceMode2D.Force);
        }
        else
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x * (1f - damping), rb.linearVelocity.y);
        }
        if (Mathf.Abs(rb.linearVelocity.x) > moveSpeed)
        {
            rb.linearVelocity = new Vector2(Mathf.Sign(rb.linearVelocity.x) * moveSpeed, rb.linearVelocity.y);
        }

        if (jumpRequested)
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
            jumpRequested = false;
        }

        if (!jumpHeld && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + groundCheckOffset, groundCheckRadius);
    }
}
