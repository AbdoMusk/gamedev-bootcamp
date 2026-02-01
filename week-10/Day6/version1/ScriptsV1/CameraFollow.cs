using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 baseOffset = new Vector3(0, 0, -10);
    [Header("Smoothing")]
    [SerializeField] private float smoothTime = 0.2f;
    [Header("Dynamic Offsets")]
    [SerializeField] private float xOffset = 1.5f;
    [SerializeField] private float yOffset = 1.0f;

    private Rigidbody2D playerRb;
    private PlayerMovement playerMovement;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
        }
        if (target != null)
        {
            playerRb = target.GetComponent<Rigidbody2D>();
            playerMovement = target.GetComponent<PlayerMovement>();
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        float directionX = 0f;
        float directionY = 0f;

        if (playerRb != null)
        {
            if (Mathf.Abs(playerRb.linearVelocity.x) > 0.1f)
                directionX = Mathf.Sign(playerRb.linearVelocity.x);

            if (Mathf.Abs(playerRb.linearVelocity.y) > 1f)
                directionY = Mathf.Sign(playerRb.linearVelocity.y);
        }

        Vector3 dynamicOffset = baseOffset;
        dynamicOffset.x += directionX * xOffset;
        dynamicOffset.y += directionY * yOffset;

        Vector3 desiredPosition = target.position + dynamicOffset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
    }
}
