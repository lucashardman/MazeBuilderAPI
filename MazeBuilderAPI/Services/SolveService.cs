using MazeBuilderAPI.Interfaces;namespace MazeBuilderAPI.Services;

using Algorithms.Pathfinding;
using Models.Enums;
using Models.Internal;
using Models.Responses;

public class SolveService(IPathfindingAlgorithmFactory factory)
{
    public SolveResponse Solve(List<List<MazeVertex>> maze, PathfindingAlgorithm pathfindingAlgorithmName)
    {
       var pathfindingAlgorithm = factory.GetAlgorithm(pathfindingAlgorithmName);
       return pathfindingAlgorithm.Solve(maze);
    }
}
