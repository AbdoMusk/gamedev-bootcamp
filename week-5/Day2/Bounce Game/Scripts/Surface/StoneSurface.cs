using UnityEngine;

public class StoneSurface : MonoBehaviour
{
    // Stone surface physics material setup
    public PhysicsMaterial2D stoneMaterial;

    void Awake()
    {
        var collider = GetComponent<Collider2D>();
        if (collider != null && stoneMaterial != null)
            collider.sharedMaterial = stoneMaterial;
    }
}