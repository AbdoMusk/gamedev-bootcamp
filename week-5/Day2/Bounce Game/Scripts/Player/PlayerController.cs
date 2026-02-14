using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float maxVelocity = 15f;
    
    [Header("Boost/Dash")]
    public float boostForce = 12f;
    
    [Header("Physics")]
    public float gravityScale = 2f;
    public float mass = 1f;
    public PhysicsMaterial2D bouncyMaterial;
    
    private float horizontalInput;
    private bool boostRequested;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
        rb.mass = mass;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        
        if (bouncyMaterial != null)
            rb.sharedMaterial = bouncyMaterial;
    }

    void FixedUpdate()
    {
        // Apply horizontal movement
        Vector2 vel = rb.linearVelocity;
        vel.x = horizontalInput * moveSpeed;
        rb.linearVelocity = vel;
        
        // Apply boost if requested
        if (boostRequested)
        {
            Vector2 boostDir = rb.linearVelocity.normalized;
            if (boostDir.sqrMagnitude < 0.1f)
                boostDir = Vector2.up;
            
            rb.AddForce(boostDir * boostForce, ForceMode2D.Impulse);
            boostRequested = false;
        }
        
        // Clamp max velocity
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxVelocity);
    }

    public void SetHorizontalInput(float input)
    {
        horizontalInput = input;
    }

    public void Boost()
    {
        boostRequested = true;
    }
}