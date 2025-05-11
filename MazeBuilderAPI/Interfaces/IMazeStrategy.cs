namespace MazeBuilderAPI.Interfaces;
using MazeBuilderAPI.Algorithms.Maze;
using MazeBuilderAPI.Models.Enums;
using MazeBuilderAPI.Models.Responses;

public interface IMazeStrategy
{
    MazeAlgorithm MazeAlgorithmName { get; }
    void Initialize(int height, int width, int seed = -1);
    void Generate();
    public MazeResponse? ConvertMazeToResponseType();
}