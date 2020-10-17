using System;

namespace Game_Project.Lib
{
    public class Game
    {
        public static void PlayGame(string difficulty, int turns)
        {
            int[,] grid = CreateGrid(difficulty);
            DisplayGrid(grid);
            Console.WriteLine($"\nTurns remaining: {turns}");

            Console.WriteLine("\nPress enter to continue");
            Console.ReadLine();
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
    }
}