**Michael Lofton**

s218033

Introduction to C#

Assesment 1

# I. Requirements

 1. **Description of Problem**

    **Name:** Assesment 1
    
    **Problem Statement:**

    Choose one of the games made in class(text based adventure, battle arena game, rpg shop)  and build upon it in order to meet the requirements.
    
 2. **Input Information:**
    * The player inputs their character's name.
    * The player inputs the number of a given option.
 3. **Output Information:**
    * The playerName will become the inputed string.
    * A result will happen based on the chosen option.
 4. **User Interface Information:**
    * The UI outside of battle displays prompts for the player and their options. 
    * The battle UI displays the player's stats, the enemy's stats, and the player's battle options.         

# II. Design 
  1. *System Architecture*
        
        The Game class handles the game scenes and initialization of player, enemy, and item stats. The Entity class is used to create enemies for the battle scene. Player inherits from entity and provides logic needed for the player: their stats, job, items, gold, and keys. The Shop takes in an array of items and will sell those items to the player. The player will progress through three events. The first event is a story sequence that will either progress the game or bring the player to the restart menu based on their choice. The second event is a puzzle that will damage the player or progress the game based on player choice. The final event is a battle against three enemies. While in battle the player can attack, equip items, buy items in the shop, save the game state, or quit the game. 
  2. *Object Information*
        
        * **File Name:** Game.cs
            * Name: Scene(enum)              
                * Description: Provides names for each scene of game.
                * Visibility: public
            * Name: ItemType(enum)               
                * Description: Provides names for item types.
                * Visibility: public
            * Name: Item(struct)
                * Description: The item object.
                * Visibility: public          
            * **Class Name:** Game          
            * Name: _gameOver(bool)               
                * Description: Used to end the game.
                * Visibility: private
            * Name: _currentScene(Scene)
               * Description: Used to keep track of each scene of the game.
               * Visibility: private
            * Name: _player(Player)
               * Description: Stores the player's information.
               * Visibility: private
            * Name: _playerName(string)
               * Description: Stores the player's name.
               * Visibility: private
            * Name: _warriorItems(Item[])
               * Description: The array of the warrior class' items.
               * Visibility: private
            * Name: _guardianItems(Item[])
               * Description: The array of the guardian class' items.
               * Visibility: private
            * Name: _archerItems(Item[])
               * Description: The array of the archer class' items.
               * Visibility: private
            * Name: _enemies(Entity[])
               * Description: The array of enemies.
               * Visibility: private
            * Name: _currentEnemyIndex(int)
               * Description: Used to keep track of the current enemy in the enemies array.
               * Visibility: private
            * Name: _currentEnemy(Entity)
               * Description: Keeps track of the current enemy's stats.
               * Visibility: private
            * Name: _sinner(Entity)
               * Description: Variable for the sinner enemy.
               * Visibility: private
            * Name: _wolves(Entity)
               * Description: Variable for the wolf enemy.
               * Visibility: private
            * Name: _magma(Entity)
               * Description: Varibale for the magma enemy.
               * Visibility: private
            * Name: _shop(Shop)
               * Description: Stores the shop's information.
               * Visibility: private
            * Name: _shopInventory(Item[])
               * Description: Array that contains the shop's items.
               * Visibility: private
            * Name: _healthPotion(Item)
               * Description: The health potion shop item.
               * Visibility: private
            * Name: _attackPotion(Item)
               * Description: The attack potion shop item.
               * Visibility: private 
            * Name: _defensePotion(Item)
               * Description: The defense potion shop item.
               * Visibility: private
            * Name: Run()
                * Description: Starts the main game loop.
                * Visibility: public
                * Arguments: None
            * Name: Start()
                * Description: Initializes starting values.
                * Visibility: private
                * Arguments: None
            * Name: Update()
                * Description: Called every time the game loops.
                * Visibility: private
                * Arguments: None
            * Name: End()
                * Description: Called before the game closes.
                * Visibility: private
                * Arguments: None
            * Name: Save()
                * Description: Saves the current game state.
                * Visibility: private
                * Arguments: None
            * Name: Load()
                * Description: Loads the player's saved game.
                * Visibility: private
                * Arguments: None
            * Name: GetInput()
                * Description: Recieves an input from the player.
                * Visibility: private
                * Arguments: description, options
            * Name: DisplayCurrentScene()
                * Description: Calls scenes based on the current scene index.
                * Visibility: private
                * Arguments: None
            * Name: DisplayStartMenu()
                * Description: Displays the games start menu.
                * Visibility: private
                * Arguments: None
            * Name: DisplayRestartMenu()
                * Description: Displays the games restart menu.
                * Visibility: private
                * Arguments: None
            * Name: GetPlayerName()
                * Description: Allows the player to set his name.
                * Visibility: private
                * Arguments: None
            * Name: CharacterSelection()
                * Description: Allows the player to choose their class.
                * Visibility: private
                * Arguments: None
            * Name: DisplayStats()
                * Description: Displays entity stats.
                * Visibility: private
                * Arguments: character
            * Name: InitializeEquipment()
                * Description: Initalizes player's starting equipment.
                * Visibility: private
                * Arguments: None
            * Name: InitializeShopItems()
                * Description: Initializes the shop's items.
                * Visibility: private.
                * Arguments: None
            * Name: InitializeEnemies()
                * Description: Initializes the enemy's stats and current enemy index.
                * Visibility: private
                * Arguments: None
            * Name: DisplayEquipMenu()
                * Description: Displays menu that lets player equip items.
                * Visibility: private
                * Arguments: None
            * Name: Battle()
                * Description: Displays the battle btween player and enemy and all the player's options.
                * Visibility: private
                * Arguments: None
            * Name: CheckBattleResults()
                * Description: Checks if the player or enemy has died and updates player's gold.
                * Visibility: private
                * Arguments: None
            * Name: TryEndGame()
                * Description: Ends game if all enemies in array have been defeated.
                * Visibility: private
                * Arguments: None
            * Name: FirstEvent()
                * Description: First event of the game.
                * Visibility: private
                * Arguments: None
            * Name: SecondEvent()
                * Description: Second event of the game.
                * Visibility: private
                * Arguments: None
            * Name: GetShopMenuOptions()
                * Description: Gets the options displayed in the shop.
                * Visibility: private
                * Arguments: None
            * Name: DisplayShopMenu()
                * Description: Displays the shop screen.
                * Visibility: private
                * Arguments: None
            * Name: EnemyDescriptions()
                * Description: Displays enemy descriptions.
                * Visibility: private
                * Arguments: None
        * **File Name:** Entity.cs
            * **Class Name:** Entity
            * Name: _name(string)
                * Description: The entity's name.
                * Visibility: private
            * Name: _health(float)
                * Description: The entity's health.
                * Visibility: private
            * Name: _attackPower(float)
                * Description:The entity's attack power.
                * Visibility: private
            * Name: _defensePower(float)
                * Description: The entity's defense power.
                * Visibility: private
            * Name: Name(string)
                * Description: Gets name.
                * Visibility: public
            * Name: Health(float)
                * Description: Gets health and sets it to value.
                * Visibility: public
            * Name: AttackPower(float)
                * Description: Gets attack power and sets it to value.
                * Visibility: public
            * Name: DefensePower(float)
                * Description: Gets defense power and sets it to value.
                * Visibility: public
            * Name: Entity()
                * Description: Entity constructor. Sets name to default and stats to zero.
                * Visibility: public
                * Arguments: None
            * Name: Entity()
                * Description: Entity constructor. Used for entity's stats.
                * Visibility: public
                * Arguments: name, health, attackPower, defensePower
            * Name: TakeDamage()
                * Description: Calculate the damage taken from an attack.
                * Visibility: public
                * Arguments: damageAmount
            * Name: Attack()
                * Description: Inflicts damage to the defending character.
                * Visibility: public
                * Arguments: defender
            * Name: Save()
                * Description: Saves entity name and stats.
                * Visibility: public
                * Arguments: writer
            * Name: Load()
                * Description: Loads entity name and stats.
                * Visibility: public
                * Arguments: reader
        * **File Name:** Player.cs
            * **Class Name:** Player
            * Name: _equipment(Item[])
                * Description: Array of player items.
                * Visibility: private
            * Name: _currentEquipment(Item)
                * Description: The player's currently equipped item.
                * Visibility: private
            * Name: _currentEquipmentIndex(int)
                * Description: The current index of the equipment array.
                * Visibility: private
            * Name: _keys(int)
                * Description: The player's shop keys.
                * Visibility: private
            * Name: _job(string)
                * Description: Stores the player's job class.
                * Visibility: private
            * Name: _gold(int)
                * Description: The player's gold.
                * Visibility: private
            * Name: DefensePower(float)
                * Description: Gets defense power.
                * Visibility: public
            * Name: AttackPower(float)
                * Description: Gets attack power.
                * Visibility: public
            * Name: Health(float)
                * Description: Gets health.
                * Visibility: public
            * Name: CurrentEquip(Item)
                * Description: Gets current equipment.
                * Visibility: public
            * Name: Job(string)
                * Description: Gets job. Sets job to value.
                * Visibility: public
            * Name: Keys(int)
                * Description: Gets keys. Sets keys to value.
                * Visibility: public
            * Name: Gold(int)
                * Description: Gets gold. Sets gold to value.
                * Visibility: public
            * Name: Player()
                * Description: Player constructor. Used for array of player items.
                * Visibility: public
                * Arguments: items
            * Name: Player()
                * Description: Player constructor. Used for player stats.
                * Visibility: public
                * Arguments: name, health, attackPower, defensePower, gold, items, job, keys
            * Name: TryEquip()
                * Description: Equips item at selected index.
                * Visibility: public
                * Arguments: index
            * Name: Buy()
                * Description: Buys item from shop.
                * Visibility: public
                * Arguments: item
            * Name: GetItemNames()
                * Description: Gets the names of player's items.
                * Visibility: public
                * Arguments: None
            * Name: Save()
                * Description: Saves player's job, stats, gold, keys, and equipment index.
                * Visibility: public
                * Arguments: writer
            * Name: Load()
                * Description: Loads player's saved values.
                * Visibility: public
                * Arguments: reader
        * **File Name:** Shop.cs
            * **Class Name:** Shop
            * Name: _gold(int)
                * Description: The shop's gold.
                * Visibility: private
            * Name: _inventory(Item[])
                * Description: Shop's inventory array.
                * Visibility: private
            * Name: Shop()
                * Description: Shop constructor. Adds items to shop's inventory.
                * Visibility: public
                * Arguments: items
            * Name: Sell()
                * Description: Sells items to player.
                * Visibility: public
                * Arguments: player, itemIndex
            * Name: GetItemNames()
                * Description: Gets names if shop items.
                * Visibility: public
                * Arguments: None