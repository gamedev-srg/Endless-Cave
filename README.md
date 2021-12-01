# The Endless Cave

## The game:
A claustrophobic Thief finds himself trapped in a nightmarish cave, can he escape?
Make your way through this endless cave and look for the treasure to pass into the next level!
Avoid enemies and break through the cave walls to carve your path!
[Heres the Itch.io link to the game](https://g-r-s.itch.io/endless-cave)

## The player:
* Move using the Arrow Keys or mouse.
* You can mine walls by pressing X and the arrow keys to break down an adjacant wall, but be warned! You are slowed while mining!
* Reach the treasure at the top right corner to pass into the next level.
* You have 3 HP, don't get hit!

## Enemies:
* The enemies in the cave will hunt you down if they have a path to you, and if they are in range to detect you.
* Some of them may be hiding so be careful!
* Enemies increase in amount, speed, and detection radius as you progress through the game!

## In regards to the task:
This game is for sections E and F in the homework pdf.

### I edited the following files (Usually means addition or altering of methods):
* [Target mover](https://github.com/gamedev-srg/Endless-Cave/blob/main/Assets/Scripts/2-player/TargetMover.cs) - added some Set method, namely the setSpeed and setTarget methods
* [KeyboardMoverByTile](https://github.com/gamedev-srg/Endless-Cave/blob/main/Assets/Scripts/2-player/KeyboardMoverByTile.cs) and [KeyboardMover](https://github.com/gamedev-srg/Endless-Cave/blob/main/Assets/Scripts/2-player/KeyboardMover.cs) - Enabled the user to move continuously by holding the movement buttons, and limited the movement speed.
* [Radius watcher](https://github.com/gamedev-srg/Endless-Cave/blob/main/Assets/Scripts/3-enemies/RadiusWatcher.cs) - added set method to easily and dynamically change values inside.
* [Tilemap Cave Generator](https://github.com/gamedev-srg/Endless-Cave/blob/main/Assets/Scripts/4-generation/TilemapCaveGenerator.cs) - Added the GenerateMap method, to generate the tilemap easily from outside classes, added there several tweaking options and an option whether or not to increase the level, or not.

### I added the following files:
* [Enemy Generator](https://github.com/gamedev-srg/Endless-Cave/blob/main/Assets/Scripts/3-enemies/EnemyGenerator.cs) - to easily add enemies in certain locations, with opetions to change settings (Which enemies, locations etc..)
* [Health System](https://github.com/gamedev-srg/Endless-Cave/blob/main/Assets/Scripts/2-player/HealthSystem.cs) - Simple health system to control player health and indicate damage taken.
* [WinAndPass](https://github.com/gamedev-srg/Endless-Cave/blob/main/Assets/Scripts/2-player/WinAndPass.cs) - Level passing system attached to a certian object, deals with generation,passing,and restarting levels.
* [Mine Rocks](https://github.com/gamedev-srg/Endless-Cave/blob/main/Assets/Scripts/2-player/MineRocks.cs) - Allows player to Destory objects and replace them(Can change what to mine, and what to replace with) at the cost of movement speed.


I experienced some performence issues playing on the web, in the editor it was fine.


