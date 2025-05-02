using MazeBuilderAPI.Algorithms.Pathfinding;
using MazeBuilderAPI.Models.Enums;
using MazeBuilderAPI.Models.Internal;
using MazeBuilderAPI.Models.Responses;

namespace MazeBuilderAPI.Services;

public class SolveService
{
    public SolveResponse? Solve(List<List<MazeVertex>> maze, PathfindingAlgorithm pathfindingAlgorithm)
    {
        switch (pathfindingAlgorithm)
        {
            case PathfindingAlgorithm.DepthFirstSearch:
                var depthFirstSearch = new DepthFirstSearch();
                return depthFirstSearch.Run(maze);
            case PathfindingAlgorithm.BreadthFirstSearch:
                return null;
            case PathfindingAlgorithm.Dijkstra:
                return null;
            case PathfindingAlgorithm.AStar:
                return null;
            default:
                throw new ArgumentException("Algoritmo de pathfinding não suportado", nameof(pathfindingAlgorithm));
        }
    }
}