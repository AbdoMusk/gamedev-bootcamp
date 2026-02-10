using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        rb.linearVelocity = new Vector3(x * speed, rb.linearVelocity.y, z * speed);
        
        // if he isnt touching the ground, he cant jump ground is on ground layer
        if (Physics.Raycast(transform.position, Vector3.down, 1.1f, LayerMask.GetMask("Ground")))
        {
            if (Input.GetKeyDown(KeyCode.Space))
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}