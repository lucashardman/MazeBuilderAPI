using MazeBuilderAPI.Models.Enums;
using MazeBuilderAPI.Models.Internal;

namespace MazeBuilderAPI.Algorithms.Maze;

public class SimplifiedPrim : MazeBuilderBaseAlgorithm
{
    public SimplifiedPrim(int columns, int rows, int seed, MazeAlgorithm algorithm)
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
        
        // Initializing the random stream
        var randomStream = seed == -1 ? new Random() : new Random(seed);
        
        var startX = randomStream.Next(Rows);
        var startY = randomStream.Next(Columns);
        
        // The active cell list contains the cells that will be used to search for unvisited neighbors 
        var active = new List<IntPoint> { new IntPoint(startX, startY) };

        while (active.Any())
        {
            var currentIndex = randomStream.Next(active.Count);
            var current = active[currentIndex];
            
            var availableNeighbors = GetAvailableNeighbors(current);
            
            if (availableNeighbors.Any())
            {
                // Choose a random neighbor
                var neighbor = availableNeighbors[randomStream.Next(availableNeighbors.Count)];

                // Remove the wall between the current cell and the neighbor
                HandleWallBetween(current.X, current.Y, neighbor.X, neighbor.Y, true);

                // Add the neighbor to the active cells list
                active.Add(neighbor);
            }
            else
            {
                // Remove the cell from the active list
                active.RemoveAt(currentIndex);
            }
        }
    }
    private List<IntPoint> GetAvailableNeighbors(IntPoint cell)
    {
        var neighbors = new List<IntPoint>();

        foreach (var direction in Directions)
        {
            var nx = cell.X + direction.X;
            var ny = cell.Y + direction.Y;

            // Check if cell is valid (not off boundaries and unvisited)
            if (IsValidVertex(nx, ny) && !IsVisited(nx, ny))
            {
                neighbors.Add(new IntPoint(nx, ny));
            }
        }
        return neighbors;
    }
    private bool IsVisited(int x, int y)
    {
        if (Maze is null) return false;
        
        var vertex = Maze[y][x];
        // Check if all walls are closed, if so the cell is not visited yet
        return (vertex.LeftEdge || vertex.RightEdge || vertex.UpEdge || vertex.DownEdge);
    }
}