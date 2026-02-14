using UnityEngine;

public class MudSurface : MonoBehaviour
{
    // Mud surface physics material setup
    public PhysicsMaterial2D mudMaterial;

    void Awake()
    {
        var collider = GetComponent<Collider2D>();
        if (collider != null && mudMaterial != null)
            collider.sharedMaterial = mudMaterial;
    }
}