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

    class Game
    {
        private bool _gameOver;
        private Scene _currentScene = 0;
        private Player _player;
        private string _playerName;
        private Entity[] _enemies;
        private int _currentEnemyIndex = 0;
        private Entity _currentEnemy;

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
    }
}
