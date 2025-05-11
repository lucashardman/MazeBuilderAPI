namespace MazeBuilderAPI.Algorithms.Maze;

using Models.Enums;

public class AldousBroder : MazeBuilderBaseAlgorithm
{
    public override MazeAlgorithm MazeAlgorithmName  => MazeAlgorithm.AldousBroder;
    protected override bool ShouldInitializeWalls => true;

    public override void Generate()
    {
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