using UnityEngine;

public class Heal : MonoBehaviour
{
    public int heal = 50;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<PlayerHealth>().Heal(heal);
            Destroy(gameObject);
    }
}
