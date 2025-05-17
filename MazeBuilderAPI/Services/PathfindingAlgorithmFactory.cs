namespace MazeBuilderAPI.Services;

using Interfaces;
using Models.Enums;

public class PathfindingAlgorithmFactory(IEnumerable<ISolveStrategy> algorithms) : IPathfindingAlgorithmFactory
{
    public ISolveStrategy GetAlgorithm(PathfindingAlgorithm pathfindingAlgorithm)
    {
        return algorithms.FirstOrDefault(a => a.PathfindingAlgorithmName == pathfindingAlgorithm)
               ?? throw new ArgumentException("Algoritmo não encontrado");
    }
}