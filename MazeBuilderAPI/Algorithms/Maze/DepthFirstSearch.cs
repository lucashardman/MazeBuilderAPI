﻿using MazeBuilderAPI.Models.Internal;

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
        bool bIsInitialized = false;
        if (Maze is null) bIsInitialized = Initialize();
        if (!bIsInitialized) return;

        // Initializing the random stream
        var randomStream = seed == -1 ? new Random() : new Random(seed);

        var visited = new bool[Columns, Rows];

        // Initialize DFS stack
        Stack<IntPoint> stack = new Stack<IntPoint>();

        // Choose a random first vertex
        var startX = randomStream.Next(Columns);
        var startY = randomStream.Next(Rows);
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
                RemoveWallBetween(x, y, next.X, next.Y);

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