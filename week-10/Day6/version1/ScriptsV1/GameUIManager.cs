using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    [Header("Win UI")]
    public TMPro.TextMeshProUGUI winText;
    public GameObject resumeButton;
    public GameObject startButton;
    public GameObject pauseButton;
    public GameObject pausePanel;

    private bool gameStarted = false;
    private bool isPaused = false;

    void Start()
    {
        Time.timeScale = 0f;
        startButton.SetActive(true);
        pauseButton.SetActive(false);
        if (pausePanel != null) pausePanel.SetActive(false);
    }

    public void OnStartButtonClicked()
    {
        gameStarted = true;
        Time.timeScale = 1f;
        startButton.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void OnPauseButtonClicked()
    {
        if (!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0f;
            if (pausePanel != null) pausePanel.SetActive(true);
        }
    }

    public void OnResumeButtonClicked()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1f;
            if (pausePanel != null) pausePanel.SetActive(false);
        }
    }

    public void OnRestartButtonClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowWinPanel()
    {
        if (pausePanel != null) pausePanel.SetActive(true);
        if (winText != null) winText.text = "Game Won!";
        if (resumeButton != null) resumeButton.SetActive(false);
        isPaused = true;
        Time.timeScale = 0f;
    }
}