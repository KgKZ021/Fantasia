# Fantasia

*A magical-themed 3D survival game with idle and incremental mechanics, developed in Unity.*

---

## 📖 Overview

Fantasia is a thesis project that explores the addictive gameplay loop of idle and survival games by combining real-time combat with incremental progression.  
Inspired by games like *Vampire Survivors*, the player fights waves of enemies, collects power-ups, and survives as long as possible while gaining rewards over time.

Developed entirely in **Unity 2023.3.53f LTS** with C#, Fantasia features:
- Procedurally generated 3D arenas
- Automated combat and passive upgrades
- Collectible weapons and stackable power-ups
- Idle progression mechanics and coin collection
- Modular, scalable code architecture

---

## 🎮 Features

✅ Procedurally generated arena  
✅ Waves of enemies with increasing difficulty  
✅ Automatic and passive attacks  
✅ Weapons and power-ups defined via ScriptableObjects  
✅ Idle coin collection and progression  
✅ Playable characters (with room for more in the future)

---

## 🛠️ Technologies Used

- Unity (2021.3 LTS)
- C#
- TextMeshPro (UI)
- Unity Asset Store assets
- FreeSound.org (for audio)
- Git + GitHub (version control)

---

## 📂 Project Structure
<pre>Assets/
│
├── Scripts/
│   ├── Audio/
│   │   ├── AudioClipRefSO.cs       
│   │   ├── MusicManager.cs         
│   │   ├── PlaySound.cs            
│   │   └── SoundManager.cs         
│   ├── Enviroment/
│   │   ├── ChunkTrigger.cs        
│   │   ├── MapController.cs        
│   │   └── PropRandomizer.cs       
│   ├── Interfaces/
│   │   └── ICollectible.cs         
│   ├── Managers/
│   │   ├── DropRateManager.cs      
│   │   ├── GameManager.cs          
│   │   ├── SaveManager.cs          
│   │   └── SceneController.cs     
│   ├── Monster/
│   │   ├── MonsterMovement.cs      
│   │   ├── MonsterSO.cs           
│   │   ├── MonsterSpawner.cs       
│   │   └── MonsterStats.cs         
│   ├── PassiveItems/
│   │   ├── PassiveItems.cs        
│   │   ├── PassiveItemsSO.cs      
│   │   ├── SpinachPassiveItem.cs  
│   │   └── WingsPassiveItem.cs    
│   ├── PickUps/
│   │   ├── BobbingAnimation.cs     
│   │   ├── CoinPickUp.cs           
│   │   ├── ExpOrbs.cs              
│   │   ├── HealthPotion.cs         
│   │   └── PickUp.cs               
│   ├── Player/
│   │   ├── HealthBarFollow.cs      
│   │   ├── Inventorymanager.cs     
│   │   ├── Player.cs               
│   │   ├── PlayerAnimator.cs       
│   │   ├── PlayerCollector.cs      
│   │   ├── PlayerSO.cs             
│   │   └── PlayerStats.cs          
│   ├── UI/
│   │   ├── UICharacterSelector.cs  
│   │   ├── UICoinDisplay.cs        
│   │   └── UISavedDataDisplay.cs   
│   ├── Weapons/
│   │   ├── WeaponBase/
│   │   │   ├── BaseWeapon.cs      	 
│   │   │   ├── MeleeWeaponBehaviour.cs    
│   │   │   ├── RangedWeaponBehaviour.cs   
│   │   │   └── WeaponSO.cs    	 
│   │   ├── WeaponBehaviours/
│   │   │   ├── DaggerBehaviour.cs   
│   │   │   └── ShieldBehaviour.cs    
│   │   ├── WeaponContrroller/
│   │   │   ├── DaggerController.cs  
│   │   │   └── ShieldController.cs   
│   ├── BreakableProps.cs        	
│   ├── GameInput.cs        		
│   └── SaveOnQuit.cs        		
   </pre> 
---

## 🚀 How to Run

To play **Fantasia**, follow these steps:

1. **Clone the repository**  
   Open a terminal and run:
   ```bash
   git clone https://github.com/KgKZ021/Fantasia.git

2.	Open the project in Unity 2022.3.53f LTS.

3. Allow Unity to compile Unity will import and compile assets—this    may take a moment.

4. Play the game

    In the Unity Editor, press the ▶️ Play button

    Or build a standalone version via:
    
    File > Build Settings > Build

---

## 📈 Future Development

- 🛒 Spendable coins & in-game shop  
- 🧍 More playable characters and character leveling  
- 🗡️ Additional weapons and weapon synergies  
- 👾 Increased enemy variety & adaptive difficulty  
- 🏆 Achievements and milestones  

---

## 👤 Credits

- **Developer**: Kaung Khant Zaw 
- **Artwork**: Unity Asset Store, Hnin Oo Shwe Yi
- **Audio**: FreeSound.org  
- **Tutorials & Learning Resources**: CodeMonkey, Terresquall, Unity Docs
