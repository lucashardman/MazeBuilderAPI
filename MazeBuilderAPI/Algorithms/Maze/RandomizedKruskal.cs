using MazeBuilderAPI.Models.Internal;

namespace MazeBuilderAPI.Algorithms.Maze;

public class RandomizedKruskal : MazeBuilderBaseAlgorithm
{
    public RandomizedKruskal(int columns, int rows)
    {
        Columns = columns;
        Rows = rows;
    }

    public void Run(int seed = -1)
    {
        if (Maze is null)
        {
            var bIsInitialized = Initialize(true);
            if (!bIsInitialized) return;
        }
        
        var randomStream = seed == -1 ? new Random() : new Random(seed);
        var walls = new List<(int x1, int y1, int x2, int y2)>();
        var sets = new Dictionary<(int, int), int>();
        var nextSet = 1;

        for (int y = 0; y < Columns; y++)
        {
            for (int x = 0; x < Rows; x++)
            {
                sets[(x, y)] = nextSet++;
                if (x < Rows - 1) walls.Add((x, y, x + 1, y));
                if (y < Columns - 1) walls.Add((x, y, x, y + 1));
            }
        }
        
        walls = walls.OrderBy(_ => randomStream.Next()).ToList();
        
        foreach (var (x1, y1, x2, y2) in walls)
        {
            if (sets[(x1, y1)] != sets[(x2, y2)])
            {
                HandleWallBetween(x1, y1, x2, y2, true);
                int oldSet = sets[(x2, y2)];
                int newSet = sets[(x1, y1)];
                foreach (var key in sets.Keys.ToList())
                {
                    if (sets[key] == oldSet)
                    {
                        sets[key] = newSet;
                    }
                }
            }
        }
    }
}