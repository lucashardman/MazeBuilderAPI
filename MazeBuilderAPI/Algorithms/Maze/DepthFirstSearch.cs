using MazeBuilderAPI.Models.Internal;

namespace MazeBuilderAPI.Algorithms.Maze;

public class DepthFirstSearch : MazeBuilderBaseAlgorithm
{
    public DepthFirstSearch(int columns, int rows)
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

        // Initializing the random stream
        var randomStream = seed == -1 ? new Random() : new Random(seed);

        var visited = new bool[Rows, Columns];

        // Initialize DFS stack
        Stack<IntPoint> stack = new Stack<IntPoint>();

        // Choose a random first vertex
        var startX = randomStream.Next(Rows);
        var startY = randomStream.Next(Columns);
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
                var next = unvisitedNeighbors[randomStream.Next(unvisitedNeighbors.Count)];

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
