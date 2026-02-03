using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private BirdController bird;
    [SerializeField] private ObstacleSpawner spawner;
    [SerializeField] private CameraController cameraController;

    private int score = 0;
    private int highScore;
    private bool isPlaying = false;
    private Label scoreLabel, highScoreLabel, finalScoreLabel, difficultyLabel, newHighScoreLabel;
    private VisualElement menuPanel, gameOverPanel, hudPanel, inputLayer;
    private Button tapButton, leftButton, rightButton, cameraToggleButton;

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        bird ??= FindFirstObjectByType<BirdController>();
        spawner ??= FindFirstObjectByType<ObstacleSpawner>();
        cameraController ??= FindFirstObjectByType<CameraController>();

        SetupUI();
        ShowMenu();
    }

    private void SetupUI()
    {
        if (uiDocument == null) return;
        var root = uiDocument.rootVisualElement;

        scoreLabel = root.Q<Label>("score-label");
        difficultyLabel = root.Q<Label>("difficulty-label");
        highScoreLabel = root.Q<Label>("high-score-label");
        newHighScoreLabel = root.Q<Label>("new-high-score-label");
        menuPanel = root.Q<VisualElement>("menu-panel");
        gameOverPanel = root.Q<VisualElement>("game-over-panel");
        hudPanel = root.Q<VisualElement>("hud-panel");
        inputLayer = root.Q<VisualElement>("input-layer");
        finalScoreLabel = root.Q<Label>("final-score-label");

        root.Q<Button>("start-button")?.RegisterCallback<ClickEvent>(x => HandleTap());
        root.Q<Button>("restart-button")?.RegisterCallback<ClickEvent>(x => HandleTap());

        tapButton = root.Q<Button>("tap-button");
        leftButton = root.Q<Button>("left-lane-button");
        rightButton = root.Q<Button>("right-lane-button");
        cameraToggleButton = root.Q<Button>("camera-toggle-button");

        tapButton?.RegisterCallback<ClickEvent>(x => HandleTap());
        leftButton?.RegisterCallback<ClickEvent>(x => bird?.MoveRight());
        rightButton?.RegisterCallback<ClickEvent>(x => bird?.MoveLeft());
        cameraToggleButton?.RegisterCallback<ClickEvent>(x => cameraController?.ToggleCamera());

        menuPanel?.RegisterCallback<ClickEvent>(x => HandleTap());
        gameOverPanel?.RegisterCallback<ClickEvent>(x => HandleTap());

        if (highScoreLabel != null) highScoreLabel.text = $"High Score: {highScore}";
        if (newHighScoreLabel != null) newHighScoreLabel.style.display = DisplayStyle.None;
    }

    private void ShowMenu()
    {
        isPlaying = false;
        if (menuPanel != null) menuPanel.style.display = DisplayStyle.Flex;
        if (gameOverPanel != null) gameOverPanel.style.display = DisplayStyle.None;
        if (hudPanel != null) hudPanel.style.display = DisplayStyle.None;
        if (inputLayer != null) inputLayer.style.display = DisplayStyle.None;
        UpdateDifficultyLabel(0f);
    }

    public void StartGame()
    {
        isPlaying = true;
        score = 0;
        if (menuPanel != null) menuPanel.style.display = DisplayStyle.None;
        if (gameOverPanel != null) gameOverPanel.style.display = DisplayStyle.None;
        if (hudPanel != null) hudPanel.style.display = DisplayStyle.Flex;
        if (inputLayer != null) inputLayer.style.display = DisplayStyle.Flex;
        spawner?.StartSpawning();
        UpdateScore();
        UpdateDifficultyLabel(0f);
        if (newHighScoreLabel != null) newHighScoreLabel.style.display = DisplayStyle.None;
        AudioEvents.RaiseGameStarted();
    }

    public void GameOver()
    {
        if (!isPlaying) return;
        isPlaying = false;
        spawner?.StopSpawning();

        bool isNewHighScore = score > highScore;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        if (gameOverPanel != null) gameOverPanel.style.display = DisplayStyle.Flex;
        if (hudPanel != null) hudPanel.style.display = DisplayStyle.None;
        if (inputLayer != null) inputLayer.style.display = DisplayStyle.None;
        if (finalScoreLabel != null) finalScoreLabel.text = $"Score: {score}";
        if (newHighScoreLabel != null)
            newHighScoreLabel.style.display = isNewHighScore ? DisplayStyle.Flex : DisplayStyle.None;
        if (highScoreLabel != null) highScoreLabel.text = $"High Score: {highScore}";
        AudioEvents.RaiseGameOver();
    }

    private void Update()
    {
        if (!isPlaying || difficultyLabel == null || spawner == null) return;
        UpdateDifficultyLabel(spawner.DifficultyPercent);
    }

    private void HandleTap()
    {
        if (bird == null) return;

        if (!isPlaying)
        {
            if (!bird.IsAlive)
            {
                spawner?.ResetSpawner();
                bird.ResetBird();
            }

            bird.Flap();
            return;
        }

        bird.Flap();
    }

    public void AddScore()
    {
        if (isPlaying)
        {
            score++;
            UpdateScore();
        }
    }

    private void UpdateScore()
    {
        if (scoreLabel != null) scoreLabel.text = $"Score: {score}";
    }

    private void UpdateDifficultyLabel(float progress)
    {
        if (difficultyLabel == null) return;
        int percent = Mathf.RoundToInt(Mathf.Clamp01(progress) * 100f);
        difficultyLabel.text = $"Difficulty: {percent}%";
    }

    private void RestartGame()
    {
        bird?.ResetBird();
        spawner?.ResetSpawner();
        ShowMenu();
    }
}
