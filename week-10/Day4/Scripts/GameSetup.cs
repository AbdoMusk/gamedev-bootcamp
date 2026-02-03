using UnityEngine;

public class GameSetup : MonoBehaviour
{
    [ContextMenu("Setup Managers")]
    public void SetupManagers()
    {
        // Create GameManager if not exists
        if (FindFirstObjectByType<GameManager>() == null)
        {
            GameObject gmObj = new GameObject("GameManager");
            gmObj.AddComponent<GameManager>();
            Debug.Log("GameManager created!");
        }

        // Create ObstacleSpawner if not exists
        if (FindFirstObjectByType<ObstacleSpawner>() == null)
        {
            GameObject spawnerObj = new GameObject("ObstacleSpawner");
            spawnerObj.AddComponent<ObstacleSpawner>();
            Debug.Log("ObstacleSpawner created!");
        }

        Debug.Log("Manager setup complete!");
    }
}
