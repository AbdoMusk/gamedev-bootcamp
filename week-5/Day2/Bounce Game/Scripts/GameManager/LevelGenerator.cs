using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject stonePlatformPrefab;
    public GameObject mudPlatformPrefab;
    public GameObject ringPrefab;
    public GameObject spikePrefab;
    public GameObject lavaPrefab;
    public GameObject goalPrefab;
    public GameObject playerPrefab;
    
    [Header("Level Settings")]
    public float levelWidth = 10f;
    public float platformSpacingY = 2.5f;
    public float maxHorizontalOffset = 4f;
    public int basePlatformCount = 8;
    public int platformsPerLevel = 3;
    
    [Header("Spawn Chances")]
    [Range(0, 1)] public float mudChance = 0.25f;
    [Range(0, 1)] public float ringChance = 0.6f;
    [Range(0, 1)] public float spikeChance = 0.2f;
    
    [Header("References")]
    public Transform levelContainer;
    
    private List<GameObject> spawnedObjects = new List<GameObject>();
    private GameObject currentPlayer;

    private Vector3 playerStartPos = new Vector3(0f, 1.5f, 0f);

    public void GenerateLevel(int level)
    {
        ClearLevel();
        
        // Initialize random with daily seed + level
        int seed = GetSeed(level);
        Random.InitState(seed);
        
        // Calculate platform count based on level
        int platformCount = basePlatformCount + (level * platformsPerLevel);
        
        // Create level container if not set
        if (levelContainer == null)
        {
            GameObject container = new GameObject("LevelContainer");
            levelContainer = container.transform;
        }
        
        // Spawn lava floor
        SpawnLava();
        
        // Generate platforms going upward
        float currentY = playerStartPos.y;
        float currentX = playerStartPos.x;
        Vector3 lastPlatformPos = playerStartPos;
        
        for (int i = 0; i < platformCount; i++)
        {
            // Determine horizontal position (zig-zag pattern)
            float targetX;
            if (i == 0)
            {
                targetX = 0f; // Start platform in center
            }
            else
            {
                // Move toward opposite side but within reachable distance
                float direction = (currentX > 0) ? -1f : 1f;
                float offset = Random.Range(1f, maxHorizontalOffset) * direction;
                targetX = Mathf.Clamp(currentX + offset, -levelWidth / 2f, levelWidth / 2f);
            }
            
            currentX = targetX;
            currentY += platformSpacingY;
            
            Vector3 platformPos = new Vector3(currentX, currentY, 0f);
            lastPlatformPos = platformPos;
            
            // Choose platform type
            bool isMud = Random.value < mudChance;
            GameObject platformPrefab = isMud ? mudPlatformPrefab : stonePlatformPrefab;
            
            if (platformPrefab != null)
            {
                GameObject platform = Instantiate(platformPrefab, platformPos, Quaternion.identity, levelContainer);
                spawnedObjects.Add(platform);
            }
            
            // Spawn ring above platform
            if (i > 0 && Random.value < ringChance && ringPrefab != null)
            {
                Vector3 ringPos = platformPos + Vector3.up * 1.5f;
                GameObject ring = Instantiate(ringPrefab, ringPos, Quaternion.identity, levelContainer);
                spawnedObjects.Add(ring);
            }
            
            // Spawn spike near platform (not on first few platforms)
            if (i > 2 && Random.value < spikeChance && spikePrefab != null)
            {
                // Place spike on left or right edge of platform
                float spikeOffsetX = (Random.value > 0.5f ? 1.5f : -1.5f);
                Vector3 spikePos = platformPos + new Vector3(spikeOffsetX, 0.5f, 0f);
                GameObject spike = Instantiate(spikePrefab, spikePos, Quaternion.identity, levelContainer);
                spawnedObjects.Add(spike);
            }
        }
        
        // Spawn goal above last platform
        if (goalPrefab != null)
        {
            Vector3 goalPos = lastPlatformPos + Vector3.up * 3f;
            GameObject goal = Instantiate(goalPrefab, goalPos, Quaternion.identity, levelContainer);
            spawnedObjects.Add(goal);
        }
        
        // Spawn player at start
        SpawnPlayer();
    }
    
    void SpawnLava()
    {
        if (lavaPrefab != null)
        {
            Vector3 lavaPos = new Vector3(0f, -3f, 0f);
            GameObject lava = Instantiate(lavaPrefab, lavaPos, Quaternion.identity, levelContainer);
            // Scale lava to cover level width
            lava.transform.localScale = new Vector3(levelWidth + 10f, 2f, 1f);
            spawnedObjects.Add(lava);
        }
    }
    
    void SpawnPlayer()
    {
        if (playerPrefab != null)
        {
            Vector3 spawnPos = playerStartPos + Vector3.up * (platformSpacingY + 2f); // Start above first platform
            currentPlayer = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
            
            // Link player to GameManager
            if (GameManager.Instance != null)
            {
                GameManager.Instance.player = currentPlayer;
                
                // Link InputHandler to PlayerController
                InputHandler inputHandler = currentPlayer.GetComponent<InputHandler>();
                PlayerController playerController = currentPlayer.GetComponent<PlayerController>();
                if (inputHandler != null && playerController != null)
                {
                    inputHandler.playerController = playerController;
                }
            }
        }
    }
    
    public void ClearLevel()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null)
                Destroy(obj);
        }
        spawnedObjects.Clear();
        
        if (currentPlayer != null)
        {
            Destroy(currentPlayer);
            currentPlayer = null;
        }
    }
    
    int GetSeed(int level)
    {
        string dailySeed = GameManager.GetDailySeed();
        int datePart = 0;
        int.TryParse(dailySeed, out datePart);
        return datePart + level * 1000;
    }
}
