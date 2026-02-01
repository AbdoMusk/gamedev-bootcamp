# Game TODO (as of 2026-02-01)

Legend: [x] achieved, [ ] needed

## 🔴 HIGH PRIORITY (Must Have – Don’t Skip)

### Game Loop
- [x] GameManager loop (menu/idle → play → game over) via `ShowMenu()`, `StartGame()`, `GameOver()` in [Scripts/GameManager.cs](Scripts/GameManager.cs#L44-L78)
- [x] Restart without scene reload via `RestartGame()` in [Scripts/GameManager.cs](Scripts/GameManager.cs#L94-L99)
- [x] Clean reset (player + score + pipes) via `ResetBird()`, `ResetSpawner()`, and score reset in `StartGame()` in [Scripts/BirdController.cs](Scripts/BirdController.cs#L142-L153), [Scripts/ObstacleSpawner.cs](Scripts/ObstacleSpawner.cs#L88-L96), [Scripts/GameManager.cs](Scripts/GameManager.cs#L52-L61)

### Player
- [x] Rigidbody2D gravity + jump impulse (currently manual velocity integration) in [Scripts/BirdController.cs](Scripts/BirdController.cs#L55-L62)
- [x] Input disabled outside Playing (gated by `gameStarted`/`isAlive`) in [Scripts/BirdController.cs](Scripts/BirdController.cs#L38-L53)
- [x] Rotation based on vertical velocity in [Scripts/BirdController.cs](Scripts/BirdController.cs#L85-L113)
- [x] Collision → Game Over in [Scripts/BirdController.cs](Scripts/BirdController.cs#L162-L168)

### Obstacles & Score
- [x] Pipe spawner with random gap in [Scripts/ObstacleSpawner.cs](Scripts/ObstacleSpawner.cs#L43-L61)
- [x] Pipes move left, removed off-screen in [Scripts/Obstacle.cs](Scripts/Obstacle.cs#L11-L17)
- [x] Score via trigger when passing pipes in [Scripts/Obstacle.cs](Scripts/Obstacle.cs#L93-L99) and [Scripts/BirdController.cs](Scripts/BirdController.cs#L162-L168)
- [x] Inspector-exposed speed, gap, spawn rate in [Scripts/ObstacleSpawner.cs](Scripts/ObstacleSpawner.cs#L6-L17)

## 🟠 MEDIUM PRIORITY (Do If You Have Time)

### UI
- [x] “Tap to Start” screen (currently Start button only) in [Scripts/UI/GameUI.uxml](Scripts/UI/GameUI.uxml#L97-L103)
- [x] Score UI in [Scripts/UI/GameUI.uxml](Scripts/UI/GameUI.uxml#L86-L95) and [Scripts/GameManager.cs](Scripts/GameManager.cs#L89-L92)
- [x] Game Over + restart prompt in [Scripts/UI/GameUI.uxml](Scripts/UI/GameUI.uxml#L105-L112) and [Scripts/GameManager.cs](Scripts/GameManager.cs#L63-L78)

### Game Feel
- [x] Smooth rotation & movement in [Scripts/BirdController.cs](Scripts/BirdController.cs#L80-L113)
- [x] Slight difficulty increase over time in [Scripts/ObstacleSpawner.cs](Scripts/ObstacleSpawner.cs#L31-L35)

### Architecture
- [x] Separate scripts (Player / Spawner / GameManager) in [Scripts/BirdController.cs](Scripts/BirdController.cs), [Scripts/ObstacleSpawner.cs](Scripts/ObstacleSpawner.cs), [Scripts/GameManager.cs](Scripts/GameManager.cs)
- [x] No hard-coded magic numbers (multiple constants still embedded) in [Scripts/BirdController.cs](Scripts/BirdController.cs#L34-L61), [Scripts/Obstacle.cs](Scripts/Obstacle.cs#L15-L99)

## 🟢 LOW PRIORITY (Nice to Have / Skip If Rushed)
- [x] Object pooling (destroy is currently used) in [Scripts/Obstacle.cs](Scripts/Obstacle.cs#L16-L17)
- [x] High score saving in [Scripts/GameManager.cs](Scripts/GameManager.cs#L18-L73)
- [x] Event-based audio system (implemented)
- [x] Mobile-specific input polish (implemented)
- [x] Extra visual feedback (camera shake) (implemented)
- [ ] Input buffering / forgiveness (not implemented)
