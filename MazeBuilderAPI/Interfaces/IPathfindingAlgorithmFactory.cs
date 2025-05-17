namespace MazeBuilderAPI.Interfaces;

using Models.Enums;

public interface IPathfindingAlgorithmFactory
{
    ISolveStrategy GetAlgorithm(PathfindingAlgorithm name);
}