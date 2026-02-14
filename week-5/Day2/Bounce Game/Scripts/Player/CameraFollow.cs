using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Follow Settings")]
    public float smoothSpeed = 5f;
    public Vector3 offset = new Vector3(0f, 2f, -10f);
    public bool constrainY = true;
    public float minY = -3f;
    
    private Transform player;

    void Start()
    {
        FindPlayer();
    }

    void LateUpdate()
    {
        // Auto-find player if not set
        if (player == null)
            FindPlayer();
        
        if (player == null) return;
        
        Vector3 targetPos = player.position + offset;
        
        // Constrain Y to not go below minimum
        if (constrainY)
            targetPos.y = Mathf.Max(targetPos.y, minY);
        
        // Smooth camera movement
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
    }
    
    void FindPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }
}
