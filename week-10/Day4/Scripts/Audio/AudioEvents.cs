using System;

public static class AudioEvents
{
    public static event Action GameStarted;
    public static event Action GameOver;
    public static event Action BirdFlyStart;
    public static event Action BirdFlyStop;
    public static event Action BirdHit;
    public static event Action Score;

    public static void RaiseGameStarted() => GameStarted?.Invoke();
    public static void RaiseGameOver() => GameOver?.Invoke();
    public static void RaiseBirdFlyStart() => BirdFlyStart?.Invoke();
    public static void RaiseBirdFlyStop() => BirdFlyStop?.Invoke();
    public static void RaiseBirdHit() => BirdHit?.Invoke();
    public static void RaiseScore() => Score?.Invoke();
}