using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public TMPro.TextMeshProUGUI gameOverScoreText;
    public TMPro.TextMeshProUGUI levelReachedText;
    
    public void SetScore(int score)
    {
        if (gameOverScoreText != null)
            gameOverScoreText.text = $"Rings Collected: {score}";
    }
    
    public void SetLevel(int level)
    {
        if (levelReachedText != null)
            levelReachedText.text = $"Level Reached: {level}";
    }
    
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
}