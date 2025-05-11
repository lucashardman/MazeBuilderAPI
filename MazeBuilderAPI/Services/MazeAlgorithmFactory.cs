namespace MazeBuilderAPI.Services;
using MazeBuilderAPI.Interfaces;
using MazeBuilderAPI.Models.Enums;


public class MazeAlgorithmFactory(IEnumerable<IMazeStrategy> algorithms) : IMazeAlgorithmFactory
{
    public IMazeStrategy GetAlgorithm(MazeAlgorithm mazeAlgorithm)
    {
        return algorithms.FirstOrDefault(a => a.MazeAlgorithmName == mazeAlgorithm)
               ?? throw new ArgumentException("Algoritmo não encontrado");
    }
}