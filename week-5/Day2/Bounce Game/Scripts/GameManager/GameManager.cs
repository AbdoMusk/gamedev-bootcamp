using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Game State")]
    public int currentLevel = 1;
    public int ringsCollected = 0;
    public float energy = 1f;
    public float energyDrainRate = 0.05f;
    public float energyPerRing = 0.25f;
    
    [Header("UI References")]
    public HUDUI hudUI;
    public GameOverUI gameOverUI;
    
    [Header("References")]
    public GameObject player;
    public LevelGenerator levelGenerator;
    
    private bool isGameOver = false;
    
    private const string LAST_SCORE_KEY = "LastRunScore";
    private const string DAILY_SEED_KEY = "DailySeed";
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    void Start()
    {
        // Generate daily seed based on date
        string dailySeed = System.DateTime.Now.ToString("yyyyMMdd");
        PlayerPrefs.SetString(DAILY_SEED_KEY, dailySeed);
        
        // Initialize game
        currentLevel = 1;
        ringsCollected = 0;
        energy = 1f;
        isGameOver = false;
        
        if (hudUI != null)
        {
            hudUI.gameObject.SetActive(true);
            hudUI.UpdateRings(ringsCollected);
            hudUI.UpdateEnergy(energy);
            hudUI.UpdateLevel(currentLevel);
        }
        
        if (gameOverUI != null)
            gameOverUI.gameObject.SetActive(false);
        
        // Generate first level
        if (levelGenerator != null)
            levelGenerator.GenerateLevel(currentLevel);
    }
    
    void Update()
    {
        if (isGameOver) return;
        
        // Drain energy over time
        energy -= energyDrainRate * Time.deltaTime;
        
        if (hudUI != null)
            hudUI.UpdateEnergy(energy);
        
        if (energy <= 0f)
            GameOver();
    }
    
    public void CollectRing()
    {
        if (isGameOver) return;
        
        ringsCollected++;
        energy = Mathf.Min(energy + energyPerRing, 1f);
        
        if (hudUI != null)
        {
            hudUI.UpdateRings(ringsCollected);
            hudUI.UpdateEnergy(energy);
        }
    }
    
    public void PlayerDied()
    {
        GameOver();
    }
    
    public void CompleteLevel()
    {
        if (isGameOver) return;
        
        currentLevel++;
        energy = Mathf.Min(energy + 0.5f, 1f); // Bonus energy for completing level
        
        if (hudUI != null)
        {
            hudUI.UpdateLevel(currentLevel);
            hudUI.UpdateEnergy(energy);
        }
        
        // Generate next level
        if (levelGenerator != null)
            levelGenerator.GenerateLevel(currentLevel);
    }
    
    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        
        // Save score
        PlayerPrefs.SetInt(LAST_SCORE_KEY, ringsCollected);
        PlayerPrefs.Save();
        
        // Disable player
        if (player != null)
            player.SetActive(false);
        
        // Show game over UI
        if (gameOverUI != null)
        {
            gameOverUI.gameObject.SetActive(true);
            gameOverUI.SetScore(ringsCollected);
            gameOverUI.SetLevel(currentLevel);
        }
        
        if (hudUI != null)
            hudUI.gameObject.SetActive(false);
    }
    
    public static int GetLastRunScore()
    {
        return PlayerPrefs.GetInt(LAST_SCORE_KEY, 0);
    }
    
    public static string GetDailySeed()
    {
        return System.DateTime.Now.ToString("yyyyMMdd");
    }
}