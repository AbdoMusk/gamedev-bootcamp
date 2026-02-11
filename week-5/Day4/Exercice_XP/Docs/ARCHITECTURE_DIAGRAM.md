# ARCHITECTURE DIAGRAM

## System Overview

```
┌─────────────────────────────────────────────────────────────────────┐
│                         UNITY SCENE (Arena)                          │
├─────────────────────────────────────────────────────────────────────┤
│                                                                      │
│  ┌──────────────────┐      ┌──────────────────┐                    │
│  │     PLAYER       │      │    ENEMY POOL    │                    │
│  ├──────────────────┤      ├──────────────────┤                    │
│  │ • CircleCollider │      │ • Pools 20 objs  │                    │
│  │ • Rigidbody2D    │      │ • Reuses enemies │                    │
│  │ • HealthComp     │      │ • No allocation  │                    │
│  │ • DamageReceiver │      │                  │                    │
│  │ • Movement       │      └──────────────────┘                    │
│  │ • InputHandler   │             ↑                               │
│  │ • Combat         │             │                               │
│  │ • Health Logic   │             │                               │
│  └──────────────────┘      EnemyFactory.CreateEnemy()            │
│         ↓                         ↑                               │
│    WASD Input            [Enemy initialization]                   │
│    Click Attack          [Movement/State setup]                   │
│         ↓                         ↑                               │
│  ┌──────────────────┐      ┌──────────────────┐                    │
│  │   EventManager   │←────→│   GameFlowFacade │                    │
│  ├──────────────────┤      ├──────────────────┤                    │
│  │ • OnEnemyDeath   │      │ • Spawn enemies  │                    │
│  │ • OnPlayerDmg    │      │ • Pause/Resume   │                    │
│  │ • OnScoreChange  │      │ • Restart game   │                    │
│  │ • OnGameStart    │      │ • Check limits   │                    │
│  │ • OnGameEnd      │      │                  │                    │
│  └──────────────────┘      └──────────────────┘                    │
│         ↑                         ↑                               │
│         └──────────────────────────┘                              │
│                    Events Flow                                     │
│                 (No Direct Calls)                                 │
│         ↓                                                          │
│  ┌──────────────────────────────────────┐                         │
│  │          CANVAS / UIManager          │                         │
│  ├──────────────────────────────────────┤                         │
│  │ • Score Text (observer)              │                         │
│  │ • Health Text (observer)             │                         │
│  │ • Pause Button → GameFlowFacade      │                         │
│  │ • Resume Button → GameFlowFacade     │                         │
│  │ • Restart Button → GameFlowFacade    │                         │
│  │ • GameOverPanel (listener)           │                         │
│  └──────────────────────────────────────┘                         │
│                                                                    │
└─────────────────────────────────────────────────────────────────────┘
```

---

## Data Flow Diagram

```
PLAYER SYSTEM:
┌─────────────────────────────────────────┐
│ PlayerInputHandler (ONLY: reads input)  │
│              ↓                          │
│ PlayerMovement (ONLY: moves)            │
│              ↓                          │
│ PlayerCombat (ONLY: deals damage)       │
│              ↓                          │
│ DamageReceiver (delegates to health)    │
│              ↓                          │
│ HealthComponent (health logic)          │
│              ↓                          │
│ PlayerHealth (fires event)              │
│              ↓                          │
│ EventManager.OnPlayerDamaged            │
│              ↓                          │
│ UIManager (updates UI)                  │
└─────────────────────────────────────────┘


ENEMY SYSTEM:
┌─────────────────────────────────────────┐
│ GameFlowFacade.SpawnEnemies()           │
│              ↓                          │
│ EnemyFactory.CreateEnemy()              │
│              ↓                          │
│ EnemyPool.GetEnemy()                    │
│              ↓                          │
│ Enemy.Initialize(stats, player)         │
│              ↓                          │
│ SetState(IdleState)                     │
│              ↓                          │
│ EnemyState.Update() [Every Frame]       │
│   ├─ IdleState → Chase if player near  │
│   ├─ ChaseState → Attack if close       │
│   └─ AttackState → Deal damage          │
│              ↓                          │
│ DamageReceiver.ReceiveDamage()          │
│              ↓                          │
│ HealthComponent.TakeDamage()            │
│              ↓                          │
│ OnDeath event fires                     │
│              ↓                          │
│ EventManager.InvokeEnemyDeath()         │
│              ↓                          │
│ GameFlowFacade.AddScore()               │
│              ↓                          │
│ EventManager.InvokeScoreChanged()       │
│              ↓                          │
│ UIManager updates score                 │
│              ↓                          │
│ EnemyPool.ReturnEnemy() [Reuse]         │
└─────────────────────────────────────────┘
```

---

## Component Attachment Diagram

