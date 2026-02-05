using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TMPro.TextMeshProUGUI collectibleText;

    public GameObject GameWonUI;


    int count;

    void Awake() { instance = this; }
    void Start() {
        if (GameWonUI != null)
            GameWonUI.SetActive(false);
    }

    public void AddCollectible()
    {
        count++;
        collectibleText.text = "Collected: " + count;
    }

    public void ShowGameWon()
    {
        if (GameWonUI != null)
            GameWonUI.SetActive(true);
        
        // Pause the game when won
        Time.timeScale = 0;
    }

}
