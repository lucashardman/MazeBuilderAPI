namespace MazeBuilderAPI.Algorithms.Maze;

using Models.Enums;
using Models.Internal;

public class DepthFirstSearch : MazeBuilderBaseAlgorithm
{
    public override MazeAlgorithm MazeAlgorithmName  => MazeAlgorithm.DepthFirstSearch;
    protected override bool ShouldInitializeWalls => true;

    public override void Generate()
    {
        var visited = new bool[Rows, Columns];

        // Initialize DFS stack
        Stack<IntPoint> stack = new Stack<IntPoint>();

        // Choose a random first vertex
        var startX = RandomStream.Next(Rows);
        var startY = RandomStream.Next(Columns);
        stack.Push(new IntPoint(startX, startY));
        visited[startX, startY] = true;

        // Execute DFS
        while (stack.Count > 0)
        {
            var current = stack.Peek();
            var x = current.X;
            var y = current.Y;

            // Get all unvisited neighbors
            var unvisitedNeighbors = GetUnvisitedNeighbors(x, y, visited);

            if (unvisitedNeighbors.Count > 0)
            {
                // Choose random neighbor
                var next = unvisitedNeighbors[RandomStream.Next(unvisitedNeighbors.Count)];

                // Remove wall between current vertex and neighbor
                HandleWallBetween(x, y, next.X, next.Y, true);

                // Stack and check neighbor as visited
                visited[next.X, next.Y] = true;
                stack.Push(next);
            }
            else
            {
                stack.Pop();
            }
        }
    }

    private List<IntPoint> GetUnvisitedNeighbors(int x, int y, bool[,] visited)
    {
        var neighbors = new List<IntPoint>();

        foreach (var direction in Directions)
        {
            int nx = x + direction.X;
            int ny = y + direction.Y;

            if (IsValidVertex(nx, ny) && !visited[nx, ny])
            {
                neighbors.Add(new IntPoint(nx, ny));
            }
        }
        return neighbors;
    }
}
