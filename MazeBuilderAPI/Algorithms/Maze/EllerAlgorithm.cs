using MazeBuilderAPI.Models.Enums;
using MazeBuilderAPI.Models.Internal;

namespace MazeBuilderAPI.Algorithms.Maze;

public class EllerAlgorithm : MazeBuilderBaseAlgorithm
{
    public EllerAlgorithm(int columns, int rows, int seed, MazeAlgorithm algorithm)
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
        var sets = new Dictionary<int, int>();
        var nextSet = 1;

        for (int y = 0; y < Columns; y++)
        {
            for (int x = 0; x < Rows; x++)
            {
                if (!sets.ContainsKey(x))
                {
                    sets[x] = nextSet++;
                }

                if (x < Rows - 1 && (randomStream.Next(2) == 0 || y == Columns - 1))
                {
                    if (!sets.ContainsKey(x + 1))
                    {
                        sets[x + 1] = nextSet++;
                    }

                    if (sets[x] != sets[x + 1])
                    {
                        HandleWallBetween(x, y, x + 1, y, true);
                        int oldSet = sets[x + 1];
                        for (int i = 0; i < Rows; i++)
                        {
                            if (sets.ContainsKey(i) && sets[i] == oldSet)
                            {
                                sets[i] = sets[x];
                            }
                        }
                    }
                }
            }
            if (y < Columns - 1)
            {
                var nextRowSets = new Dictionary<int, int>();
                for (int x = 0; x < Rows; x++)
                {
                    if (randomStream.Next(2) == 0 || !nextRowSets.ContainsValue(sets[x]))
                    {
                        HandleWallBetween(x, y, x, y + 1, true);
                        nextRowSets[x] = sets[x];
                    }
                }
                sets = nextRowSets;
            }
        }
    }
}