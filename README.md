# The Forbidden Papyrus

A 3D dungeon crawler game built with **Unity Engine (6000.0.43f1)** using **C#**.

> *Developed as a university assignment — designed, coded, and built from scratch as a solo project.*

---

## About the Game

You play as a treasure hunter exploring a dungeon in search of the legendary **Forbidden Papyrus**. Inside the cave, an immortal skeleton guards the ancient scroll, rival treasure hunters want you dead, and a trapped wizard needs your help.

**Your goal:** Free the wizard, solve the skeleton's riddle, and escape alive.

### Story

The player enters yet another cave to explore. Inside:

- A **Wizard** is imprisoned and offers you a scroll with the answer to the skeleton's riddle — if you free him.
- An immortal **Skeleton** guards the Papyrus and challenges you with a riddle before letting you pass.
- A **Zombie** blocks the exit and demands 4 keys scattered throughout the dungeon.
- **Enemy treasure hunters** roam the dungeon and will attack on sight.

---

## Features

| Feature | Description |
|---|---|
| **Player Movement** | Walk, sprint, jump with full 3D character controller, camera-relative movement, and smooth rotation |
| **AI Enemies** | Two enemy types — a ranged shooter (NavMesh + shooting with cooldown) and a melee attacker (bat with hitbox + animation events) |
| **Dialogue System** | Reusable NPC dialogue system with actor portraits, names, and branching choices (DialogueTrigger + DialogueManager) |
| **Inventory System** | Collectible keys, scrolls, and a "MegaPack" item — tracked in a UI inventory |
| **Coin Collection** | Collectible items with counter UI and sound effects |
| **Door Mechanics** | Animated doors that detect which side the player is on and play the correct open/close animation; locked doors requiring keys |
| **Health System** | Player health bar with damage, healing, and death state (triggers death animation + death screen) |
| **NPC Interactions** | Multiple NPCs (Wizard, Skeleton, Zombie, Alex) each with unique dialogue triggers and game-state effects |
| **Music System** | Ambient music, chase music that triggers dynamically when enemies detect the player, and a skeleton boss theme |
| **Death Screen** | Fade-in death screen with restart/quit options |
| **Main Menu & Options** | Start menu with play, options (settings), and quit |
| **Credits** | Animated credits roll scene |
| **Footstep Audio** | Player footstep sounds synced with movement |

---

## Project Structure

```
Assets/
└── Scripts/
    ├── Player/            # PlayerController, PlayerStats, HealthBar, AnimationsStateController, FootSteps
    ├── Enemies/           # AiLocomotion (ranged), Ai2Locomotion (melee), Bullet, BatHitbox, AIHitController
    ├── NPC/               # DialogueManager, DialogTrigger, ManageChoice, per-NPC triggers (Skeleton, Wizard, Zombie, Teacher)
    ├── Doors/             # DoorManager, DoorTriggerHandler, DoubleDoor, LockDoor, DoorSound
    ├── Money/             # Collectible, CollectibleCount
    ├── inventory/         # Inventory, Key, MegaPack
    ├── Camera/            # CameraManager
    ├── StartMenu/         # MainMenu, OptionsMenu
    ├── DeathScreen/       # DeathScreen, FadeIn, ManageButtons
    └── Credits/           # EndCredits
└── Scenes/
    ├── StartMenu.unity
    ├── Game.unity
    └── Credits.unity
```

---

## Technical Highlights

### Player Controller
Camera-relative 3D movement using Unity's **Input System** and **CharacterController**. Implements custom gravity with a multiplier, sprint toggle with acceleration, and smooth quaternion-based rotation toward the movement direction.

### AI Enemy System
Two types of AI enemies built with Unity's **NavMesh Agent**:
- **Ranged enemy** — Chases the player within a detection range, stops at shooting range, and fires bullets on a cooldown timer. Triggers chase music dynamically via a `MusicManager` singleton.
- **Melee enemy** — Chases and attacks with a bat. Uses **Animation Events** to enable/disable the hitbox precisely during the attack animation, ensuring the player takes damage only once per swing.

### Dialogue System
A modular dialogue system composed of:
- `DialogueTrigger` — detects player proximity and sends messages + actor data to the manager.
- `DialogueManager` — displays messages sequentially with actor portraits, handles branching choices (e.g., the Skeleton's riddle), and triggers game events on dialogue completion (e.g., awarding items, unlocking doors).

### Door System
Doors use two `BoxColliders` with different tags to detect which side the player is approaching from, then play the corresponding open/close animation. Includes locked doors that check the player's inventory for keys.

---

## Built With

- **Unity Engine** 6000.0.43f1 (Unity 6)
- **C#** — all gameplay scripts
- **Unity Input System** — player controls
- **NavMesh** — enemy AI pathfinding
- **TextMesh Pro** — UI text rendering
- **LeanTween** — UI animations
- **Animation Controllers** — character & object state machines

---

## Assets & Credits

**3D Models:**
- [Free Low Poly Human - RPG Character](https://assetstore.unity.com/packages/3d/characters/humanoids/fantasy/free-low-poly-human-rpg-character) — Blink
- [Survival Stylized Characters + 5 Weapons](https://assetstore.unity.com/packages/3d/characters/survival-stylized-characters) — Alex Lenk
- [Lowpoly Magician RIO](https://assetstore.unity.com/packages/3d/characters/humanoids/lowpoly-magician-rio) — VertexModeler Team
- [Stylized Hand Painted Dungeon (Free)](https://assetstore.unity.com/packages/3d/environments/stylized-hand-painted-dungeon) — L2S ARTS
- [3D Characters Zombie City Streets Lowpoly Pack - Lite](https://assetstore.unity.com/packages/3d/characters/humanoids/3d-characters-zombie-city-streets-lowpoly-pack-lite-277409) — CatBorg Studio

**Libraries:**
- [LeanTween](https://assetstore.unity.com/packages/tools/animation/leantween) — Dented Pixel

**Learning Resources:**
- [iHeartGameDev](https://www.youtube.com/@iHeartGameDev)
- [UnityGuy](https://www.youtube.com/@UnityGuy)
- [Codecodile](https://www.youtube.com/@Codecodile)
- [Brackeys](https://www.youtube.com/@Brackeys)
- [Mike's Code](https://www.youtube.com/@MikesCode)

---

## How to Play

### Option 1 — Play the Build
Download the standalone Windows build from the separate repository:  
[**The Forbidden Papyrus — Game Build**](https://github.com/YOUR_USERNAME/Game)

### Option 2 — Open in Unity
1. Install **Unity 6000.0.43f1** (or compatible version).
2. Clone this repository.
3. Open the `4thTime/` folder as a Unity project.
4. Open `Assets/Scenes/StartMenu.unity` and press **Play**.

### Controls
| Action | Key |
|---|---|
| Move | W A S D |
| Sprint | Left Shift |
| Jump | Space |
| Interact / Talk | F |
| Look Around | Mouse |

---
