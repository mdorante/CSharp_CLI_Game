using System;
using System.Collections.Generic;

namespace Game_Project.Lib
{
    public class Game
    {
        public static void PlayGame(string difficulty, ref int turns)
        {
            int[,] grid = CreateGrid(difficulty);
            bool winner = false;
            int turnsPlayed = 0;

            while (turns > 0 && !winner)
            {
                DisplayGrid(grid);

                Console.WriteLine($"\nTurns remaining: {turns}");

                UpdateGrid(grid, ref turns, ref turnsPlayed);
                winner = AmIAWinner(grid);
            }

            if (!winner)
                MenuNavigation.ShowError("Out of turns.\nGame Over.");
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Congratulations!");
                Console.WriteLine("----------------");

                Console.ResetColor();
                Console.WriteLine($"\nYou won the game in {turnsPlayed} turns!");

                Console.WriteLine("\nPress Enter to return to the main menu.");
                Console.ReadLine();
            }
        }

        private static int[,] CreateGrid(string difficulty)
        {
            int gridDimension = 0;

            if (difficulty == "Easy")
                gridDimension = 4;
            else if (difficulty == "Hard")
                gridDimension = 3;

            int[,] grid = new int[gridDimension, gridDimension];

            return grid;
        }

        private static void DisplayGrid(int[,] grid)
        {
            Console.Clear();

            int rows = grid.GetLength(0);
            int columns = grid.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write($"{grid[i, j]} ");
                }
                Console.WriteLine();
            }
        }

        private static void UpdateGrid(int[,] grid, ref int turns, ref int turnsPlayed)
        {
            int[] nums = ReadCoordinate(grid);
            int x = nums[0];
            int y = nums[1];

            int gridRows = grid.GetLength(0);
            int gridColumns = grid.GetLength(1);

            // Switch chosen coord
            SwitchValue(ref grid[x, y]);

            // Switch upper coord
            if (x > 0)
                SwitchValue(ref grid[x - 1, y]);

            // Switch lower coord
            if (x < gridRows - 1)
                SwitchValue(ref grid[x + 1, y]);

            // Switch left coord
            if (y > 0)
                SwitchValue(ref grid[x, y - 1]);

            // Switch right coord
            if (y < gridColumns - 1)
                SwitchValue(ref grid[x, y + 1]);

            turns--;
            turnsPlayed++;
        }

        private static int[] ReadCoordinate(int[,] grid)
        {
            int num1 = 0;
            int num2 = 0;
            int gridRows = grid.GetLength(0);
            int gridColumns = grid.GetLength(1);

            bool validRow = false;
            bool validColumn = false;

            while (!validRow || !validColumn)
            {
                Console.WriteLine("Enter the coordinates using the following format: x,y");
                string userInput = Console.ReadLine();

                List<string> coordinates = new List<string>();

                if (userInput.Contains(','))
                {
                    string[] inputNums = userInput.Split(',');

                    if (inputNums.Length != 2)
                    {
                        MenuNavigation.ShowError("Incorrect format, you must specify 2 coordinates (x and y).");
                        DisplayGrid(grid);
                        continue;
                    }

                    for (int i = 0; i < inputNums.Length; i++)
                    {
                        coordinates.Add(inputNums[i].Trim());
                    }
                }
                else
                {
                    MenuNavigation.ShowError("Invalid coordinates.");
                    DisplayGrid(grid);
                    continue;
                }

                bool validNum1 = int.TryParse(coordinates[0], out num1);
                bool validNum2 = int.TryParse(coordinates[1], out num2);

                if (validNum1 && validNum2)
                {
                    validRow = MenuNavigation.IsValidOption(1, gridRows, num1, "Row out of range");

                    if (validRow)
                        validColumn = MenuNavigation.IsValidOption(1, gridColumns, num2, "Column out of range");
                }
                else
                {
                    MenuNavigation.ShowError("Invalid coordinates.");
                }
                DisplayGrid(grid);
            }

            int[] nums = { num1 - 1, num2 - 1 };

            return nums;
        }

        private static void SwitchValue(ref int num)
        {
            if (num == 0)
                num = 1;
            else
                num = 0;
        }

        private static bool AmIAWinner(int[,] grid)
        {
            int gridRows = grid.GetLength(0);
            int gridColumns = grid.GetLength(1);

            for (int i = 0; i < gridRows; i++)
            {
                for (int j = 0; j < gridColumns; j++)
                {
                    if (grid[i, j] == 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}