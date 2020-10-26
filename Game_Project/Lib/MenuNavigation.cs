using System;

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

        public static int DisplayMenu(string difficulty)
        {
            int option = 0;
            bool isNum = false;
            bool validOption = false;

            while (!isNum || !validOption)
            {
                Console.Clear();

                Console.WriteLine("1. Start");
                Console.WriteLine("2. See Instructions");
                Console.WriteLine("3. Select Difficulty");
                Console.WriteLine("4. See History");
                Console.WriteLine("5. Exit");

                Console.WriteLine($"\nCurrent Difficulty Level: {difficulty}");

                Console.WriteLine("\nSelect an option:");
                isNum = int.TryParse(Console.ReadLine(), out option);

                if (isNum)
                    validOption = IsValidOption(1, 5, option, "Invalid option");
                else
                    ShowMessage("Invalid option.", ConsoleColor.Red);
            }

            return option;
        }

        public static string SetDifficulty()
        {
            string difficulty = string.Empty;
            bool isNum = false;
            int level = 0;

            while (!isNum)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Difficulty Levels:");
                Console.WriteLine("------------------");
                Console.ResetColor();

                Console.WriteLine("\n1. Easy");
                Console.WriteLine("2. Hard");

                Console.WriteLine("\nSelect an option:");
                int.TryParse(Console.ReadLine(), out level);

                isNum = IsValidOption(1, 2, level, "Invalid option.");
            }

            if (level == 1)
            {
                difficulty = "Easy";
            }
            else if (level == 2)
            {
                difficulty = "Hard";
            }

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
