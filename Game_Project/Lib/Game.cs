using System;
using System.Collections.Generic;

namespace Game_Project.Lib
{
    public class Game
    {
        public static void PlayGame(string difficulty, ref int turns)
        {
            int[,] grid = CreateGrid(difficulty);
            int[,] savedGrid = CreateGrid(difficulty);

            bool winner = false;
            int turnsPlayed = 0;
            int initialTurns = turns;

            while (turns > 0 && !winner)
            {
                DisplayGrid(grid, ref turns);

                UpdateGrid(grid, ref savedGrid, ref turns, ref turnsPlayed, ref initialTurns);
                winner = SearchGrid(grid, 0);
            }

            DisplayGrid(grid, ref turns);
            Console.ReadLine();

            if (!winner)
            {
                History.LogEvent($"Player lost the game in {turnsPlayed} turns.");
                MenuNavigation.ShowMessage("Out of turns.\nGame Over.", ConsoleColor.Red);
            }
            else
            {
                History.LogEvent($"Player won the game in {turnsPlayed} turns!");
                MenuNavigation.ShowMessage("You win, congratulations!\n-------------------------", ConsoleColor.Green);
            }

            // reset turns after game is over
            turns = initialTurns;
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

        private static void DisplayGrid(int[,] grid, ref int turns)
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

            Console.WriteLine($"\nTurns remaining: {turns}");
        }

        private static void UpdateGrid(int[,] grid, ref int[,] savedGrid, ref int turns, ref int turnsPlayed, ref int initialTurns)
        {
            int[] nums = ReadCoordinate(grid, ref savedGrid, ref turns, ref turnsPlayed);

            // if the player saved or loaded, don't update the grid
            if (nums == null)
                return;

            int x = nums[0];
            int y = nums[1];

            History.LogEvent($"Turn {++turnsPlayed} of {initialTurns} - Player entered: {x--},{y--}");

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
        }

        private static int[] ReadCoordinate(int[,] grid, ref int[,] savedGrid, ref int turns, ref int turnsPlayed)
        {
            int num1 = 0;
            int num2 = 0;
            int gridRows = grid.GetLength(0);
            int gridColumns = grid.GetLength(1);

            bool validRow = false;
            bool validColumn = false;

            bool saveLoadExit = false;

            while (!validRow || !validColumn)
            {
                DisplayGrid(grid, ref turns);

                Console.WriteLine("Enter the coordinates using the following format: x,y");
                string userInput = Console.ReadLine();

                List<string> coordinates = new List<string>();

                if (userInput.Contains(','))
                {
                    string[] inputNums = userInput.Split(',');

                    if (inputNums.Length != 2)
                    {
                        MenuNavigation.ShowMessage("Incorrect format, you must specify 2 coordinates (x and y).", ConsoleColor.Red);
                        DisplayGrid(grid, ref turns);
                        continue;
                    }

                    for (int i = 0; i < inputNums.Length; i++)
                    {
                        coordinates.Add(inputNums[i].Trim());
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
                        MenuNavigation.ShowMessage("Invalid coordinates.", ConsoleColor.Red);
                    }
                }
                else if (userInput == "Save")
                {
                    Array.Copy(grid, savedGrid, grid.Length);
                    History.LogEvent("Player saved the game.");

                    saveLoadExit = true;
                    turns--;
                    turnsPlayed++;
                    break;
                }
                else if (userInput == "Load")
                {
                    if (!SearchGrid(savedGrid, 1))
                    {
                        Array.Copy(savedGrid, grid, savedGrid.Length);
                        History.LogEvent("Player loaded a saved game.");
                    }
                    else
                    {
                        MenuNavigation.ShowMessage("No saved game to load.", ConsoleColor.Red);
                    }

                    saveLoadExit = true;
                    turns--;
                    turnsPlayed++;
                    break;
                }
                else
                {
                    MenuNavigation.ShowMessage("Invalid input.", ConsoleColor.Red);
                    DisplayGrid(grid, ref turns);
                    continue;
                }

                turns--;
            }

            if (saveLoadExit)
                return null;

            int[] nums = { num1, num2 };
            return nums;
        }

        private static void SwitchValue(ref int num)
        {
            if (num == 0)
                num = 1;
            else
                num = 0;
        }

        private static bool SearchGrid(int[,] grid, int num)
        {
            int gridRows = grid.GetLength(0);
            int gridColumns = grid.GetLength(1);

            for (int i = 0; i < gridRows; i++)
            {
                for (int j = 0; j < gridColumns; j++)
                {
                    if (grid[i, j] == num)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}