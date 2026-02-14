using UnityEngine;

public class Ring : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance != null)
                GameManager.Instance.CollectRing();
            
            Destroy(gameObject);
        }
    }
}