using UnityEngine;

/// <summary>
/// Facade Pattern - Single point of control for game flow
/// All game state changes go through this
/// Simplifies complex subsystems
/// </summary>
public class GameFlowFacade : MonoBehaviour
{
    public static GameFlowFacade Instance { get; private set; }

    [SerializeField] private DifficultyConfig difficultyConfig;
    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private Transform player;
    [SerializeField] private Transform spawnPoint;

    private bool isGameRunning;
    private bool isGamePaused;
    private float spawnTimer;
    private int currentScore;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (difficultyConfig == null || enemyStats == null || player == null || spawnPoint == null)
        {
            Debug.LogError("GameFlowFacade: Missing required field assignments!");
            return;
        }
        StartGame();
    }

    private void Update()
    {
        if (!isGameRunning || isGamePaused) return;

        SpawnEnemies();
    }

    public void StartGame()
    {
        isGameRunning = true;
        currentScore = 0;
        EventManager.InvokeGameStarted();
    }

    public void EndGame()
    {
        isGameRunning = false;
        EventManager.InvokeGameEnded();
    }

    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        EventManager.InvokeGamePaused();
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        EventManager.InvokeGameResumed();
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private void SpawnEnemies()
    {
        if (EnemyPool.Instance.GetActiveEnemyCount() >= difficultyConfig.maxEnemies)
            return;

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            Vector3 randomPos = spawnPoint.position + (Vector3)Random.insideUnitCircle * 3f;
            EnemyFactory.CreateEnemy(enemyStats, randomPos, player);
            spawnTimer = 1f / difficultyConfig.spawnRate;
            EventManager.InvokeEnemySpawned(EnemyPool.Instance.GetActiveEnemyCount());
        }
    }

    public void AddScore(int points)
    {
        currentScore += points;
    }

    public int GetScore() => currentScore;
}
