using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Assessment1
{
    public enum Scene
    {
        STARTMENU,
        NAMECREATION,
        CHARACTERSELECTION
        
    }

    public enum ItemType
    {
        DEFENSE,
        ATTACK,
        HEALTH,
        NONE
    }

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
        private Entity sinner;
        private Entity wolves;
        private Entity magma;
        //Shop related Variables
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

        private void Save()
        {

        }

        private bool Load()
        {
            return true;
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
        private void DisplayMainMenu()
        {
            int input = GetInput("Would you like to play again?", "Yes", "No");

            if (input == 0)
            {
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

        private void InitializeShopItems()
        {
            _healthPotion = new Item { Name = "Health Potion", StatBoost = 15, Type = ItemType.HEALTH, Cost = 100 };
            _attackPotion = new Item { Name = "Attack Potion", StatBoost = 15, Type = ItemType.ATTACK, Cost = 150 };
            _defensePotion = new Item { Name = "Defense Potion", StatBoost = 15, Type = ItemType.DEFENSE, Cost = 200 };

            _shopInventory = new Item[] { _healthPotion, _attackPotion, _defensePotion };
        }

        /// <summary>
        /// Initializes the enemy stats and enemies array.
        /// </summary>
        private void InitializeEnemies()
        {
            _currentEnemyIndex = 0;

            sinner = new Entity("The Nameless Sinner", 30, 17, 5);

            wolves = new Entity("The Nest of Wolves", 55, 25, 15);

            magma = new Entity("The Dripping Dinosaurus", 100, 50, 20);

            _enemies = new Entity[] { sinner, wolves, magma };

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
    }
}
