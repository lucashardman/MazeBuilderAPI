using MazeBuilderAPI.Models.Enums;

namespace MazeBuilderAPI.Algorithms.Maze;

public class BinaryTree : MazeBuilderBaseAlgorithm
{
    public override MazeAlgorithm MazeAlgorithmName  => MazeAlgorithm.BinaryTree;

    public override void Generate()
    {
        if (Maze is null)
        {
            var bIsInitialized = InitializeBoard(true);
            if (!bIsInitialized) return;
        }

        for (int y = 0; y < Columns; y++)
        {
            for (int x = 0; x < Rows; x++)
            {
                bool canGoLeft = x > 0; // Can go left, if it's not in the first column
                bool canGoUp = y > 0; //Can go up, if it's not in the first row

                // If it can go Up and Left, choose a random path between them
                if (canGoLeft && canGoUp)
                {
                    if (RandomStream.Next(2) == 0) // 0 = Up, 1 = Left
                    {
                        HandleWallBetween(x, y, x, y - 1, true); // Remove Up wall
                    }
                    else
                    {
                        HandleWallBetween(x, y, x - 1, y, true); // Remove Left wall
                    }
                }
                else if (canGoLeft)
                {
                    // If it can't go Up, go Left
                    HandleWallBetween(x, y, x - 1, y, true);
                }
                else if (canGoUp)
                {
                    // If it can't go Left, go Up
                    HandleWallBetween(x, y, x, y - 1, true);
                }
            }
        }
    }
}