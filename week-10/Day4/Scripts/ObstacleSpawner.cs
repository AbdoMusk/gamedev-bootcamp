using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private float spawnDistance = 40f;
    [SerializeField] private float baseSpawnInterval = 2f;
    [SerializeField] private float minSpawnInterval = 0.8f;
    [SerializeField] private float baseGapSize = 3f;
    [SerializeField] private float minGapSize = 2f;
    [SerializeField] private float baseObstacleSpeed = 5f;
    [SerializeField] private float maxObstacleSpeed = 12f;
    [SerializeField] private float difficultyRampTime = 120f;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private bool usePrefabVisuals = true;
    [SerializeField] private int laneCount = 2;
    [SerializeField] private float laneSpacing = 2f;

    private float spawnTimer;
    private float gameTime;
    private bool isSpawning = false;
    private List<GameObject> activeObstacles = new List<GameObject>();

    public float DifficultyPercent { get; private set; }

    private void Update()
    {
        if (!isSpawning) return;

        gameTime += Time.deltaTime;
        spawnTimer += Time.deltaTime;

        float diffProgress = Mathf.Clamp01(gameTime / difficultyRampTime);
        DifficultyPercent = diffProgress;
        float currentInterval = Mathf.Lerp(baseSpawnInterval, minSpawnInterval, diffProgress);
        float currentSpeed = Mathf.Lerp(baseObstacleSpeed, maxObstacleSpeed, diffProgress);
        float currentGapSize = Mathf.Lerp(baseGapSize, minGapSize, diffProgress);

        if (spawnTimer >= currentInterval)
        {
            SpawnObstacles(currentSpeed, currentGapSize);
            spawnTimer = 0f;
        }
    }

    private void SpawnObstacles(float speed, float gapSize)
    {
        bool generatePrimitives = !usePrefabVisuals || obstaclePrefab == null;

        for (int lane = 0; lane < laneCount; lane++)
        {
            GameObject obs = new GameObject($"Obstacle_Lane{lane}");

            Obstacle obstacleScript = obs.GetComponent<Obstacle>();
            if (obstacleScript == null)
            {
                obstacleScript = obs.AddComponent<Obstacle>();
            }
            obstacleScript.SetGeneratePrimitives(generatePrimitives);
            obstacleScript.SetVisualPrefab(obstaclePrefab);
            float gapY = Random.Range(1f, 6f);
            obstacleScript.Initialize(lane, gapY, gapSize, laneCount, laneSpacing, spawnDistance);
            obstacleScript.SetSpeed(speed);

            activeObstacles.Add(obs);
        }
        activeObstacles.RemoveAll(o => o == null);
    }

    public void StartSpawning()
    {
        isSpawning = true;
        gameTime = 0f;
        spawnTimer = 0f;
        DifficultyPercent = 0f;
    }

    public void StopSpawning()
    {
        isSpawning = false;
        foreach (var obs in activeObstacles)
        {
            if (obs != null)
            {
                Obstacle obstacleScript = obs.GetComponent<Obstacle>();
                if (obstacleScript != null)
                    obstacleScript.Stop();
            }
        }
    }

    public void ResetSpawner()
    {
        StopSpawning();
        gameTime = 0f;
        spawnTimer = 0f;
        DifficultyPercent = 0f;
        foreach (var obs in activeObstacles)
            if (obs != null) Destroy(obs);
        activeObstacles.Clear();
    }
}
