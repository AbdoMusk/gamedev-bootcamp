using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameFinish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.instance.ShowGameWon();
        }
    }
}
