using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public TMPro.TextMeshProUGUI dailySeedText;
    public TMPro.TextMeshProUGUI lastRunScoreText;
    
    void Start()
    {
        // Load and display daily seed
        string seed = GameManager.GetDailySeed();
        if (dailySeedText != null)
            dailySeedText.text = $"Seed: {seed}";
        
        // Load and display last run score
        int lastScore = GameManager.GetLastRunScore();
        if (lastRunScoreText != null)
            lastRunScoreText.text = $"Last Run: {lastScore}";
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}