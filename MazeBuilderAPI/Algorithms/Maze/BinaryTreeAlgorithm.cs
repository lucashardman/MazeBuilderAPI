﻿using MazeBuilderAPI.Models.Internal;

namespace MazeBuilderAPI.Algorithms.Maze;

public class BinaryTreeAlgorithm : MazeBuilderBaseAlgorithm
{
    public BinaryTreeAlgorithm(int columns, int rows)
    {
        Columns = columns;
        Rows = rows;
    }

    public void Run(int seed = -1)
    {
        bool bIsInitialized = false;
        if (Maze is null) bIsInitialized = Initialize();
        if (!bIsInitialized) return;
        
        // Initializing the random stream
        var randomStream = seed == -1 ? new Random() : new Random(seed);
        
        for (int y = 0; y < Rows; y++)
        {
            for (int x = 0; x < Columns; x++)
            {
                bool canGoLeft = x > 0; // Can go left, if it's not in the first column
                bool canGoUp = y > 0; //Can go up, if it's not in the first row

                // If can go Up and Left, choose a random path between them
                if (canGoLeft && canGoUp)
                {
                    if (randomStream.Next(2) == 0) // 0 = Up, 1 = Left
                    {
                        RemoveWallBetween(x, y, x, y - 1); // Remove Up wall
                    }
                    else
                    {
                        RemoveWallBetween(x, y, x - 1, y); // Remove Left wall
                    }
                }
                else if (canGoLeft)
                {
                    // If can't go Up, go Left
                    RemoveWallBetween(x, y, x - 1, y);
                }
                else if (canGoUp)
                {
                    // If can't go Left, go Up
                    RemoveWallBetween(x, y, x, y - 1);
                }
            }
        }
    }
}