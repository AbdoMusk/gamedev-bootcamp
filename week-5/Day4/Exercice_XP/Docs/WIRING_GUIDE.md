# WIRING GUIDE - Quick Setup

## Step 1: Create Scene Objects

1. **Create an empty scene** called "Arena"
2. **Create these GameObjects:**
   - Player (Sprite 2D Circle)
   - EnemyPool (Empty GameObject)
   - GameFlowManager (Empty GameObject) 
   - Canvas (for UI)
   - SpawnPoint (Empty, marks enemy spawn location)

---

## Step 2: Setup Player (Top-Down Arena)

### Player GameObject:
1. **Add Components:**
   - Rigidbody2D (Body Type: Dynamic, Gravity Scale: 0, Constraints: Freeze Rotation Z)
   - CircleCollider2D (Radius: 0.5)
   - HealthComponent
   - DamageReceiver
   - PlayerMovement
   - PlayerInputHandler
   - PlayerHealth
   - PlayerCombat

2. **Assign Values:**
   - PlayerMovement: moveSpeed = 5
   - PlayerCombat: attackDamage = 10, attackRange = 1, attackCooldown = 0.5
   - HealthComponent: maxHealth = 100
   - **Tag the player as "Player"**

---

## Step 3: Create Enemy Prefab

1. **Create a prefab folder:** `Assets/Week5Day4/Prefabs`
2. **Create empty GameObject** called "Enemy"
3. **Add Components:**
   - Rigidbody2D (Body Type: Dynamic, Gravity Scale: 0)
   - CircleCollider2D (Radius: 0.4)
   - HealthComponent
   - DamageReceiver
   - Enemy
   - Sprite2D Circle (different color than player)

4. **Tag as "Enemy"**
5. **Drag into prefab folder to create prefab**
6. **Delete from scene**

---

## Step 4: Create ScriptableObjects (Data)

### Create EnemyStats:
1. Right-click in Project > Create > Gameplay > Enemy Stats
2. Name it "EnemyStats_Basic"
3. Set values:
   - Max Health: 30
   - Move Speed: 2
   - Attack Damage: 5
   - Attack Range: 1
   - Attack Cooldown: 1.5
   - Score Value: 10

### Create WeaponStats:
1. Right-click > Create > Gameplay > Weapon Stats
2. Name it "WeaponStats_Basic"
3. Set values:
   - Damage: 10
   - Range: 1
   - Cooldown: 0.5

### Create DifficultyConfig:
1. Right-click > Create > Gameplay > Difficulty Config
2. Name it "DifficultyConfig_Normal"
3. Set values:
   - Max Enemies: 10
   - Spawn Rate: 2
   - Enemy Health Multiplier: 1
   - Enemy Damage Multiplier: 1

---

## Step 5: Setup EnemyPool

1. **Select EnemyPool GameObject**
2. **Add Component: EnemyPool**
3. **Assign:**
   - Enemy Prefab: Drag the Enemy prefab
   - Initial Pool Size: 20

---

## Step 6: Setup GameFlowManager

1. **Select GameFlowManager**
2. **Add Component: GameFlowFacade**
3. **Assign:**
   - Difficulty Config: DifficultyConfig_Normal
   - Enemy Stats: EnemyStats_Basic
   - Player: Drag Player from scene
   - Spawn Point: Drag SpawnPoint from scene

---

## Step 7: Setup UI

### Canvas Setup:
1. **Select Canvas**
2. **Create UI elements (Right-click > UI > ...):**
   - Text (Score) - for displaying score
   - Text (Health) - for displaying player health
   - Button (Pause)
   - Button (Resume)
   - Button (Restart)
   - Panel (GameOverPanel) - set inactive by default

### Add UIManager:
1. **Select Canvas**
2. **Add Component: UIManager**
3. **Assign all UI elements:**
   - Score Text
   - Health Text
   - Pause Button
   - Resume Button
   - Restart Button
   - Game Over Panel

---

## Step 8: Test

1. **Play the scene**
2. **WASD** to move player
3. **Left click** to attack enemies
4. **Enemies spawn** and chase/attack
5. **Score increases** when you kill enemies
6. **Health decreases** when enemies hit you
7. **Pause** button hides and **Resume** button appears
8. **Resume** button hides pause and shows resume button
9. **Restart** button reloads the scene

---

## Quick Troubleshooting

- **Player can't attack?** 
  - Verify enemies have **"Enemy" tag** (case-sensitive)
  - Check enemies have **CircleCollider2D** component
  - Check PlayerCombat attackRange overlaps enemy position
  - Check Console for warning messages

- **Enemies not spawning?** 
  - Check EnemyPool prefab is assigned
  - Check Difficulty Config maxEnemies > 0
  - Check Difficulty Config spawnRate > 0

- **UI not updating?** 
  - Check UIManager is on Canvas with all fields assigned
  - Check both text elements are TextMeshProUGUI

- **Resume button always visible?**
  - Resume button should be inactive at start
  - It activates when Pause is clicked
  - It deactivates when Resume is clicked

- **No movement?** 
  - Check Rigidbody2D gravity is 0
  - Check Horizontal/Vertical input axes exist

- **Performance issues?** 
  - Pool size might be too small - increase it
  - Check profiler for GC allocations

---

## File Structure

```
Assets/Week5Day4/
├── Scripts/
│   ├── Components/
│   │   ├── HealthComponent.cs
│   │   └── DamageReceiver.cs
│   ├── Player/
│   │   ├── PlayerMovement.cs
│   │   ├── PlayerInputHandler.cs
│   │   ├── PlayerHealth.cs
│   │   └── PlayerCombat.cs
│   ├── Enemy/
│   │   ├── MovementStrategy.cs
│   │   ├── EnemyState.cs
│   │   ├── Enemy.cs
│   │   ├── EnemyPool.cs
│   │   └── EnemyFactory.cs
│   ├── Systems/
│   │   ├── EventManager.cs
│   │   └── GameFlowFacade.cs
│   ├── Data/
│   │   ├── EnemyStats.cs
│   │   ├── WeaponStats.cs
│   │   └── DifficultyConfig.cs
│   └── UI/
│       └── UIManager.cs
├── Prefabs/
│   └── Enemy.prefab
└── Scenes/
    └── Arena.unity
```
