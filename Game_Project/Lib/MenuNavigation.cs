using System;

namespace Game_Project.Lib
{
    public class MenuNavigation
    {
        public static int DisplayMenu(string difficulty)
        {
            int option = 0;
            bool isNum = false;

            Console.Clear();

            while (!isNum)
            {
                Console.WriteLine("1. Start");
                Console.WriteLine("2. See Instructions");
                Console.WriteLine("3. Select Difficulty");
                Console.WriteLine("4. See Last Game");
                Console.WriteLine("5. Exit");

                Console.WriteLine($"\nCurrent Difficulty Level: {difficulty}");

                Console.WriteLine("\nSelect an option:");
                isNum = int.TryParse(Console.ReadLine(), out option);

                if (option <= 0 || option > 5)
                {
                    Console.WriteLine("\nInvalid option.");
                    isNum = false;
                }
            }

            return option;
        }

        public static string SetDifficulty()
        {
            string difficulty = string.Empty;
            bool isNum = false;
            int level = 0;

            Console.Clear();
            Console.WriteLine("Difficulty Levels:");
            Console.WriteLine("1. Easy");
            Console.WriteLine("2. Hard");


            while (!isNum)
            {
                Console.WriteLine("\nSelect an option:");
                isNum = int.TryParse(Console.ReadLine(), out level);

                if (level < 1 || level > 2)
                {
                    Console.WriteLine("Invalid option.");
                    isNum = false;
                }
            }

            if (level == 1)
            {
                difficulty = "Easy";
            }
            else if (level == 2)
            {
                difficulty = "Hard";
            }

            return difficulty;
        }
    }
}
