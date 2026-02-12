using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Mediator Pattern - UI communicates through here
/// UI elements don't talk directly to gameplay systems
/// All communication flows through events and this manager
/// </summary>
public class UIManager : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    [SerializeField] private TMPro.TextMeshProUGUI healthText;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject gameOverPanel;

    private int currentScore;
    private float currentHealth = 100f;

    private void Start()
    {
        // Validate all UI elements are assigned
        if (scoreText == null || healthText == null || pauseButton == null || 
            resumeButton == null || restartButton == null || gameOverPanel == null)
        {
            Debug.LogError("UIManager: Missing UI element assignments!");
            return;
        }

        // Subscribe to events
        EventManager.OnScoreChanged += UpdateScore;
        EventManager.OnPlayerDamaged += UpdateHealth;
        EventManager.OnPlayerDeath += ShowGameOver;

        // Button listeners
        pauseButton.onClick.AddListener(OnPauseClicked);
        resumeButton.onClick.AddListener(OnResumeClicked);
        restartButton.onClick.AddListener(OnRestartClicked);

        // Resume button should be hidden until pause is clicked
        resumeButton.gameObject.SetActive(false);
        gameOverPanel.SetActive(false);

        // Subscribe to pause/resume events
        EventManager.OnGamePaused += ShowResumeButton;
        EventManager.OnGameResumed += HideResumeButton;
    }

    private void UpdateScore(int score)
    {
        currentScore += score;
        scoreText.text = "Score: " + currentScore;
    }

    private void UpdateHealth(float health)
    {
        currentHealth = health;
        healthText.text = "Health: " + health.ToString("F0");
    }

    private void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    private void OnPauseClicked()
    {
        Time.timeScale = 0f;
    }

    private void OnResumeClicked()
    {
        Time.timeScale = 1f;
    }

    private void OnRestartClicked()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private void ShowResumeButton()
    {
        pauseButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(true);
    }

    private void HideResumeButton()
    {
        resumeButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        EventManager.OnScoreChanged -= UpdateScore;
        EventManager.OnPlayerDamaged -= UpdateHealth;
        EventManager.OnPlayerDeath -= ShowGameOver;
        EventManager.OnGamePaused -= ShowResumeButton;
        EventManager.OnGameResumed -= HideResumeButton;
    }
}
