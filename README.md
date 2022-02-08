# Project-Goosebumps

## Main Premise
Story-Driven Fantasy First Person Sandbox with cool swords, guns, and magic

## Assets
Currently used assets:<br/>
[Scar L 3D Model](https://www.cgtrader.com/free-3d-models/military/gun/scar-l-01574b59-91cf-4a67-902c-fe52300fff9d)<br/>
[Katana 3D Model](https://www.cgtrader.com/free-3d-models/military/melee/touken-ranbu-mikazuki-munechika-sword)<br/>
[Player 3D Model](https://www.cgtrader.com/free-3d-models/character/sci-fi/sci-fi-free-robot-bh-2)<br/>

## Code Write Up/Plan
### Players/Enemies
#### Stats
May need to make this an abstract class, but it contains the health value of players and enemies right now
#### Controller
Abstract parent class for all types of controllers for AI enemies and players
##### PlayerController : Controller
Handles how the player’s input interacts with the environment<br/>
WASD movement<br/>
Rotation using the mouse<br/>
Jumping with spacebar<br/>
Interacting with items using “e”<br/>
Pickup items with the tag “inventoryitem”<br/>
Call the interact() method on items with the “interactable” tag<br/>
Should attack be in here or in weapon?<br/><br/>
Should switching weapons with 1,2,3 be in here or in inventory?
##### EnemyController : Controller
Handles how an enemy moves and thinks<br/>
The name of this script may change depending on the different types of enemies I implement, that’s why all of them will inherit from the Controller class
#### Inventory/Weapons
##### Inventory
Loadout : list of 3? Weapons the player has equipped<br/>
Items : list of all the gameobjects the player has picked up
##### Weapon
The abstract parent class for all weapons<br/>
Contains basic information about the weapon like base damage, headshot multiplier, full auto, etc.<br/>
The point of this is so that we can call GetComponent<Weapon> and get every type of weapon’s attack functions
##### Gun : Weapon
Handles bullet lines, gun animations, hit detection when attacking, and fire rate logic
##### Sword : Weapon
Handles sword animations, hit detection when attacking, and swing rate logic
##### Magic : Weapon
Handles magic animations, hit detection when attacking, and fire rate logic