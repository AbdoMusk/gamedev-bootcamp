using UnityEngine;
 
public class DangerZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // game manager GameManager is a public static class we can import and call the TouchDanger function when we touch the danger zone
        if (other.CompareTag("Player"))
            GameManager.instance.TouchDanger();
    }
}
