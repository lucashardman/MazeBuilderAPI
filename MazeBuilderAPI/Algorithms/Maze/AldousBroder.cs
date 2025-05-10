using MazeBuilderAPI.Models.Enums;
using MazeBuilderAPI.Models.Internal;

namespace MazeBuilderAPI.Algorithms.Maze;

public class AldousBroder : MazeBuilderBaseAlgorithm
{
    public AldousBroder(int columns, int rows, int seed, MazeAlgorithm algorithm)
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
        var visited = new HashSet<(int, int)>();
        int x = randomStream.Next(Rows);
        int y = randomStream.Next(Columns);
        visited.Add((x, y));
        
        while (visited.Count < Columns * Rows)
        {
            var directions = new List<(int dx, int dy)>(Directions.Select(d => (d.X, d.Y)));
            var (dx, dy) = directions[randomStream.Next(directions.Count)];
            int nx = x + dx;
            int ny = y + dy;

            if (IsValidVertex(nx, ny))
            {
                if (!visited.Contains((nx, ny)))
                {
                    HandleWallBetween(x, y, nx, ny, true);
                    visited.Add((nx, ny));
                }
                x = nx;
                y = ny;
            }
            Console.WriteLine($"({x}, {y})");
        }
    }
}