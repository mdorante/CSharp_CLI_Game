using System;
using System.Collections.Generic;

namespace Game_Project.Lib
{
    public class MenuNavigation
    {
        public static void ShowMessage(string message, ConsoleColor color)
        {
            Console.Clear();
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();

            Console.WriteLine("\nPress Enter to return");
            Console.ReadLine();
        }

        public static bool IsValidOption(int minValue, int maxValue, int option, string message)
        {
            if (option < minValue || option > maxValue)
            {
                ShowMessage(message, ConsoleColor.Red);
                return false;
            }
            else
            {
                return true;
            }
        }

        public static int ValidateOption(List<string> menuOptions, int minValue, int maxValue)
        {
            int maxAttempts = 3;
            int failedAttempts = 0;

            bool validOption = false;
            bool isNum = false;

            int option = 0;

            while (!validOption || !isNum)
            {
                if (failedAttempts == maxAttempts)
                {
                    ShowMessage("You ran out of attempts, exiting the game.", ConsoleColor.Red);
                    History.LogEvent("Player ran out of attempts to select a valid option.");
                    return 0;
                }

                Console.Clear();
                for (int i = 0; i < menuOptions.Count; i++)
                {
                    Console.WriteLine(menuOptions[i]);
                }

                isNum = int.TryParse(Console.ReadLine(), out option);

                if (isNum)
                {
                    validOption = IsValidOption(minValue, maxValue, option, $"Invalid option.\nYou have {maxAttempts - ++failedAttempts} attempts left.");
                }
                else
                {
                    ShowMessage($"Invalid option.\nYou have {maxAttempts - ++failedAttempts} attempts left.", ConsoleColor.Red);
                }
            }

            return option;
        }

        public static int DisplayMenu(string difficulty)
        {
            List<string> optionsMenu = new List<string>();

            optionsMenu.Add("1. Start");
            optionsMenu.Add("2. See Instructions");
            optionsMenu.Add("3. Select Difficulty");
            optionsMenu.Add("4. See History");
            optionsMenu.Add("5. Exit");
            optionsMenu.Add($"\nCurrent Difficulty Level: {difficulty}");
            optionsMenu.Add("\nSelect an option:");

            int option = ValidateOption(optionsMenu, 1, 5);

            // player ran out of attempts to select a valid option
            if (option == 0)
                option = 5;

            return option;
        }

        public static string SetDifficulty()
        {
            string difficulty;
            List<string> optionsMenu = new List<string>();

            optionsMenu.Add("Difficulty Levels:");
            optionsMenu.Add("------------------");
            optionsMenu.Add("\n1. Easy");
            optionsMenu.Add("2. Hard");
            optionsMenu.Add("\nSelect an option:");

            int option = ValidateOption(optionsMenu, 1, 2);

            if (option == 1)
                difficulty = "Easy";
            else if (option == 2)
                difficulty = "Hard";
            else
                difficulty = null;

            if (difficulty != null)
                History.LogEvent($"Player changed the difficulty to {difficulty}.");

            return difficulty;
        }

        public static void ShowInstructions()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Instructions:");
            Console.WriteLine("-------------");

            Console.ResetColor();

            Console.WriteLine("\nThe game consists in trying to convert an all 0's grid into all 1's.");
            Console.WriteLine("In order to do this, you have to select a coordinate (x, y),");
            Console.WriteLine("this will switch the element at that coordinate and all adjacent elements (up, down, left and right).");
            Console.WriteLine("By switch I mean, if the element is a 0, it will turn into a 1 or vice versa.");

            Console.WriteLine("\nYou also have two extra commands:");
            Console.WriteLine("- Save");
            Console.WriteLine("- Load");

            History.LogEvent("Player viewed instructions.");

            Console.WriteLine("\nPress Enter to return");
            Console.ReadLine();
        }
    }
}
