namespace MazeBuilderAPI.Interfaces;

using Models.Enums;

public interface IMazeAlgorithmFactory
{
    IMazeStrategy GetAlgorithm(MazeAlgorithm name);
}