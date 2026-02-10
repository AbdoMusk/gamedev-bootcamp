using UnityEngine;

public class EndZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            GameManager.instance.Win();
    }
}
