using MazeBuilderAPI.Models.Internal;

namespace MazeBuilderAPI.Interfaces;

using Models.Enums;
using Models.Responses;

public interface ISolveStrategy
{
    PathfindingAlgorithm PathfindingAlgorithmName { get; }
    SolveResponse Solve(List<List<MazeVertex>> maze);
}