using MazeBuilderAPI.Models.Enums;
using MazeBuilderAPI.Models.Internal;

namespace MazeBuilderAPI.Algorithms.Maze;

public class Sidewinder : MazeBuilderBaseAlgorithm
{
    public Sidewinder(int columns, int rows, int seed, MazeAlgorithm algorithm)
    {
        Columns = columns;
        Rows = rows;
        Seed = seed;
        Algorithm = algorithm;
    }

    public void Run(int seed = -1)
    {
        if (Maze is null)
        {
            var bIsInitialized = Initialize(true);
            if (!bIsInitialized) return;
        }
        
        var randomStream = seed == -1 ? new Random() : new Random(seed);

        for (int y = 0; y < Columns; y++)
        {
            var run = new List<int>();

            for (int x = 0; x < Rows; x++)
            {
                run.Add(x);

                bool atEasternBoundary = (x == Rows - 1);
                bool atNorthernBoundary = (y == 0);
                bool shouldCloseOut = atEasternBoundary || (!atNorthernBoundary && randomStream.Next(2) == 0);

                if (shouldCloseOut)
                {
                    int member = run[randomStream.Next(run.Count)];
                    if (!atNorthernBoundary)
                    {
                        HandleWallBetween(member, y, member, y - 1, true);
                    }

                    run.Clear();
                }
                else
                {
                    HandleWallBetween(x, y, x + 1, y, true);
                }
            }
        }
    }
}