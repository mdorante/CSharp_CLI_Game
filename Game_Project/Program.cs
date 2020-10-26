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
            int turns = 10;
            int option = 0;

            History.LogEvent("Player entered the game");

            while (option != 5)
            {
                option = MenuNavigation.DisplayMenu(difficulty);

                switch (option)
                {
                    case 1:
                        History.LogEvent("Player started a new game.");
                        Game.PlayGame(difficulty, ref turns);
                        break;
                    case 2:
                        MenuNavigation.ShowInstructions();
                        break;
                    case 3:
                        difficulty = MenuNavigation.SetDifficulty();
                        if (difficulty == "Hard")
                            turns = 5;
                        else if (difficulty == "Easy")
                            turns = 10;

                        // player runs out of attempts to select difficulty
                        else
                            option = 5;
                        break;
                    case 4:
                        History.ReadLog();
                        break;
                    case 5:
                        Console.WriteLine("Exit");
                        History.LogEvent("Player exited the game.");
                        break;
                }
            }
        }
    }
}
