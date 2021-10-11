using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Assessment1
{
    /// <summary>
    /// Names of game scenes.
    /// </summary>
    public enum Scene
    {
        STARTMENU,
        NAMECREATION,
        CHARACTERSELECTION,
        FIRSTEVENT,
        SECONDEVENT,
        THIRDEVENT,
        RESTARTMENU
    }

    /// <summary>
    /// Item stat boost types.
    /// </summary>
    public enum ItemType
    {
        DEFENSE,
        ATTACK,
        HEALTH,
        NONE
    }

    /// <summary>
    /// Items used throughout game.
    /// </summary>
    public struct Item
    {
        public string Name;
        public float StatBoost;
        public ItemType Type;
        public int Cost;
    }

    class Game
    {
        //Basic Variables
        private bool _gameOver;
        private Scene _currentScene = 0;
        private bool _showEnemyDescription = false;
        //Player related Variables
        private Player _player;
        private string _playerName;
        private Item[] _warriorItems;
        private Item[] _gaurdianItems;
        private Item[] _archerItems;
        //Enemy related Variables
        private Entity[] _enemies;
        private int _currentEnemyIndex = 0;
        private Entity _currentEnemy;
        private Entity _sinner;
        private Entity _wolves;
        private Entity _magma;
        //Shop related Variables
        private Shop _shop;
        private Item[] _shopInventory;
        private Item _healthPotion;
        private Item _attackPotion;
        private Item _defensePotion;

        /// <summary>
        /// Function that starts the main game loop
        /// </summary>
        public void Run()
        {
            Start();

            while (!_gameOver)
            {
                Update();
            }

            End();
        }

        /// <summary>
        /// Function used to initialize any starting values.
        /// </summary>
        private void Start()
        {
            InitializeEnemies();
            InitializeEquipment();
            InitializeShopItems();
            _shop = new Shop(_shopInventory);
        }

        /// <summary>
        /// Funtion that is called every time the game loops.
        /// </summary>
        private void Update()
        {
            DisplayCurrentScene();
            Console.Clear();
        }

        /// <summary>
        /// Function that is called before the game closes.
        /// </summary>
        private void End()
        {
            Console.WriteLine("You have reached the end of your path. Farewell.");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Saves the player's curent game state.
        /// </summary>
        private void Save()
        {
            //Create new stream writer
            StreamWriter writer = new StreamWriter("SaveData.txt");

            //Save the current scene
            writer.WriteLine(_currentScene);

            //Save enemy index
            writer.WriteLine(_currentEnemyIndex);

            //Save player and enemy stats
            _player.Save(writer);
            _currentEnemy.Save(writer);

            //Closes stream writer
            writer.Close();
        }

        private bool Load()
        {
            bool loadSuccessful = true;

            //If the file doesn't exist...
            if (!File.Exists("SaveData.txt"))
                //...returns false
                loadSuccessful = false;

            //Create a new reader to read from the text file
            StreamReader reader = new StreamReader("SaveData.txt");

            if (!Scene.TryParse(reader.ReadLine(), out _currentScene))             
                loadSuccessful = false;

            //If the first line can't be converted into an integer...
            if (!int.TryParse(reader.ReadLine(), out _currentEnemyIndex))
                //...returns false
                loadSuccessful = false;

            string job = reader.ReadLine();

            if (job == "Warrior")
                _player = new Player(_warriorItems);
            else if (job == "Guardian")
                _player = new Player(_gaurdianItems);
            else if (job == "Archer")
                _player = new Player(_archerItems);
            else
                loadSuccessful = false;

            _player.Job = job;

            //Creates a new instance and try load the player          
            if (!_player.Load(reader))
                loadSuccessful = false;

            //Create a new instance and try to load the enemy
            _currentEnemy = new Entity();
            if (!_currentEnemy.Load(reader))
                loadSuccessful = false;

            //Update the array to match the current enemy stats
            _enemies[_currentEnemyIndex] = _currentEnemy;

            //Close the reader once loading is finished
            reader.Close();

            return loadSuccessful;

        }

        /// <summary>
        /// Function recieves an input from the player and will return either an input recieved integer
        /// or label the input invalid.
        /// </summary>
        /// <param name="description">The set up or scenerio</param>
        /// <param name="options">The options the player is given</param>
        /// <returns>Returns the input recieved integer</returns>
        private int GetInput(string description, params string[] options)
        {
            string choice = "";
            int inputRecieved = -1;

            while (inputRecieved == -1)
            {
                Console.WriteLine(description);
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine((i + 1) + ". " + options[i]);
                }
                Console.Write("> ");

                choice = Console.ReadLine();

                if (int.TryParse(choice, out inputRecieved))
                {
                    inputRecieved--;
                    if (inputRecieved < 0 || inputRecieved >= options.Length)
                    {
                        inputRecieved = -1;
                        //Display error message
                        Console.WriteLine("Invalid Input");
                        Console.ReadKey(true);
                    }
                }
                else
                {
                    inputRecieved = -1;
                    Console.WriteLine("Invalid Input");
                    Console.ReadKey(true);
                }

                Console.Clear();
            }

            return inputRecieved;
        }

        /// <summary>
        /// Calls the appropriate functions based on the current scene index.
        /// </summary>
        private void DisplayCurrentScene()
        {
            switch (_currentScene)
            {
                case Scene.STARTMENU:
                    DisplayStartMenu();
                    break;

                case Scene.NAMECREATION:
                    GetPlayerName();
                    break;

                case Scene.CHARACTERSELECTION:
                    CharacterSelection();
                    break;

                case Scene.FIRSTEVENT:
                    FirstEvent();               
                    break;

                case Scene.SECONDEVENT:
                    SecondEvent();
                    break;

                case Scene.THIRDEVENT:
                    //ThirdEvent();                   
                    Battle();
                    CheckBattleResults();
                    Console.ReadKey(true);
                    break;

                case Scene.RESTARTMENU:
                    DisplayRestartMenu();
                    break;

                default:
                    Console.WriteLine("Invalid scene index");
                    break;
            }
        }

        /// <summary>
        /// Displays the menu that allows the player to start new game or load previous one.
        /// </summary>
        private void DisplayStartMenu()
        {
            int input = GetInput("Welcome to Ascension", "Start New Game", "Load Game");

            if (input == 0)
            {
                _currentScene = Scene.NAMECREATION;
            }
            else if (input == 1)
            {
                if (Load())
                {
                    Console.WriteLine("Load Successful!");
                    Console.ReadKey(true);
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Load Failed.");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }
        }

        /// <summary>
        /// Displays the menu that allows the player to restart or quit the game.
        /// </summary>
        private void DisplayRestartMenu()
        {
            int input = GetInput("Would you like to play again?", "Yes", "No");

            if (input == 0)
            {
                InitializeEnemies();
                _currentScene = Scene.STARTMENU;
            }
            else if (input == 1)
            {
                _gameOver = true;
            }
        }

        /// <summary>
        /// Displays text asking for the players name. Doesn't transition to the next section until
        /// the player decides to keep the name.
        /// </summary>
        private void GetPlayerName()
        {
            Console.WriteLine("What is thine name?");
            Console.Write("> ");
            _playerName = Console.ReadLine();

            int input = GetInput($"You've entered {_playerName} are you sure you want to keep this name?",
                "Yes", "No");

            if (input == 0)
            {
                _currentScene = Scene.CHARACTERSELECTION;
            }
            else if (input == 1)
            {
                _currentScene = Scene.NAMECREATION;
            }
        }

        /// <summary>
        /// Allows player to select their class and updates stats accordingly. 
        /// </summary>
        private void CharacterSelection()
        {
            int input = GetInput("Choose thine role.", "Warrior", "Guardian", "Archer");

            if (input == 0)
            {
                _player = new Player(_playerName, 100, 15, 5, 100, _warriorItems, "Warrior", 3);
            }
            else if (input == 1)
            {
                _player = new Player(_playerName, 150, 10, 15, 100, _gaurdianItems, "Guardian", 3);
            }
            else if (input == 2)
            {
                _player = new Player(_playerName, 70, 20, 10, 100, _archerItems, "Archer", 3);
            }

            _currentScene++;
        }

        /// <summary>
        /// Displays player and enemy stats
        /// </summary>
        /// <param name="character"></param>
        private void DisplayStats(Entity character)
        {
            Console.WriteLine($"Name: {character.Name}");
            Console.WriteLine($"Health: {character.Health}");
            Console.WriteLine($"Attack Power: {character.AttackPower}");
            Console.WriteLine($"Defense Power: {character.DefensePower}");          
        }

        /// <summary>
        /// Initializes the players starting equipment.
        /// </summary>
        private void InitializeEquipment()
        {
            //Warrior Items
            Item sword = new Item { Name = "The Sword of Fate", StatBoost = 10, Type = ItemType.ATTACK, Cost = 1 };
            Item armor = new Item { Name = "Sinner's Chestplate", StatBoost = 5, Type = ItemType.DEFENSE, Cost = 1};

            //Guardian Items
            Item shield = new Item { Name = "The Shield of Destiny", StatBoost = 10, Type = ItemType.DEFENSE, Cost = 1 };
            Item knuckles = new Item { Name = "Knuckle Duster", StatBoost = 3, Type = ItemType.ATTACK, Cost = 1 };

            //Archer Items
            Item bow = new Item { Name = "The Bow of Despair", StatBoost = 5, Type = ItemType.ATTACK, Cost = 1 };
            Item necklace = new Item { Name = "Phantom Necklace", StatBoost = 10, Type = ItemType.HEALTH, Cost = 1 };

            //Initialize Arrays
            _warriorItems = new Item[] { sword, armor };
            _gaurdianItems = new Item[] { shield, knuckles };
            _archerItems = new Item[] { bow, necklace };
        }

        /// <summary>
        /// Initializes the items that can be bought from the shop.
        /// </summary>
        private void InitializeShopItems()
        {
            _healthPotion = new Item { Name = "Health Potion", StatBoost = 30, Type = ItemType.HEALTH, Cost = 100 };
            _attackPotion = new Item { Name = "Attack Potion", StatBoost = 30, Type = ItemType.ATTACK, Cost = 150 };
            _defensePotion = new Item { Name = "Defense Potion", StatBoost = 30, Type = ItemType.DEFENSE, Cost = 300 };

            _shopInventory = new Item[] { _healthPotion, _attackPotion, _defensePotion };
        }

        /// <summary>
        /// Initializes the enemy stats and enemies array.
        /// </summary>
        private void InitializeEnemies()
        {
            _currentEnemyIndex = 0;

            _sinner = new Entity("The Nameless Sinner", 25, 10, 5);

            _wolves = new Entity("The Nest of Wolves", 50, 20, 10);

            _magma = new Entity("The Dripping Dinosaurus", 75, 25, 15);

            _enemies = new Entity[] { _sinner, _wolves, _magma };

            _currentEnemy = _enemies[_currentEnemyIndex];
        }

        /// <summary>
        /// Gives you the ability to equip your starting equipment.
        /// </summary>
        private void DisplayEquipMenu()
        {
            int input = GetInput("What do you want to equip?", _player.GetItemNames());

            if (!_player.TryEquip(input))
                Console.WriteLine("The item isn't there.");

            Console.WriteLine($"You equipped {_player.CurrentEquip.Name} !");
        }

        /// <summary>
        /// Displays the battle btween player and enemy and all the player's options.
        /// </summary>
        private void Battle()
        {
            if (!_showEnemyDescription)
            {
                EnemyDescriptions();
                _showEnemyDescription = true;
            }

            DisplayStats(_player);
            Console.WriteLine($"Shop Keys: {_player.Keys}");
            Console.WriteLine("--------------------------");
            DisplayStats(_currentEnemy);

            int input = GetInput($"{_currentEnemy.Name} stands before you. Choose your action.",
                "Attack", "Equip Item", "Shop", "Save", "Quit");

            if (input == 0)
            {
                float damageDealt = _player.Attack(_currentEnemy);
                Console.WriteLine($"You dealt {damageDealt} damage!");

                damageDealt = _currentEnemy.Attack(_player);
                Console.WriteLine($"{_currentEnemy.Name} dealt {damageDealt}");
            }
            else if (input == 1)
            {
                DisplayEquipMenu();
            }
            else if (input == 2)
            {
                if (_player.Keys > 0)
                {                           
                   DisplayShopMenu();
                  _player.Keys -= 1;        
                }
                else
                {
                    Console.WriteLine("You cannot enter the shop");
                }
            }
            else if (input == 3)
            {
                Save();
                Console.WriteLine("Save Successful");
            }
            else if (input == 4)
            {
                _gameOver = true;
            }
        }

        /// <summary>
        /// Checks if the player or enemy has died. And gives player gold accordingly.
        /// </summary>
        private void CheckBattleResults()
        {
            if (_player.Health <= 0)
            {
                Console.WriteLine("You have fallen...");
                Console.ReadKey(true);
                Console.Clear();
                _currentScene = Scene.RESTARTMENU;
            }
            else if (_currentEnemy.Health <= 0)
            {
                Console.ReadKey(true);
                Console.Clear();
                Console.WriteLine($"{_currentEnemy.Name} has fallen.");            
                _player.Gold += 100;
                Console.WriteLine($"You gain 100 gold. Total Gold: {_player.Gold}");

                _currentEnemyIndex++;
                _showEnemyDescription = false;

                if (TryEndGame())
                {
                    return;
                }

                _currentEnemy = _enemies[_currentEnemyIndex];
            }
        }

        /// <summary>
        /// Ends the battle if all enemies in array have died.
        /// </summary>
        /// <returns></returns>
        private bool TryEndGame()
        {
            bool endGame = _currentEnemyIndex >= _enemies.Length;

            if (endGame)
            {
                Console.Clear();
                Console.WriteLine("You have conquered the three beasts");              
                _currentScene = Scene.RESTARTMENU;
            }

            return endGame;
        }

        /// <summary>
        /// Fist event of the game
        /// </summary>
        private void FirstEvent()
        {
            Console.WriteLine("You stand at the entrance of the mighty Celestia Tower. The large " +
                "black structure stands so high that even the birds can't hope to see the top. You" +
                " feel an unnatural energy surrounding the tower. \n");

            int input = GetInput("Will you enter?", "Yes", "No");

            if (input == 0)
            {
                Console.WriteLine("You feel cold. God has abandoned this place.");
                _currentScene++;
                Console.ReadKey(true);
            }
            else if (input == 1)
            {
                Console.WriteLine("You have abandoned your journey. Run home cowardly traveler.");
                Console.ReadKey(true);
                _currentScene = Scene.RESTARTMENU;
            }   
        }

        /// <summary>
        /// Second event of the game
        /// </summary>
        private void SecondEvent()
        {        
            Console.WriteLine("Floor 1 \n");

            Console.WriteLine("Upon entering the door slams shut behind you. Three hallways stand before you" +
                  " Above the entryway of each hall is a sign marked with an animal. A dog, A bird, and A chimp. " +
                  "\nA voice enters your mind. Which beast is above all? \n");

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Your health: {_player.Health} \n");

                int input = GetInput("Which path will you choose", "The Dog", "The Bird", "The Chimp");

                if (input == 0)
                {
                    Console.WriteLine("Spikes stick up from the ground piercing your body. " +
                        "You take 30 damage");
                    _player.Health -= 30;
                    Console.ReadKey(true);

                    if (_player.Health <= 0)
                    {
                        _currentScene = Scene.RESTARTMENU;
                        break;
                    }
                }
                else if (input == 1)
                {
                    Console.WriteLine("You have chosen correctly.");
                    Console.ReadKey(true);
                    _currentScene++;
                    break;
                }
                else if (input == 2)
                {
                    Console.WriteLine("Rocks fall from the ceiling crushing your body. " +
                       "You take 30 damage");
                    _player.Health -= 30;
                    Console.ReadKey(true);

                    if (_player.Health <= 0)
                    {
                        _currentScene = Scene.RESTARTMENU;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the shops options
        /// </summary>
        /// <returns></returns>
        private string[] GetShopMenuOptions()
        {
            string[] itemsForSale = _shop.GetItemNames();
            string[] menuOptions = new string[itemsForSale.Length + 1];

            for (int i = 0; i < itemsForSale.Length; i++)
            {
                menuOptions[i] = itemsForSale[i];
            }

            menuOptions[itemsForSale.Length] = "Return to Battle";            

            return menuOptions;
        }

        /// <summary>
        /// Displays the shop
        /// </summary>
        private void DisplayShopMenu()
        {
            string[] playerItemNames = _player.GetItemNames();

            Console.WriteLine("Your gold: " + _player.Gold);
                  
            Console.WriteLine();

            int input = GetInput("Please help yourself to my wares", GetShopMenuOptions());

            if (input >= 0 && input < GetShopMenuOptions().Length - 1)
            {
                if (_shop.Sell(_player, input))
                {
                    Console.Clear();
                    Console.WriteLine($"You purchased the {_shop.GetItemNames()[input]}!");
                    Console.WriteLine($"You've become stronger!");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("You don't have enough gold for that.");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }
        }

        /// <summary>
        /// The descriptions for each enemy appearance.
        /// </summary>
        private void EnemyDescriptions()
        {
            if (_currentEnemyIndex == 0)
            {
                Console.WriteLine("At the end of the hall you come across an emaciated man, clad in only a loincloth, weilding a broken blade. He lunges at you! Prepare for battle!");
                Console.ReadKey(true);
                Console.Clear();
            }
            else if (_currentEnemyIndex == 1)
            {
                Console.WriteLine("The sinner's body fades to ash. The ash begins to swirl into a new beastly form. A wolf-like creature, with dozens of heads, tails, and claws, wildly attacks as if" +
                    " its many brains can't choose where to strike first. Prepare yourself!");
                Console.ReadKey(true);
                Console.Clear();
            }
            else if (_currentEnemyIndex == 2)
            {
                Console.WriteLine("The ashen remains of the wolves seep into the ground's cracks. The floor below burst open! Rising from the depths is a dinosaur covered in lava and molten" +
                    " rock. Its roars causes your very bones to rattle. Prepare yourself!");
                Console.ReadKey(true);
                Console.Clear();
            }
        }
    }
}
