using UnityEngine;
using System;

/// <summary>
/// Global event manager for Observer pattern
/// All systems communicate via events - no direct references
/// </summary>
public static class EventManager
{
    // Game events
    public static event Action OnGameStarted;
    public static event Action OnGameEnded;
    public static event Action OnGamePaused;
    public static event Action OnGameResumed;

    // Enemy events
    public static event Action<int> OnEnemySpawned; // enemy count
    public static event Action<int> OnEnemyDeath; // score from enemy

    // Player events
    public static event Action<float> OnPlayerDamaged; // damage amount
    public static event Action OnPlayerDeath;

    // Score events
    public static event Action<int> OnScoreChanged;

    // Invoke methods
    public static void InvokeGameStarted() => OnGameStarted?.Invoke();
    public static void InvokeGameEnded() => OnGameEnded?.Invoke();
    public static void InvokeGamePaused() => OnGamePaused?.Invoke();
    public static void InvokeGameResumed() => OnGameResumed?.Invoke();

    public static void InvokeEnemySpawned(int count) => OnEnemySpawned?.Invoke(count);
    public static void InvokeEnemyDeath(int scoreValue) => OnEnemyDeath?.Invoke(scoreValue);

    public static void InvokePlayerDamaged(float damage) => OnPlayerDamaged?.Invoke(damage);
    public static void InvokePlayerDeath() => OnPlayerDeath?.Invoke();

    public static void InvokeScoreChanged(int score) => OnScoreChanged?.Invoke(score);
}
