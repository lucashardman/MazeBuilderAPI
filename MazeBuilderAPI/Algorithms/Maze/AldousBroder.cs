using MazeBuilderAPI.Models.Enums;

namespace MazeBuilderAPI.Algorithms.Maze;

public class AldousBroder : MazeBuilderBaseAlgorithm
{
    public override MazeAlgorithm MazeAlgorithmName  => MazeAlgorithm.AldousBroder;

    public override void Generate()
    {
        if (Maze is null)
        {
            var bIsInitialized = InitializeBoard(true);
            if (!bIsInitialized) return;
        }

        // var randomStream = Seed == -1 ? new Random() : new Random(Seed);
        var visited = new HashSet<(int, int)>();
        int x = RandomStream.Next(Rows);
        int y = RandomStream.Next(Columns);
        visited.Add((x, y));
        
        while (visited.Count < Columns * Rows)
        {
            var directions = new List<(int dx, int dy)>(Directions.Select(d => (d.X, d.Y)));
            var (dx, dy) = directions[RandomStream.Next(directions.Count)];
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
        }
    }
}