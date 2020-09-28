using System;
using Game_Project.Lib;

namespace Game_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            // Default difficulty is Easy, if the user wants, they can change it to Hard later
            string difficulty = "Easy";

            int option = 0;

            while (option != 5)
            {
                option = MenuNavigation.DisplayMenu(difficulty);
                switch (option)
                {
                    case 2:
                        MenuNavigation.ShowInstructions();
                        break;
                    case 3:
                        difficulty = MenuNavigation.SetDifficulty();
                        break;
                    case 5:
                        Console.WriteLine("Exit");
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
