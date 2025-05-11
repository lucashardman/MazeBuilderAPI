namespace MazeBuilderAPI.Interfaces;

using Models.Enums;
using Models.Responses;

public interface IMazeStrategy
{
    MazeAlgorithm MazeAlgorithmName { get; }
    bool Initialize(int height, int width, int seed = -1);
    void Generate();
    public MazeResponse? ConvertMazeToResponseType();
}