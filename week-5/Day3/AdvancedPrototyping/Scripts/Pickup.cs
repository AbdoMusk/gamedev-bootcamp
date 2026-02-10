using UnityEngine;

public class Pickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {   
            Destroy(gameObject);
            GameManager.instance.CollectCoin();
        }

    }
}
