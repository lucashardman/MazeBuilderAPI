namespace MazeBuilderAPI.Services;

using Interfaces;
using Models.Enums;

public class MazeAlgorithmFactory(IEnumerable<IMazeStrategy> algorithms) : IMazeAlgorithmFactory
{
    public IMazeStrategy GetAlgorithm(MazeAlgorithm mazeAlgorithm)
    {
        return algorithms.FirstOrDefault(a => a.MazeAlgorithmName == mazeAlgorithm)
               ?? throw new ArgumentException("Algoritmo não encontrado");
    }
}