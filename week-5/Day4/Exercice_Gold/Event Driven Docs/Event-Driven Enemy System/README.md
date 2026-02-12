# Event-Driven Enemy System (Hard & Compact)

## Deliverables
- One Unity scene: use `Assets/Week5Day4/Scenes/Arena.unity` (configured per wiring below)
- Scripts used in-scene (5): `EnemyData_SO`, `EnemyPool`, `Enemy`, `EnemyState` (3 states), `GameFacade`
- This README (½ page)

## Why no Update() was used
The enemy lifecycle (spawn → active → damaged → dead → respawn) is driven by events and coroutines. There is no frame-polling decision loop, so behavior is explicit and testable.

## How states transition without conditionals
There are 3 states (`InactiveState`, `ActiveState`, `DeadState`), each its own class. Transitions happen only via events:
- `InactiveState` listens to `EventManager.OnEnemySpawnRequested` and transitions to `ActiveState`.
- `ActiveState` listens to `EventManager.OnEnemyDamageRequested` and applies damage.
- `HealthComponent.OnDeath` triggers `DeadState`.
No enum/switch is used.

## How pooling is safely handled
Pool size is 1. The pool instantiates once in `EnemyPool.Awake()` (so it’s ready before `GameFacade.Start()`). On death, `GameFacade` returns the enemy to the pool and respawns after `EnemyData_SO.respawnDelay`. States subscribe/unsubscribe in `Enter/Exit` to avoid event leaks across reuse.

## What would break at 100 enemies
If we scaled to 100 enemies, the **global command events** would need to be targeted per-enemy (IDs or per-instance event channels), otherwise all active enemies would react to the same damage request. Pool size and event routing would need to expand, but the state + pooling pattern remains the same.
