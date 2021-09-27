using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Assessment1
{
    class Game
    {
        private bool gameOver;

        public void Run()
        {

        }

        private void Start()
        {

        }

        private void Update()
        {

        }

        private void End()
        {

        }

        private void Save()
        {

        }

        private bool Load()
        {

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
    }
}
