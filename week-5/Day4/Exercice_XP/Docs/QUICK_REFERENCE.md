# QUICK REFERENCE - Architecture at a Glance

## Patterns by File

| File | Pattern | Purpose |
|------|---------|---------|
| `HealthComponent.cs` | Component | Shared health logic |
| `DamageReceiver.cs` | Component | Receive damage interface |
| `EventManager.cs` | Observer | Event-driven communication |
| `PlayerMovement.cs` | SOLID | Single Responsibility |
| `PlayerInputHandler.cs` | SOLID | Single Responsibility |
| `PlayerCombat.cs` | SOLID | Single Responsibility |
| `PlayerHealth.cs` | SOLID | Single Responsibility |
| `EnemyState.cs` | State | AI state machine |
| `MovementStrategy.cs` | Strategy | Swappable movement |
| `Enemy.cs` | State + Strategy | Combined patterns |
| `EnemyFactory.cs` | Factory | Enemy creation |
| `EnemyPool.cs` | Object Pool + Singleton | Reuse objects |
| `GameFlowFacade.cs` | Facade + Singleton | Game control |
| `UIManager.cs` | Mediator | UI communication |

---

## How Data Flows

```
Input → PlayerInputHandler → PlayerMovement/PlayerCombat
                           ↓
                    Events (EventManager)
                           ↓
        UI Updates ← UIManager ← Event listeners
        
Enemy Spawn → EnemyFactory → EnemyPool → Enemy
Enemy AI → State Machine → Strategy (Movement)
        ↓
    Events → GameFlowFacade → Score/UI
```

---

## Key Concepts

### SOLID Principles
- **S**ingle: One class, one job
- **O**pen/Closed: Add features without modifying existing code
- **L**iskov: Strategies can replace each other
- **I**nterface: IMovementStrategy, not specific classes
- **D**ependency: Inject stats, strategies, etc.

### Design Patterns
1. **Factory** → Abstracted creation
2. **Object Pool** → Performance
3. **Component** → Code reuse
4. **Facade** → Simplified interface
5. **Observer** → Loose coupling
6. **State** → Clean behavior
7. **Strategy** → Swappable logic
8. **Mediator** → UI isolation
9. **Flyweight** → Memory efficiency

---

## Common Changes (How to Extend)

### Add New Enemy Type
1. Create new EnemyStats ScriptableObject
2. That's it! Factory handles the rest

### Add New Movement Strategy
1. Create class: `public class NewStrategy : IMovementStrategy`
2. Implement `Move()` method
3. Set in Enemy: `movement = new NewStrategy()`

### Add New Enemy State
1. Create: `public class NewState : EnemyState`
2. Implement `Update()` method
3. Transition in other states: `enemy.SetState(new NewState(enemy))`

### Change Game Settings
1. Edit DifficultyConfig ScriptableObject in Inspector
2. Adjust EnemyStats values
3. No code changes needed

---

## Performance Tips

✅ DO:
- Use object pool for repeated objects
- Communicate via events
- Store data in ScriptableObjects
- Use state machines instead of if-statements
- Reuse components

❌ DON'T:
- Instantiate/destroy every frame
- Have direct references everywhere
- Hardcode values
- Use lots of if-statements in Update
- Make everything a Singleton

---

## Testing in-game

Press play and verify:
```
WASD = Move
Click = Attack enemies
Enemies spawn and attack back
Score increases (watch text)
Health decreases (watch text)
Pause/Resume/Restart buttons work
No lag with 10 enemies
```

If enemies aren't spawning, check:
- EnemyPool has Enemy prefab assigned
- Enemy prefab has "Enemy" tag
- DifficultyConfig maxEnemies > 0

---

## Why This Architecture?

**Without patterns:** Messy, hard to change, slow, buggy
**With patterns:** Clean, scalable, fast, maintainable

Every pattern solves a problem:
- Factory → Don't hardcode creation
- Pool → Don't allocate constantly
- Component → Don't duplicate code
- Facade → Don't expose complexity
- Observer → Don't create dependencies
- State → Don't use endless if-statements
- Strategy → Don't hardcode behavior
- Mediator → Don't let UI touch game logic

**Result:** Professional architecture in ~600 lines of code
