# Wiring (Exact Steps)

## 0) IMPORTANT: Stop the “click anywhere damages enemy”
That behavior comes from the existing Player scripts:
- `PlayerInputHandler` calls `PlayerCombat.Attack()` on mouse click.
- `PlayerCombat` overlap-checks and damages objects tagged `Enemy`.

For this assignment scene:
- Remove the Player object from the scene, OR
- Disable/remove `PlayerInputHandler` and/or `PlayerCombat` components.

Damage must come ONLY from the UI button → Facade.

## 1) Create the ScriptableObject data
- In Project window: right-click → Create → Gameplay → Enemy Data
- Name: `EnemyData_Main`
- Set:
  - `maxHealth` = 20
  - `respawnDelay` = 2
  - `moveSpeed` = 2.5 (optional follow)

## 2) Scene objects
Create these GameObjects:

### A) EnemyPoolManager
- Add `EnemyPool`
- Assign `enemyPrefab` = `Prefabs/Enemy.prefab`
- `poolSize` = 1

### B) GameManager
- Add `GameFacade`
- Assign:
  - `enemyData` = `EnemyData_Main`
  - `spawnPoint` = a child empty transform `SpawnPoint`
  - `enemyPool` = drag `EnemyPoolManager`
  - `followTarget` = (optional) drag your Player transform
  - `simulatedDamage` = 5 (or any)

### C) UI Button
- Create UI Button (TextMeshPro is fine)
- OnClick: drag `GameManager` → `GameFacade.SimulateDamage()`
  - (Use `SimulateDamage()` so damage is data/config driven, not hard-coded in OnClick.)

## 4) Player and collision notes
To enable contact damage and player health updates:
- Ensure your Player GameObject is tagged `Player` (Inspector → Tag)
- The Player must have a `HealthComponent` and `PlayerHealth` script attached
- If using physics collisions, ensure both Enemy prefab and Player have Collider2D components; to detect triggers, set one collider to "Is Trigger"
- If you want to disable click-to-attack, remove or disable `PlayerInputHandler` and/or `PlayerCombat` components

## 3) Enemy prefab
Open `Prefabs/Enemy.prefab` and ensure:
- Has `Enemy` component
- Has `HealthComponent` component
- Has Collider2D (any) if you still keep physics

## Expected test
- Press Play → enemy spawns
- Click button → enemy takes damage (event-driven)
- Enemy dies → pooled → respawns after delay
- If followTarget assigned → enemy moves toward it (no Update used)