```
PLAYER GAMEOBJECT:
├─ Transform
├─ Sprite
├─ CircleCollider2D
├─ Rigidbody2D
├─ HealthComponent ──→ maxHealth, events
├─ DamageReceiver ───→ ReceiveDamage()
├─ PlayerMovement ───→ moveSpeed
├─ PlayerInputHandler ─→ reads input only
├─ PlayerCombat ─────→ attackDamage, range
└─ PlayerHealth ─────→ fires events


ENEMY GAMEOBJECT (from pool):
├─ Transform
├─ Sprite
├─ CircleCollider2D
├─ Rigidbody2D
├─ HealthComponent ──→ maxHealth (from stats)
├─ DamageReceiver ───→ ReceiveDamage()
└─ Enemy ───────────→ state machine
                      + movement strategy
                      + reference to player
```

---

## State Machine Diagram

```
┌─────────────────────────────────────────────┐
│              ENEMY STATE MACHINE             │
└─────────────────────────────────────────────┘

     ┌──────────────┐
     │  IdleState   │
     │ (Waiting)    │
     └──────────────┘
           ↓
      Player near?
           ↓
     ┌──────────────┐
     │ ChaseState   │
     │ (Following)  │
     └──────────────┘
           ↓
      Close enough?
           ↓
     ┌──────────────┐
     │ AttackState  │
     │ (Hitting)    │
     └──────────────┘
           ↓
      Player dies?
           ↓
     ┌──────────────┐
     │  DeadState   │
     │ (No logic)   │
     └──────────────┘
           ↓
      Return to pool


NO IF STATEMENTS - Each state handles its own logic
Each transition is a SetState() call
```

---

## Design Pattern Locations

```
┌──────────────────────────────────────────────────────────────┐
│ CREATIONAL PATTERNS                                         │
├──────────────────────────────────────────────────────────────┤
│ Factory ────────→ EnemyFactory.cs                            │
│ Object Pool ────→ EnemyPool.cs + Singleton                  │
│ Singleton ──────→ EnemyPool.cs, GameFlowFacade.cs           │
└──────────────────────────────────────────────────────────────┘

┌──────────────────────────────────────────────────────────────┐
│ STRUCTURAL PATTERNS                                         │
├──────────────────────────────────────────────────────────────┤
│ Component ──────→ HealthComponent.cs, DamageReceiver.cs     │
│ Facade ─────────→ GameFlowFacade.cs                         │
│ Flyweight ──────→ EnemyStats.cs ScriptableObject            │
└──────────────────────────────────────────────────────────────┘

┌──────────────────────────────────────────────────────────────┐
│ BEHAVIORAL PATTERNS                                         │
├──────────────────────────────────────────────────────────────┤
│ Observer ───────→ EventManager.cs + static events           │
│ State ──────────→ EnemyState.cs + IdleState, ChaseState...  │
│ Strategy ───────→ IMovementStrategy interface               │
│ Mediator ───────→ UIManager.cs                              │
└──────────────────────────────────────────────────────────────┘

┌──────────────────────────────────────────────────────────────┐
│ ARCHITECTURAL PRINCIPLES                                    │
├──────────────────────────────────────────────────────────────┤
│ SOLID ──────────→ Split player into 4 controllers           │
│ Data-Driven ────→ EnemyStats, WeaponStats, DifficultyConfig│
│ Event-Driven ───→ Everything communicates via EventManager  │
│ Loose Coupling ──→ No direct references, only events        │
│ High Cohesion ───→ Each script does one thing well         │
└──────────────────────────────────────────────────────────────┘
```

---

## Memory Efficiency (Flyweight)

```
WITHOUT FLYWEIGHT:
Each Enemy stores its own stats (30KB × 20 enemies = 600KB)

WITH FLYWEIGHT:
All enemies point to 1 EnemyStats asset (30KB)
Only runtime data stored: position, health, timers (~5KB × 20 = 100KB)
SAVINGS: 500KB for 20 enemies!

Example:
┌─────────────────────────────────────────┐
│       EnemyStats (ONE ASSET)            │
│  maxHealth, moveSpeed, damage, etc.     │
└─────────────────────────────────────────┘
         ↑         ↑         ↑
         │         │         │
    Enemy1    Enemy2    Enemy3 ... Enemy20
    (runtime  (runtime  (runtime
     data)     data)     data)
```

---

## Event Communication (Observer Pattern)

```
NO DIRECT CALLS:

❌ BAD:
gameFlow.uiManager.UpdateScore(score)
gameFlow.player.TakeDamage(damage)

✅ GOOD:
EventManager.InvokeScoreChanged(score)
UIManager listens → updates on its own

EventManager.InvokePlayerDamaged(damage)
PlayerHealth listens → updates on its own

NO CLASSES KNOW ABOUT EACH OTHER
```

---

## Summary: Why This Works

```
1. SEPARATED CONCERNS
   └─ Each class has one job
   
2. LOOSE COUPLING
   └─ Systems don't know about each other
   
3. EASY TO EXTEND
   └─ Add features without modifying existing code
   
4. HIGH PERFORMANCE
   └─ Pooling, event-driven, optimized loops
   
5. DATA-DRIVEN
   └─ Change behavior in inspector, not code
   
6. SCALABLE
   └─ Can add 100+ enemies, UI, weapons without refactoring
   
7. TESTABLE
   └─ Each component can be tested in isolation
   
8. MAINTAINABLE
   └─ Code is readable, organized, and self-documenting
```
