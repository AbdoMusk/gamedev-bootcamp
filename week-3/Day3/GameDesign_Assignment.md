# Game Design Assignment: Medina Cats

## Initial Concept & SCAMPER

**Goal, Rules, Core Loop (3 lines)**
*   **Goal:** Ascend to the highest minaret with a stolen fish without being caught.
*   **Rules:** You can climb rugs but slip on smooth tiles; getting hit by a broom loses the fish.
*   **Core Loop:** Climb vertical walls → Dodge angry merchants → Snatch food → Reach safety.

**Key Action Feedback**
*   **Action:** Landing a long jump safely.
*   **Feedback:** A puff of dust appears at your paws and a soft "thump" sound plays.

**SCAMPER Technique (Favorite Game: Simple "Snake")**
*   **Substitute:** Replace the Snake with a Conductor picking up passengers (Game: "Train Sim").
*   **Combine:** Snake + Shooter. You shoot barriers to clear your own path.
*   **Adapt:** 3D space. You can move up/down in a cube volume.
*   **Modify:** You don't grow longer; you get faster and the map gets smaller.
*   **Put to another use:** Use the snake movement to paint a canvas (Art tool).
*   **Eliminate:** No walls. If you go off-screen, you wrap around (Pac-Man style).
*   **Reverse:** You are the apple running away from the AI-controlled snake.

---

## Exercises

## Exercise: About the Game
*   **One Line:** Stray Cat (Player) → Parkours/Sneaks → Steals a fresh sardine (Goal).
*   **Goal:** Fill your belly with fish from the market stalls.
*   **Rules:**
    1.  **The Floor is Danger:** If you touch the street level, kids chase you away (Game Over).
    2.  **Stamina Grip:** You can only hang on vertical rugs for 3 seconds before sliding.
*   **Feedback:** When you grab a fish, the music speeds up and the cat makes a happy "Mraow!" sound while a fish icon animates into your UI.
*   **Rule Change:** If we removed the "Floor is Danger" rule, players would just walk on the ground to the fish. It would turn a vertical platformer into a boring walking simulator.

## Exercise: Pillars
*   **Pillars:** Mischievous • Vertical • Authentic.
*   **Non-Goal:** No combat. You are a small cat, you run from trouble; you don't fight guards or dogs.

## Exercise: MDA
1.  **Mechanic:** "Swat" button. **Feeling:** Naughty/Playful (knocking over pots).
2.  **Mechanic:** Wall-Climb on rugs. **Feeling:** Tactile/Grippy.
3.  **Mechanic:** Hiding in clay jars. **Feeling:** Safe/Sneaky.
*   **Remove "Hiding":** If you remove the jars, the game feels much faster and more panic-inducing because you can never stop to breathe.

## Exercise: Core Loop & Progression
*   **Core Loop:** Spot Lunch → Climb Route → Distract Shopkeeper → Snatch Fish → Escape to Rooftop → Repeat.
*   **Progression (Mechanic: Bouncing Awnings)**
    1.  **Teach:** A wide, stationary awning that bounces the player up automatically when they land on it.
    2.  **Test:** A gap between two roofs that requires using an awning to cross.
    3.  **Twist:** The awning is wind-blown; it moves up and down. You have to time the jump.
    4.  **Master:** A sequence of 3 moving awnings where you must bounce without touching solid ground to reach the windowsill.

## Exercise: Goals, Rules, Feedback, Rewards
*   **Goal:** Reach the open window of the Royal Kitchen.
*   **Rule 1:** Blue tiles are cool/safe to stand on.
*   **Rule 2:** Red tiles are hot sun-baked/damage you over time.
*   **Feedback (Success):** Cool blue particle ring effect when landing on blue tiles.
*   **Feedback (Fail):** Hissing sound and steam effect if you touch red tiles.
*   **Reward:** Collect 100 fish bones to unlock a "Fez" hat for your cat.

## Exercise: Fair Challenge & Balance
*   **Fairness Tool:** **Coyote Time**.
    *   *Why:* Beginners often press jump a split-second *after* they run off a ledge. Coyote time lets that jump still count so they don't fall unfairly.
*   **Tuning Knobs:**
    1.  *Grip Stamina Duration:* If players keep falling off rugs, I'll increase this.
    2.  *Merchant View Distance:* If stealth is too hard, I'll shorten the merchant's "vision cone".

## Exercise: Level Design (Teach → Test → Twist → Master)
*   **Concept:** **The Drying Lines (Tightropes)**
    1.  **Teach:** A thick wooden plank between rooftops. Hard to fall off.
    2.  **Test:** A thin clothesline rope. You have to balance (don't run too fast) or you wobble.
    3.  **Twist:** A clothesline with hanging laundry. The laundry blocks your view occasionally.
    4.  **Master:** Jumping between multiple crossing clotheslines while a bird tries to peck you off.
*   **Checkpoints:** Placed immediately *after* the "Twist" but *before* the "Master" section.
    *   *Why:* The Master section is hard. If they die there, we don't want them to have to redo the slow balancing act of the Twist section. It saves frustration.

## Exercise: UX & Accessibility
*   **Accessibility 1:** **High-Contrast "Cat/Prey" Vision**. Toggle a mode that turns the world grey but spotlights climbable paths in yellow and enemies in red.
*   **Accessibility 2:** **Hold-to-Sprint Toggle**. Option to make running automatic so you don't have to hold a button constantly (eases hand strain).
*   **In-Game Prompt:** "Press [SPACE] near a Rug to Climb."
